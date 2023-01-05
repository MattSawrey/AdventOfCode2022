using System.Text;

namespace AdventOfCode.Day10CathodeRayTube
{
    public static class Task
    {
        public static void Run()
        {
            var instructions = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Day10CathodeRayTube\\input.txt")
                .Split(Environment.NewLine)
                .ToList();

            // Part1
            // single register - X. Initialises as 1
            // addx V takes 2 cycles. After 2 cycles it increases the value of X by V.
            // noop takes 1 cycle and does nothing.

            //var X = 1;
            //var signalStrengthMeasurementPoints = new int[] { 20, 60, 100, 140, 180, 220 };
            //var totalSignalStrength = 0;
            //var programCounter = 0;

            //for (int i = 0; i < instructions.Count; i++)
            //{
            //    programCounter++;

            //    if (signalStrengthMeasurementPoints.Contains(programCounter))
            //        totalSignalStrength += programCounter * X;

            //    if (instructions[i] == "noop")
            //        continue;

            //    if (instructions[i].StartsWith("addx"))
            //    {
            //        programCounter++;
            //        if (signalStrengthMeasurementPoints.Contains(programCounter))
            //            totalSignalStrength += programCounter * X;

            //        var V = int.Parse(instructions[i].Split(' ')[1]);
            //        X += V;
            //        Console.WriteLine($"Program Cycle: {programCounter}. Adding value: {V}. New X Value: {X}");
            //    }
            //}
            //Console.WriteLine(totalSignalStrength);

            // Part 2
            // X register contains the horizontal position of a sprite. The sprite is 3 pixels wide. X register represent the positions of the middle pixel 
            // No vertical position. Screen is 40 wide and 6 high
            // Each row of pixels is 40 wide

            var programCounter = -1; // Needs to start at position 0
            var rowString = new StringBuilder();
            var X = 1;

            for (int i = 0; i < instructions.Count; i++)
            {
                programCounter++;
                //Console.WriteLine($"Instruction: {instructions[i]}. Program Counter: {programCounter}. X: {X}. Is within range: {programCounter == X || programCounter == X - 1 || programCounter == X + 1}");
                if (programCounter == X ||
                    programCounter == X - 1 ||
                    programCounter == X + 1)
                    rowString.Append('#');
                else
                    rowString.Append(' ');

                if (programCounter == 39)
                {
                    Console.WriteLine(rowString);
                    rowString.Clear();
                    programCounter = -1;
                }

                if (instructions[i] == "noop")
                {
                    continue;
                }
                
                if (instructions[i].StartsWith("addx"))
                {
                    programCounter++;

                    //Console.WriteLine($"Instruction: {instructions[i]}. Program Counter: {programCounter}. X: {X}. Is within range: {programCounter == X || programCounter == X - 1 || programCounter == X + 1}");
                    if (programCounter == X ||
                        programCounter == X - 1 ||
                        programCounter == X + 1)
                        rowString.Append('#');
                    else
                        rowString.Append(' ');

                    if (programCounter == 39)
                    {
                        Console.WriteLine(rowString);
                        rowString.Clear();
                        programCounter = -1;
                    }

                    X += int.Parse(instructions[i].Split(' ')[1]);
                }
            }
            Console.WriteLine(rowString);
        }
    }
}
