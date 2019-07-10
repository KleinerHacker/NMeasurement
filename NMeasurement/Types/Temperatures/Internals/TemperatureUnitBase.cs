using System;
using NMeasurement.Types.Temperatures.Interfaces;

namespace NMeasurement.Types.Temperatures.Internals
{
    internal abstract class TemperatureUnitBase : SingleUnitBase, ITemperatureUnit
    {
        protected TemperatureUnitBase(Func<double, uint, double> toRawValue, Func<double, uint, double> fromRawValue, string abbreviation) : base(toRawValue, fromRawValue, abbreviation)
        {
        }

        public double CalculateToRawValue(double value, uint dimension = 1)
        {
            if (dimension != 1)
                throw new ArgumentException("Temperature do not support multiple dimensions");
            
            return base.CalculateToRawValue(value, dimension);
        }

        public double CalculateFromRawValue(double rawValue, uint dimension = 1)
        {
            if (dimension != 1)
                throw new ArgumentException("Temperature do not support multiple dimensions");

            return base.CalculateFromRawValue(rawValue, dimension);
        }
    }
}