using System;
using System.Diagnostics.CodeAnalysis;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Energies.Attributes;
using NMeasurement.Types.Energies.Interfaces;
using NMeasurement.Types.Energies.Internals;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Energies
{
    /// <summary>
    /// Base implementation for all energy types.
    /// </summary>
    /// <typeparam name="T">Type of acceptable energy units</typeparam>
    /// <typeparam name="TE">Type of concrete energy implementation itself</typeparam>
    public abstract class EnergyBase<T, TE> : MeasurementBase<T, EnergyAttribute, TE, IPrefix>, IEnergy<T> where T : IEnergyUnitBase where TE : EnergyBase<T, TE>
    {
        #region Static Init

        public static TE FromJoule(double value, IPrefix prefix = null) => Create(value, (T) Unit.Joule, prefix);
        public static TE FromKilogramSquareMeterPerSquareSecond(double value) => Create(value, (T) Unit.KilogramSquareMeterPerSquareSecond);

        #endregion

        #region Properties

        public double GetJoule(IPrefix prefix) => GetValue((T) Unit.Joule, prefix);
        public double Joule => GetValue((T) Unit.Joule);
        public double KilogramSquareMeterPerSquareSecond => GetValue((T) Unit.KilogramSquareMeterPerSquareSecond);

        #endregion
        
        protected internal EnergyBase(double value) : base(value)
        {
        }

        protected internal EnergyBase(double value, T unit, IPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Abstact Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(rawValue);

        public override string ToString(T unit, IPrefix prefix) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Known energy units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            /// <summary>
            /// <b>Base Unit</b>
            /// </summary>
            public static IEnergyUnit Joule { get; } = new EnergyUnit(1000d, "J");
            /// <summary>
            /// = 1
            /// </summary>
            public static IEnergyUnit KilogramSquareMeterPerSquareSecond { get; } = new EnergyUnit((Mass.Unit.Gram, UnitPrefix.Kilo), (Length.Unit.Meter, null), (Duration.Unit.Second, null));
        
            /// <summary>
            /// Creates a custom energy unit based on given factor.
            /// </summary>
            /// <param name="factor">Factor to use for custom energy unit, based on gram*square-meter/square-second</param>
            /// <param name="abbreviation">Abbreviation for custom energy unit</param>
            /// <returns>A valid custom energy unit</returns>
            public static IEnergyUnit CreateUnit(double factor, string abbreviation = "<custom>") => new EnergyUnit(factor, abbreviation);
            
            /// <summary>
            /// Creates a custom energy unit based on given factor.
            /// </summary>
            /// <param name="massUnit">Mass unit to use for custom energy unit</param>
            /// <param name="lengthUnit">Square length unit to use for custom energy unit</param>
            /// <param name="durationUnit">Square duration unit to use for custom energy unit</param>
            /// <param name="abbreviation">Abbreviation for custom energy unit</param>
            /// <returns>A valid custom energy unit</returns>
            public static IEnergyUnit CreateUnit((IMassUnit, IPrefix) massUnit, (ISquareLengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null)
            {
                return new EnergyUnit(massUnit, lengthUnit, durationUnit, abbreviation);
            }
        }

        #endregion
    }
}