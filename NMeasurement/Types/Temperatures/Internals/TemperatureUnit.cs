using System;

namespace NMeasurement.Types.Temperatures.Internals
{
    internal sealed class TemperatureUnit : TemperatureUnitBase
    {
        public TemperatureUnit(Func<double, uint, double> toRawValue, Func<double, uint, double> fromRawValue, string abbreviation) : base(toRawValue, fromRawValue, abbreviation)
        {
        }
    }
}