using System.Collections.Generic;

namespace AdventOfCode.Day1ElfCalories
{
    public static class Program
    {
        public static void Main()
        {
            // Task 1
            Console.WriteLine("Task 1:");

            var inputFileDir1 = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName)?.FullName}";
            var elfCalories1 = File.ReadAllText($"{inputFileDir1}\\Day1ElfCalories\\input.txt")
                .Split(Environment.NewLine)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => int.Parse(x))
                .ToList();

            int maxCaloriesIndex = 0;
            for (int i = 1; i < elfCalories1.Count; i++)
            {
                if (elfCalories1[i] > elfCalories1[maxCaloriesIndex])
                    maxCaloriesIndex = i;
            }
            Console.WriteLine($"Max Calories: {elfCalories1[maxCaloriesIndex]}. Index: {maxCaloriesIndex}.");

            // Task 2
            Console.WriteLine("Task 2:");

            var inputFileDir2 = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName)?.FullName}";
            var elfCalories2 = File.ReadAllText($"{inputFileDir2}\\Day1ElfCalories\\input.txt")
                .Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(x => x.Split(Environment.NewLine)
                              .Select(y => int.Parse(y))
                              .Sum())
                .ToList();

            int threeLargestCalorieCount = 0;
            for (int x = 0; x < 3; x++)
            {
                int largestCalorieCountIndex = 0;
                for (int i = 1; i < elfCalories2.Count; i++)
                {
                    if (elfCalories2[i] > elfCalories2[largestCalorieCountIndex])
                        largestCalorieCountIndex = i;
                }
                threeLargestCalorieCount += elfCalories2[largestCalorieCountIndex];
                elfCalories2.RemoveAt(largestCalorieCountIndex);
            }
            Console.WriteLine($"Calorie count of the 3 Elves with most calories: {threeLargestCalorieCount}.");
        }
    }
}