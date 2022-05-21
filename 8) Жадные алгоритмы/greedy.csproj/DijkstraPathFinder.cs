using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;

namespace Greedy
{
    internal class Data
    {
        public Point Previous
        {
            get;
            set;
        }
        public int Price
        {
            get;
            set;
        }
    }

    public class DijkstraPathFinder
    {
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
          IEnumerable<Point> targets)
        {
            var n = new HashSet<Point>();
            var path = new Dictionary<Point,Data>
            {
                [start] = new Data{ Price = 0, Previous = new Point(-1, -1) }
            };

            var nCount = targets.Count();
            while (nCount > 0)
            {
                var unlocked = GetPointToOpen(n, path);

                if (unlocked.Equals(new Point(-1, -1)))
                    yield break;

                if (targets.Any(target => target.Equals(unlocked)))
                {
                    nCount--;
                    yield return GetResult(unlocked, path);
                }

                AddPointsToTrack(state, n, path, unlocked);
            }
        }

        private static Point GetPointToOpen(ICollection<Point> visited, Dictionary<Point, Data> track)
        {
            var unlockedPlace = new Point(-1, -1);
            var maxV = int.MaxValue;
            foreach (var point in track.Keys.Where(e => track[e].Price < maxV && !visited.Contains(e)))
            {
                maxV = track[point].Price;
                unlockedPlace = point;
            }

            return unlockedPlace;
        }

        private static PathWithCost GetResult(Point end, IReadOnlyDictionary<Point, Data> track)
        {
            var income = new List<Point>();
            var cost = track[end].Price;
            while (!end.Equals(new Point(-1, -1)))
            {
                income.Add(end);
                end = track[end].Previous;
            }

            income.Reverse();
            return new PathWithCost(cost, income.ToArray());
        }

        private static void AddPointsToTrack(State state, ISet<Point> visited,
          IDictionary<Point, Data> track, Point toOpen)
        {
            foreach (var point in GetNextPoints(state, toOpen))
            {
                var newCost = track[toOpen].Price + state.CellCost[point.X, point.Y];
                if (!track.ContainsKey(point) || track[point].Price > newCost)
                {
                    track[point] = new Data
                    {
                        Previous = toOpen,
                        Price = newCost
                    };
                }
            }

            visited.Add(toOpen);
        }

        private static IEnumerable<Point> GetNextPoints(State state, Point point)
        {
            var neighbors = new[]
            {
                new Size(-1, 0), new Size(1, 0),
                new Size(0, -1),new Size(0, 1),
            };

            foreach (var neighbor in neighbors)
            {
                var newPlace = point + neighbor;
                if (state.InsideMap(newPlace) && !state.IsWallAt(newPlace))
                {
                    yield return newPlace;
                }
            }
        }
    }
}