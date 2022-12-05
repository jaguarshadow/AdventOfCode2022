using System.Text.RegularExpressions;

namespace Day05 {
    public static class CrateHelper {
        
        public enum CraneModel {
            CrateMover9000,
            CrateMover9001
        }

        public class Inventory { 
            public List<CrateStack> Stacks { get; set; }

            public void MoveCrates(int quantity, int startStack, int endStack, CraneModel model) {
                var toMove = Stacks[startStack - 1].Crates.TakeLast(quantity);
                Stacks[startStack - 1].Crates = Stacks[startStack - 1].Crates.SkipLast(quantity).ToList();
                if (model == CraneModel.CrateMover9000) Stacks[endStack - 1].Crates.AddRange(toMove.Reverse());
                else Stacks[endStack - 1].Crates.AddRange(toMove);
            }
        
            static public Inventory Parse(IList<string> input) {
                var columns = input.Last().Where(x => !char.IsWhiteSpace(x))
                    .Select((x, i) => new CrateStack(int.Parse(x.ToString()), i)).ToList();
                for (int i = 0; i < input.Count - 1; i++){
                    var craterow = input[i].Select((letter, index) => (letter, index: index)).Where(x => char.IsLetter(x.letter));
                    foreach (var crate in craterow) {
                        var columnIndex = crate.index / 4;
                        columns[columnIndex].Crates.Insert(0, crate.letter);
                    }
                }
                return new Inventory() { Stacks = columns };
            }

            public void ProcessMovements(IEnumerable<string> instructions, CraneModel model)
            {
                Regex regex = new Regex(@"move (?<quantity>\d+) from (?<from>\d+) to (?<to>\d)", RegexOptions.Compiled);
                foreach (var line in instructions) {
                    var match = regex.Match(line);
                    var quantity = int.Parse(match.Groups["quantity"].Value);
                    var from = int.Parse(match.Groups["from"].Value);
                    var to = int.Parse(match.Groups["to"].Value);
                    MoveCrates(quantity, from, to, model);
                }
            }
        }

        public class CrateStack {
            public List<char> Crates { get; set; }
            public int Number    { get; set; }
            public CrateStack(int number, int index) {
                Crates = new List<char>();
                Number = number;
            }
        }
    }
}
