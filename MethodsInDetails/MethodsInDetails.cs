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
            int sign = (int)(((ulong)dub >> 63) & 1);

            // Finding an exponent
            string exp = intToBit(Math.Abs((int)dub));
            string exponent = intToBit(1023 - (-exp.Length + 1));
            exponent = new string('0', 11 - exponent.Length) + exponent;

            // Finding a mantissa
            string fraction = calculateFraction(Math.Abs(dub - Math.Truncate(dub)), new List<double>(), out int diff);
            string mantissa = exp == string.Empty ? fraction : exp.Substring(1) + fraction;
            while (mantissa.Length < 52)
            {
                // Fill mantissa output with repeating digits if it doesn't have enough digits
                mantissa += fraction.Remove(0, diff + 1);
            }

            if (mantissa.Length > 52)
            {
                mantissa = mantissa.Remove(52);
            }

            return sign.ToString() + exponent + mantissa;

            string intToBit(int intObject)
            {
                List<int> toOutPut = new List<int>();

                int degree = 0;
                while (intObject != 0)
                {
                    int temp = (int)Math.Pow(2, degree);
                    if ((intObject & temp) == temp)
                    {
                        intObject -= intObject & temp;
                        toOutPut.Add(1);
                    }
                    else
                    {
                        toOutPut.Add(0);
                    }

                    degree++;
                }

                toOutPut.Reverse();
                string testResult = string.Join(string.Empty, toOutPut);
                return testResult;
            }
            
            string calculateFraction(double value, List<double> values, out int startsDifference)
            {
                value *= 2;
                int result = 0;
                if (value >= 1.0d)
                {
                    value--;
                    result = 1;
                }

                if (values.Contains(value))
                {
                    startsDifference = values.IndexOf(value);
                    return result.ToString();
                }

                values.Add(value);
                return result + calculateFraction(value, values, out startsDifference);
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
