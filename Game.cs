using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Zmej
{
    class Game
    {
        private Snake snake;
        private FoodCreator foodCreator;
        private Walls walls;
        private int score;
        double totalSpeedIncrease = 1;
        bool hasWon = false;       

        public Game(int width, int height)
        {
            Console.SetWindowSize(width, height);
            walls = new Walls(width, height);
            walls.Draw();
            Point p = new Point(4, 5, '*');
            snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();
            foodCreator = new FoodCreator(width, height, '$');
            sounds = new Sounds(".");
            sounds.PlayFont();
        }

        public void Run()
        {
            Point food = foodCreator.CreateFood();
            food.Draw();
            DateTime lastFoodCreationTime = DateTime.Now;

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    sounds.PlayGameOver();
                    EndGame();
                    break;
                }

                DateTime currentTime = DateTime.Now;
                if ((currentTime - lastFoodCreationTime).TotalSeconds >= 20)
                {
                    food = foodCreator.CreateFood();
                    if (food.sym == '/')
                    {
                        food = foodCreator.CreateFood();
                    }
                    food.Draw();
                    lastFoodCreationTime = currentTime;
                }

                if (snake.Eat(food))
                {
                    char foodType = snake.GetCurrentFoodType();

                    if (foodType == '#')
                    {
                        totalSpeedIncrease -= 0.25;
                        score++;
                        food = foodCreator.CreateFood();
                        food.Draw();
                        sounds.PlayEatSound();
                        Thread.Sleep(200);
                        sounds.PlayFont();
                    }

                    else if (foodType == '&')
                    {
                        totalSpeedIncrease += 0.35;
                        score++;
                        food = foodCreator.CreateFood();
                        food.Draw();
                        sounds.PlayEatSound();
                        Thread.Sleep(200);
                        sounds.PlayFont();
                    }

                    else if (foodType == '/')
                    {                       
                        sounds.PlayGameOver();
                        EndGame();
                        break;
                    }

                    else if (foodType == '$')
                    {
                        totalSpeedIncrease += 0.2;
                        score++;
                        food = foodCreator.CreateFood();
                        food.Draw();
                        sounds.PlayEatSound();
                        Thread.Sleep(200);
                        sounds.PlayFont();

                    }                    
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep((int)(100 * (1 /totalSpeedIncrease)));

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }

                if (score == 20)
                {
                    GG();
                    Thread.Sleep(2000);
                }               
            }
        }

        public void GG()
        {
            if (hasWon) return;

            hasWon = true;
            Console.Clear();
            sounds.Stop();
            Thread.Sleep(2000);
            Console.Write("Palju õnne!");
            sounds.PlayWin();
            Thread.Sleep(10000);
            Console.Clear();

            double lastTotalSpeedIncrease = totalSpeedIncrease;
            totalSpeedIncrease = 1;

            walls = new Walls(80, 25);
            walls.Draw();
            Point p = new Point(4, 5, '*');
            snake = new Snake(p, 21, Direction.RIGHT);
            snake.Draw();
            foodCreator = new FoodCreator(80-2, 25-2, '$');
            sounds = new Sounds(".");
            sounds.PlayFont();

            totalSpeedIncrease += lastTotalSpeedIncrease;

            Run();
        }

        private void EndGame()
        {
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.Write("Sisesta oma nimi: ");
            string name;
            while (true)
            {
                Console.Write("Sisesta oma nimi (vähemalt 3 tähe ja mitte numbrid): ");
                name = Console.ReadLine();

                try
                {
                    if (name.Any(char.IsDigit))
                    {
                        throw new Exception("Nimi ei tohi sisaldada numbreid!");
                    }

                    if (name.Length < 3)
                    {
                        throw new Exception("Nimi peab olema vähemalt 3 tähemärki pikk!");
                    }

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            string filePath = "Nimed.txt";
            string line = $"{name}: {score}";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(line);
            }
        }

        public static void ShowLeaderboard(int topN)
        {
            string filePath = "Nimed.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Pole andmed.");
                return;
            }

            var scores = File.ReadAllLines(filePath)
                .Select(line => line.Split(':'))
                .Select(parts => new { Name = parts[0], Score = int.Parse(parts[1]) })
                .OrderByDescending(player => player.Score)
                .Take(topN);

            Console.WriteLine($"Top:");
            foreach (var player in scores)
            {
                Console.WriteLine($"{player.Name}: {player.Score}");
            }
        }

        private Sounds sounds;

        static void Draw(Figure figure)
        {
            figure.Draw();
        }
        
    }
}
