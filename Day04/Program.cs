using System.Security.Cryptography;

namespace Day04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(@"input.txt");
            var output = FindSharedAssignments(input);
            Console.WriteLine($"Fully overlapped assignments: {output.full} \nPartially overlapped assignments: {output.partial}");
        }

    static (int full, int partial) FindSharedAssignments(IEnumerable<string> input) {
        var full_overlap = 0;
        var partial_overlap = 0;

        foreach (var line in input) {
            var sections = line.Split(new char[] { '-', ',' });
            var a1 = (start: int.Parse(sections[0]), end: int.Parse(sections[1]));
            var a2 = (start: int.Parse(sections[2]), end: int.Parse(sections[3]));
            if (a1.start >= a2.start && a2.end >= a1.end
                || a2.start >= a1.start && a1.end >= a2.end) full_overlap++;
            if (a1.start <= a2.end && a2.start <= a1.start 
                || a2.start <= a1.end && a1.start <= a2.start) partial_overlap++;
        }
        return (full_overlap, partial_overlap);
    }
    }
}