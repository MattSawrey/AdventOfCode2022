namespace AdventOfCode.Day9RopeBridge
{
    public struct MovementInstruction
    {
        public MovementInstruction(char direction, int distance)
        {
            Direction = direction;
            Distance = distance;
        }

        public char Direction { get; set; }
        public int Distance { get; set; }
    }

    public struct GridPosition
    {
        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static GridPosition operator +(GridPosition a, GridPosition b) => new GridPosition(a.X + b.X, a.Y + b.Y);
        public static GridPosition operator -(GridPosition a, GridPosition b) => new GridPosition(a.X - b.X, a.Y - b.Y);
        public static bool operator ==(GridPosition a, GridPosition b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(GridPosition a, GridPosition b) => a.X != b.X || a.Y != b.Y;
        public int MaxMagnitude => Math.Max(Math.Abs(X), Math.Abs(Y));

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var inputFileDir = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName)?.FullName}";
            var instructions = File.ReadAllText($"{inputFileDir}\\Day9RopeBridge\\input.txt")
                .Split(Environment.NewLine)
                .Select(x => x.Split(" "))
                .Select(y => new MovementInstruction(char.Parse(y[0]), int.Parse(y[1])))
                .ToList();

            // Part 1:
            //List<GridPosition> visitedTailPositions = new List<GridPosition>();
            //GridPosition headPosition = new GridPosition(0, 0);
            //GridPosition tailPosition = new GridPosition(0, 0);

            //visitedTailPositions.Add(tailPosition);
            //for (int i = 0; i < instructions.Count; i++)
            //{
            //    for (int m = 0; m < instructions[i].Distance; m++)
            //    {
            //        // Move the head
            //        switch (instructions[i].Direction)
            //        {
            //            case 'U':
            //                headPosition.Y++;
            //                break;
            //            case 'D':
            //                headPosition.Y--;
            //                break;
            //            case 'R':
            //                headPosition.X++;
            //                break;
            //            case 'L':
            //                headPosition.X--;
            //                break;
            //            default:
            //                throw new Exception("Direction not recognised. Broken input....");
            //        }

            //        var headTailPositionDiff = headPosition - tailPosition;
            //        if (headTailPositionDiff.MaxMagnitude == 2)
            //        {
            //            var xDiff = Math.Sign(headTailPositionDiff.X);
            //            var yDiff = Math.Sign(headTailPositionDiff.Y);

            //            tailPosition += new GridPosition(xDiff, yDiff);

            //            if (!visitedTailPositions.Contains(tailPosition))
            //                visitedTailPositions.Add(tailPosition);
            //        }
            //    }
            //}
            //Console.WriteLine($"No. visited tail positions: {visitedTailPositions.Count()}");

            // Part 2:
            var ropeLength = 10;
            List<GridPosition> visitedTailPositions = new List<GridPosition>();
            GridPosition[] ropePositions = new GridPosition[ropeLength];

            visitedTailPositions.Add(ropePositions[ropeLength - 1]);
            for (int i = 0; i < instructions.Count; i++)
            {
                for (int m = 0; m < instructions[i].Distance; m++)
                {
                    // Move the head
                    switch (instructions[i].Direction)
                    {
                        case 'U':
                            ropePositions[0].Y++;
                            break;
                        case 'D':
                            ropePositions[0].Y--;
                            break;
                        case 'R':
                            ropePositions[0].X++;
                            break;
                        case 'L':
                            ropePositions[0].X--;
                            break;
                        default:
                            throw new Exception("Direction not recognised. Broken input....");
                    }

                    // Move the trailing positions
                    for (int j = 1; j < ropeLength; j++)
                    {
                        var headTailPositionDiff = ropePositions[j - 1] - ropePositions[j];
                        if (headTailPositionDiff.MaxMagnitude == 2)
                        {
                            var xDiff = Math.Sign(headTailPositionDiff.X);
                            var yDiff = Math.Sign(headTailPositionDiff.Y);

                            ropePositions[j] += new GridPosition(xDiff, yDiff);
                        }
                    }

                    if (!visitedTailPositions.Contains(ropePositions[ropeLength - 1]))
                        visitedTailPositions.Add(ropePositions[ropeLength - 1]);
                }
            }
            Console.WriteLine($"No. visited tail positions: {visitedTailPositions.Count()}");
        }
    }
}
