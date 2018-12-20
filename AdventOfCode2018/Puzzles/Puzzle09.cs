using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle09
    {
        private static readonly string[] Input = File
            .ReadAllText(Path.Combine("Inputs", "Input09.txt"))
            .Split(' ');

        private static readonly int NumberOfPlayers = int.Parse(Input[0]);
        private static readonly int LastMarbleValue = int.Parse(Input[6]);

        public static long Part1()
        {
            var winner = GameLoop(NumberOfPlayers, LastMarbleValue);
            return winner.Score;
        }

        public static long Part2()
        {
            var winner = GameLoop(NumberOfPlayers, LastMarbleValue * 100);
            return winner.Score;
        }

        private static Player GameLoop(int numberOfPlayers, int lastMarbleValue)
        {
            var players = numberOfPlayers.Select(i => new Player()).ToArray();
            var marbleDispenser = new MarbleDispenser(lastMarbleValue + 1);

            var placedMarbles = new MarbleCollection(marbleDispenser.NextMarble());

            for (var i = 0; marbleDispenser.HasMarblesLeft; i = (i + 1) % numberOfPlayers)
            {
                var currentPlayer = players[i];

                if (marbleDispenser.NextMarbleValue % 23 == 0)
                {
                    currentPlayer.GiveMarble(marbleDispenser.NextMarble());
                    currentPlayer.GiveMarble(placedMarbles.Remove(-7));
                }
                else
                {
                    placedMarbles.Add(marbleDispenser.NextMarble(), 2);
                }
            }

            return players.OrderByDescending(p => p.Score).First();
        }

        private class Player
        {
            public long Score { get; private set; }
            public void GiveMarble(Marble marble) => Score += marble.Value;
        }

        private class Marble
        {
            public int Value { get; }
            public Marble Previous { get; private set; }
            public Marble Next { get; private set; }
            public Marble(int value) { Value = value; }
            public override string ToString() => $"Marble: {Value}";
            public void SetPrevious(Marble previous) { Previous = previous; }
            public void SetNext(Marble next) { Next = next; }

            public Marble GetNeighbor(int offset)
            {
                if (offset > 0) { return Next.GetNeighbor(offset - 1); }
                else if (offset < 0) { return Previous.GetNeighbor(offset + 1); }
                else { return this; }
            }
        }

        private class MarbleDispenser
        {
            private readonly Marble[] _marbles;
            private int _index = 0;
            public bool HasMarblesLeft => _index < _marbles.Count();
            public int NextMarbleValue => _marbles[_index].Value;

            public MarbleDispenser(int size)
            {
                _marbles = size.Select(i => new Marble(i)).ToArray();
            }

            public Marble NextMarble()
            {
                if (!HasMarblesLeft) { throw new Exception($"Dispenser out of bounds."); }
                var marble = _marbles[_index];
                _index++;
                return marble;
            }
        }

        private class MarbleCollection
        {
            public Marble CurrentMarble { get; private set; }
            public int Count { get; private set; }

            public MarbleCollection(Marble firstMarble)
            {
                CurrentMarble = firstMarble;
                CurrentMarble.SetNext(CurrentMarble);
                CurrentMarble.SetPrevious(CurrentMarble);
            }

            public void Add(Marble marble, int offset)
            {
                var target = CurrentMarble.GetNeighbor(offset);
                marble.SetPrevious(target.Previous);
                marble.SetNext(target);
                target.Previous.SetNext(marble);
                target.SetPrevious(marble);
                CurrentMarble = marble;
                Count++;
            }
            
            public Marble Remove(int offset)
            {
                var target = CurrentMarble.GetNeighbor(offset);
                CurrentMarble = target.Next;
                target.Previous.SetNext(target.Next);
                target.Next.SetPrevious(target.Previous);
                Count--;
                return target;
            }
        }
    }
}