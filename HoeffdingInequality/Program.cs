using System;

namespace Perceptron
{
    public class HoeffdingInequlity
    {
        private static Random random = new Random();

        public static void Main(string[] args)
        {
            var results = new ExperimentResult[100000];

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = RunExperiment();
            }

            var totalFirstCoinValue = 0.0;
            var totalRandomCoinValue = 0.0;
            var totalMinCoinValue = 0.0;

            for (int i = 0; i < results.Length; i++)
            {
                totalFirstCoinValue += results[i].FirstCoinValue;
                totalRandomCoinValue += results[i].RandomCoinValue;
                totalMinCoinValue += results[i].MinCoinValue;
            }

            Console.WriteLine(totalFirstCoinValue / results.Length);
            Console.WriteLine(totalRandomCoinValue / results.Length);
            Console.WriteLine(totalMinCoinValue / results.Length);
        }

        private static ExperimentResult RunExperiment()
        {
            var coins = new double[1000];

            for (int i = 0; i < coins.Length; i++)
            {
                coins[i] = FlipFairCoin(10);
            }

            var minCoinValue = 1.0;

            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i] < minCoinValue)
                {
                    minCoinValue = coins[i];
                }
            }

            return new ExperimentResult
            {
                FirstCoinValue = coins[0],
                RandomCoinValue = coins[random.Next(coins.Length)],
                MinCoinValue = minCoinValue
            };
        }

        private static double FlipFairCoin(int n)
        {
            var numOfHeads = 0;

            for (int i = 0; i < n; i++)
            {
                if (random.Next(2) == 0) numOfHeads++;
            }

            return numOfHeads / (double)n;
        }
    }

    public class ExperimentResult
    {
        public double FirstCoinValue { get; set; }
        public double RandomCoinValue { get; set; }
        public double MinCoinValue { get; set; }
    }
}
