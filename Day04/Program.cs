using System.ComponentModel.DataAnnotations;
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
                var sections = line.Split(new char[] { '-', ',' }).Select(x => int.Parse(x)).ToList();
                var a1 = (start: sections[0], end: sections[1]);
                var a2 = (start: sections[2], end: sections[3]);

                var nooverlap = a1.end < a2.start || a2.end < a1.start;
                if (nooverlap)
                {
                    Console.WriteLine($"{a1.start}-{a1.end}  ,  {a2.start}-{a2.end}, NO OVERLAP");
                    continue;
                }
                Console.WriteLine($"{a1.start}-{a1.end}  ,  {a2.start}-{a2.end}, XXX");
                partial_overlap++;

                /*                if (a1.end < a2.start || a2.end < a1.start) continue; 
                                partial_overlap++;
                                if (a1.start >= a2.start && a2.end >= a1.end
                                    || a2.start >= a1.start && a1.end >= a2.end) full_overlap++;*/
            }
            return (full_overlap, partial_overlap);
        }
    }
}