using System.Text;

namespace AdventOfCode.Day5SupplyStacks
{
    public static class Program
    {
        public static Stack<T> InitialiseStack<T>(this Stack<T> value, List<T> values)
        {
            foreach (var item in values)
                value.Push(item);

            return value;
        }

        static List<Stack<char>> crateStacks = new List<Stack<char>>
        {
            new Stack<char> ().InitialiseStack(new List<char> { 'B', 'L', 'D', 'T', 'W', 'C', 'F', 'M'} ),
            new Stack<char> ().InitialiseStack(new List<char> { 'N', 'B', 'L' }),
            new Stack<char> ().InitialiseStack(new List<char> { 'J', 'C', 'H', 'T', 'L', 'V' }),
            new Stack<char> ().InitialiseStack(new List<char> { 'S', 'P', 'J', 'W' }),
            new Stack<char> ().InitialiseStack(new List<char> { 'Z', 'S', 'C', 'F', 'T', 'L', 'R' }),
            new Stack<char> ().InitialiseStack(new List<char> { 'W', 'D', 'G', 'B', 'H', 'N', 'Z' }),
            new Stack<char> ().InitialiseStack(new List<char> { 'F', 'M', 'S', 'P', 'V', 'G', 'C', 'N'} ),
            new Stack<char> ().InitialiseStack(new List<char> { 'W', 'Q', 'R', 'J', 'F', 'V', 'C', 'Z'} ),
            new Stack<char> ().InitialiseStack(new List<char> { 'R', 'P', 'M', 'L', 'H'} )
        };

        private struct Instruction
        {
            public int numberToMove;
            public int from;
            public int to;

            public Instruction(int numberToMove, int from, int to)
            {
                this.numberToMove = numberToMove;
                this.from = from;
                this.to = to;
            }
        }

        public static void Main()
        {
            var inputFileDir = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName)?.FullName}";

            var data = File.ReadAllText($"{inputFileDir}\\Day5SupplyStacks\\input.txt")
                .Split(Environment.NewLine)
                .Select(row => row.Split(' '))
                .Select(x => new Instruction(int.Parse(x[1]), int.Parse(x[3]), int.Parse(x[5])))
                .ToList();

            // Part 1:
            //foreach (var instruction in data)
            //{
            //    for (int i = 0; i < instruction.numberToMove; i++)
            //        crateStacks[instruction.to - 1].Push(crateStacks[instruction.from - 1].Pop());
            //}

            //Console.WriteLine("Part 1:");
            //foreach (var stack in crateStacks)
            //{
            //    Console.Write($"{stack.Pop()}");
            //}
            //Console.Write(Environment.NewLine);

            // Part 2:
            Stack<char> movingCrates = new Stack<char>();
            foreach (var instruction in data)
            {
                for (int i = 0; i < instruction.numberToMove; i++)
                    movingCrates.Push(crateStacks[instruction.from - 1].Pop());

                for (int i = 0; i < instruction.numberToMove; i++)
                    crateStacks[instruction.to - 1].Push(movingCrates.Pop());
            }

            Console.WriteLine("Part 2:");
            foreach (var stack in crateStacks)
                Console.Write($"{stack.Pop()}");
        }
    }
}
