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
            if (!double.IsInfinity(dub) && !double.IsNaN(dub))
            {
                exp = intToBit(Math.Truncate(dub));
            }

            int expValue = double.IsInfinity(dub) || double.IsNaN(dub) ? 2047 : 1023 - (-exp.Length + 1);
            if (exp.Length > 52)
            {
                exp = exp.Remove(52);
            }

            string exponent = intToBit(expValue);
            if (exp == string.Empty)
            {
                exponent = "0";
                exp = "0";
            }

            exponent = new string('0', 11 - exponent.Length) + exponent;

            // Finding a mantissa
            double test = Math.Abs(dub - Math.Truncate(dub));
            string fraction = calculateFraction(Math.Abs(dub - Math.Truncate(dub)), exp.Length);
            string mantissa = exp;
            if (exp.Length < 52)
            {
                mantissa = exp.Substring(1) + fraction;
            }

            return sign.ToString() + exponent + mantissa;

            string intToBit(double dubObj)
            {
                List<int> toOutPut = new List<int>();

                while (dubObj != 0)
                {
                    dubObj = Math.Truncate(dubObj);
                    if (dubObj == 1)
                    {
                        toOutPut.Add(1);
                        break;
                    }

                    toOutPut.Add((int)Math.Truncate(dubObj % 2));
                    dubObj /= 2;
                }

                toOutPut.Reverse();
                return string.Join(string.Empty, toOutPut);
            }
            
            string calculateFraction(double value, int max)
            {
                value *= 2;
                string result = "0";
                if (value >= 1.0d)
                {
                    value--;
                    result = "1";
                }

                if (max >= 52)
                {
                    return result;
                }

                max++;
                return result + calculateFraction(value, max);
            }
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
    }
}
