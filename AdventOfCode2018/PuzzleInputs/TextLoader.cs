using System.IO;

namespace AdventOfCode2018.PuzzleInputs
{
    public static class TextLoader
    {
        public static string[] FromFile(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}