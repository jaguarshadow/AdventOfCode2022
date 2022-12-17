using System.Collections.Immutable;
using System.Security.AccessControl;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Day13
{
    internal class Program {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(@"input.txt").Split("\r\n\r\n").Select(x => x.Split("\r\n")).Select(x => (left: x[0], right: x[1])).ToList();

            int a = 0, b = 0;
            foreach (var pair in input) {
                ++b;
                a += Compare(pair.left, pair.right) < 0 ? b : 0;
            }
            Console.WriteLine($"(Part 1) {a}");

            string[] dividers = { "[[]]", "[[2]]", "[[6]]" };
            List<string> ss = new(dividers);
            foreach (var pair in input) {
                ss.Add(pair.left);
                ss.Add(pair.right);
            }
            ss.Sort(Compare);
            var part2 = ss.IndexOf(dividers[1]) * ss.IndexOf(dividers[2]);
            Console.WriteLine($"(Part 2) {part2}");
            

        }

        protected static int Compare(string s1, string s2) =>
            Compare(JsonSerializer.Deserialize<JsonElement>(s1), JsonSerializer.Deserialize<JsonElement>(s2));

        private static int Compare(JsonElement j1, JsonElement j2) =>
            (j1.ValueKind, j2.ValueKind) switch
            {
                (JsonValueKind.Number, JsonValueKind.Number) =>
                    j1.GetInt32() - j2.GetInt32(),
                (JsonValueKind.Number, _) =>
                    DoCompare(JsonSerializer.Deserialize<JsonElement>($"[{j1.GetInt32()}]"), j2),
                (_, JsonValueKind.Number) => 
                    DoCompare(j1, JsonSerializer.Deserialize<JsonElement>($"[{j2.GetInt32()}]")),
                _ => DoCompare(j1, j2),
            };
        
        private static int DoCompare(JsonElement j1, JsonElement j2)
        {
            int res;
            JsonElement.ArrayEnumerator e1 = j1.EnumerateArray();
            JsonElement.ArrayEnumerator e2 = j2.EnumerateArray();
            while (e1.MoveNext() && e2.MoveNext())
                if ((res = Compare(e1.Current, e2.Current)) != 0) return res;
            return j1.GetArrayLength() - j2.GetArrayLength();
        }
    }
}