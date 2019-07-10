using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using NMeasurement.Types.Lengths.Attributes;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Lengths.Internals;

namespace NMeasurement.Types.Lengths
{
    /// <summary>
    /// Abstract base class for all length types
    /// </summary>
    /// <typeparam name="T">Type of <see cref="ILengthUnitBase"/> to accept as unit</typeparam>
    /// <typeparam name="TL">Type of concrete implementation of itself</typeparam>
    public abstract class LengthBase<T, TL> : MeasurementBase<T, LengthAttribute, TL, IPrefix>, ILength<T> where T : ILengthUnitBase where TL : LengthBase<T, TL>
    {
        #region Static Init

        public static TL FromMeter(double value, IPrefix prefix = null) => Create(value, (T) Unit.Meter, prefix);
        
        public static TL FromSeaMile(double value, IPrefix prefix = null) => Create(value, (T) Unit.SeaMile, prefix);
        public static TL FromMile(double value, IPrefix prefix = null) => Create(value, (T) Unit.Mile, prefix);
        public static TL FromFeet(double value, IPrefix prefix = null) => Create(value, (T) Unit.Feet, prefix);
        public static TL FromYard(double value, IPrefix prefix = null) => Create(value, (T) Unit.Yard, prefix);

        #endregion

        #region Properties

        public double GetMeter(IPrefix prefix) => GetValue((T) Unit.Meter, prefix);
        public double Meter => GetValue((T) Unit.Meter);
        
        public double GetSeaMile(IPrefix prefix) => GetValue((T) Unit.SeaMile, prefix);
        public double SeaMile => GetValue((T) Unit.SeaMile);
        public double GetMile(IPrefix prefix) => GetValue((T) Unit.Mile, prefix);
        public double Mile => GetValue((T) Unit.Mile);
        public double GetFeet(IPrefix prefix) => GetValue((T) Unit.Feet, prefix);
        public double Feet => GetValue((T) Unit.Feet);
        public double GetYard(IPrefix prefix) => GetValue((T) Unit.Yard, prefix);
        public double Yard => GetValue((T) Unit.Yard);

        #endregion
        
        private uint _dimension;

        protected internal LengthBase(double value) : base(value)
        {
        }

        protected internal LengthBase(double value, T unit, IPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value, _dimension);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(rawValue, _dimension);

        //Multi dimension universal unit always fits, all other must be calculated by dimension difference
        protected override double RecalculateFactor(double factor, T unit) => Math.Pow(factor, unit.DimensionBase == 0 ? _dimension : unit.DimensionBase - _dimension + 1); //update factor by dimension

        protected override void HandleAttribute(LengthAttribute attribute) => _dimension = attribute.Dimension;

        public override string ToString(T unit, IPrefix prefix) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.GetAbbreviation(_dimension)}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known length units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            public static ILengthUnitUniversal Meter { get; } = new LengthUnitUniversal(1d, "m");

            public static ILengthUnitUniversal Mile { get; } = new LengthUnitUniversal(1609.34d, "mi");
            public static ILengthUnitUniversal SeaMile { get; } = new LengthUnitUniversal(1852d, "mi");
            public static ILengthUnitUniversal Feet { get; } = new LengthUnitUniversal(0.3048d, "ft");
            public static ILengthUnitUniversal Yard { get; } = new LengthUnitUniversal(0.9144d, "yd");

            public static ILengthUnitUniversal CreateUniversalUnit(double factor, string abbreviation = "<custom>") => new LengthUnitUniversal(factor, abbreviation);

            public static class Simple
            {
                public static ILengthUnit CreateUnit(double factor, string abbreviation = "<custom>") => new LengthUnit(factor, abbreviation);
            }

            public static class Square
            {
                public static ISquareLengthUnit Hectare { get; } = new SquareLengthUnit(10000d, "ha");
                public static ISquareLengthUnit Ar { get; } = new SquareLengthUnit(100d, "ar");

                public static ISquareLengthUnit CreateUnit(double factor, string abbreviation = "<custom>") => new SquareLengthUnit(factor, abbreviation);
            }

            public static class Cubic
            {
                public static ICubicLengthUnit Litre { get; } = new CubicLengthUnit(0.001d, "l");

                public static ICubicLengthUnit Barrel { get; } = new CubicLengthUnit(0.158987294928d, "bbl");
                public static ICubicLengthUnit Cup { get; } = new CubicLengthUnit(0.00025d, "cp");

                public static ICubicLengthUnit CreateUnit(double factor, string abbreviation = "<custom>") => new CubicLengthUnit(factor, abbreviation);
            }
        }

        #endregion
    }
}