#include <stdio.h>

int main()
{
    // Get input file path
    char filePath[256];
    _getcwd(filePath, 256);
    concatenate_string(filePath, "\\input.txt");
    // printf("File path: \n%s\n", filePath);

    // Task 1
    printf("Task 1:\n");
    FILE *fpointer = fopen(filePath, "r"); // r file mode to read contents of the files
    char line[20];

    while (fgets(line, sizeof(line), fpointer) != NULL)
    {
    }

    fclose(fpointer);

    printf("\n");

    // Task 2
    printf("Task 2:\n");

    return 0;
}
