using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle09
    {
        private static readonly string[] Input = File
            .ReadAllText("Inputs/Input09.txt")
            .Split(' ');

        public static readonly int NumberOfPlayers = int.Parse(Input[0]);
        public static readonly int LastMarbleValue = int.Parse(Input[6]);

        public static object Part1()
        {
            var players = NumberOfPlayers.Select(i => new Player(i));
            throw new NotImplementedException();
        }

        public static object Part2()
        {
            var players = NumberOfPlayers.Select(i => new Player(i));
            throw new NotImplementedException();
        }

        private class Player
        {
            internal int Id { get; }
            private List<Marble> _marbles;
            internal IReadOnlyCollection<Marble> Marbles => _marbles.AsReadOnly();
            internal void GiveMarble(Marble marble) => _marbles.Add(marble);

            internal Player(int id)
            {
                _marbles = new List<Marble>();
                Id = id;
            }
        }
        
        private class Marble
        {
            internal int Id { get; }
            internal int Points { get; }

            internal Marble(int points)
            {
                Points = points;
            }
        }
    }
}