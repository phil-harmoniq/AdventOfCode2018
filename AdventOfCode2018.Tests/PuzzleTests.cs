using System;
using System.Diagnostics;
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
            var answer = TestTimer("01", "1", Puzzle01.Part1);
            Assert.Equal(408, answer);
        }

        [Fact]
        public void Puzzle01Part2Test()
        {
            var answer = TestTimer("01", "2", Puzzle01.Part2);
            Assert.Equal(55250, answer);
        }
        #endregion

        #region Puzzle02
        [Fact]
        public void Puzzle02Part1Test()
        {
            var answer = TestTimer("02", "1", Puzzle02.Part1);
            Assert.Equal(8820, answer);
        }

        [Fact]
        public void Puzzle02Part2Test()
        {
            var answer = TestTimer("02", "2", Puzzle02.Part2);
            Assert.Equal("bpacnmglhizqygfsjixtkwudr", answer);
        }
        #endregion

        #region Puzzle03
        [Fact]
        public void Puzzle03Part1Test()
        {
            var answer = TestTimer("03", "1", Puzzle03.Part1);
            Assert.Equal(120408, answer);
        }

        [Fact]
        public void Puzzle03Part2Test()
        {
            var answer = TestTimer("03", "2", Puzzle03.Part2);
            Assert.Equal(1276, answer);
        }
        #endregion
        
        #region Puzzle04
        [Fact]
        public void Puzzle04Part1Test()
        {
            var answer = TestTimer("04", "1", Puzzle04.Part1);
            Assert.Equal(39698, answer);
        }

        [Fact]
        public void Puzzle04Part2Test()
        {
            var answer = TestTimer("04", "2", Puzzle04.Part2);
            Assert.Equal(14920, answer);
        }
        #endregion

        #region Puzzle05
        [Fact]
        public void Puzzle05Part1Test()
        {
            var answer = TestTimer("05", "1", Puzzle05.Part1);
            Assert.Equal(9900, answer);
        }

        [Fact]
        public void Puzzle05Part2Test()
        {
            var answer = TestTimer("05", "2", Puzzle05.Part2);
            Assert.Equal(4992, answer);
        }
        #endregion

        #region Puzzle06
        [Fact]
        public void Puzzle06Part1Test()
        {
            var answer = TestTimer("06", "1", Puzzle06.Part1);
            Assert.Equal(3238, answer);
        }

        [Fact]
        public void Puzzle06Part2Test()
        {
            var answer = TestTimer("06", "2", Puzzle06.Part2);
            Assert.Equal(45046, answer);
        }
        #endregion

        private static T TestTimer<T>(string puzzle, string part, Func<T> test)
        {
            var timer = new Stopwatch();
            timer.Start();
            var answer = test();
            timer.Stop();
            Console.WriteLine();
            Console.WriteLine($"The answer to puzzle {puzzle} part {part} is: {answer}");
            Console.WriteLine($"Time elapsed: {timer.Elapsed.ToString(@"mm\:ss\.ff")}");
            return answer;
        }
    }
}