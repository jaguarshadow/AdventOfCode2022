namespace Day07
{
    internal class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines(@"input.txt");

            var directories = GetDirectories(input);
            var sum = directories.Values.Where(x => x <= 100000).Sum();
            
            Console.WriteLine($"Part 1 = {sum}");
            Console.WriteLine($"Part 2 = {FindBestDirectoryToDelete(directories)}");
        }

        static Dictionary<string, long> GetDirectories(IEnumerable<string> input) {
            var directories = new Dictionary<string, long>();
            string path = "/";
            directories.Add(path, 0);
            bool inList = false;
            foreach (var line in input) {
                if (line.StartsWith("$ ls")) {
                    inList = directories[path] == 0;
                }
                else if (line.StartsWith("$ cd ")) {
                    var current = line.Substring(5);
                    if (current == "..") {
                        path = path.Substring(0, path.LastIndexOf('/'));
                        path = string.IsNullOrEmpty(path) ? "/" : path;
                    }
                    else if (current == "/") path = "/";
                    else path += (path.Length == 1 ? "" : "/") + current;
                    inList = false;
                }
                else if (line.StartsWith("dir")) {
                    var dir = line.Substring(4);
                    directories.Add(path + (path.Length == 1 ? "" : "/") + dir, 0);
                }
                else {
                    if (inList) {
                        var size = long.Parse(line.Substring(0, line.IndexOf(" ")));
                        var addTo = "";
                        directories["/"] += size;
                        var folders = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
                        foreach (var folder in folders) {
                            addTo += "/" + folder;
                            directories[addTo] += size;
                        }
                    }
                }
            }
            return directories;
        }

        static long FindBestDirectoryToDelete(Dictionary<string, long> dirs) {
            var capacity = 70000000;
            var required = 30000000;
            var free = capacity - dirs["/"];
            var best = dirs.Where(x => free + x.Value >= required).OrderBy(x => x.Value).First();
            return best.Value;
        }
    }
}