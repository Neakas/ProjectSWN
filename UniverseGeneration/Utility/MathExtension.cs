using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniverseGeneration
{
    static class MathExtension
    {
        public static double Round(this float value, int precision)
        {
            if (precision < -4 && precision > 15)
                throw new ArgumentOutOfRangeException("precision", "Must be and integer between -4 and 15");

            if (precision >= 0) return Math.Round(value, precision);
            else
            {
                precision = (int)Math.Pow(10, Math.Abs(precision));
                value = value + (5 * precision / 10);
                return Math.Round(value - (value % precision), 0);
            }
        }
    }
}
