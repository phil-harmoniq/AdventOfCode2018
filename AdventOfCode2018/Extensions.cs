using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public static class Extensions
    {
        public static void Enumerate2D<T>(this T[,] input, Action<T> action)
        {
            Enumerate2D(input.GetLength(0), input.GetLength(1), (x, y) => action(input[x,y]));
        }

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
            for (var y = yOffset; y < yLength; y++)
            {
                for (var x = xOffset; x < xLength; x++)
                {
                    action(x, y);
                }
            }
        }

        public static void Enumerate(this int count, Action<int> action)
        {
            for (var i = 0; i < count; i++)
            {
                action(i);
            }
        }

        public static IEnumerable<T> Select<T>(this int count, Func<int, T> action)
        {
            var output = new T[count];
            return output.Select((item, index) => action(index));
        }
    }
}