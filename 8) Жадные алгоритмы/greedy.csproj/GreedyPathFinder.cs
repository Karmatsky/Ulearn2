using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
using System.Linq;

namespace Greedy
{
	public class GreedyPathFinder : IPathFinder
	{
		public List<Point> FindPathToCompleteGoal(State state)
		{
			double pathCost = 0;
			var chest = new HashSet<Point>(state.Chests);
			var dijkstrapathFinder = new DijkstraPathFinder();
			var position = state.Position;
			var list = new List<Point>();

            for (var j= 0; j < state.Goal; j++)
            {
                var path = dijkstrapathFinder.DijkstraGetPaths(state, position, chest).FirstOrDefault();
                if (path == null)
                {
                    return new List<Point>();
                }
                position = path.End;
                pathCost += path.Cost;

                if (state.Energy < pathCost)
                {
                    return new List<Point>();
                }

                chest.Remove(path.End);
                list = list.Concat(path.Path.Skip(1)).ToList();
            }

            return list;

        }
	}
}