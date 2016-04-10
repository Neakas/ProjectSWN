using System;

namespace UniverseGeneration.Utility
{
    internal static class MathExtension
    {
        public static double Round( this float value, int precision )
        {
            if (precision < -4 && precision > 15)
            {
                throw new ArgumentOutOfRangeException(nameof(precision), "Must be and integer between -4 and 15");
            }

            if (precision >= 0)
            {
                return Math.Round(value, precision);
            }
            precision = (int) Math.Pow(10, Math.Abs(precision));
            value = (float) ( value + 5.00 * precision / 10 );
            return Math.Round(value - value % precision, 0);
        }
    }
}