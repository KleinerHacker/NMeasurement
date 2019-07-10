using System;
using System.Diagnostics.CodeAnalysis;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Expeditions.Attributes;
using NMeasurement.Types.Expeditions.Interfaces;
using NMeasurement.Types.Expeditions.Internals;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Expeditions
{
    /// <summary>
    /// Base implementation for all expedition types
    /// </summary>
    /// <typeparam name="T">Type of acceptable expedition units</typeparam>
    /// <typeparam name="TE">Type of concrete expedition itself</typeparam>
    public abstract class ExpeditionBase<T, TE> : MeasurementBase<T, ExpeditionAttribute, TE, IPrefix>, IExpedition<T> where T : IExpeditionUnitBase where TE : ExpeditionBase<T, TE>
    {
        #region Static Init

        public static TE FromGravity(double value, IPrefix prefix = null) => Create(value, (T) Unit.Gravity, prefix);
        public static TE FromMetersPerSquareSecond(double value) => Create(value, (T) Unit.MetersPerSquareSecond);

        #endregion

        #region Properties

        public double GetGravity(IPrefix prefix) => GetValue((T) Unit.Gravity, prefix);
        public double Gravity => GetValue((T) Unit.Gravity);
        public double MetersPerSquareSecond => GetValue((T) Unit.MetersPerSquareSecond);

        #endregion
        
        protected internal ExpeditionBase(double value) : base(value)
        {
        }

        protected internal ExpeditionBase(double value, T unit, IPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(Value);

        public override string ToString(T unit, IPrefix prefix) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known expedition units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            /// <summary>
            /// Earth gravity - <b>Base unit</b>
            /// </summary>
            public static IExpeditionUnit Gravity { get; } = new ExpeditionUnit(9.80665d, "g");
            /// <summary>
            /// = 9.80665 g
            /// </summary>
            public static IExpeditionUnit MetersPerSquareSecond { get; } = new ExpeditionUnit((Length.Unit.Meter, null), (Duration.Unit.Second, null));
        
            /// <summary>
            /// Create a custom expedition unit
            /// </summary>
            /// <param name="factor">Factor of custom expedition unit to use, based on meter/second²</param>
            /// <param name="abbreviation">Abbreviation for custom expedition unit</param>
            /// <returns>A valid custom expedition unit</returns>
            public static IExpeditionUnit CreateUnit(double factor, string abbreviation = "<custom>") => new ExpeditionUnit(factor, abbreviation);
            
            /// <summary>
            /// Create a custom expedition unit
            /// </summary>
            /// <param name="lengthUnit">Length unit of custom expedition unit to use</param>
            /// <param name="durationUnit">Square duration unit of custom expedition unit to use</param>
            /// <param name="abbreviation">Abbreviation for custom expedition unit</param>
            /// <returns>A valid custom expedition unit</returns>
            public static IExpeditionUnit CreateUnit((ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = "<custom>")
            {
                return new ExpeditionUnit(lengthUnit, durationUnit, abbreviation);
            }
        }

        #endregion
    }
}