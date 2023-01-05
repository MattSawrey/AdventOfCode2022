use std::env;
use std::fs;

fn main() {
    env::set_var("RUST_BACKTRACE", "1");
    task1();
}

fn task1() {
    let cur_dir = env::current_dir().expect("The current working directory");
    let dir = cur_dir.display();

    let file_path = format!("{}{}", dir, "\\src\\Day1ElfCalories\\input.txt");

    // print!("{}", &file_path);

    let elf_calories = fs::read_to_string(file_path).unwrap();
    let elf_calories_arr: Vec<&str> = elf_calories.split('\n').collect();

    // Part 1
    let mut max_calories = 0;
    for calorie in elf_calories_arr {
        if calorie.trim().is_empty() {
            continue;
        }

        let c = calorie.trim().parse::<u64>();

        if let Err(e) = c {
            println!("Error parsing: {}", e);
            continue;
        }

        let cal = c.unwrap();

        // println!("Index: {}, Cur: {}, Max: {}", i, cal, max_calories);

        if cal > max_calories {
            max_calories = cal;
        }
    }
    println!("Max calorie value: {}", max_calories);

    // Part 2
}
