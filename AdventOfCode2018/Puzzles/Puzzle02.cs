using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle02
    {
        public static readonly string[] Input = File.ReadAllLines(Path.Combine("Inputs", "Input02.txt"));

        public static int Part1()
        {
            var linesWithLettersTwoTimes = 0;
            var linesWithLettersThreeTimes = 0;

            foreach (var line in Input)
            {
                var characterCounts = new Dictionary<char, int>();

                foreach (var character in line)
                {
                    if (characterCounts.ContainsKey(character))
                    {
                        characterCounts[character]++;
                    }
                    else
                    {
                        characterCounts[character] = 1;
                    }
                }

                var hasACharacterTwoTimes = !characterCounts
                    .FirstOrDefault(c => c.Value == 2)
                    .Equals(default(KeyValuePair<char, int>));

                var hasACharacterThreeTimes = !characterCounts
                    .FirstOrDefault(c => c.Value == 3)
                    .Equals(default(KeyValuePair<char, int>));
                
                if (hasACharacterTwoTimes) { linesWithLettersTwoTimes++; }
                if (hasACharacterThreeTimes) { linesWithLettersThreeTimes++; }
            }

            return linesWithLettersTwoTimes * linesWithLettersThreeTimes;
        }

        public static string Part2()
        {
            var boxOneId = "";
            var boxTwoId = "";

            foreach (var line in Input)
            {
                var stringDifferingByOneChar = FindStringDifferingByOneChar(line, Input);
                if (!string.IsNullOrEmpty(stringDifferingByOneChar))
                {
                    boxOneId = line;
                    boxTwoId = stringDifferingByOneChar;
                    break;
                }
            }
            return GetMatchingChars(boxOneId, boxTwoId);
        }

        private static string FindStringDifferingByOneChar(string inputToMatch, string[] collectionToCheck)
        {
            var output = "";

            foreach (var line in collectionToCheck)
            {
                var differingCharacters = NumberOfDifferingCharacters(inputToMatch, line);
                if (differingCharacters == 1)
                {
                    output = line;
                    break;
                }
            }

            return output;
        }

        private static int NumberOfDifferingCharacters(string inputOne, string inputTwo)
        {
            var output = 0;

            for (var i = 0; i < inputOne.Length; i++)
            {
                if (inputOne[i] != inputTwo[i]) { output++; }
            }
            
            return output;
        }

        private static string GetMatchingChars(string boxOneId, string boxTwoId)
        {
            var listOfChars = new List<char>();

            for (var i = 0; i < boxOneId.Length; i++)
            {
                if (boxOneId[i] == boxTwoId[i]) { listOfChars.Add(boxOneId[i]); }
            }
            
            return string.Concat(listOfChars);
        }
    }
}