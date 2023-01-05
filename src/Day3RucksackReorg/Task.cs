namespace AdventOfCode.Day3RucksackReorg
{
    public static class Task
    {
        const int numCharsInAlphabet = 26;

        public class Rucksack
        {
            public string Items { get; set; }

            // line lengths are always even in the dataset, so able to split by half the length.
            public string Compartment1 => Items.Substring(0, (Items.Length / 2)); // first half of the items
            public string Compartment2 => Items.Substring((Items.Length / 2), (Items.Length / 2)); // second half of the items

            public Rucksack(string items)
            {
                Items = items;
            }
        }

        public static void Run()
        {
            // Part 1:
            var data = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Day3RucksackReorg\\input.txt")
                .Split(Environment.NewLine)
                .Select(items => new Rucksack(items))
                .ToList();

            var sum1 = 0;
            foreach (var rucksack in data)
            {
                // There is only ever 1 match between the compartments
                var character = rucksack.Compartment1.Intersect(rucksack.Compartment2).FirstOrDefault();
                var value = GetAlphabetIndexOfChar(character);
                if (char.IsUpper(character))
                    value += numCharsInAlphabet;

                sum1 += value;
            }
            Console.WriteLine($"Challenge 1: {sum1}");

            // Part 2:
            var sum2 = 0;
            while (data.Any())
            {
                var top3Rucksacks = data.Take(3).Select(x => x.Items).ToList();
                var character = top3Rucksacks.Aggregate<IEnumerable<char>>((a, b) => a.Intersect(b)).FirstOrDefault();
                var value = GetAlphabetIndexOfChar(character);
                if (char.IsUpper(character))
                    value += numCharsInAlphabet;

                sum2 += value;
                data.RemoveRange(0, 3);
            }
            Console.WriteLine($"Challenge 2: {sum2}");
        }

        private static int GetAlphabetIndexOfChar(char c)
        {
            return c % 32;
        }
    }
}
