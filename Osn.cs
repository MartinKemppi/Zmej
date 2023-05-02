﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zmej
{
    class Osn
    {
        public static void Main(string[] args)
        {
            

            VerticalLine v1 = new VerticalLine(0, 10, 5, '%');
            Draw(v1);

            Point p = new Point(4, 5, '*');
            Figure fSnake = new Snake(p, 4, Direction.RIGHT);
            Draw(fSnake);
            Snake snake = (Snake)fSnake;

            HorizontalLine h1 = new HorizontalLine(0, 5, 6, '&');

            List<Figure> figures = new List<Figure>();
            figures.Add(fSnake);
            figures.Add(v1);
            figures.Add(h1);

            foreach (var f in figures)
            {
                f.Draw();
            }
        }
        
        static void Draw(Figure figure)
        {
            figure.Draw();
        }

        //    FoodCreator foodCreator = new FoodCreator(80, 25, '$');
        //    Point food = foodCreator.CreateFood();
        //    food.Draw();

        //    while (true)
        //    {
        //        if (snake.Eat(food))
        //        {
        //            food = foodCreator.CreateFood();
        //            food.Draw();
        //        }
        //        else
        //        {
        //            snake.Move();
        //        }

        //        Thread.Sleep(100);

        //        if (Console.KeyAvailable)
        //        {
        //            ConsoleKeyInfo key = Console.ReadKey();
        //            snake.HandleKey(key.Key);
        //        }
        //    }

        //}

        ////Console.SetBufferSize( 80,25 );
        //const int consoleWidth = 80;
        //const int consoleHeight = 25;

        //Console.SetWindowSize(consoleWidth, consoleHeight);

        ////Frame
        //HorizontalLine upLine = new HorizontalLine(0, 78, 0, '+');
        //HorizontalLine downLine = new HorizontalLine(0, 78, 24, '+');
        //VerticalLine leftLine = new VerticalLine(0, 24, 0, '+');
        //VerticalLine rightLine = new VerticalLine(0, 24, 78, '+');
        //upLine.Drow();
        //downLine.Drow();
        //leftLine.Drow();
        //rightLine.Drow();

        ////Dot
        //Point p = new Point(4, 5, '*');
        //Snake snake = new Snake(p, 4, Direction.RIGHT);
        //snake.Drow();
    }
}
