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

enum Shape
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
};
const char *shape_to_string(enum Shape shape)
{
    switch (shape)
    {
    case Rock:
        return "Rock";
    case Paper:
        return "Paper";
    case Scissors:
        return "Scissors";
    }
}

enum Shape shape_from_input_value(char character)
{
    switch (character)
    {
    case 'A':
    case 'X':
        return Rock;
    case 'B':
    case 'Y':
        return Paper;
    case 'C':
    case 'Z':
        return Scissors;
    }
}

int score_from_shape(enum Shape shape)
{
    switch (shape)
    {
    case Rock:
        return 1;
    case Paper:
        return 2;
    case Scissors:
        return 3;
    }
}

// loss = 0
// draw = 3
// win = 6
int score_from_result(enum Shape p1Shape, enum Shape p2Shape)
{
    if (p1Shape == p2Shape)
    {
        return 3;
    }

    if ((p1Shape == Rock && p2Shape == Scissors) ||
        (p1Shape == Scissors && p2Shape == Paper) ||
        (p1Shape == Paper && p2Shape == Rock))
    {
        return 6;
    }

    return 0;
}

int shape_from_expected_score(char result, enum Shape opponent)
{
    if (result == 'Y')
        return opponent;

    if (opponent == Scissors)
    {
        switch (result)
        {
        case 'X':
            return Paper; // Loss
        case 'Z':
            return Rock; // Victory
        }
    }

    if (opponent == Rock)
    {
        switch (result)
        {
        case 'X':
            return Scissors; // Loss
        case 'Z':
            return Paper; // Victory
        }
    }

    if (opponent == Paper)
    {
        switch (result)
        {
        case 'X':
            return Rock; // Loss
        case 'Z':
            return Scissors; // Victory
        }
    }
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

    int totalScore = 0;
    while (fgets(line, sizeof(line), fpointer) != NULL)
    {
        if (strlen(line) > 1) // if this line has chars
        {
            enum Shape opponentShape = shape_from_input_value(line[0]);
            enum Shape playerShape = shape_from_input_value(line[2]);

            totalScore += score_from_shape(playerShape);
            totalScore += score_from_result(playerShape, opponentShape);
        }
    }
    printf("Answer: %i\n", totalScore);

    fclose(fpointer);

    // Task 2
    printf("Task 2:\n");
    FILE *fpointer2 = fopen(filePath, "r"); // r file mode to read contents of the files
    char line2[20];

    while (fgets(line2, sizeof(line2), fpointer2) != NULL)
    {
        if (strlen(line2) > 1) // if this line has chars
        {
            enum Shape opponentShape = shape_from_input_value(line2[0]);
            enum Shape playerShape = shape_from_expected_score(line2[2], opponentShape);

            totalScore += score_from_shape(playerShape);
            totalScore += score_from_result(playerShape, opponentShape);
        }
    }
    printf("Answer: %i\n", totalScore);

    fclose(fpointer2);

    return 0;
}