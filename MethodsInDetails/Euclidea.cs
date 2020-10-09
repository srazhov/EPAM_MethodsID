//-----------------------------------------------------------------------
// <copyright file="Euclidea.cs" company="EPAM">
// Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Srazhov Miras</author>
//-----------------------------------------------------------------------

namespace MethodsInDetails
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Class that holds various realizations of Euclidean algorithm 
    /// </summary>
    public class Euclidea
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Euclidea"/> class
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Numbers' length must not be less than 2</exception>
        /// <param name="numbers">Numbers to find GCD</param>
        public Euclidea(params int[] numbers)
        {
            if (numbers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Numbers' length must not be less than 2");
            }

            this.Numbers = numbers;
        }

        /// <summary>
        /// Gets numbers to look for
        /// </summary>
        public int[] Numbers { get; }

        /// <summary>
        /// Find the GCD from the given numbers
        /// by using Euclidean algorithm
        /// </summary>
        /// <param name="numbers">Numbers to look for</param>
        /// <exception cref="ArgumentOutOfRangeException">Numbers' length must not be less than 2</exception>
        /// <returns>The Greatest Common Divisor (GCD)</returns>
        public static int EuclideanAlgorithm(params int[] numbers)
        {
            if (numbers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Numbers' length must not be less than 2");
            }

            int first = Math.Abs(numbers[0]);
            int second = Math.Abs(numbers[1]);

            if (first == 0 && second == 0)
            {
                return 0;
            }

            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                }
                else
                {
                    second %= first;
                }
            }

            if (numbers.Length == 2)
            {
                return first + second;
            }

            int[] gcd = numbers.Skip(2).Concat(new int[] { first + second }).ToArray();
            return EuclideanAlgorithm(gcd);
        }

        /// <summary>
        /// Find the GCD from the given numbers
        /// by using Steins algorithm
        /// </summary>
        /// <param name="numbers">Numbers to look for</param>
        /// <returns>The Greatest Common Divisor (GCD)</returns>
        public static int SteinsAlgorithm(params int[] numbers)
        {
            if (numbers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Numbers' length must not be less than 2");
            }

            int a = Math.Abs(numbers[0]);
            int b = Math.Abs(numbers[1]);

            if (numbers.Length > 2)
            {
                b = SteinsAlgorithm(numbers.Skip(1).ToArray());
            }

            if (a == b)
            {
                return a;
            }

            if (a == 0 || b == 0)
            {
                return a + b;
            }

            if ((a & 1) == 0)
            {
                if ((b & 1) == 0)
                {
                    return 2 * SteinsAlgorithm(a / 2, b / 2);
                }
                else
                {
                    return SteinsAlgorithm(a / 2, b);
                }
            }
            else
            {
                if ((b & 1) == 0)
                {
                    return SteinsAlgorithm(a, b / 2);
                }
                else
                {
                    if (a > b)
                    {
                        return SteinsAlgorithm((a - b) / 2, b);
                    }
                    else
                    {
                        return SteinsAlgorithm((b - a) / 2, a);
                    }
                }
            }
        }

        /// <summary>
        /// Find the GCD from the given numbers
        /// by using Euclidean algorithm
        /// </summary>
        /// <returns>The Greatest Common Divisor (GCD)</returns>
        public int EuclideanAlgorithm()
        {
            return EuclideanAlgorithm(this.Numbers);
        }

        /// <summary>
        /// Find the GCD from the given numbers
        /// by using Steins algorithm
        /// </summary>
        /// <returns>The Greatest Common Divisor (GCD)</returns>
        public int SteinsAlgorithm()
        {
            return SteinsAlgorithm(this.Numbers);
        }

        /// <summary>
        /// Finds the amount of time required to execute <see cref="EuclideanAlgorithm()"/> method
        /// </summary>
        /// <returns>Time required to execute</returns>
        public string GetRequiredTimeForExecutionEuclid()
        {
            Stopwatch sw = Stopwatch.StartNew();
            EuclideanAlgorithm();
            sw.Stop();

            TimeSpan ts = sw.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:0000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            return elapsedTime;
        }

        /// <summary>
        /// Finds the amount of time required to execute <see cref="SteinsAlgorithm()"/> method
        /// </summary>
        /// <returns>Time required to execute</returns>
        public string GetRequiredTimeForExecutionStein()
        {
            Stopwatch sw = Stopwatch.StartNew();
            SteinsAlgorithm();
            sw.Stop();

            TimeSpan ts = sw.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:0000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            return elapsedTime;
        }
    }
}
