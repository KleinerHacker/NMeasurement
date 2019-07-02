using System;

namespace NMeasurement.Types.Temperatures.Internals
{
    internal sealed class TemperatureUnit : TemperatureUnitBase
    {
        public TemperatureUnit(Func<double, double> calculationToRawValue, Func<double, double> calculationFromRawValue, string abbreviation) : base(calculationToRawValue, calculationFromRawValue, abbreviation)
        {
        }
    }
}