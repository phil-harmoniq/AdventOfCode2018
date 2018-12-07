using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle03
    {
        public static int Part1()
        {
            var file = File.ReadAllLines("Inputs/Input03.txt");
            var claims = new List<Claim>();
            var map = new int[1000, 1000];
            foreach (var line in file) { claims.Add(Claim.ParseNew(line)); }
            foreach (var claim in claims) { claim.AddToMap(map); }

            var overlapping = 0;

            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] > 1)
                    {
                        overlapping++;
                    }
                }
            }

            return overlapping;
        }

        public static int Part2()
        {
            var file = File.ReadAllLines("Inputs/Input03.txt");
            var claims = new List<Claim>();
            var map = new int[1000, 1000];
            foreach (var line in file) { claims.Add(Claim.ParseNew(line)); }
            foreach (var claim in claims) { claim.AddToMap(map); }
            
            foreach (var claim in claims)
            {
                if (claim.HasNoOverlaps(map))
                {
                    return claim.Id;
                }
            }
            
            return -1;
        }

        private class Claim
        {
            internal int Id { get; private set; }
            internal int OffsetX { get; private set; }
            internal int OffsetY { get; private set; }
            internal int LengthX { get; private set; }
            internal int LengthY { get; private set; }

            internal static Claim ParseNew(string claimString)
            {
                var splitString = claimString.Split(new string[] { "#", " @ ", ",", ": ", "x" }, StringSplitOptions.RemoveEmptyEntries);

                return new Claim
                {
                    Id = int.Parse(splitString[0]),
                    OffsetX = int.Parse(splitString[1]),
                    OffsetY = int.Parse(splitString[2]),
                    LengthX = int.Parse(splitString[3]),
                    LengthY = int.Parse(splitString[4])
                };
            }

            internal void AddToMap(int[,] map)
            {
                for (var x = OffsetX; x < OffsetX + LengthX; x++)
                {
                    for (var y = OffsetY; y < OffsetY + LengthY; y++)
                    {
                        map[x, y]++;
                    }
                }
            }

            internal bool HasNoOverlaps(int[,] map)
            {
                var hasOverlaps = true;

                for (var x = OffsetX; x < OffsetX + LengthX; x++)
                {
                    for (var y = OffsetY; y < OffsetY + LengthY; y++)
                    {
                        if (map[x,y] != 1)
                        {
                            hasOverlaps = false;
                        }
                    }
                }

                return hasOverlaps;
            }
        }
    }
}