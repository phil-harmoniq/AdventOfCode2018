using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle08
    {
        public static readonly int[] Input = File
            .ReadAllText(Path.Combine("Inputs", "Input08.txt"))
            .Split(' ')
            .Select(n => int.Parse(n))
            .ToArray();

        public static int Part1()
        {
            var allNodes = new List<Node>();
            GenerateNodes(allNodes, 0);
            return allNodes.Sum(n => n.MetaDataSum);
        }

        public static object Part2()
        {
            var allNodes = new List<Node>();
            GenerateNodes(allNodes);
            return allNodes.First().GetValue();
        }

        private static int GenerateNodes(List<Node> allNodes, int index = 0, Node parent = null)
        {
            var node = new Node(Input[index], Input[index+1]);
            allNodes.Add(node);
            if (parent != null) { parent.ChildNodes.Add(node); }
            var metaDataIndex = index + 2;

            for (var n = 0; n < node.ChildCount; n++)
            {
                metaDataIndex = GenerateNodes(allNodes, metaDataIndex, node);
            }

            for (var n = 0; n < node.MetaData.Length; n++)
            {
                node.MetaData[n] = Input[metaDataIndex + n];
            }

            return metaDataIndex + node.MetaData.Length;
        }
        
        private class Node
        {
            internal List<Node> ChildNodes { get; }
            internal int ChildCount{ get; }
            internal int[] MetaData { get; }
            internal int MetaDataSum => MetaData.Sum();

            internal Node(int childCount, int metaDataCount)
            {
                ChildNodes = new List<Node>();
                ChildCount = childCount;
                MetaData = new int[metaDataCount];
            }

            internal int GetValue()
            {
                if (ChildCount == 0) { return MetaDataSum; }
                var nodesForTotal = new List<Node>();

                foreach (var data in MetaData)
                {
                    if (data > 0 && data <= ChildCount)
                    {
                        nodesForTotal.Add(ChildNodes.ElementAt(data - 1));
                    }
                }

                return nodesForTotal.Sum(n => n.GetValue());
            }
        }
    }
}