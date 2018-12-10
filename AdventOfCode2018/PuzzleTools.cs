using System;

namespace AdventOfCode2018
{
    public static class PuzzleTools
    {
        public static void Enumerate2D<T>(this T[,] input, Action<T, int, int> action)
        {
            Enumerate2D(input.GetLength(0), input.GetLength(1), (x, y) => action(input[x,y], x, y));
        }
        
        public static void Enumerate2D(int xLength, int yLength, Action<int, int> action)
        {
            Enumerate2D(0, 0, xLength, yLength, (x, y) => action(x, y));
        }
        
        public static void Enumerate2D(int xOffset, int yOffset, int xLength, int yLength, Action<int, int> action)
        {
            for (var x = xOffset; x < xLength; x++)
            {
                for (var y = yOffset; y < yLength; y++)
                {
                    action(x, y);
                }
            }
        }
    }
}