//-----------------------------------------------------------------------
// <copyright file="MethodsInDetailsTest.cs" company="EPAM">
// Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Srazhov Miras</author>
//-----------------------------------------------------------------------

namespace MethodsInDetails.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Test class of MethodsInDetails
    /// </summary>
    [TestFixture]
    public class MethodsInDetailsTest
    {
        /// <summary>
        /// Test of GetIEEE754
        /// </summary>
        /// <param name="dubObject">Double to check</param>
        /// <returns>String representation of double</returns>
        [TestCase(-255.255, ExpectedResult = "1100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(255.255, ExpectedResult = "0100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(4294967295.0, ExpectedResult = "0100000111101111111111111111111111111111111000000000000000000000")]
        [TestCase(double.MinValue, ExpectedResult = "1111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.MaxValue, ExpectedResult = "0111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.Epsilon, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000001")]
        [TestCase(double.NaN, ExpectedResult = "1111111111111000000000000000000000000000000000000000000000000000")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "1111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "0111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(-0.0, ExpectedResult = "1000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(0.0, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(1.0, ExpectedResult = "0011111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(3.14, ExpectedResult = "0100000000001001000111101011100001010001111010111000010100011111")]
        public string GetIEEE754_GetsDoubleObject_ReturnsInFormat_IEEE754_InString(double dubObject)
        {
            return dubObject.GetIEEE754();
        }

        /// <summary>
        /// Test of IsNull()
        /// </summary>
        /// <param name="input">Strings to check</param>
        /// <returns>True if input is Null</returns>
        [TestCase("Katherine", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = true)]
        public bool IsNull_GetString_ReturnTrueIfNull(string input)
        {
            return input.IsNull();
        }

        /// <summary>
        /// Test of IsNull()
        /// </summary>
        [Test]
        public void IsNull_NullInt_ReturnTrue()
        {
            int? age = null;

            bool expected = true;

            bool actual = age.IsNull();

            Assert.AreEqual(expected, actual);
        }
    }
}