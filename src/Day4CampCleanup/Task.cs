using System.Text;

namespace AdventOfCode.Day4CampCleanup
{
    public static class Task
    {
        public static void Run()
        {
            // Part 1:
            var data1 = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Day4CampCleanup\\input.txt")
                .Split(Environment.NewLine)
                .Select(x => x.Split(',')
                              .Select(y => y.Split('-'))
                              .Select(z => new int[] { int.Parse(z[0]), int.Parse(z[1]) })
                              .Select(q => Enumerable.Range(q[0], (q[1] - q[0]) + 1)
                                                     .ToList())
                              .ToList())
                .ToList();

            var sum1 = 0;
            foreach (var pairAssignment in data1)
            {
                var elf1 = pairAssignment[0];
                var elf2 = pairAssignment[1];

                //OutputElfAssignmentData(elf1, elf2);

                if (elf1.Intersect(elf2).Count() == elf2.Count ||
                    elf2.Intersect(elf1).Count() == elf1.Count)
                    sum1++;
            }
            Console.WriteLine($"Challenge 1: {sum1}");

            // Part 2:
            var sum2 = 0;
            foreach (var pairAssignment in data1)
            {
                var elf1 = pairAssignment[0];
                var elf2 = pairAssignment[1];

                //OutputElfAssignmentData(elf1, elf2);

                if (elf1.Intersect(elf2).Any() ||
                    elf2.Intersect(elf1).Any())
                    sum2++;
            }
            Console.WriteLine($"Challenge 2: {sum2}");
        }

        private static void OutputElfAssignmentData(List<int> elf1Assignments, List<int> elf2Assignments)
        {
            Console.WriteLine("Elf 1 assignment:");
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var area in elf1Assignments)
                stringBuilder.Append($"{area},");
            Console.WriteLine(stringBuilder.ToString());
            Console.Write("\n");

            Console.WriteLine("Elf 2 assignment:");
            StringBuilder stringBuilder2 = new StringBuilder();
            foreach (var area in elf2Assignments)
                stringBuilder2.Append($"{area},");
            Console.WriteLine(stringBuilder2.ToString());
            Console.Write("\n");
        }
    }
}
