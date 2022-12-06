namespace Day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(@"input.txt").ToString();

            var packet_length = 4;
            var message_length = 14;

            Console.WriteLine($"Part 1: {FindDistinctMarker(input, packet_length)}");
            Console.WriteLine($"Part 2: {FindDistinctMarker(input, message_length)}");
        }

        static int FindDistinctMarker(string s, int length) {
            for (int i = 0; i < s.Length - length; i++) {
                if (s.Substring(i, length).Distinct().Count() == length) return i + length;
            }
            return 0;
        }
    }
}