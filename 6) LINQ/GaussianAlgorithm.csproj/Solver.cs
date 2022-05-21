using System;
using System.Linq;
using System.Collections.Generic;

namespace GaussAlgorithm
{
    public class Solver
    {
        public double[] Solve(double[][] items, double[] freeMembers)
        {
            var freeElements = new List<double>();
            var gaussMatrix = new List<List<double>>();
            int сolumnСhange = 0;

            gaussMatrix = new List<List<double>>(items.Select(x => x.ToList()));

            freeElements.AddRange(freeMembers.ToList());

            for (int k = 0; k < gaussMatrix[0].Count; k++)
                if (сolumnСhange + k < gaussMatrix[0].Count && k < gaussMatrix.Count)
                {
                    var mainRatio = gaussMatrix[k][k + сolumnСhange];
                    if (mainRatio == 0)
                    {
                        var posibleLine = gaussMatrix.FindIndex(k, x => x[k + сolumnСhange] != 0);
                        if (posibleLine == -1)
                        {
                            сolumnСhange++;
                            k--;
                            continue;
                        }
                        var temporary = gaussMatrix[k];
                        gaussMatrix[k] = gaussMatrix[posibleLine];
                        gaussMatrix[posibleLine] = temporary;
                        mainRatio = gaussMatrix[k][k + сolumnСhange];
                        var temporary2 = freeElements[k];
                        freeElements[k] = freeElements[posibleLine];
                        freeElements[posibleLine] = temporary2;
                    }
                    for (int y = 0; y < gaussMatrix[0].Count; y++)
                    {
                        gaussMatrix[k][y] = gaussMatrix[k][y] / mainRatio;
                    }

                    freeElements[k] = freeElements[k] / mainRatio;

                    for (int x = 0; x < gaussMatrix.Count; x++)
                        if (x != k)
                        {
                            var temporaryCoefficient = gaussMatrix[x][k + сolumnСhange];

                            for (int y = 0; y < gaussMatrix[0].Count; y++)
                            {
                                gaussMatrix[x][y] = gaussMatrix[x][y] - gaussMatrix[k][y] * temporaryCoefficient;
                            }
                            freeElements[x] = freeElements[x] - freeElements[k] * temporaryCoefficient;
                            if (gaussMatrix[x].All(z => Math.Abs(z) < 1e-3) && Math.Abs(freeElements[x]) > 1e-3)
                            {
                                throw new NoSolutionException("Нет решения" + freeElements[x] + " " + x);
                            }

                        }
                }

            var result = new List<double>();
            сolumnСhange = 0;

            for (int k = 0; k < gaussMatrix[0].Count; k++)
            {
                if (k - сolumnСhange >= freeElements.Count)
                {
                    result.Add(0);
                }
                else if (gaussMatrix[k - сolumnСhange][k] == 0)
                {
                    result.Add(0);
                    сolumnСhange++;
                }
                else
                    result.Add(freeElements[k - сolumnСhange] / gaussMatrix[k - сolumnСhange][k]);
            }
            return result.ToArray();
        }
    }
}
