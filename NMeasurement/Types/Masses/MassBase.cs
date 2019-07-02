using System;
using System.Diagnostics.CodeAnalysis;
using NMeasurement.Types.Masses.Attributes;
using NMeasurement.Types.Masses.Interfaces;
using NMeasurement.Types.Masses.Internals;

namespace NMeasurement.Types.Masses
{
    /// <summary>
    /// Base class for all mass types
    /// </summary>
    /// <typeparam name="T">Type of acceptable mass units</typeparam>
    /// <typeparam name="TM">Type of concrete implementation of itself</typeparam>
    public abstract class MassBase<T, TM> : MeasurementBase<T, MassAttribute, TM, IPrefix>, IMass<T> where T : IMassUnitBase where TM : MassBase<T, TM>
    {
        #region Static Init

        public static TM FromGram(double value, IPrefix prefix = null) => Create(value, (T) Unit.Gram, prefix);
        public static TM FromCentner(double value, IPrefix prefix = null) => Create(value, (T) Unit.Centner, prefix);
        public static TM FromMeterCentner(double value, IPrefix prefix = null) => Create(value, (T) Unit.MeterCentner, prefix);
        public static TM FromTonne(double value, IPrefix prefix = null) => Create(value, (T) Unit.Tonne, prefix);
        
        public static TM FromEnglishPound(double value, IPrefix prefix = null) => Create(value, (T) Unit.EnglishPound, prefix);
        public static TM FromPound(double value, IPrefix prefix = null) => Create(value, (T) Unit.Pound, prefix);
        public static TM FromOunce(double value, IPrefix prefix = null) => Create(value, (T) Unit.Ounce, prefix);
        
        public static TM FromAtomMass(double value, IPrefix prefix = null) => Create(value, (T) Unit.AtomMass, prefix);

        #endregion

        #region Properties
        public double GetGram(IPrefix prefix) => GetValue((T) Unit.Gram, prefix);
        public double Gram => GetValue((T) Unit.Gram);
        public double GetCentner(IPrefix prefix) => GetValue((T) Unit.Centner, prefix);
        public double Centner => GetValue((T) Unit.Centner);
        public double GetMeterCentner(IPrefix prefix) => GetValue((T) Unit.MeterCentner, prefix);
        public double MeterCentner => GetValue((T) Unit.MeterCentner);
        public double GetTonne(IPrefix prefix) => GetValue((T) Unit.Tonne, prefix);
        public double Tonne => GetValue((T) Unit.Tonne);
        
        public double GetEnglishPound(IPrefix prefix) => GetValue((T) Unit.EnglishPound, prefix);
        public double EnglishPound => GetValue((T) Unit.EnglishPound);
        public double GetPound(IPrefix prefix) => GetValue((T) Unit.Pound, prefix);
        public double Pound => GetValue((T) Unit.Pound);
        public double GetOunce(IPrefix prefix) => GetValue((T) Unit.Ounce, prefix);
        public double Ounce => GetValue((T) Unit.Ounce);
        
        public double GetAtomMass(IPrefix prefix) => GetValue((T) Unit.AtomMass, prefix);
        public double AtomMass => GetValue((T) Unit.AtomMass);

        #endregion
        
        protected internal MassBase(double value) : base(value)
        {
        }

        protected internal MassBase(double value, T unit, IPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(Value);

        public override string ToString(T unit, IPrefix prefix) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known mass units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            /// <summary>
            /// <b>Base Unit</b>
            /// </summary>
            public static IMassUnit Gram { get; } = new MassUnit(1d, "g");

            /// <summary>
            /// = 453.592 gram
            /// </summary>
            public static IMassUnit EnglishPound { get; } = new MassUnit(453.592d, "pf");

            /// <summary>
            /// = 50 gram
            /// </summary>
            public static IMassUnit Pound { get; } = new MassUnit(50d, "pf");

            /// <summary>
            /// = 28.3495 gram
            /// </summary>
            public static IMassUnit Ounce { get; } = new MassUnit(28.3495d, "oc");

            /// <summary>
            /// = 50 kg
            /// </summary>
            public static IMassUnit Centner { get; } = new MassUnit(50_000d, "ct");

            /// <summary>
            /// = 100 kg
            /// </summary>
            public static IMassUnit MeterCentner { get; } = new MassUnit(100_000d, "ct");

            /// <summary>
            /// = 1000 kg
            /// </summary>
            public static IMassUnit Tonne { get; } = new MassUnit(1_000_000d, "t");

            /// <summary>
            /// = 1.6605389209999996e-24
            /// </summary>
            public static IMassUnit AtomMass { get; } = new MassUnit(1.6605389209999996e-24d, "u");

            /// <summary>
            /// Create a custom mass unit based on given factor.
            /// </summary>
            /// <param name="factor">Factor for calculation</param>
            /// <param name="abbreviation">Abbreviation for custom mass unit</param>
            /// <returns>A new valid mass unit</returns>
            public static IMassUnit CreateUnit(double factor, string abbreviation = "<custom>") => new MassUnit(factor, abbreviation);
        }

        #endregion
    }
}