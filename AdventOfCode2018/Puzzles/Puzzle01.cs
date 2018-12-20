using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018.Puzzles
{
    public static class Puzzle01
    {
        public static readonly string[] Input = File.ReadAllLines(Path.Combine("Inputs", "Input01.txt"));

        public static int Part1()
        {
            var answer = 0;

            foreach (var line in Input)
            {
                answer += int.Parse(line);
            }

            return answer;
        }

        public static int Part2()
        {
            var answer = 0;
            var uniqueValues = new List<int> { answer };

            return DuplicateCheckLoop(Input, answer, uniqueValues);
        }

        private static int DuplicateCheckLoop(string[] input, int answer, List<int> uniqueValues)
        {
            var duplicateFound = false;

            foreach (var line in input)
            {
                answer += int.Parse(line);
                if (uniqueValues.Contains(answer))
                {
                    duplicateFound = true;
                    break;
                }
                else
                {
                    uniqueValues.Add(answer);
                }
            }

            if (duplicateFound) { return answer; }
            else { return DuplicateCheckLoop(input, answer, uniqueValues); }
        }
    }
}