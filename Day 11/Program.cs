namespace Day11
{
    internal class Program {
        static void Main(string[] args)
        {
            var rounds = 10000;
            var input = File.ReadAllText(@"input.txt").Split("\n\r\n").Select(x => x.Split("\r\n").ToList()).ToList();
            foreach (var monkeyinfo in input) {
                Monkey.Troop.Add(Monkey.Parse(monkeyinfo));
            }

            while (rounds > 0) {
                foreach (var m in Monkey.Troop) m.InspectAndThrowItems();
                rounds--;
            }

            var active = Monkey.Troop.OrderBy(x => x.NumInspected).TakeLast(2).ToList();
            long x = active[0].NumInspected * active[1].NumInspected;
            Console.WriteLine($"(Monkey Business): {x}");
        }
    }
}