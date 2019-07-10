using System;

namespace NMeasurement.Utils
{
    internal static class ExtendedMathUtils
    {
        public static double CalculateToRawValue(double value, double factor, uint dimension)
        {
            return value * Math.Pow(factor, dimension);
        }

        public static double CalculateFromRawValue(double rawValue, double factor, uint dimension)
        {
            return rawValue / Math.Pow(factor, dimension);
        }
    }
}