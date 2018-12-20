using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AdventOfCode2018.Extensions;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle11
    {
        private static readonly int Input = int.Parse(File.ReadAllText(Path.Combine("Inputs", "Input11.txt")));

        public static (int X, int Y) Part1()
        {
            var mapSize = 300;
            var map = new FuelCell[mapSize,mapSize];

            var clusters = new List<List<FuelCell>>();

            Enumerate2D(mapSize, mapSize, (x, y) =>
            {
                map[x,y] = new FuelCell(x + 1, y + 1);
            });

            Enumerate2D(mapSize, mapSize, (x, y) =>
            {
                if (x < mapSize - 3 && y < mapSize - 3)
                {
                    var cellCluster = new List<FuelCell>();
                    Enumerate2D(3, 3, (newX, newY) =>
                    {
                        var targetCell = map[x + newX, y + newY];
                        cellCluster.Add(targetCell);
                    });
                    clusters.Add(cellCluster);
                }
            });

            var highestPowerCluster = clusters.OrderByDescending(cluster => cluster.Sum(cell => cell.GetPowerLevel(Input))).First().First();
            return (X: highestPowerCluster.X, Y: highestPowerCluster.Y);
        }

        public static object Part2()
        {
            var mapSize = 300;
            var map = new FuelCell[mapSize,mapSize];

            var clusters = new List<List<FuelCell>>();

            Enumerate2D(mapSize, mapSize, (x, y) =>
            {
                map[x,y] = new FuelCell(x + 1, y + 1);
            });

            Enumerate2D(mapSize, mapSize, (x, y) =>
            {
                var lengthToEdges = new int[]
                {
                    mapSize - x,
                    mapSize - y
                };
                
                var lengthToClosestEdge = lengthToEdges.Min();

                for (var newSize = 0; newSize < lengthToClosestEdge; newSize++)
                {
                    var cellCluster = new List<FuelCell>();
                    Enumerate2D(newSize, newSize, (newX, newY) =>
                    {
                        var targetCell = map[x + newX, y + newY];
                        cellCluster.Add(targetCell);
                    });
                    clusters.Add(cellCluster);
                }
            });

            var highestPowerCluster = clusters.OrderByDescending(cluster => cluster.Sum(cell => cell.GetPowerLevel(Input))).First().First();
            return (X: highestPowerCluster.X, Y: highestPowerCluster.Y);
        }

        private class FuelCell
        {
            public int X { get; }
            public int Y { get; }
            public int RackId => X + 10;

            public FuelCell(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int GetPowerLevel(int gridSerialNumber)
            {
                var begin = RackId * Y;
                var gridded = begin + gridSerialNumber;
                var newId = (gridded * RackId).ToString();
                var thirdDigit = int.Parse(newId[newId.Length - 3].ToString());
                return thirdDigit - 5;
            }
        }
    }
}