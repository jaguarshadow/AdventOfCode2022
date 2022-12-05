using static Day05.CrateHelper;

namespace Day05
{
    internal class Program
    {
        static void Main(string[] args) {
            var crate_input = File.ReadLines(@"input.txt").TakeWhile(x => !string.IsNullOrEmpty(x)).ToList();
            var instructions = File.ReadLines(@"input.txt").Where(x => x.StartsWith("move")).ToList();
            Console.WriteLine($"Part 1: {GetTopCrates(crate_input, instructions, CraneModel.CrateMover9000)}");
            Console.WriteLine($"Part 2: {GetTopCrates(crate_input, instructions, CraneModel.CrateMover9001)}");
            
        }

        static string GetTopCrates(IList<string> crate_input, IList<string> instructions, CraneModel model) {
            var inventory = Inventory.Parse(crate_input);
            inventory.ProcessMovements(instructions, model);
            var topcrates = string.Join("", inventory.Stacks.Select(x => x.Crates.Last()));
            return topcrates;
        }
    }
}