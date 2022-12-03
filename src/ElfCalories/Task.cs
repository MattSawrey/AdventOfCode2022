namespace AdventCode.ElfCalories
{
    public static class Task
    {
        public static void Run()
        {
            var elfCalories = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\ElfCalories\\input.txt")
                .Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(x => x.Split(Environment.NewLine)
                              .Select(y => int.Parse(y))
                              .Sum())
                .ToList();

            // Part1
            int maxCaloriesIndex = 0;
            for (int i = 1; i < elfCalories.Count; i++)
            {
                if (elfCalories[i] > elfCalories[maxCaloriesIndex])
                    maxCaloriesIndex = i;
            }
            Console.WriteLine($"Elf with the most calories is Elf number {maxCaloriesIndex}, with {elfCalories[maxCaloriesIndex]} calories.");

            // Part 2
            int threeLargestCalorieCount = 0;
            for (int x = 0; x < 3; x++)
            {
                int largestCalorieCountIndex = 0;
                for (int i = 1; i < elfCalories.Count; i++)
                {
                    if (elfCalories[i] > elfCalories[largestCalorieCountIndex])
                        largestCalorieCountIndex = i;
                }
                threeLargestCalorieCount += elfCalories[largestCalorieCountIndex];
                elfCalories.RemoveAt(largestCalorieCountIndex);
            }
            Console.WriteLine($"Calorie count of the 3 Elves with most calories is {threeLargestCalorieCount}.");
        }
    }
}
