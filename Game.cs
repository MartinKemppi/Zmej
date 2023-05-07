using System;
using System.Collections.Generic;
using System.Linq;
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

        public Game(int width, int height)
        {
            Console.SetWindowSize(width, height);
            walls = new Walls(width, height);
            walls.Draw();
            Point p = new Point(4, 5, '*');
            snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();
            foodCreator = new FoodCreator(width, height, '$');
            Sounds sounds = new Sounds(".");
            sounds.Play("Snake Music");
        }

        public void Run()
        {
            Point food = foodCreator.CreateFood();
            food.Draw();

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    // play game over sound
                    sounds.Play("gameover");
                    EndGame();
                    break;
                }

                if (snake.Eat(food))
                {
                    score++;
                    food = foodCreator.CreateFood();
                    food.Draw();
                    // play eat sound
                    sounds.PlayEat();
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
        }
        private void EndGame()
        {
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.Write("Sisesta oma nimi: ");
            string name = Console.ReadLine();
            string filePath = "snakeLength.txt";
            string line = $"{name}: {score}";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(line);
            }

            // play game over sound
            sounds.Play("gameover");
        }

        public static void ShowLeaderboard(int topN)
        {
            string filePath = "snakeLength.txt";
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

            Console.WriteLine("Top:");
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
