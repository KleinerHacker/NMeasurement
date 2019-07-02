using System;
using NMeasurement.Types.Temperatures.Interfaces;

namespace NMeasurement.Types.Temperatures.Internals
{
    internal abstract class TemperatureUnitBase : ITemperatureUnit
    {
        private readonly Func<double, double> _calculationToRawValue;
        private readonly Func<double, double> _calculationFromRawValue;

        public string Abbreviation { get; }

        protected TemperatureUnitBase(Func<double, double> calculationToRawValue, Func<double, double> calculationFromRawValue, string abbreviation)
        {
            _calculationToRawValue = calculationToRawValue;
            _calculationFromRawValue = calculationFromRawValue;
            Abbreviation = abbreviation;
        }

        public double CalculateToRawValue(double value)
        {
            return _calculationToRawValue.Invoke(value);
        }

        public double CalculateFromRawValue(double rawValue)
        {
            return _calculationFromRawValue.Invoke(rawValue);
        }
    }
}