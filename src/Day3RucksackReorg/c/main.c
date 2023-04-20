#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <direct.h>

#define MAX_LINE_LENGTH 100

char get_common_char_from_split_line(const char *s)
{
    int len = strlen(s);
    int half_len = len / 2;

    for (int i = 0; i < half_len; i++)
    {
        for (int j = half_len; j < len; j++)
        {
            if (s[i] == s[j])
            {
                return s[i];
            }
        }
    }

    return '\0';
}

int get_alphabetical_index_of_char(char c)
{
    return c % 32;
}

char find_common_char_across_lines(char *lines[], int num_lines)
{
    for (int i = 0; lines[0][i] != '\0'; i++)
    {
        char c = lines[0][i];
        int found = 1;

        for (int j = 1; j < num_lines; j++)
        {
            if (strchr(lines[j], c) == NULL)
            {
                found = 0;
                break;
            }
        }

        if (found)
        {
            return c;
        }
    }

    return '\0';
}

int main()
{
    // Get input file path
    char filePath[256] = "";
    sprintf(filePath, "%s\\input.txt", _getcwd(NULL, 0));
    FILE *fpointer = fopen(filePath, "r"); // r file mode to read contents of the files
    char *line = NULL;
    size_t lineLength = 0;

    int challenge_1_total_value = 0;
    int challenge_2_total_value = 0;
    int challenge_2_chunk_length = 3;
    int challenge_2_index = 1;
    char *challenge_2_line_chunk[3];

    while (getline(&line, &lineLength, fpointer) != -1)
    {
        char common_character = get_common_char_from_split_line(line);
        int char_index = get_alphabetical_index_of_char(common_character);

        if (isupper(common_character))
        {
            challenge_1_total_value += 26;
        }

        challenge_1_total_value += char_index;

        if (challenge_2_index < challenge_2_chunk_length)
        {
            challenge_2_line_chunk[challenge_2_index - 1] = malloc(MAX_LINE_LENGTH * sizeof(char));
            strcpy(challenge_2_line_chunk[challenge_2_index - 1], line);
            challenge_2_index++;
        }
        else
        {
            challenge_2_line_chunk[challenge_2_index - 1] = malloc(MAX_LINE_LENGTH * sizeof(char));
            strcpy(challenge_2_line_chunk[challenge_2_index - 1], line);

            char common_character2 = find_common_char_across_lines(challenge_2_line_chunk, challenge_2_chunk_length);
            int char_index2 = get_alphabetical_index_of_char(common_character2);

            if (isupper(common_character2))
            {
                challenge_2_total_value += 26;
            }

            challenge_2_total_value += char_index2;
            challenge_2_index = 1;
        }
    }

    for (int i = 0; i < challenge_2_chunk_length; i++)
    {
        free(challenge_2_line_chunk[i]);
    }

    fclose(fpointer);

    printf("Challenge 1: %i\n", challenge_1_total_value);
    printf("Challenge 2: %i\n", challenge_2_total_value);

    return 0;
}