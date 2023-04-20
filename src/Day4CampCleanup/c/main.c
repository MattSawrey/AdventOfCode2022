#include <direct.h>
#include <stdio.h>
#include <stdbool.h>

bool ranges_overlap_fully(char *s)
{
    int range1Start, range1End, range2Start, range2End;
    sscanf(s, "%d-%d,%d-%d", &range1Start, &range1End, &range2Start, &range2End);

    // Check if one range is fully contained within the other
    return (range1Start <= range2Start && range1End >= range2End) ||
           (range2Start <= range1Start && range2End >= range1End);
}

bool ranges_overlap_partially(char *s)
{
    int range1Start, range1End, range2Start, range2End;
    sscanf(s, "%d-%d,%d-%d", &range1Start, &range1End, &range2Start, &range2End);

    // Check if the ranges intersect
    int maxStart = range1Start > range2Start ? range1Start : range2Start;
    int minEnd = range1End < range2End ? range1End : range2End;
    return maxStart <= minEnd;
}

int main()
{
    // Get input file path
    char filePath[256] = "";
    sprintf(filePath, "%s\\input.txt", _getcwd(NULL, 0));
    FILE *fpointer = fopen(filePath, "r"); // r file mode to read contents of the files
    char *line = NULL;
    size_t lineLength = 0;

    int total_fully_intersecting_count = 0;
    int any_intersecting_count = 0;
    while (getline(&line, &lineLength, fpointer) != -1)
    {
        if (ranges_overlap_fully(line))
        {
            total_fully_intersecting_count++;
        }

        if (ranges_overlap_partially(line))
        {
            any_intersecting_count++;
        }
    }
    fclose(fpointer);
    printf("Challenge 1: %i.\n", total_fully_intersecting_count);
    printf("Challenge 2: %i.\n", any_intersecting_count);

    return 0;
}
