use std::env;
use std::fs::File;
use std::io::{BufRead, BufReader};

fn get_common_char_from_split_line(s: &str) -> Option<char> {
    let len = s.len();
    let half_len = len / 2;

    for (_i, c1) in s.chars().take(half_len).enumerate() {
        for c2 in s.chars().skip(half_len) {
            if c1 == c2 {
                return Some(c1);
            }
        }
    }

    None
}

fn get_alphabetical_index_of_char(c: char) -> usize {
    c as usize % 32
}

fn find_common_char_across_lines(lines: &[String]) -> Option<char> {
    for (_i, c) in lines[0].chars().enumerate() {
        let mut found = true;

        for line in &lines[1..] {
            if !line.contains(c) {
                found = false;
                break;
            }
        }

        if found {
            return Some(c);
        }
    }

    None
}

fn main() {
    let input_path = env::current_dir()
        .unwrap()
        .parent()
        .unwrap()
        .join("input.txt");
    let file = File::open(input_path).unwrap();
    let reader = BufReader::new(file);
    let mut challenge_1_total_value = 0;
    let mut challenge_2_total_value = 0;
    let mut challenge_2_chunk: Vec<String> = Vec::new();

    for (i, line) in reader.lines().enumerate() {
        let line = line.unwrap();
        let common_character = get_common_char_from_split_line(&line);
        let char_index = common_character
            .map(get_alphabetical_index_of_char)
            .unwrap_or(0);

        if let Some(c) = common_character {
            if c.is_ascii_uppercase() {
                challenge_1_total_value += 26;
            }

            challenge_1_total_value += char_index;
        }

        if i % 3 == 2 {
            challenge_2_chunk.push(line);

            if let Some(common_character2) = find_common_char_across_lines(&challenge_2_chunk) {
                let char_index2 = get_alphabetical_index_of_char(common_character2);

                if common_character2.is_ascii_uppercase() {
                    challenge_2_total_value += 26;
                }

                challenge_2_total_value += char_index2;
            }

            challenge_2_chunk.clear();
        } else {
            challenge_2_chunk.push(line);
        }
    }

    println!("Challenge 1: {}", challenge_1_total_value);
    println!("Challenge 2: {}", challenge_2_total_value);
}
