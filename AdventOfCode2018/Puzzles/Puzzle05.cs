using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle05
    {
        public static object Part1()
        {
            var polymers = File.ReadAllText("Inputs/Input05.txt")
                .Select(c => new Polymer(c))
                .ToList();
            
            MarkDuplicates(polymers);
            var answer  = BeginReaction(polymers);

            return answer.Count();
        }

        public static object Part2()
        {
            throw new NotImplementedException();
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
                if (!polymers[c].MarkedForDeletion && polymers[c].IsOppositePolarity(polymers[c+1]))
                {
                    polymers[c].MarkedForDeletion = true;
                    polymers[c+1].MarkedForDeletion = true;
                }
            }
        }

        private class Polymer
        {
            public char Type { get; }
            public Polarity Polarity { get; }
            public bool MarkedForDeletion { get; set; }

            internal Polymer(char @char)
            {
                Type = @char;
                Polarity = char.IsUpper(@char) ? Polarity.Positive : Polarity.Negative;
            }

            internal bool IsOppositePolarity(Polymer polymer)
            {
                return char.ToLower(Type) == char.ToLower(polymer.Type) && Polarity != polymer.Polarity;
            }
        }

        private enum Polarity
        {
            Positive,
            Negative
        }
    }
}