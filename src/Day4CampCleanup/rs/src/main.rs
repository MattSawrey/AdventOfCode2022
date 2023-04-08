use std::env;
use std::fs;

fn main() {
    let current_dir = env::current_dir().unwrap();
    let parent_dir = current_dir.parent().unwrap();
    let file_path = format!("{}{}", parent_dir.display(), "\\input.txt");
    let input_file_contents = fs::read_to_string(file_path).unwrap();
    let lines = input_file_contents.lines();
    let mut data = Vec::new();

    // parse the input string to a Vec of the two int ranges represented on each line.
    for line in lines {
        let ranges: Vec<_> = line
            .split(',')
            .map(|x| {
                x.split('-')
                    .map(|y| y.parse::<i32>().unwrap())
                    .collect::<Vec<_>>()
            })
            .collect();
        let ranges_as_vec = ranges
            .iter()
            .map(|range| (range[0]..=range[1]).collect::<Vec<_>>())
            .collect::<Vec<_>>();
        data.push(ranges_as_vec);
    }

    let mut sum1 = 0;
    for pair_assignment in &data {
        let elf1 = &pair_assignment[0];
        let elf2 = &pair_assignment[1];

        //output_elf_assignment_data(&elf1, &elf2);

        if intersect_vecs(elf1, elf2).len() == elf1.len()
            || intersect_vecs(elf1, elf2).len() == elf2.len()
        {
            sum1 += 1;
        }
    }
    println!("Challenge 1: {}", sum1);

    let mut sum2 = 0;
    for pair_assignment in &data {
        let elf1 = &pair_assignment[0];
        let elf2 = &pair_assignment[1];

        //output_elf_assignment_data(&elf1, &elf2);

        if elf1.iter().any(|&x| elf2.contains(&x)) || elf2.iter().any(|&x| elf1.contains(&x)) {
            sum2 += 1;
        }
    }
    println!("Challenge 2: {}", sum2);
}

fn output_elf_assignment_data(elf1_assignments: &Vec<i32>, elf2_assignments: &Vec<i32>) {
    println!("Elf 1 assignment:");
    let mut string_builder = String::new();
    for area in elf1_assignments {
        string_builder.push_str(&format!("{},", area));
    }
    println!("{}", string_builder);

    println!("Elf 2 assignment:");
    let mut string_builder2 = String::new();
    for area in elf2_assignments {
        string_builder2.push_str(&format!("{},", area));
    }
    println!("{}", string_builder2);
}

fn intersect_vecs<T: PartialEq + Clone>(v1: &Vec<T>, v2: &Vec<T>) -> Vec<T> {
    let mut result = Vec::new();
    for item in v1.iter() {
        if v2.contains(item) {
            result.push(item.clone());
        }
    }
    return result;
}
