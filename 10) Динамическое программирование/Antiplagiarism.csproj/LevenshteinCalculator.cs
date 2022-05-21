using System;
using System.Configuration;
using System.Collections.Generic;

// Каждый документ — это список токенов. То есть List<string>.
// Вместо этого будем использовать псевдоним DocumentTokens.
// Это поможет избежать сложных конструкций:
// вместо List<List<string>> будет List<DocumentTokens>

using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var rslt = new List<ComparisonResult>();
            for (var i = 0; i < documents.Count; i++)
            {
                for (var j = i + 1; j < documents.Count; j++)
                {
                    rslt.Add(CompareDocs(documents[i], documents[j]));
                }
            }
              
            return rslt;
        }

        private static ComparisonResult CompareDocs(DocumentTokens first, DocumentTokens second)
        {
            var opt = new double[first.Count + 1, second.Count + 1];
            for (var i = 0; i <= first.Count; i++)
            {
                opt[i, 0] = i;
            }
            for (var j = 0; j <= second.Count; j++)
            {
                opt[0, j] = j;
            }
            for (var k = 1; k <= first.Count; k++)
            {
                for (var l = 1; l <= second.Count; l++)
                {
                    opt[k, l] = Math.Min(Math.Min(opt[k - 1, l] + 1,
                            opt[k, l - 1] + 1),
                        opt[k - 1, l - 1] + (first[k - 1] == second[l - 1] ? 0 : TokenDistanceCalculator
                            .GetTokenDistance(first[k - 1], second[l - 1])));
                }
            }
                
            return new ComparisonResult(first, second, opt[first.Count, second.Count]);
        }
    }
}
