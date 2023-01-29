use std::env;
use std::fs;

fn main() {
    // env::set_var("RUST_BACKTRACE", "1");
    task1();
    task2();
}

fn task1() {
    println!("Task 1:");

    // Get input file
    let current_dir = env::current_dir().unwrap();
    let parent_dir = current_dir.parent().unwrap();
    let file_path = format!("{}{}", parent_dir.display(), "\\input.txt");

    let input_file = fs::read_to_string(file_path).unwrap();
    let lines: Vec<&str> = input_file.split('\n').collect();

    let mut result: i16 = 0;

    for line in lines {
        let o_char = line.chars().nth(0).unwrap();
        let p_char = line.chars().nth(2).unwrap();
        let opponent_shape = char_to_shape(o_char);
        let player_shape = char_to_shape(p_char);

        result += round_to_points(player_shape, opponent_shape);
    }

    println!("Result: {}", result);
}

fn task2() {
    println!("Task 2:");

    // Get input file
    let current_dir = env::current_dir().unwrap();
    let parent_dir = current_dir.parent().unwrap();
    let file_path = format!("{}{}", parent_dir.display(), "\\input.txt");

    let input_file = fs::read_to_string(file_path).unwrap();
    let lines: Vec<&str> = input_file.split('\n').collect();

    let mut result: i16 = 0;

    for line in lines {
        let p_char = line.chars().nth(2).unwrap();
        let o_char = line.chars().nth(0).unwrap();
        let opponent_shape = char_to_shape(o_char);
        let (player_shape, opponent_shape) = shape_from_expected_score(p_char, opponent_shape);

        result += round_to_points(player_shape, opponent_shape);
    }

    println!("Result: {}", result);
}

#[derive(PartialEq, Clone)]
enum Shape {
    Rock = 1,
    Paper = 2,
    Scissors = 3,
}
impl std::fmt::Display for Shape {
    fn fmt(&self, f: &mut std::fmt::Formatter) -> std::fmt::Result {
        match self {
            Shape::Rock => write!(f, "Rock"),
            Shape::Paper => write!(f, "Paper"),
            Shape::Scissors => write!(f, "Scissors"),
        }
    }
}

fn char_to_shape(character: char) -> Shape {
    return match character {
        'A' | 'X' => Shape::Rock,
        'B' | 'Y' => Shape::Paper,
        'C' | 'Z' => Shape::Scissors,
        _ => panic!("Unidentified char passed into method: {}", character),
    };
}

fn round_to_points(player_shape: Shape, opponent_shape: Shape) -> i16 {
    let mut points: i16 = 0;

    // number of points from the player shape
    match player_shape {
        Shape::Rock => points += 1,
        Shape::Paper => points += 2,
        Shape::Scissors => points += 3,
    };

    // number of points from the result
    if player_shape == opponent_shape {
        points += 3;
    }

    if (player_shape == Shape::Rock && opponent_shape == Shape::Scissors)
        || (player_shape == Shape::Scissors && opponent_shape == Shape::Paper)
        || (player_shape == Shape::Paper && opponent_shape == Shape::Rock)
    {
        points += 6;
    }

    return points;
}

fn shape_from_expected_score(expected_score: char, opponent_shape: Shape) -> (Shape, Shape) {
    let player_shape: Shape;

    if expected_score == 'Y' {
        player_shape = opponent_shape.clone();
        return (player_shape, opponent_shape);
    }

    if opponent_shape == Shape::Scissors {
        match expected_score {
            'X' => player_shape = Shape::Paper, // Loss
            'Z' => player_shape = Shape::Rock,  // Victory
            _ => panic!(
                "Unidentified expected score passed into method: {}",
                expected_score
            ),
        }
    } else if opponent_shape == Shape::Rock {
        match expected_score {
            'X' => player_shape = Shape::Scissors, // Loss
            'Z' => player_shape = Shape::Paper,    // Victory
            _ => panic!(
                "Unidentified expected score passed into method: {}",
                expected_score
            ),
        }
    } else if opponent_shape == Shape::Paper {
        match expected_score {
            'X' => player_shape = Shape::Rock,     // Loss
            'Z' => player_shape = Shape::Scissors, // Victory
            _ => panic!(
                "Unidentified expected score passed into method: {}",
                expected_score
            ),
        }
    } else {
        panic!("Unrecognised shape passed into method");
    }

    return (player_shape, opponent_shape);
}
