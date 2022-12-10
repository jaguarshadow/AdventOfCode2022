namespace Day10
{
    internal class Program
    {
        static int _x = 1;
        static int _cycles = 1;
        static Dictionary<int, int> _cycleValues = new Dictionary<int, int>();
        static List<List<char>> _crt = new List<List<char>>();

        static void Main(string[] args) {
            for (int i = 0; i < 6; i++) _crt.Add(Enumerable.Repeat('.', 40).ToList());
            var input = File.ReadAllLines(@"input.txt");

            
            foreach (var line in input) {
                ProcessLine(line);
            }

            var x = GetSignalStrength();

            Console.WriteLine($"(Part 1) Sum of signal strengths: {x}");
            Console.WriteLine();
            Console.WriteLine($"(Part 2) CRT Render:");
            foreach (var line in _crt)
            {
                Console.WriteLine($"{string.Join("", line)}");
            }
        }

        static void ProcessLine(string line)
        {
            if (line.StartsWith("addx")) {
                var toAdd = int.Parse(line.Split(' ')[1]);
                NextCycle();
                NextCycle();
                _x += toAdd;
            }
            else NextCycle();
        }

        static void NextCycle() {
            if (_cycles % 20 == 0) _cycleValues.Add(_cycles, _x);
            DrawPixel(_cycles, _x);
            _cycles++;
        }

        static int GetSignalStrength() {
            var total = 0;
            var cycle = 20;
            while (cycle <= 220) {
                total += (cycle * _cycleValues[cycle]);
                cycle += 40;
            }
            return total;
        }

        static void DrawPixel(int cycle, int x) {
            var sprite = new int[] { x - 1, x, x + 1};
            var row = cycle / 40;
            var index = (cycle % 40) - 1;
            if (sprite.Contains(index)) 
                _crt[row][index] = '#';
        }
    }
}