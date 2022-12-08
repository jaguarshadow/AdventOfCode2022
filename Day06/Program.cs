using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

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

        static int FindDistinctMarker(string s, int len) {
            for (int i = len - 1; i < s.Length - len; i++) {
                if (s.Substring(i, len).Distinct().Count() == len) return i + len;
            }
            return 0;
        } 
    }
}