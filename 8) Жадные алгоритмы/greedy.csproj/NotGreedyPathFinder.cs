using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
using System.Linq;


namespace Greedy
{
    public class NotGreedyPathFinder : IPathFinder
    {
        private List<Point> bestPath = new List<Point>();
        private int maxChest;
        private readonly Dictionary<Point, Dictionary<Point, PathWithCost>> paths =
           new Dictionary<Point, Dictionary<Point, PathWithCost>>();


        public List<Point> FindPathToCompleteGoal(State state)
        {
            var patH = new List<Point>();
            var pathFinder = new DijkstraPathFinder();
            var points = new List<Point>(state.Chests) { state.Position };
            foreach (var point in points)
            {
                if (paths != null && !paths.ContainsKey(point))
                {
                    paths.Add(point, new Dictionary<Point, PathWithCost>());
                }
                foreach (var path in pathFinder.DijkstraGetPaths(state, point, state.Chests))
                {
                    if (path.Start.Equals(path.End))
                    {
                        continue;
                    }
                    if (paths != null)
                    {
                        paths[path.Start][path.End] = path;
                    }
                }
            }
            foreach (var path in paths[state.Position])
            {
                var hSet = new HashSet<Point>();
                foreach (var pair in paths[path.Key])
                {
                    hSet.Add(pair.Key);
                }    
                FindPath(state.Energy - path.Value.Cost,
                path.Key, hSet, 1, new List<Point> { state.Position, path.Key });
            }
            for (var j = 0; j < bestPath.Count - 1; j++)
            {
                patH = patH.Concat(paths[bestPath[j]][bestPath[j + 1]].Path.Skip(1)).ToList();
            }
            return patH;
        }

        private void FindPath(int currentEnergy, Point currentPosition, IEnumerable<Point> leftoverChests,
            int takenChests, List<Point> points)
        {
            var chest = new HashSet<Point>(leftoverChests);
            chest.Remove(currentPosition);
            foreach (var point in chest)
            {
                if (paths[currentPosition][point].Cost > currentEnergy)
                {
                    continue;
                }
                var nPath = new List<Point>(points) { point };
                FindPath(currentEnergy - paths[currentPosition][point].Cost,
                    point, chest, takenChests + 1, nPath);
            }
            if (takenChests <= maxChest) return;
            maxChest = takenChests;
            bestPath = points;
        }
    }
}