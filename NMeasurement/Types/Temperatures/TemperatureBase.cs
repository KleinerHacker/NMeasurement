using System;
using System.Diagnostics.CodeAnalysis;
using System.Media;
using NMeasurement.Types.Temperatures.Attributes;
using NMeasurement.Types.Temperatures.Interfaces;
using NMeasurement.Types.Temperatures.Internals;

namespace NMeasurement.Types.Temperatures
{
    public abstract class TemperatureBase<T, TT> : MeasurementBase<T, TemperatureAttribute, TT>, ITemperature<T> where T : ITemperatureUnitBase where TT : TemperatureBase<T, TT>
    {
        #region Static Init

        /// <summary>
        /// <inheritdoc cref="Unit.Celsius"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TT FromCelsius(double value) => Create(value, (T) Unit.Celsius);
        /// <summary>
        /// <inheritdoc cref="Unit.Fahrenheit"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TT FromFahrenheit(double value) => Create(value, (T) Unit.Fahrenheit);
        /// <summary>
        /// <inheritdoc cref="Unit.Kelvin"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TT FromKelvin(double value) => Create(value, (T) Unit.Kelvin);

        #endregion

        #region Properties

        /// <summary>
        /// <inheritdoc cref="Unit.Celsius"/>
        /// </summary>
        public double Celsius => GetValue((T) Unit.Celsius);
        /// <summary>
        /// <inheritdoc cref="Unit.Fahrenheit"/>
        /// </summary>
        public double Fahrenheit => GetValue((T) Unit.Fahrenheit);
        /// <summary>
        /// <inheritdoc cref="Unit.Kelvin"/>
        /// </summary>
        public double Kelvin => GetValue((T) Unit.Kelvin);

        #endregion
        
        protected internal TemperatureBase(double value) : base(value)
        {
            if (value < 0d)
                throw new ArgumentException("Cannot accept value lower than 0 °K: " + value);
        }

        protected internal TemperatureBase(double value, T unit) : base(value, unit)
        {
            if (value < 0d)
                throw new ArgumentException("Cannot accept value lower than 0 °K: " + value);
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(Value);

        public override string ToString(T unit) => $"{GetValue(unit)} {unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known temperature units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            /// <summary>
            /// <b>Base unit</b>
            /// </summary>
            public static ITemperatureUnit Celsius { get; } = new TemperatureUnit((v, d) => v + 273.15d, (v, d) => v - 273.15d, "°C");
            /// <summary>
            /// = (base - 32) * 5/9 + 273,15
            /// </summary>
            public static ITemperatureUnit Fahrenheit { get; } = new TemperatureUnit((v, d) => (v - 32d) * 5d / 9d + 273.15d, (v, d) => (v - 273.15d) * 9d / 5d + 32d, "°F");
            /// <summary>
            /// <b>Base unit</b>
            /// </summary>
            public static ITemperatureUnit Kelvin { get; } = new TemperatureUnit((v, d) => v, (v, d) => v, "°K");

            /// <summary>
            /// Create a custom temperature unit based on given calculation function
            /// </summary>
            /// <param name="calculationToRawValue">Function to call to calculate raw value (based on <b>1° celsius</b>) from value</param>
            /// <param name="calculationFromRawValue">Function to call to calculate value from raw value (based on <b>1° celsius</b>)</param>
            /// <param name="abbreviation">Abbreviation for custom temperature unit</param>
            /// <returns>A customized temperature unit</returns>
            public static ITemperatureUnit CreateUnit(Func<double, uint, double> calculationToRawValue, Func<double, uint, double> calculationFromRawValue, string abbreviation = "<custom>")
            {
                return new TemperatureUnit(calculationToRawValue, calculationFromRawValue, abbreviation);
            }
        }

        #endregion
    }
}