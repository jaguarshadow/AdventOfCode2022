namespace AStarPathfinder
{
    public class Pathfinder {

        public class Location {
            public int X;
            public int Y;
            public int Cost = 0; // G score is the distance from the starting point
            public Location Parent = null;
        }

        public static List<Location> GetWalkableTiles(int x, int y, List<List<char>> map) {
            var proposedLocations = new List<Location>();

            proposedLocations.Add(new Location { X = x, Y = y + 1 });
            proposedLocations.Add(new Location { X = x + 1, Y = y });
            proposedLocations.Add(new Location { X = x, Y = y - 1 });
            proposedLocations.Add(new Location { X = x - 1, Y = y });

            var maxX = map.First().Count - 1;
            var maxY = map.Count - 1;

            var pl = proposedLocations.Where(tile => (tile.X >= 0 && tile.X <= maxX && tile.Y >= 0 && tile.Y <= maxY)
                                              && map[tile.Y][tile.X] - map[y][x] <= 1).ToList();
            return pl;
        }

        public static int FindShortestPath(List<List<char>> graph, Location start, Location target, int? limit = null) {
            var current_graph = graph;
            Location current = new Location() { X = start.X, Y = start.Y, Cost = 1 };
            var active = new List<Location>();
            var visited = new List<Location>();
            bool pathfound = false;

            active.Add(start);

            while (active.Any()) {
                var lowest = active.Min(x => x.Cost);
                current = active.First(x => x.Cost == lowest);

                active.Remove(current);
                visited.Add(current);
                
                var walkable = GetWalkableTiles(current.X, current.Y, graph);

                if (visited.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null) { pathfound = true; break; }
                if (limit != null && visited.Count > limit) { break; }
                foreach (var tile in walkable) {
                    if (visited.FindIndex(t => t.X == tile.X && t.Y == tile.Y) > -1) continue;
                    if (active.FirstOrDefault(l => l.X == tile.X && l.Y == tile.Y) != null) {
                        if (current.Cost + 1 < tile.Cost) {
                            tile.Cost = current.Cost + 1;
                            tile.Parent = current;
                        }
                    }
                    else active.Insert(0, new Location() { X = tile.X, Y = tile.Y, Cost = current.Cost + 1, Parent = current });

                }
            }

            int final;
            if (pathfound) final = current.Cost;
            else final = -1;
            // Draw the path on the console 
/*            var count = -1;
            while (current != null)
            {
                count++;
                Console.SetCursorPosition(current.X, current.Y);
                Console.Write('*');
                Console.SetCursorPosition(current.X, current.Y);
                current = current.Parent;
                System.Threading.Thread.Sleep(50);
            }
*/
            return final;
        }
    }
}
