using System;
using System.Collections.Generic;

namespace Antiplagiarism
{
    public static class LongestCommonSubsequenceCalculator
    {
        public static List<string> Calculate(List<string> first, List<string> second)
        {
            var opt = CreateOptimizationTable(first, second);
            return RestoreAnswer(opt, first, second);
        }

        private static int[,] CreateOptimizationTable(List<string> first, List<string> second)
        {
            var opt = new int[first.Count + 1, second.Count + 1];
            for (var i = first.Count - 1; i >= 0; i--)
            {
                for (var j = second.Count - 1; j >= 0; j--)
                {
                    int v = first[i] == second[j]
                                                                ? 1 + opt[i + 1, j + 1]
                                                                : Math.Max(opt[i + 1, j], opt[i, j + 1]);
                    opt[i, j] = v;
                }
            }
                
            return opt;
        }

        private static List<string> RestoreAnswer(int[,] opt, List<string> first, List<string> second)
        {
            int x = 0;
            int y = 0;
            var rslt = new List<string>();

            while (x != first.Count && y != second.Count)
            {
                if (first[x] == second[y])
                {
                    rslt.Add(first[x]);
                    x++;
                    y++;
                }
                else
                {
                    if (opt[x, y] == opt[x + 1, y])
                    {
                        x++;
                    }
                    else
                    {
                        y++;
                    }
                }
            }

            return rslt;
        }
    }
}