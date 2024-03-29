﻿using System.Text;

namespace AdventOfCode.Day7NoSpaceLeftOnDevice
{
    public static class Program
    {
        public static void Main()
        {
            var inputFileDir = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName)?.FullName}";
            var data = File.ReadAllText($"{inputFileDir}\\Day7NoSpaceLeftOnDevice\\input.txt")
                .Split($"{Environment.NewLine}")
                .ToList();

            // Build the directory structure
            Dir currentDirectory = new Dir("/", null);
            foreach (var instruction in data)
            {
                var instructionElements = instruction.Split(' ');

                if (instructionElements[0] == "$")
                {
                    if (instructionElements[1] == "cd")
                    {
                        if (instructionElements[2] == "..")
                        {
                            currentDirectory = currentDirectory.ParentDirectory;
                        }
                        else if (instructionElements[2] == "/")
                        {
                            currentDirectory = currentDirectory.GetDirectoryOrParentDirectoryWithName(instructionElements[2]);
                        }
                        else
                        {
                            currentDirectory = currentDirectory.SubDirectories.FirstOrDefault(x => x.Name == instructionElements[2]);
                        }
                    }
                    else if (instructionElements[1] == "ls")
                    {
                        // don't need to do anything
                    }
                }
                else if (instructionElements[0] == "dir")
                {
                    currentDirectory.AddDirectory(instructionElements[1], currentDirectory);
                }
                else if (int.TryParse(instructionElements[0], out var fileSize))
                {
                    currentDirectory.FileSizes.Add(int.Parse(instructionElements[0]));
                }
            }
            Console.WriteLine($"Finished building directory structure!");

            // Part 1:
            //currentDirectory = currentDirectory.GetDirectoryOrParentDirectoryWithName("/");
            //Console.WriteLine(currentDirectory.GetSumOfFileSizesFromAllDirsWithFilesBelowThreshold(100000));

            // Part 2:
            currentDirectory = currentDirectory.GetDirectoryOrParentDirectoryWithName("/");
            int totalDiskSpace = 70000000;
            int totalFreeSpaceNeeded = 30000000;
            int totalSizeOfAllCurrentFiles = currentDirectory.GetTotalSizeOfAllFilesInDirectoryAndSubDirectories();
            Console.WriteLine($"Total size of all current files: {totalSizeOfAllCurrentFiles}");
            int currentTotalFreeSpace = totalDiskSpace - totalSizeOfAllCurrentFiles;
            int neededFreeSpace = totalFreeSpaceNeeded - currentTotalFreeSpace;
            Console.WriteLine($"Need to free up: {neededFreeSpace}");
            List<int> directoryFileSizes = new List<int>();
            currentDirectory.BuildDirectoryWithSizesDataset(directoryFileSizes);
            directoryFileSizes = directoryFileSizes.OrderBy(x => x).ToList();
            Console.WriteLine($"{directoryFileSizes.FirstOrDefault(x => x > neededFreeSpace)}");
        }

        public class Dir
        {
            public string Name { get; set; }
            public Dir ParentDirectory { get; set; }
            public List<Dir> SubDirectories { get; set; }
            public List<int> FileSizes { get; set; }

            public Dir(string name, Dir parentDirectory)
            {
                Name = name;
                ParentDirectory = parentDirectory;
                SubDirectories = new List<Dir>();
                FileSizes = new List<int>();
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"\t {Name} - total size of all files in this dir and all sub dirs - {GetTotalSizeOfAllFilesInDirectoryAndSubDirectories()}");

                sb.AppendLine($"\t {FileSizes.Count()} Files:");
                foreach (var file in FileSizes)
                    sb.AppendLine($"\t {file}");

                sb.AppendLine($"\t {SubDirectories.Count()} Sub Directories:");
                foreach (var directory in SubDirectories)
                    sb.AppendLine($"\t {directory.ToString()}");

                return sb.ToString();
            }

            public void AddDirectory(string name)
            {
                SubDirectories.Add(new Dir(name, this));
            }

            public void AddDirectory(string name, Dir directory)
            {
                SubDirectories.Add(new Dir(name, directory));
            }

            // the file size is always the first element of the file name string
            public int TotalFileSize()
            {
                return FileSizes.Sum();
            }

            public int GetTotalSizeOfAllFilesInDirectoryAndSubDirectories()
            {
                return TotalFileSize() + SubDirectories.Select(x => x.GetTotalSizeOfAllFilesInDirectoryAndSubDirectories()).Sum();
            }

            public Dir GetDirectoryOrParentDirectoryWithName(string name)
            {
                if (Name == name)
                    return this;
                else if (ParentDirectory.Name == name)
                    return ParentDirectory;
                else
                    return ParentDirectory.GetDirectoryOrParentDirectoryWithName(name);
            }

            public int GetSumOfFileSizesFromAllDirsWithFilesBelowThreshold(int threshold)
            {
                int sum = GetTotalSizeOfAllFilesInDirectoryAndSubDirectories();
                Console.WriteLine($"{Name} - {sum}");
                if (sum >= threshold)
                    sum = 0;

                sum += SubDirectories.Select(x => x.GetSumOfFileSizesFromAllDirsWithFilesBelowThreshold(threshold)).Sum();

                return sum;
            }

            public void BuildDirectoryWithSizesDataset(List<int> directorySizes)
            {
                directorySizes.Add(GetTotalSizeOfAllFilesInDirectoryAndSubDirectories());
                foreach (var directory in SubDirectories)
                    directory.BuildDirectoryWithSizesDataset(directorySizes);
            }
        }
    }
}
