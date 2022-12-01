namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(@"input.txt").Split("\r\n\r\n").Select(x => x.Split("\r\n"));

            Console.WriteLine($"Part 1:\n The highest amount of calories an elf is carrying is: {FindMostCalories(input)}\n\n");
            Console.WriteLine($"Part 2:\n The top three elves calorie sum: {TopThreeCaloriesSum(input)}");
        }

        static int FindMostCalories(IEnumerable<IEnumerable<string>> input) {
            var max = input.Select(x => x.Sum(y => int.Parse(y))).Max();
            return max;
        }

        static int TopThreeCaloriesSum(IEnumerable<IEnumerable<string>> input) {
            var sum = input.Select(x => x.Sum(y => int.Parse(y))).OrderByDescending(x => x).Take(3).Sum();
            return sum;
        }
    }
}