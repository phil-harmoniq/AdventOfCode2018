using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle10
    {
        public static readonly string[] Input = File.ReadAllLines(Path.Combine("Inputs", "Input10.txt"));

        public static void Part1()
        {
            var points = new List<Point>();
            foreach (var line in Input) { points.Add(Point.Parse(line)); }
            var running = true;
            var iteration = 0;

            while (running)
            {
                for (var i = 0; i < 10946; i++)
                {
                    foreach (var point in points) { point.Update(); }
                }

                PrintPoints(points);
                running = YesNoPrompt("Continue? ");
            }
        }

        public static object Part2()
        {
            var points = new List<Point>();
            foreach (var line in Input) { points.Add(Point.Parse(line)); }
            throw new NotImplementedException();
        }

        private static bool YesNoPrompt(string message)
        {
            var validInputs = new string[] { "n", "no", "" };
            var inputIsValid = false;
            var userInput = "";

            while (!inputIsValid)
            {
                Console.WriteLine(message);
                userInput = Console.ReadLine().ToLower();
                if (validInputs.Contains(userInput)) { inputIsValid = true; }
                else { Console.WriteLine($"Invalid input: '{userInput}'"); }
            }

            return userInput != "n" || userInput != "no";
        }

        private static void PrintPoints(IList<Point> points)
        {
            var xSize = 100;
            var ySize = 50;
            var map = new string[xSize, ySize];
            var output = "";

            foreach (var point in points)
            {
                var isWithinXSize = point.PositionX >= 0 && point.PositionX < xSize;
                var isWithinYSize = point.PositionY >= 0 && point.PositionY < ySize;

                if (isWithinXSize && isWithinYSize)
                {
                    map[point.PositionX, point.PositionY] = "X";
                }
            }

            for (var y = 0; y < ySize; y++)
            {
                for (var x = 0; x < xSize; x++)
                {
                    output += map[x, y] == "X" ? "X" : ".";
                }
                output += Environment.NewLine;
            }

            Console.WriteLine(output + Environment.NewLine);
        }

        private class Point
        {
            internal int PositionX { get; private set; }
            internal int PositionY { get; private set; }
            internal int VelocityX { get; }
            internal int VelocityY { get; }

            private Point(int posX, int posY, int velX, int velY)
            {
                PositionX = posX;
                PositionY = posY;
                VelocityX = velX;
                VelocityY = velY;
            }

            internal void Update()
            {
                PositionX += VelocityX;
                PositionY += VelocityY;
            }

            internal static Point Parse(string input)
            {
                var split = input.Split(new char[] { ',', '<', '>' });
                var posX = int.Parse(split[1]);
                var posY = int.Parse(split[2]);
                var velX = int.Parse(split[4]);
                var velY = int.Parse(split[5]);
                return new Point(posX, posY, velX, velY);
            }
        }
    }
}