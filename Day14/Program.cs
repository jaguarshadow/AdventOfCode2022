using System.Drawing;

namespace Day14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(@"input.txt");

            var rocksandsand = SandFlow.FindRocks(input).ToHashSet();
/*            foreach (var p in rocksandsand)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write("#");
            }
*/
            var ybounds = rocksandsand.Max(p => p.Y);
            
            var count = 0;
            bool filled = false;
            while (!filled) {
                var grain = new Point(500, 0);
                bool atRest = false;
                while (!atRest && !filled)
                {
                    if (!rocksandsand.Contains(new Point(grain.X, grain.Y + 1))) grain.Y++;
                    else if (!rocksandsand.Contains(new Point(grain.X - 1, grain.Y + 1))) { grain.X--; grain.Y++; }
                    else if (!rocksandsand.Contains(new Point(grain.X + 1, grain.Y + 1))) { grain.X++; grain.Y++; }
                    else 
                    {
                        rocksandsand.Add(grain);
/*                        Console.SetCursorPosition(grain.X, grain.Y);
                        Console.Write("o");*/

                        count++;
                        atRest = true;
                    }
                    if (atRest && grain.Y == 0 && grain.X == 500) filled = true;
                }
            }
            //Console.SetCursorPosition(500, ybounds + 5);
            Console.WriteLine($"(Part 2) Full after {count} grains");
        }
    }

    public class SandFlow {

        public static Point FlowPoint = new Point(500, 0);

        public static List<Point> FindRocks(IEnumerable<string> input) {
            var rocks = new List<Point>();
            foreach (var line in input) {
                var points = line.Split(" -> ").Select(x => x.Split(',')).Select(x => new Point(int.Parse(x[0]), int.Parse(x[1]))).ToList();
                for (int i = 1; i < points.Count(); i++) {
                    if (points[i].X == points[i - 1].X)
                        foreach (var y in Enumerable.Range(Math.Min(points[i].Y, points[i - 1].Y), Math.Abs(points[i].Y - points[i - 1].Y) + 1))
                            rocks.Add(new Point(points[i].X, y));
                    else
                        foreach (var x in Enumerable.Range(Math.Min(points[i].X, points[i - 1].X), Math.Abs(points[i].X - points[i - 1].X) + 1))
                            rocks.Add(new Point(x, points[i].Y));
                }
            }
            var ymax = rocks.Max(x => x.Y) + 2;
            foreach (var x in Enumerable.Range(200, 600))
                rocks.Add(new Point(x, ymax));
            return rocks;

        }
    }
}