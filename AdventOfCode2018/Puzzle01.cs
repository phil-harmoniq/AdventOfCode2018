using System.Collections.Generic;
using AdventOfCode2018.PuzzleInputs;

namespace AdventOfCode2018
{
    public static class Puzzle01
    {
        public static int Part1()
        {
            var answer = 0;
            var input = TextLoader.FromFile("PuzzleInputs/Input01.txt");

            foreach (var line in input)
            {
                answer += int.Parse(line);
            }

            return answer;
        }

        public static int Part2()
        {
            var answer = 0;
            var uniqueValues = new List<int> { answer };
            var input = TextLoader.FromFile("PuzzleInputs/Input01.txt");

            return DuplicateCheckLoop(input, answer, uniqueValues);
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