namespace Day09
{
    internal class Program
    {
        static (int x, int y)[] oldSnake = new (int, int)[10];
        static void Main(string[] args) {
            var input = File.ReadAllLines(@"input.txt");

            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.CursorVisible = false;
            Console.CursorSize = 1;
            Console.SetCursorPosition(Console.LargestWindowWidth / 2, Console.LargestWindowWidth / 2);
            
            //var part1 = GetTailMovements(input, 2);
            var part2 = GetTailMovements(input, 10);
            // 6181, 2386
            //Console.WriteLine($"(Part 1) Unique tail points visited: {part1.Distinct().Count()}");
            Console.WriteLine($"(Part 2) Unique tail points visited: {part2.Result.Distinct().Count()}");
        }

        static async Task<List<(int x, int y)>> GetTailMovements(IEnumerable<string> input, int knotCount) {
            var knots = new (int x, int y)[knotCount];
            Array.Fill(knots, (Console.LargestWindowWidth / 2, Console.LargestWindowWidth / 2));
            oldSnake = knots.ToArray();
            var tailVisited = new List<(int x, int y)>();
            tailVisited.Add(knots[0]);
            foreach (var motion in input) {
                var velocity = GetVelocity(motion[0]);
                var steps = int.Parse(motion.Substring(2));
                var speed = 1;
                var maxdiff = 1;
                while (steps > 0) {
                    steps--;
                    await DrawSnake(knots);
                    var current = 0;
                    knots[current].x += velocity.x;
                    knots[current].y += velocity.y;
                    for (int i = 1; i < knots.Count(); i++) {

                        // Check distance between knots
                        var diffx = knots[current].x - knots[i].x;
                        var diffy = knots[current].y - knots[i].y;
                        current = i; 
                        // Stop here if tail is close enough
                        if (Math.Abs(diffx) <= maxdiff && Math.Abs(diffy) <= maxdiff) break;
                        // Move tail
                        if (diffy == 0) knots[i].x += (diffx > 0) ? speed : -speed;
                        else if (diffx == 0) knots[i].y += (diffy > 0) ? speed : -speed;
                        else {
                            knots[i].x += (diffx > 0) ? speed : -speed;
                            knots[i].y += (diffy > 0) ? speed : -speed;
                        }
                        if (i == knots.Length - 1) tailVisited.Add(knots[i]);
                    }
                }
                
            }
            return tailVisited;
        }

        static (int x, int y) GetVelocity(char direction) {
            switch (direction) {
                case 'R': return (1,  0);
                case 'U': return (0,  1);
                case 'L': return (-1, 0);
                case 'D': return (0, -1);
                default:  return (0,  0);
            }
        }

        static async Task DrawSnake((int x, int y)[] snake)
        {
            Thread.Sleep(1);
            for (int i = 0; i < snake.Length; i++)
            {
                
                Console.SetCursorPosition(oldSnake[i].x, oldSnake[i].y);
                Console.Write(' ');
                
                Console.SetCursorPosition(snake[i].x, snake[i].y);
                Console.Write('0');
            }
            oldSnake = snake.ToArray();
            return;

        }
    }
}