using System;
using AdventOfCode2018.Puzzles;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class PuzzleTests
    {
        #region Puzzle01
        [Fact]
        public void Puzzle01Part1Test()
        {
            var answer = Puzzle01.Part1();
            Console.WriteLine($"The answer to puzzle 1 part 1 is: {answer}");
            Assert.Equal(408, answer);
        }

        [Fact]
        public void Puzzle01Part2Test()
        {
            var answer = Puzzle01.Part2();
            Console.WriteLine($"The answer to puzzle 1 part 2 is: {answer}");
            Assert.Equal(55250, answer);
        }
        #endregion

        #region Puzzle02
        [Fact]
        public void Puzzle02Part1Test()
        {
            var answer = Puzzle02.Part1();
            Console.WriteLine($"The answer to puzzle 2 part 1 is: {answer}");
            Assert.Equal(8820, answer);
        }

        [Fact]
        public void Puzzle02Part2Test()
        {
            var answer = Puzzle02.Part2();
            Console.WriteLine($"The answer to puzzle 2 part 2 is: {answer}");
            Assert.Equal("bpacnmglhizqygfsjixtkwudr", answer);
        }
        #endregion
    }
}