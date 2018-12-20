using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AdventOfCode2018.Extensions;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle06
    {
        public static readonly string[] Input = File.ReadAllLines(Path.Combine("Inputs", "Input06.txt"));

        public static int Part1()
        {
            var givenCoordinates = Input.Select(line => Coordinates.Parse(line)).ToArray();

            Enumerate2D(1000, 1000, (x, y) =>
            {
                var distances = givenCoordinates.Select(c => new KeyValuePair<Coordinates, int>(c, c.DistanceTo(x, y)));
                var sorted = distances.OrderBy(c => c.Value);
                var closest = sorted.First();
                var nextClosest = sorted.ElementAt(1);

                if (closest.Value != nextClosest.Value)
                {
                    if (x == 0 || y == 0 || x == 999 || y == 999)
                    {
                        closest.Key.IsOnEdge = true;
                    }
                    closest.Key.AddOwnedCoordinates(x, y);
                }
            });

            var largestZone = givenCoordinates.Where(c => !c.IsOnEdge).OrderByDescending(c => c.Size).FirstOrDefault();
            return largestZone.Size;
        }

        public static int Part2()
        {
            var givenCoordinates = Input.Select(line => Coordinates.Parse(line));
            var underOneThousand = 0;

            Enumerate2D(1000, 1000, (x, y) =>
            {
                var distancesTotal = givenCoordinates
                    .Select(d => new KeyValuePair<Coordinates, int>(d, d.DistanceTo(x, y)))
                    .Sum(d => d.Value);
                
                if (distancesTotal < 10000) { underOneThousand++; }
            });

            return underOneThousand;
        }

        private class Coordinates
        {
            internal string Id { get; }
            internal int X { get; private set; }
            internal int Y { get; private set; }
            internal List<int[]> OwnedCoordinates { get; private set; }
            internal bool IsOnEdge { get; set; }
            internal int Size => OwnedCoordinates.Count;
            internal void AddOwnedCoordinates(int x, int y) => OwnedCoordinates.Add(new int[] {x, y});
            internal int DistanceTo(int x, int y) => Math.Abs(X - x) + Math.Abs(Y - y);
            public override string ToString() => $"({X},{Y}) - {Id}";

            internal Coordinates(int x, int y)
            {
                Id = Guid.NewGuid().ToString();
                OwnedCoordinates = new List<int[]>();
                X = x;
                Y = y;
            }

            internal static Coordinates Parse(string input)
            {
                var stringSplit = input.Split(new string[] {", "}, StringSplitOptions.None);
                return new Coordinates(int.Parse(stringSplit[0]), int.Parse(stringSplit[1]));
            }
        }
    }
}