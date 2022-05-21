using System;
using System.Collections.Generic;
using System.Drawing;

namespace Rivals
{
	public class RivalsTask
	{
		public static IEnumerable<OwnedLocation> AssignOwners(Map map)
		{
            var visitedPoints = new HashSet<Point>();
            var queue = new Queue<Tuple<Point, int, int>>();

            for (int j = 0; j < map.Players.Length; j++)
            {
                queue.Enqueue(Tuple.Create(new Point(map.Players[j].X,
                                                     map.Players[j].Y), j, 0));
            }

            while (queue.Count != 0)
            {
                var dequeued = queue.Dequeue();
                var point = dequeued.Item1;
                if (point.X < 0 || point.X >= map.Maze.GetLength(0)
                    || point.Y < 0 || point.Y >= map.Maze.GetLength(1))
                {
                    continue;
                }
                if (map.Maze[point.X, point.Y] == MapCell.Wall)
                {
                    continue;
                }
                if (visitedPoints.Contains(point))
                {
                    continue;
                }

                visitedPoints.Add(point);
                yield return new OwnedLocation(dequeued.Item2,
                                               new Point(point.X, point.Y), dequeued.Item3);

                for (var derivativeY = -1; derivativeY <= 1; derivativeY++)
                {
                    for (var derivativeX = -1; derivativeX <= 1; derivativeX++)
                    {
                        if (derivativeX != 0 && derivativeY != 0)
                        {
                            continue;
                        }
                        else queue.Enqueue(Tuple.Create(new Point
                        {
                            X = point.X + derivativeX,
                            Y = point.Y + derivativeY
                        },
                            dequeued.Item2, dequeued.Item3 + 1));
                    }
                }
            }
        }
	}
}
