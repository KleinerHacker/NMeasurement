using System;
using System.Diagnostics.CodeAnalysis;
using NMeasurement.Types.Durations.Attributes;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Durations.Internals;

namespace NMeasurement.Types.Durations
{
    /// <summary>
    /// Base implementation for all duration types.
    /// </summary>
    /// <typeparam name="T">Type of acceptable duration units</typeparam>
    /// <typeparam name="TD">Type of concrete duration implementation itself</typeparam>
    public abstract class DurationBase<T, TD> : MeasurementBase<T, DurationAttribute, TD, ISmallPrefix>, IDuration<T> where T : IDurationUnitBase where TD : DurationBase<T, TD>
    {
        #region Static Init

        public static TD FromSecond(double value, ISmallPrefix prefix = null) => Create(value, (T) Unit.Second, prefix);
        public static TD FromMinute(double value) => Create(value, (T) Unit.Minute);
        public static TD FromHour(double value) => Create(value, (T) Unit.Hour);
        public static TD FromDay(double value) => Create(value, (T) Unit.Day);

        #endregion

        #region Properties
        public double GetSecond(ISmallPrefix prefix) => GetValue((T) Unit.Second, prefix);
        public double Second => GetValue((T) Unit.Second);
        public double Minute => GetValue((T) Unit.Minute);
        public double Hour => GetValue((T) Unit.Hour);
        public double Day => GetValue((T) Unit.Day);

        #endregion

        protected internal DurationBase(double value) : base(value)
        {
        }

        protected internal DurationBase(double value, T unit, ISmallPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Abstract Implemnentation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(rawValue);

        public override string ToString(T unit, ISmallPrefix prefix) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known duration units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            /// <summary>
            /// <b>Base Unit</b>
            /// </summary>
            public static IDurationUnit Second { get; } = new DurationUnit(1d, "s");
            /// <summary>
            /// = 60
            /// </summary>
            public static IDurationUnit Minute { get; } = new DurationUnit(60d, "m");
            /// <summary>
            /// = 60 * 60
            /// </summary>
            public static IDurationUnit Hour { get; } = new DurationUnit(60d * 60d, "h");
            /// <summary>
            /// = 60 * 60 * 24
            /// </summary>
            public static IDurationUnit Day { get; } = new DurationUnit(60d * 60d * 24d, "d");
        
            /// <summary>
            /// Create a custom duration unit based on given factor. 
            /// </summary>
            /// <param name="factor">Factor for custom duration unit</param>
            /// <param name="abbreviation">Abbreviation for custom duration unit</param>
            /// <returns>A valid custom duration unit</returns>
            public static IDurationUnit CreateUnit(double factor, string abbreviation = "<custom>") => new DurationUnit(factor, abbreviation);
        }

        #endregion
    }
}