using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NMeasurement.Types.Densities.Attributes;
using NMeasurement.Types.Densities.Interfaces;
using NMeasurement.Types.Densities.Internals;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Densities
{
    /// <summary>
    /// Density base implementation. 
    /// </summary>
    /// <typeparam name="T">Type of acceptable density units</typeparam>
    /// <typeparam name="TD">Type of concrete density implementation itself</typeparam>
    public abstract class DensityBase<T, TD> : MeasurementBase<T, DensityAttribute, TD>, IDensity<T> where T : IDensityUnitBase where TD : DensityBase<T, TD>
    {
        #region Static Init

        public static TD FromGramPerCubicMeter(double value) => Create(value, (T) Unit.GramPerCubicMeter);
        public static TD FromGramPerLitre(double value) => Create(value, (T) Unit.GramPerLitre);

        public static TD FromAtomMassPerCubicMeter(double value) => Create(value, (T) Unit.AtomMassPerCubicMeter);

        #endregion

        #region Properties

        public double GramPerCubicMeter => GetValue((T) Unit.GramPerCubicMeter);
        public double GramPerLitre => GetValue((T) Unit.GramPerLitre);

        public double AtomMassPerCubicMeter => GetValue((T) Unit.AtomMassPerCubicMeter);

        #endregion

        protected internal DensityBase(double value) : base(value)
        {
        }

        protected internal DensityBase(double value, T unit) : base(value, unit)
        {
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(rawValue);

        public override string ToString(T unit) => $"{GetValue(unit)} {unit.TotalAbbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known density units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            /// <summary>
            /// <b>Base Unit</b>
            /// </summary>
            public static IDensityUnit GramPerCubicMeter { get; } = new DensityUnit((Mass.Unit.Gram, null), (Length.Unit.Meter, null));

            /// <summary>
            /// = 1000
            /// </summary>
            public static IDensityUnit GramPerLitre { get; } = new DensityUnit((Mass.Unit.Gram, null), (Length.Unit.Cubic.Litre, null));

            /// <summary>
            /// = 1.6605389209999996e-24
            /// </summary>
            public static IDensityUnit AtomMassPerCubicMeter { get; } = new DensityUnit((Mass.Unit.AtomMass, null), (Length.Unit.Meter, null));

            /// <summary>
            /// Creates a custom density unit
            /// </summary>
            /// <param name="massUnit">Mass unit (numerator) for custom density unit</param>
            /// <param name="lengthUnit">Cubic length unit (denominator) for custom density unit</param>
            /// <param name="abbreviation">Abbreviation for custom density unit</param>
            /// <returns>A valid custom density unit</returns>
            public static IDensityUnit CreateUnit((IMassUnit, IPrefix) massUnit, (ICubicLengthUnit, IPrefix) lengthUnit, string abbreviation = "<custom>") => new DensityUnit(massUnit, lengthUnit, abbreviation);
        }

        #endregion
    }
}