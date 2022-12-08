namespace Day08 {
    internal class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines(@"input.txt").ToList();
            var forest = GetForestGrid(input);

            var trees = ScoreTrees(forest);
            Console.WriteLine($"(Part 1) Number of visible trees: {trees.visible}");
            Console.WriteLine($"(Part 2) Highest Tree Senic Score: {trees.highscore}");
        }

        static int[][] GetForestGrid(List<string> input) {
            var grid = input.Select(l => l.Select(i => int.Parse(i.ToString())).ToArray()).ToArray();
            return grid;
        } 

        static (int visible, int highscore) ScoreTrees(int[][] forest) {
            var numVisible = (forest.Length * 2) + ((forest[0].Length - 2) * 2);
            var highscore = 0;
            var row = 1;
            while (row < forest.Length - 1) {
                var col = 1;
                while (col < forest[0].Length - 1)
                {
                    var tree = forest[row][col];

                    var score = (u: 0, l: 0, d: 0, r: 0);
                    bool visible = false;

                    var up = forest.Select(x => x[col]).Take(row).Reverse().TakeWhile(x => x < tree);
                    if (up.Count() == row) { visible = true; score.u = up.Count(); }
                    else score.u = up.Count() + 1;

                    var left = forest[row].Take(col).Reverse().TakeWhile(x => x < tree);
                    if (left.Count() == col) { visible = true; score.l = left.Count(); }
                    else score.l = left.Count() + 1;

                    var down = forest.Select(x => x[col]).Skip(row + 1).TakeWhile(x => x < tree);
                    if (down.Count() == forest.Length - row - 1) { visible = true; score.d = down.Count(); }
                    else score.d = down.Count() + 1;

                    var right = forest[row].Skip(col + 1).TakeWhile(x => x < tree);
                    if (right.Count() == forest[0].Length - col - 1) { visible = true; score.r = right.Count(); }
                    else score.r = right.Count() + 1;

                    var total = score.u * score.l * score.d * score.r;
                    if (total > highscore) highscore = total;

                    if (visible) numVisible++;

                    col++;
                }
                row++;
            }
            return (numVisible, highscore);
        }
    }
} 