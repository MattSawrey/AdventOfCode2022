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

    let elf_calories = fs::read_to_string(file_path).unwrap();
    let elf_calories_arr: Vec<&str> = elf_calories.split('\n').collect();

    let mut max_calories = 0;
    let mut max_index = 0;
    let mut index = 0;
    for calorie in elf_calories_arr {
        if calorie.trim().is_empty() {
            continue;
        }

        let c = calorie.trim().parse::<i32>();

        if let Err(e) = c {
            println!("Error parsing: {}", e);
            continue;
        }

        let cal = c.unwrap();

        if cal > max_calories {
            max_calories = cal;
            max_index = index;
        }
        index += 1;
    }
    println!("Max Calories: {}. Index: {}.", max_calories, max_index);
}

fn task2() {
    println!("Task 2:");

    // Get input file
    let current_dir = env::current_dir().unwrap();
    let parent_dir = current_dir.parent().unwrap();
    let file_path = format!("{}{}", parent_dir.display(), "\\input.txt");

    let elf_calories = fs::read_to_string(file_path).unwrap();
    let elf_calories_arr: Vec<&str> = elf_calories.split('\n').collect();

    let mut chunk_sums: Vec<i32> = vec![];
    let mut chunk_sum = 0;
    for calorie in elf_calories_arr {
        if calorie.trim().is_empty() {
            // Chunk break
            chunk_sums.push(chunk_sum);
            chunk_sum = 0;
            continue;
        }

        let c = calorie.trim().parse::<i32>().unwrap();
        chunk_sum += c;
    }

    let mut three_largest_calories_count = 0;
    for _x in 0..3 {
        let mut largest_calorie_count_index = 0;
        for (i, chunk_sum) in chunk_sums.iter().enumerate() {
            if chunk_sum > &chunk_sums[largest_calorie_count_index] {
                largest_calorie_count_index = i;
            }
        }
        three_largest_calories_count += chunk_sums[largest_calorie_count_index];
        chunk_sums.remove(largest_calorie_count_index);
    }

    println!(
        "Calorie count of the 3 Elves with most calories: {}.",
        three_largest_calories_count
    );
}
