namespace AdventOfCode.Day8TreetopTreeHouse
{
    public static class Program
    {
        public static void Main()
        {
            var inputFileDir = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName)?.FullName}";
            var forest = File.ReadAllText($"{inputFileDir}\\Day8TreetopTreeHouse\\input.txt")
                .Split(Environment.NewLine);

            //int[,] forest = new int[forest.GetLength(0), forest.Length];

            //for(int i = 0; i < forest.Length; i++) // for each row
            //    for (int j = 0; j < forest.Length; j++) // for each character in that row
            //        forest[i, j] = forest[i][j];

            // Part 1:
            //int forestWidth = forest[0].Length;
            //int forestHeight = forest.GetLength(0);

            //int visibleTrees = 0;

            //// Check every tree
            //for (int row = 0; row < forestHeight; row++)
            //    for (int col = 0; col < forestWidth; col++)
            //    {
            //        bool isVisible = false;

            //        // is this an edge tree?
            //        if(row == 0 || row == forestHeight - 1 || col == 0 || col == forestWidth - 1)
            //            isVisible = true;

            //        // Check North
            //        if(!isVisible)
            //            for (int fromTreeRow = row - 1; fromTreeRow >= 0; fromTreeRow--)
            //            {
            //                //Console.WriteLine($"Checking: {col},{row} height: {forest[col][row]}. Against: {forest[col][fromTreeRow]}");
            //                if (forest[col][fromTreeRow] >= forest[col][row])
            //                {
            //                    isVisible = false;
            //                    break;
            //                }

            //                if (fromTreeRow == 0) // We've reached the edge without finding a blocking tree
            //                {
            //                    isVisible = true;
            //                    break;
            //                }
            //            }

            //        // Check South
            //        if(!isVisible)
            //            for (int fromTreeRow = row + 1; fromTreeRow <= forestHeight - 1; fromTreeRow++)
            //            {
            //                //Console.WriteLine($"Checking: {col},{row} height: {forest[col][row]}. Against: {forest[col][fromTreeRow]}");
            //                if (forest[col][fromTreeRow] >= forest[col][row])
            //                {
            //                    //Console.WriteLine($"{forest[col][row]} - blocking tree: {forest[col][fromTreeRow]}");
            //                    isVisible = false;
            //                    break;
            //                }

            //                if (fromTreeRow == forestHeight - 1) // We've reached the edge without finding a blocking tree
            //                {
            //                    isVisible = true;
            //                    break;
            //                }
            //            }

            //        // Check East
            //        if (!isVisible)
            //            for (int fromTreeCol = col - 1; fromTreeCol >= 0; fromTreeCol--)
            //            {
            //                Console.WriteLine($"Checking: {col},{row} height: {forest[col][row]}. Against: {forest[fromTreeCol][row]}");
            //                if (forest[fromTreeCol][row] >= forest[col][row])
            //                {
            //                    //Console.WriteLine($"{forest[col][row]} - blocking tree: {forest[fromTreeCol][row]}");
            //                    isVisible = false;
            //                    break;
            //                }

            //                if (fromTreeCol == 0) // We've reached the edge without finding a blocking tree
            //                {
            //                    isVisible = true;
            //                    break;
            //                }
            //            }

            //        // Check west
            //        if (!isVisible)
            //            for (int fromTreeCol = col + 1; fromTreeCol <= forestWidth - 1; fromTreeCol++)
            //            {
            //                Console.WriteLine($"Checking: {col},{row} height: {forest[col][row]}. Against: {forest[fromTreeCol][row]}");
            //                if (forest[fromTreeCol][row] >= forest[col][row])
            //                {
            //                    //Console.WriteLine($"{forest[col][row]} - blocking tree: {forest[fromTreeCol][row]}");
            //                    isVisible = false;
            //                    break;
            //                }

            //                if (fromTreeCol == forestWidth - 1) // We've reached the edge without finding a blocking tree
            //                {
            //                    Console.WriteLine("---------");
            //                    isVisible = true;
            //                    break;
            //                }
            //            }

            //        if (isVisible)
            //            visibleTrees++;
            //    }
            //Console.WriteLine($"Part 1: {visibleTrees}");

            // Part 2:
            int forestWidth = forest[0].Length;
            int forestHeight = forest.GetLength(0);

            // Check every tree
            int maxScenicScore = 0;
            for (int row = 0; row < forestHeight; row++)
                for (int col = 0; col < forestWidth; col++)
                {
                    int numberVisibleTreesNorth = 0;
                    int numberVisibleTreesSouth = 0;
                    int numberVisibleTreesWest = 0;
                    int numberVisibleTreesEast = 0;

                    // Check North
                    for (int fromTreeRow = row - 1; fromTreeRow >= 0; fromTreeRow--)
                    {
                        //Console.WriteLine($"Checking: {col},{row} height: {forest[col][row]}. Against: {forest[col][fromTreeRow]}");
                        if (fromTreeRow == -1) // this is an edge tree
                            break;

                        numberVisibleTreesNorth += 1;

                        if (forest[col][fromTreeRow] >= forest[col][row])
                            break;
                    }

                    // Check South
                    for (int fromTreeRow = row + 1; fromTreeRow <= forestHeight - 1; fromTreeRow++)
                    {
                        //Console.WriteLine($"Checking: {col},{row} height: {forest[col][row]}. Against: {forest[col][fromTreeRow]}");
                        if (fromTreeRow == forestHeight) // this is an edge tree
                            break;

                        numberVisibleTreesSouth += 1;

                        if (forest[col][fromTreeRow] >= forest[col][row])
                            break;
                    }

                    // Check East
                    for (int fromTreeCol = col - 1; fromTreeCol >= 0; fromTreeCol--)
                    {
                        //Console.WriteLine($"Checking: {col},{row} height: {forest[col][row]}. Against: {forest[fromTreeCol][row]}");

                        if (fromTreeCol == -1) // this is an edge tree
                            break;

                        numberVisibleTreesEast += 1;

                        if (forest[fromTreeCol][row] >= forest[col][row])
                            break;
                    }

                    // Check west
                    for (int fromTreeCol = col + 1; fromTreeCol <= forestWidth - 1; fromTreeCol++)
                    {
                        //Console.WriteLine($"Checking: {col},{row} height: {forest[col][row]}. Against: {forest[fromTreeCol][row]}");
                        if (fromTreeCol == forestWidth) // this is an edge tree
                            break;

                        numberVisibleTreesWest += 1;

                        if (forest[fromTreeCol][row] >= forest[col][row])
                            break;
                    }

                    var scenicScore = numberVisibleTreesNorth * numberVisibleTreesSouth * numberVisibleTreesEast * numberVisibleTreesWest;
                    if (scenicScore > maxScenicScore)
                        maxScenicScore = scenicScore;
                }
            Console.WriteLine($"Part 2: {maxScenicScore}");
        }
    }
}
