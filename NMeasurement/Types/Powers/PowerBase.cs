using System;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Energies;
using NMeasurement.Types.Energies.Interfaces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses;
using NMeasurement.Types.Masses.Interfaces;
using NMeasurement.Types.Powers.Attributes;
using NMeasurement.Types.Powers.Interfaces;
using NMeasurement.Types.Powers.Internals;

namespace NMeasurement.Types.Powers
{
    /// <summary>
    /// Base implementation for all power types.
    /// </summary>
    /// <typeparam name="T">Type of acceptable power units</typeparam>
    /// <typeparam name="TP">Type of concrete power implementation itself</typeparam>
    public abstract class PowerBase<T, TP> : MeasurementBase<T, PowerAttribute, TP, IPrefix>, IPower<T> where T : IPowerUnitBase where TP : PowerBase<T, TP>
    {
        #region Static Init

        public static TP FromWatt(double value, IPrefix prefix = null) => Create(value, (T) Unit.Watt, prefix);
        public static TP FromKilogramSquareMeterPerCubicSecond(double value) => Create(value, (T) Unit.KilogramSquareMeterPerCubicSecond);
        public static TP FromJoulePerSecond(double value) => Create(value, (T) Unit.JoulePerSecond);

        #endregion

        #region Properties

        public double GetWatt(IPrefix prefix) => GetValue((T) Unit.Watt, prefix);
        public double Watt => GetValue((T) Unit.Watt);
        public double KilogramSquareMeterPerCubicSecond => GetValue((T) Unit.KilogramSquareMeterPerCubicSecond);
        public double JoulePerSecond => GetValue((T) Unit.JoulePerSecond);

        #endregion

        protected internal PowerBase(double value) : base(value)
        {
        }

        protected internal PowerBase(double value, T unit, IPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(Value);

        public override string ToString(T unit, IPrefix prefix) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known power units
        /// </summary>
        public static class Unit
        {
            /// <summary>
            /// <b>Base Unit</b>
            /// </summary>
            public static IPowerUnit Watt { get; } = new PowerUnit(1000d, "W");

            /// <summary>
            /// = 1
            /// </summary>
            public static IPowerUnit KilogramSquareMeterPerCubicSecond { get; } = new PowerUnit((Mass.Unit.Gram, UnitPrefix.Kilo), (Length.Unit.Meter, null), (Duration.Unit.Second, null));

            /// <summary>
            /// = 1
            /// </summary>
            public static IPowerUnit JoulePerSecond { get; } = new PowerUnit((Energy.Unit.Joule, null), (Duration.Unit.Second, null));

            /// <summary>
            /// Creates a custom power unit based on given factor
            /// </summary>
            /// <param name="factor">Factor for custom power unit</param>
            /// <param name="abbreviation">Abbreviation for custom power unit</param>
            /// <returns>A valid custom power unit</returns>
            public static IPowerUnit CreateFactorUnit(double factor, string abbreviation = "<custom>") => new PowerUnit(factor, abbreviation);

            /// <summary>
            /// Creates a new combined unit from builder
            /// </summary>
            /// <returns></returns>
            public static PowerUnitCombinedBuilder CreateCombinedUnit() => new PowerUnitCombinedBuilder();
        }

        #endregion
    }
}