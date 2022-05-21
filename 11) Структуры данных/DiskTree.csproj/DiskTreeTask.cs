using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskTree
{
    public class DiskTreeTask
    {
        public class IveGotNoRoots
        {
            public string Name;
            public Dictionary<string, IveGotNoRoots> Nodes = new Dictionary<string, IveGotNoRoots>();

            public IveGotNoRoots(string name)
            {
                Name = name;
            }

            public IveGotNoRoots GetDirection(string subRoot) =>
                Nodes.TryGetValue(subRoot, out var node)? node : Nodes[subRoot] = new IveGotNoRoots(subRoot);

            public List<string> MakeConclusion(int j, List<string> list)
            {
                if (j >= 0)
                {
                    list.Add(new string(' ', j) + Name);
                }
                j++;
                return Nodes
                    .Values
                    .OrderBy(root => root.Name, StringComparer.Ordinal)
                    .Aggregate(list, (current, root) => root.MakeConclusion(j, current));
            }
        }

        public static List<string> Solve(List<string> input)
        {
            var root = new IveGotNoRoots("");
            foreach (var name in input)
            {
                var node = root;
                var path = name.Split('\\');

                foreach (var item in path)
                {
                    node = node.GetDirection(item);
                }
            }
            return root.MakeConclusion(-1, new List<string>());
        }
    }
}
