//-----------------------------------------------------------------------
// <copyright file="EuclideaTest.cs" company="EPAM">
// Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Srazhov Miras</author>
//-----------------------------------------------------------------------

namespace MethodsInDetails.Tests
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using NUnit.Framework;

    /// <summary>
    /// Test class of the <see cref="Euclidea"/> class methods
    /// </summary>
    [TestFixture]
    public class EuclideaTest
    {
        /// <summary>
        /// Test method of the <see cref="Euclidea.EuclideanAlgorithm(int[])"/> method
        /// </summary>
        /// <param name="cases">Given cases</param>
        /// <returns>The Greatest Common Divisor</returns>
        [TestCase(1071, 462, ExpectedResult = 21)]
        [TestCase(78, 294, 570, 36, ExpectedResult = 6)]
        [TestCase(15, 255, 750, 430, 340, ExpectedResult = 5)]
        [TestCase(-5, 10, ExpectedResult = 5)]
        [TestCase(0, 10, ExpectedResult = 10)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(10, 10, ExpectedResult = 10)]
        public int EuclideanAlgorithm_GetsTwoOrMoreValues_ReturnsGCD(params int[] cases)
        {
            return Euclidea.EuclideanAlgorithm(cases);
        }

        /// <summary>
        /// Test method of the <see cref="Euclidea.EuclideanAlgorithm(int[])"/>
        /// </summary>
        [Test]
        public void EuclideanAlgorithm_GetsPowOf4and8_Returns5()
        {
            int[] pows = new int[16];
            for (int i = 0; i < 8; i++)
            {
                pows[i] = (int)Math.Pow(4, i + 1);
            }

            for (int i = 8; i < pows.Length; i++)
            {
                pows[i] = (int)Math.Pow(8, i - 7);
            }

            int expected = 4;
            int actual = Euclidea.EuclideanAlgorithm(pows);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method of the <see cref="Euclidea.SteinsAlgorithm(int[])"/> method
        /// </summary>
        /// <param name="cases">Given cases</param>
        /// <returns>The Greatest Common Divisor</returns>
        [TestCase(1071, 462, ExpectedResult = 21)]
        [TestCase(72, 36, 570, 294, ExpectedResult = 6)]
        [TestCase(15, 255, 750, 430, 340, ExpectedResult = 5)]
        [TestCase(-5, 10, ExpectedResult = 5)]
        [TestCase(0, 10, ExpectedResult = 10)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(10, 10, ExpectedResult = 10)]
        public int SteinsAlgorithm_GetsTwoOrMoreValues_ReturnsGCD(params int[] cases)
        {
            return Euclidea.SteinsAlgorithm(cases);
        }

        /// <summary>
        /// Test method of the <see cref="Euclidea.SteinsAlgorithm(int[])"/> method
        /// </summary>
        [Test]
        public void SteinsAlgorithm_GetsPowOf4and8_Returns5()
        {
            int[] pows = new int[16];
            for (int i = 0; i < 8; i++)
            {
                pows[i] = (int)Math.Pow(4, i + 1);
            }

            for (int i = 8; i < pows.Length; i++)
            {
                pows[i] = (int)Math.Pow(8, i - 7);
            }

            int expected = 4;
            int actual = Euclidea.EuclideanAlgorithm(pows);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method of the <see cref="Euclidea.GetRequiredTimeForExecutionEuclid"/>
        /// </summary>
        [Test]
        public void TestTimeCheckerEuclid()
        {
            Euclidea euclid = new Euclidea(15, 255, 750, 430, 340);
           
            string expectedElapsedTime = euclid.GetRequiredTimeForExecutionEuclid();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            euclid.EuclideanAlgorithm();
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;

            string actualElapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:0000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            Console.WriteLine($"actual: /{actualElapsedTime}/. while expected: /{expectedElapsedTime}/");
            Assert.AreEqual(expectedElapsedTime, actualElapsedTime);
        }
    }
}
