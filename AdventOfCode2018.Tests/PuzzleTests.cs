using System;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class PuzzleTests
    {
        [Fact]
        public void Puzzle01Test()
        {
            var answer = Puzzle01.Part1();
            Console.WriteLine($"The answer to puzzle 1 part 1 is: {answer}");
        }

        [Fact]
        public void Puzzle02Test()
        {
            var answer = Puzzle01.Part2();
            Console.WriteLine($"The answer to puzzle 1 part 2 is: {answer}");
        }
    }
}