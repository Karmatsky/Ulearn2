using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            var queue = new Queue<Point>();
            var visitedPoints = new HashSet<Point>();
            var dictionary = new Dictionary<Point, SinglyLinkedList<Point>>();

            visitedPoints.Add(start);
            queue.Enqueue(start);
            dictionary.Add(start, new SinglyLinkedList<Point>(start));

            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (point.X < 0 || point.X >= map.Dungeon.GetLength(0) || point.Y < 0 || point.Y >= map.Dungeon.GetLength(1))
                {
                    continue;
                }

                if (map.Dungeon[point.X, point.Y] != MapCell.Empty)
                {
                    continue;
                }

                for (var dy = -1; dy <= 1; dy++)
                {
                    for (var dx = -1; dx <= 1; dx++)
                    {
                        if (dx != 0 && dy != 0) continue;
                        var nextPoint = new Point { X = point.X + dx, Y = point.Y + dy };

                        if (visitedPoints.Contains(nextPoint)) continue;

                        queue.Enqueue(nextPoint);
                        visitedPoints.Add(nextPoint);
                        dictionary.Add(nextPoint, new SinglyLinkedList<Point>(nextPoint, dictionary[point]));
                    }
                }    
            }

            foreach (var chest in chests)
            {
                if (dictionary.ContainsKey(chest)) yield return dictionary[chest];
            }
        }
    }
}