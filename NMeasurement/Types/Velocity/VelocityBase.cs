using System;
using System.Diagnostics.CodeAnalysis;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Velocity.Attributes;
using NMeasurement.Types.Velocity.Interfaces;
using NMeasurement.Types.Velocity.Internals;

namespace NMeasurement.Types.Velocity
{
    /// <summary>
    /// Base implementation for all velocity types.
    /// </summary>
    /// <typeparam name="T">Type of acceptable velocity units</typeparam>
    /// <typeparam name="TV">Type of concrete implementation of velocity itself</typeparam>
    public abstract class VelocityBase<T, TV> : MeasurementBase<T, VelocityAttribute, TV>, IVelocity<T> where T : IVelocityUnitBase where TV : VelocityBase<T, TV>
    {
        #region Static Init

        public static TV FromKilometersPerHour(double value) => Create(value, (T) Unit.KilometerPerHour);
        public static TV FromMeterPerSecond(double value) => Create(value, (T) Unit.MeterPerSecond);

        public static TV FromMilePerHour(double value) => Create(value, (T) Unit.MilePerHour);
        public static TV FromFeetPerSecond(double value) => Create(value, (T) Unit.FeetPerSecond);

        public static TV FromKnot(double value) => Create(value, (T) Unit.Knot);

        #endregion

        #region Properties

        public double KilometerPerHour => GetValue((T) Unit.KilometerPerHour);
        public double MeterPerSecond => GetValue((T) Unit.MeterPerSecond);

        public double MilePerHour => GetValue((T) Unit.MilePerHour);
        public double FeetPerSecond => GetValue((T) Unit.FeetPerSecond);

        public double Knot => GetValue((T) Unit.Knot);

        #endregion

        protected internal VelocityBase(double value) : base(value)
        {
        }

        protected internal VelocityBase(double value, T unit) : base(value, unit)
        {
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(Value);

        public override string ToString(T unit) => $"{GetValue(unit)} {unit.TotalAbbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known velocity units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            public static IVelocityUnit KilometerPerHour { get; } = new VelocityUnit((Length.Unit.Meter, UnitPrefix.Kilo), (Duration.Unit.Hour, null));
            public static IVelocityUnit MeterPerSecond { get; } = new VelocityUnit((Length.Unit.Meter, null), (Duration.Unit.Second, null));

            public static IVelocityUnit MilePerHour { get; } = new VelocityUnit((Length.Unit.Mile, null), (Duration.Unit.Hour, null), "mph");
            public static IVelocityUnit FeetPerSecond { get; } = new VelocityUnit((Length.Unit.Feet, null), (Duration.Unit.Second, null), "ftps");

            public static IVelocityUnit Knot { get; } = new VelocityUnit(0.51444d, "kn");

            /// <summary>
            /// Create a custom velocity unit based on given factor.
            /// </summary>
            /// <param name="factor">Factor for custom velocity unit</param>
            /// <param name="abbreviation">Abbreviation for custom velocity unit</param>
            /// <returns>A valid custom velocity unit</returns>
            public static IVelocityUnit CreateUnit(double factor, string abbreviation = "<custom>") => new VelocityUnit(factor, abbreviation);

            /// <summary>
            /// Create a custom velocity unit based on combined units.
            /// </summary>
            /// <param name="lengthUnit">Numerator length unit</param>
            /// <param name="durationUnit">Denominator duration unit (time)</param>
            /// <param name="abbreviation">Optional overwrite of abbreviation</param>
            /// <returns>A valid custom velocity unit</returns>
            public static IVelocityUnit CreateUnit((ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, IPrefix) durationUnit, string abbreviation = null) => new VelocityUnit(lengthUnit, durationUnit, abbreviation);
        }

        #endregion
    }
}