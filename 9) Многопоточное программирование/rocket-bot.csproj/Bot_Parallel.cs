using System;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            var tasks = new Task<Tuple<Turn, double>>[threadsCount];
            
            for (var j = 0; j < threadsCount; j++)
            {
                tasks[j] = Task.Run(() =>
                {
                    var random = new Random();
                    return SearchBestMove(rocket, random, iterationsCount / threadsCount);
                });
            }

            var result = Task.WhenAll(tasks);
            var maximum = result.Result.Max(tuple => tuple.Item2);

            foreach (var (firstItem, secondItem) in result.Result)
                if (secondItem == maximum)
                {
                    return rocket.Move(firstItem, level);
                }
            return rocket.Move(Turn.None, level);
        }
    }
}