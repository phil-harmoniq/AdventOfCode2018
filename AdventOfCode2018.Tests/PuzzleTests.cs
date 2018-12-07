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

        #region Puzzle03
        [Fact]
        public void Puzzle03Part1Test()
        {
            var answer = Puzzle03.Part1();
            Console.WriteLine($"The answer to puzzle 3 part 1 is: {answer}");
            Assert.Equal(120408, answer);
        }

        [Fact]
        public void Puzzle03Part2Test()
        {
            var answer = Puzzle03.Part2();
            Console.WriteLine($"The answer to puzzle 3 part 2 is: {answer}");
            Assert.Equal(1276, answer);
        }
        #endregion
        
        #region Puzzle04
        [Fact]
        public void Puzzle04Part1Test()
        {
            var answer = Puzzle04.Part1();
            Console.WriteLine($"The answer to puzzle 4 part 1 is: {answer}");
            Assert.Equal(39698, answer);
        }

        [Fact]
        public void Puzzle04Part2Test()
        {
            var answer = Puzzle04.Part2();
            Console.WriteLine($"The answer to puzzle 4 part 2 is: {answer}");
            Assert.Equal(14920, answer);
        }
        #endregion

        #region Puzzle04
        [Fact]
        public void Puzzle05Part1Test()
        {
            var answer = Puzzle05.Part1();
            Console.WriteLine($"The answer to puzzle 5 part 1 is: {answer}");
            Assert.Equal(9900, answer);
        }

        [Fact]
        public void Puzzle05Part2Test()
        {
            var answer = Puzzle05.Part2();
            Console.WriteLine($"The answer to puzzle 5 part 2 is: {answer}");
            Assert.Equal(14920, answer);
        }
        #endregion
    }
}