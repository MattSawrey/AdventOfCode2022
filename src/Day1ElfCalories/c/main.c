#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <direct.h>

void concatenate_string(char *s, char *s1)
{
    int i;
    int j = strlen(s);

    for (i = 0; s1[i] != '\0'; i++)
    {
        s[i + j] = s1[i];
    }
    s[i + j] = '\0';
    return;
}

// Define the resizable Vec
struct Vec
{
    int *data;
    int size;
    int capacity;
};

void vec_init(struct Vec *vec)
{
    vec->data = NULL;
    vec->size = 0;
    vec->capacity = 0;
}

void vec_push(struct Vec *vec, int value)
{
    if (vec->size == vec->capacity)
    {
        vec->capacity = vec->capacity == 0 ? 1 : vec->capacity * 2;
        vec->data = (int *)realloc(vec->data, vec->capacity * sizeof(int));
    }
    vec->data[vec->size++] = value;
}

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

    int maxCalories = 0;
    int index = 0;
    int maxIndex = 0;
    // printf("MC: %i", maxCalories);
    while (fgets(line, sizeof(line), fpointer) != NULL)
    {
        if (strlen(line) > 1)
        {
            char *endptr;
            int num = strtol(line, &endptr, 10);
            // printf("Line: %s. Num: %i \n", line, num);

            if (num > maxCalories)
            {
                maxCalories = num;
                maxIndex = index;
            }
            index++;
        }
    }

    fclose(fpointer);

    printf("Max Calories: %i. Index: %i. \n", maxCalories, maxIndex);

    // Task 2
    printf("Task 2:\n");
    FILE *fpointer2 = fopen(filePath, "r"); // r file mode to read contents of the files
    char line2[20];

    // int chunkSums[100];
    struct Vec chunkSums;
    vec_init(&chunkSums);
    int chunkSum = 0;
    int chunkIndex = 0;
    while (fgets(line2, sizeof(line2), fpointer2) != NULL)
    {
        if (strlen(line2) > 1)
        {
            char *endptr;
            int num = strtol(line2, &endptr, 10);
            chunkSum += num;
        }
        else // This is a line break between chunks
        {
            vec_push(&chunkSums, chunkSum);
            chunkIndex += 1;
            chunkSum = 0;
        }
    }

    int threeLargestCalorieCount = 0;
    for (int x = 0; x < 3; x++)
    {
        int largestCalorieCountIndex = 0;
        for (int y = 0; y < chunkSums.size; y++)
        {
            if (chunkSums.data[y] > chunkSums.data[largestCalorieCountIndex])
                largestCalorieCountIndex = y;
        }
        threeLargestCalorieCount += chunkSums.data[largestCalorieCountIndex];
        chunkSums.data[largestCalorieCountIndex] = 0;
    }

    fclose(fpointer2);

    printf("Calorie count of the 3 Elves with most calories: %i.\n", threeLargestCalorieCount);

    return 0;
}
