using System;
using System.Diagnostics.CodeAnalysis;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Forces.Attributes;
using NMeasurement.Types.Forces.Interfaces;
using NMeasurement.Types.Forces.Internals;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Forces
{
    /// <summary>
    /// Base implementation for all force types
    /// </summary>
    /// <typeparam name="T">Type of acceptable units</typeparam>
    /// <typeparam name="TF">Type of concrete force implementation itself</typeparam>
    public abstract class ForceBase<T, TF> : MeasurementBase<T, ForceAttribute, TF, IPrefix>, IForce<T> where T : IForceUnitBase where TF : ForceBase<T, TF>
    {
        #region Static Init

        public static TF FromNewton(double value, IPrefix prefix = null) => Create(value, (T) Unit.Newton, prefix);
        public static TF FromKilogramMeterPerSquareSecond(double value) => Create(value, (T) Unit.KilogramMeterPerSquareSecond);

        #endregion

        #region Properties

        public double GetNewton(IPrefix prefix) => GetValue((T) Unit.Newton, prefix);
        public double Newton => GetValue((T) Unit.Newton);
        public double KilogramMeterPerSquareSecond => GetValue((T) Unit.KilogramMeterPerSquareSecond);

        #endregion
        
        protected internal ForceBase(double value) : base(value)
        {
        }

        protected internal ForceBase(double value, T unit, IPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(rawValue);

        public override string ToString(T unit, IPrefix prefix) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known force units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            /// <summary>
            /// Newton - <b>Base unit</b>
            /// </summary>
            public static IForceUnit Newton { get; } = new ForceUnit(1000d, "N");
            /// <summary>
            /// = 1 N
            /// </summary>
            public static IForceUnit KilogramMeterPerSquareSecond { get; } = new ForceUnit((Mass.Unit.Gram, UnitPrefix.Kilo), (Length.Unit.Meter, null), (Duration.Unit.Second, null));
        
            /// <summary>
            /// Creates a custom force unit based on given factor
            /// </summary>
            /// <param name="factor">factor to use for force unit</param>
            /// <param name="abbreviation">Abbreviation for custom force unit</param>
            /// <returns>A valid custom force unit</returns>
            public static IForceUnit CreateUnit(double factor, string abbreviation = "<custom>") => new ForceUnit(factor, abbreviation);

            /// <summary>
            /// Creates a custom force unit based on combined elements (m*l/t²)
            /// </summary>
            /// <param name="massUnit">Mass unit for custom force unit</param>
            /// <param name="lengthUnit">length unit for custom force unit</param>
            /// <param name="durationUnit">Square duration unit for custom force unit</param>
            /// <param name="abbreviation">Optional abbreviation (overwrite) for custom force unit</param>
            /// <returns>A valid custom force unit</returns>
            public static IForceUnit CreateUnit((IMassUnit, IPrefix) massUnit, (ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null)
            {
                return new ForceUnit(massUnit, lengthUnit, durationUnit, abbreviation);
            }
        }

        #endregion
    }
}