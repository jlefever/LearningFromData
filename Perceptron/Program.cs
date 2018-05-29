﻿using System;
using System.Collections.Generic;

namespace Perceptron
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            var n = 100;
            var numOfRuns = 10000;

            var total = 0;

            for (int i = 0; i < numOfRuns; i++)
            {
                total += Run(n);
            }

            Console.WriteLine(total / (double)numOfRuns);
        }

        private static int Run(int n)
        {
            var a = GetRandomPoint();
            var b = GetRandomPoint();

            var data = new Tuple<Point, Sign>[n];

            for (int i = 0; i < n; i++)
            {
                var c = GetRandomPoint();
                var sign = SideOfTheLine(a, b, c);
                data[i] = new Tuple<Point, Sign>(c, sign);
            }

            // PLA
            var misclasified = new List<Tuple<Point, Sign>>(data);
            var w = new double[3] { 0, 0, 0 };
            var numberOfIterations = 0;

            while (misclasified.Count != 0)
            {
                numberOfIterations++;

                // Pick random misclassified point
                var datum = misclasified[random.Next(misclasified.Count)];

                // Apply update rule to weight vector
                w[0] = w[0] + (int)datum.Item2;
                w[1] = w[1] + (int)datum.Item2 * datum.Item1.X;
                w[2] = w[2] + (int)datum.Item2 * datum.Item1.Y;

                // Recompute misclassified
                misclasified.Clear();
                for (int i = 0; i < data.Length; i++)
                {
                    var sign = GetSign(w[0] + w[1] * data[i].Item1.X + w[2] * data[i].Item1.Y);

                    if (sign != data[i].Item2)
                    {
                        misclasified.Add(data[i]);
                    }
                }
            }

            return numberOfIterations;
        }

        //public static double P(Point a, Point b, double[] w)
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        var c = GetRandomPoint();
        //        var sign = SideOfTheLine(a, b, c);

        //        GetSign(w[0] + w[1] * data[i].Item1.X + w[2] * data[i].Item1.Y);
        //    }
        //}

        private static Sign SideOfTheLine(Point a, Point b, Point c)
        {
            return GetSign((b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X));
        }

        private static Sign GetSign(double input)
        {
            if (input == 0)
            {
                return Sign.Zero;
            }

            return input < 0 ? Sign.Negative : Sign.Positive;
        }

        private static Point GetRandomPoint()
        {
            return new Point
            {
                X = random.NextDouble(-1, 1),
                Y = random.NextDouble(-1, 1)
            };
        }
    }

    public enum Sign
    {
        Positive = 1,
        Negative = -1,
        Zero = 0
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }

    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
