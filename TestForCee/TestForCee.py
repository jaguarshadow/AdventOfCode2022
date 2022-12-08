from random import randint

with open(file='input.txt') as file:
    terminal_output = file.read().split("\n")
    new_directory_ids = {str(randint(-100000, 100000)): dir_id.split(" ")[-1] for dir_id in terminal_output if dir_id.startswith("dir")}
    new_directory_ids['-999999'] = "/"


def get_directory_paths(filesystem: dict, current_dir: str, current_dir_path: list, total_dir_size: int):
    current_dir_path.append(current_dir)
    curr_dir_size = sum([file for file in filesystem[current_dir] if type(file) is int])

    for new_dir in filesystem[current_dir]:
        if new_dir not in current_dir_path and new_dir in filesystem:
            new_dir_size = sum([file for file in filesystem[new_dir] if type(file) is int]) + curr_dir_size
            get_directory_paths(filesystem, new_dir, current_dir_path.copy(), new_dir_size + total_dir_size)

    directory_paths.append(current_dir_path)

    if len(current_dir_path) == 1:
        if current_dir_path[0] not in directory_sizes:
            directory_sizes[current_dir_path[0]] = curr_dir_size
    else:
        if current_dir_path[0] not in directory_sizes:
            directory_sizes[current_dir_path[0]] = total_dir_size


directories = {"-999999": []}
previous_directories = []
current_directory = "-999999"

for i, line in enumerate(terminal_output):

    if line[:4] == "$ cd" and line[-1].isalpha():
        previous_directories.append(current_directory)
        current_directory = [k for k, v in new_directory_ids.items() if v == line.split(" ")[-1] and v not in directories][0]
    elif line == "$ cd ..":
        current_directory = previous_directories.pop()

    if line == "$ ls":
        for file in terminal_output[i+1:]:
            if file == "$ ls":
                break
            if "$ cd" in file:
                continue
            if "dir" in file:
                lookup_key = [k for k, v in new_directory_ids.items() if v == file.split(" ")[-1] and k not in directories][0]
                directories[lookup_key] = []  # create new directory
                directories[current_directory].append(lookup_key)
            elif file.split(" ")[0].isnumeric():
                directories[current_directory].append(int(file.split(" ")[0]))


directory_paths, directory_sizes = [], {}
test_sum = 0
for directory in directories:
    get_directory_paths(directories, directory, [], 0)


print(f"ids: {new_directory_ids}")
print(f"directory sizes: {directory_sizes}")
print(f'directory: {directories}')

output = sum([num for num in directory_sizes.values() if num <= 100000])
print(output)