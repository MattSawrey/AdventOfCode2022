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

    println!(".");
}

fn task2() {
    println!("Task 2:");

    // Get input file
    let current_dir = env::current_dir().unwrap();
    let parent_dir = current_dir.parent().unwrap();
    let file_path = format!("{}{}", parent_dir.display(), "\\input.txt");

    println!(".");
}
