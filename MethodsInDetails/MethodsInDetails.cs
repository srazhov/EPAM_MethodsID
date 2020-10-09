//-----------------------------------------------------------------------
// <copyright file="MethodsInDetails.cs" company="EPAM">
// Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Srazhov Miras</author>
//-----------------------------------------------------------------------

namespace MethodsInDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// NET-01.
    /// 9. Methods In Details
    /// </summary>
    public static class MethodsInDetails
    {
        /// <summary>
        /// Turns double object to IEEE754 binary representation
        /// </summary>
        /// <param name="dub">This object</param>
        /// <returns>Return IEEE754 format of double in string</returns>
        public static string GetIEEE754(this double dub)
        {
            // Sign
            int sign = double.IsNegative(dub) ? 1 : 0;
            dub = double.IsNegative(dub) ? dub * -1 : dub;

            // Finding an exponent
            string exp = "0";
            int expValue = 2047;
            if (!double.IsInfinity(dub) && !double.IsNaN(dub))
            {
                exp = DoubleToBit(Math.Truncate(dub));
                expValue = 1023 - (-exp.Length + 1);
            }
            else if (double.IsNaN(dub))
            {
                exp += "1";
            }

            if (exp.Length > 52)
            {
                exp = exp.Remove(52);
            }

            string exponent = DoubleToBit(expValue);
            if (exp == string.Empty)
            {
                // In denormal cases or when dub is 0
                // We should fill exponent with zeros
                exponent = "0";
                exp = "0";
            }

            exponent = new string('0', 11 - exponent.Length) + exponent;

            string fraction;
            if (exponent.All(expo => expo == '0') && dub != 0)
            {
                // Denormal values
                // Mantissa's value if exponent is complete 0
                fraction = DenormalToBit(Math.Abs(dub - Math.Truncate(dub)));
                if (fraction.Length < 52)
                {
                    // When fraction's Length is less than 52, it only means that it's gone to its limit
                    // So there are no more values. In this case we have to add 0 to the end
                    fraction += new string('0', 52 - fraction.Length);
                }
            }
            else
            {
                // Finding a mantissa in other cases
                fraction = CalculateFraction(Math.Abs(dub - Math.Truncate(dub)), exp.Length);
            }

            string mantissa = exp;
            if (exp.Length < 52)
            {
                // Sometimes, exp's Lenght can be equal to 52
                // But in all the other cases we should go with the regular formula
                mantissa = exp.Substring(1) + fraction;
            }

            return sign.ToString() + exponent + mantissa;
        }

        /// <summary>
        /// Checks object if it does not have a Value
        /// </summary>
        /// <param name="nullable">This object</param>
        /// <returns>Is object is null</returns>
        public static bool IsNull(this object nullable)
        {
            return nullable == null;
        }

        /// <summary>
        /// Converts double value to its Base2 interpretation
        /// </summary>
        /// <param name="dubObj">This double object</param>
        /// <returns>Base2 code in string</returns>
        private static string DoubleToBit(double dubObj)
        {
            StringBuilder toOutPut = new StringBuilder();

            while (dubObj != 0)
            {
                dubObj = Math.Truncate(dubObj);
                if (dubObj == 1)
                {
                    toOutPut.Append(1);
                    break;
                }

                toOutPut.Append((int)Math.Truncate(dubObj % 2));
                dubObj /= 2;
            }

            return new string(toOutPut.ToString().Reverse().ToArray());
        }

        /// <summary>
        /// Finds Mantissa from a given subnormal value
        /// </summary>
        /// <param name="dubObj">This double object</param>
        /// <returns>Mantissa in string</returns>
        private static string DenormalToBit(double dubObj)
        {
            StringBuilder toOutPut = new StringBuilder();

            int degree = -1023;

            while (dubObj != 0)
            {
                double temp = Math.Pow(2, degree);
                if (dubObj >= temp)
                {
                    dubObj %= temp;
                    toOutPut.Append(1);
                }
                else
                {
                    toOutPut.Append(0);
                }

                degree--;
            }

            return new string(toOutPut.ToString().ToArray());
        }

        /// <summary>
        /// Finds non-subnormal values' Fraction
        /// </summary>
        /// <param name="value">This double object</param>
        /// <param name="start">Start from</param>
        /// <returns>Mantissa of the given value</returns>
        private static string CalculateFraction(double value, int start)
        {
            value *= 2;
            string result = "0";
            if (value >= 1.0d)
            {
                value--;
                result = "1";
            }

            if (start >= 52)
            {
                return result;
            }

            start++;
            return result + CalculateFraction(value, start);
        }
    }
}
