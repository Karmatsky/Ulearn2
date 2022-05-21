using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tickets
{
    internal class TicketsTask
    {
        private const int MaximumLength = 100;
        private const int MaximumSum = 2000;

        public static BigInteger Solve(int halfLength, int totalSum)
        {
            if (totalSum % 2 != 0)
            {
                return 0;
            }

            var happyT = HappyTicketsContainer();
            var half = CountHappyTickets(happyT, halfLength, totalSum / 2);

            return half * half;
        }

        private static BigInteger[,] HappyTicketsContainer()
        {
            var happyT = new BigInteger[MaximumLength + 1, MaximumSum + 1];

            for (var i = 0; i < MaximumLength; i++)
            {
                for (var j = 0; j < MaximumSum; j++)
                {
                    happyT[i, j] = -1;
                }
            }
            return happyT;
        }

        private static BigInteger CountHappyTickets(BigInteger[,] happyTickets, int len, int sum)
        {
            if (happyTickets[len, sum] >= 0)
            {
                return happyTickets[len, sum];
            }
            if (sum == 0)
            {
                return 1;
            }
            if (len == 0)
            {
                return 0;
            }

            happyTickets[len, sum] = 0;
            for (var k = 0; k < 10; k++)
            {
                if (sum - k >= 0)
                {
                    happyTickets[len, sum] += CountHappyTickets(happyTickets, len - 1, sum - k);
                }
            }

            return happyTickets[len, sum];
        }
    }
}