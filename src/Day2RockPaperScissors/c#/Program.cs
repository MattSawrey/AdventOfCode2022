namespace AdventOfCode.Day2RockPaperScissors
{
    public static class Program
    {
        // Enum int values also denote score value from shape
        public enum Shape
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        public struct Round
        {
            public Shape OpponentChoice;
            public Shape PlayerChoice;

            public Round(Shape opponentChoice, Shape playerChoice)
            {
                OpponentChoice = opponentChoice;
                PlayerChoice = playerChoice;
            }
        }

        public static void Main()
        {
            var inputFileDir = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName)?.FullName}";

            // Part 1:
            Console.WriteLine("Task 1:");

            var data1 = File.ReadAllText($"{inputFileDir}\\Day2RockPaperScissors\\input.txt")
                .Split(Environment.NewLine)
                .Select(x => x.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                              .ToList())
                .Select(y => new Round(MapShapeFromInputValue(y[0]), MapShapeFromInputValue(y[1])))
                .ToList();

            int totalScore = 0;
            for (int i = 0; i < data1.Count; i++)
            {
                totalScore += (int)data1[i].PlayerChoice; // add the player choice score
                totalScore += CalculateResultScore(data1[i].PlayerChoice, data1[i].OpponentChoice); // Add the result score
                // Console.WriteLine($"Round {i}: Player Choice: {data1[i].PlayerChoice} v Opponent Choice: {data1[i].OpponentChoice}. Points from Player selection: {(int)data1[i].PlayerChoice}. Points from result: {CalculateResultScore(data1[i].PlayerChoice, data1[i].OpponentChoice)}");
            }
            Console.WriteLine($"Answer: {totalScore}");

            // Part 2:
            Console.WriteLine("Task 2:");

            var data2 = File.ReadAllText($"{inputFileDir}\\Day2RockPaperScissors\\input.txt")
                .Split(Environment.NewLine)
                .Select(x => x.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                              .ToList())
                .Select(y => new Round(MapShapeFromInputValue(y[0]), GetPlayerShapeFromExpectedResult(y[1], MapShapeFromInputValue(y[0]))))
                .ToList();

            int totalScore2 = 0;
            for (int i = 0; i < data2.Count; i++)
            {
                totalScore2 += (int)data2[i].PlayerChoice; // add the player choice score
                totalScore2 += CalculateResultScore(data2[i].PlayerChoice, data2[i].OpponentChoice); // Add the result score
                //Console.WriteLine($"Round {i}: Player Choice: {data2[i].PlayerChoice} v Opponent Choice: {data2[i].OpponentChoice}. Points from Player selection: {(int)data2[i].PlayerChoice}. Points from result: {CalculateResultScore(data2[i].PlayerChoice, data2[i].OpponentChoice)}");
            }
            Console.WriteLine($"Answer: {totalScore2}");

        }

        private static Shape MapShapeFromInputValue(string value)
        {
            return value switch
            {
                "A" or "X" => Shape.Rock,
                "B" or "Y" => Shape.Paper,
                "C" or "Z" => Shape.Scissors,
                _ => throw new Exception("Shape value not found in mapping from input value.")
            };
        }

        // loss = 0
        // draw = 3
        // win = 6
        private static int CalculateResultScore(Shape player, Shape opponent)
        {
            // Draw
            if (player == opponent)
                return 3;

            // Victory
            if ((player == Shape.Rock && opponent == Shape.Scissors) ||
                (player == Shape.Scissors && opponent == Shape.Paper) ||
                (player == Shape.Paper && opponent == Shape.Rock))
                return 6;

            return 0;
        }

        private static Shape GetPlayerShapeFromExpectedResult(string result, Shape opponent)
        {
            // Draw
            if (result == "Y")
                return opponent;

            if (opponent == Shape.Scissors)
                return result switch
                {
                    "X" => Shape.Paper, // Loss
                    "Z" => Shape.Rock, // Victory
                    _ => throw new Exception("Can't parse player shape from char.")
                };

            if (opponent == Shape.Rock)
                return result switch
                {
                    "X" => Shape.Scissors, // Loss
                    "Z" => Shape.Paper, // Victory
                    _ => throw new Exception("Can't parse player shape from char.")
                };

            if (opponent == Shape.Paper)
                return result switch
                {
                    "X" => Shape.Rock, // Loss
                    "Z" => Shape.Scissors, // Victory
                    _ => throw new Exception("Can't parse player shape from char.")
                };

            throw new Exception("Shape not found from the expected result value.");
        }
    }
}
