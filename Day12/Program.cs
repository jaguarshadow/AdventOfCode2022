using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static AStarPathfinder.Pathfinder;

namespace Day12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var graph = File.ReadAllLines(@"input.txt").Select(x => x.ToCharArray().ToList()).ToList();

            Location start = null;
            Location target = null;
            List<Location> aStartList = new List<Location>();

            for (int i = 0; i < graph.Count; i++) {
                int x = graph[i].IndexOf('S');
                if (x > -1) {
                    start = new Location { X = x, Y = i };
                    graph[i][x] = 'a';
                }
                x = graph[i].IndexOf('E');
                if (x > -1) {
                    target = new Location { X = x, Y = i };
                    graph[i][x] = 'z';
                }
                var aList = graph[i].Select((c, x) => (height: c, index: x)).Where(x => x.height == 'a').Select((x) => x.index);
                foreach (var a in aList) aStartList.Add(new Location { X = a, Y = i });
                if (start != null) aStartList.Add(start);
            }

            var cost = FindShortestPath(graph, start, target);
            Console.WriteLine($"(Part 1) We are at the destination! Steps: {cost}");

            var shortestACost = int.MaxValue;
            var sw = new Stopwatch();
            sw.Start();
            foreach (var a in aStartList)
            {
                var current = FindShortestPath(graph, a, target, shortestACost);
                if (current > -1 && current < shortestACost) shortestACost = current;
            }
            sw.Stop();
            Console.WriteLine($"(Part 2) The shortest path from any 'a' start: {shortestACost}, time: {sw.ElapsedMilliseconds}");
        }
    }
}