using System.Diagnostics;

namespace Day02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Rock Paper Scissors Strategy Guide
            var input = File.ReadAllLines(@"input.txt");
            RPSHelper.ProcessStrategyGuide(input);
        }
    }

    public static class RPSHelper
    {

        /*     Possible Shapes:
         *          A, X: Rock
         *          B, Y: Paper
         *          C, Z: Scissors
         *          
         *     Possible Outcomes:
         *          X: Need to lose
         *          Y: Need to draw
         *          Z: Need to win
         */


        public enum ScoreMethod {
            Shape,
            Outcome
        }

        static Dictionary<char, int> _scoreKey = new Dictionary<char, int>() {
            { 'X', 1 },
            { 'Y', 2 },
            { 'Z', 3 }
        };

        static readonly int _lossPoints = 0;
        static readonly int _drawPoints = 3;
        static readonly int _winPoints  = 6;

        public static void ProcessStrategyGuide(IEnumerable<string> input) {
            var sw = new Stopwatch();
            sw.Start();
            // Part 1
            var total = input.Select(x => ScoreRound(x[0], x[2], ScoreMethod.Shape)).Sum();
            sw.Stop();
            Console.WriteLine($"Shape Score Method Total: {total}");
            Console.WriteLine($"part 1 took {sw.ElapsedMilliseconds}ms");

            sw.Restart();
            // Part 2
            total = input.Select(x => ScoreRound(x[0], x[2], ScoreMethod.Outcome)).Sum();
            sw.Stop();
            Console.WriteLine($"Outcome Score Method Total: {total}");
            Console.WriteLine($"part 2 took {sw.ElapsedMilliseconds}ms");
        }

        public static int ScoreRound(char first, char second, ScoreMethod method) {
            int score = 0;
            if (method == ScoreMethod.Shape) {
                if (first == 'A') score = _scoreKey[second] + (second == 'X' ? _drawPoints : second == 'Y' ? _winPoints : _lossPoints);
                if (first == 'B') score = _scoreKey[second] + (second == 'Y' ? _drawPoints : second == 'Z' ? _winPoints : _lossPoints);
                if (first == 'C') score = _scoreKey[second] + (second == 'Z' ? _drawPoints : second == 'X' ? _winPoints : _lossPoints);
            } 
            else { // ScoreMethod.Outcome
                if (second == 'X') score = _lossPoints + (first == 'A' ? _scoreKey['Z'] : first == 'B' ? _scoreKey['X'] : _scoreKey['Y']);
                if (second == 'Y') score = _drawPoints + (first == 'A' ? _scoreKey['X'] : first == 'B' ? _scoreKey['Y'] : _scoreKey['Z']);
                if (second == 'Z') score = _winPoints  + (first == 'A' ? _scoreKey['Y'] : first == 'B' ? _scoreKey['Z'] : _scoreKey['X']);
            }
            return score;
        }
    }
}