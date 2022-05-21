using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {
            var exitPath = BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit }).FirstOrDefault();

            if (exitPath == null)
                return new MoveDirection[0];

            if (map.Chests.Any(chest => exitPath.ToList().Contains(chest)))
                return exitPath.ToList().ParseToDirections();

            var chestz = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
            var result = chestz.Select(chest => Tuple.Create(
                chest, BfsTask.FindPaths(map, chest.Value, new[] { map.Exit }).FirstOrDefault()))
                .SearchingMinimumElement();

            if (result == null) return exitPath.ToList().ParseToDirections();
            return result.Item1.ToList().ParseToDirections().Concat(
                result.Item2.ToList().ParseToDirections())
                .ToArray();
        }
    }

    public static class MinimumPath
    {
        public static Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>
            SearchingMinimumElement(this IEnumerable<Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>> items)
        {
            if (items.Count() == 0 || items.First().Item2 == null)
                return null;

            var minimum = int.MaxValue;
            var minimumElement = items.First();
            foreach (var e in items)
                if (e.Item1.Length + e.Item2.Length < minimum)
                {
                    minimum = e.Item1.Length + e.Item2.Length;
                    minimumElement = e;
                }
            return minimumElement;
        }

        public static MoveDirection[] ParseToDirections(this List<Point> items)
        {
            var listResult = new List<MoveDirection>();
            if (items == null)
                return new MoveDirection[0];
            var lengthOfItems = items.Count;

            for (var j = lengthOfItems - 1; j > 0; j--)
            {
                listResult.Add(GetDirection(items[j], items[j - 1]));
            }
            return listResult.ToArray();
        }

        static MoveDirection GetDirection(Point firstPoint, Point secondPoint)
        {
            var point = new Point(firstPoint.X - secondPoint.X, firstPoint.Y - secondPoint.Y);

            if (point.X == 1) return MoveDirection.Left;
            if (point.X == -1) return MoveDirection.Right;
            if (point.Y == 1) return MoveDirection.Up;
            if (point.Y == -1) return MoveDirection.Down;
            throw new ArgumentException();
        }
    }
}