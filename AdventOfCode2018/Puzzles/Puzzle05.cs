using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle05
    {
        public static readonly string Input = File.ReadAllText(Path.Combine("Inputs", "Input05.txt"));
        
        public static int Part1()
        {
            var polymers = Input.Select(c => new Polymer(c)).ToList();

            MarkDuplicates(polymers);
            var answer = BeginReaction(polymers);

            return answer.Count();
        }

        public static int Part2()
        {
            var polymers = Input.Select(c => new Polymer(c));
            var results = new Dictionary<char, IEnumerable<Polymer>>();

            for (var c = 'a'; c <= 'z'; c++)
            {
                var uniquePolymers = new List<Polymer>(polymers)
                    .Where(p => !p.IsType(c))
                    .ToList();
                
                MarkDuplicates(uniquePolymers);
                results[c] = BeginReaction(uniquePolymers);
            }

            return results.OrderBy(x => x.Value.Count()).First().Value.Count();
        }

        private static IEnumerable<Polymer> BeginReaction(List<Polymer> polymers)
        {
            polymers = polymers.Where(polymer => !polymer.MarkedForDeletion).ToList();
            MarkDuplicates(polymers);

            if (polymers.Any(p => p.MarkedForDeletion)) { return BeginReaction(polymers); }
            else { return polymers; }
        }

        private static void MarkDuplicates(List<Polymer> polymers)
        {
            for (var c = 0; c < polymers.Count - 1; c++)
            {
                if (!polymers[c].MarkedForDeletion && polymers[c].IsOppositePolarity(polymers[c + 1]))
                {
                    polymers[c].MarkedForDeletion = true;
                    polymers[c + 1].MarkedForDeletion = true;
                }
            }
        }

        private class Polymer
        {
            public char Type { get; }
            public Polarity Polarity { get; }
            public bool MarkedForDeletion { get; set; }
            internal bool IsType(char @char) => char.ToLower(Type) == char.ToLower(@char);
            internal bool IsType(Polymer polymer) => IsType(polymer.Type);

            internal Polymer(char @char)
            {
                Type = @char;
                Polarity = char.IsUpper(@char) ? Polarity.Positive : Polarity.Negative;
            }

            internal bool IsOppositePolarity(Polymer polymer)
            {
                return IsType(polymer) && Polarity != polymer.Polarity;
            }
        }

        private enum Polarity
        {
            Positive,
            Negative
        }
    }
}