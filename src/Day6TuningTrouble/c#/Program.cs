namespace AdventOfCode.Day6TuningTrouble
{
    public static class Program
    {
        public static void Main()
        {
            var inputFileDir = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName)?.FullName}";
            var data = File.ReadAllText($"{inputFileDir}\\Day6TuningTrouble\\input.txt");

            // Part 1:
            Console.WriteLine($"Part 1: {GetIndexOfEndOfMarkerChar(data, 4)}");

            // Part 2:
            Console.WriteLine($"Part 2: {GetIndexOfEndOfMarkerChar(data, 14)}");
        }

        static int GetIndexOfEndOfMarkerChar(string data, int lengthOfMarker)
        {
            Queue<char> code = new Queue<char>();
            for (int i = 0; i < lengthOfMarker - 1; i++)
                code.Enqueue(data[i]);

            for (int i = lengthOfMarker - 1; i < data.Length; i++)
            {
                code.Enqueue(data[i]);

                if (code.Distinct().Count() == lengthOfMarker)
                    return i + 1;

                code.Dequeue();
            }

            throw new Exception("No marker found at all in the input string!");
        }
    }
}
