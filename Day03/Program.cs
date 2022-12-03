namespace Day03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(@"input.txt");

            Console.WriteLine($"Part 1: {Part1(input)}");
            Console.WriteLine($"Part 2: {Part2(input)}");
        }

        static int Part1(IEnumerable<string> input)
        {
            var total = 0;
            foreach (var s in input) {
                // Split the rucksack into two compartments
                var rucksack = (c1: s.Substring(0, s.Length / 2), c2: s.Substring(s.Length / 2));

                // Find items that occur in both
                var intersect = rucksack.c1.Intersect(rucksack.c2);

                // Add priority value to total
                // We can offset the char's unicode value to match the priority we need
                total += intersect.Sum(x => Convert.ToInt32(x) - (char.IsUpper(x) ? 38 : 96 ));
            }
            return total;
        }

        static int Part2(IEnumerable<string> input)
        {
            var total = 0;
            var skip = 0;
            while (skip != input.Count()) {
                // Take a slice of 3 elements
                var group = input.Skip(skip).Take(3).ToList(); 

                // Find items that occur in all three
                var intersect = group[0].Intersect(group[1]).Intersect(group[2]);

                // Add priority value to total
                total += intersect.Sum(x => Convert.ToInt32(x) - (char.IsUpper(x) ? 38 : 96));

                // Offset by 3 for the next group
                skip += 3;
            }
            return total;
        }


    }
}