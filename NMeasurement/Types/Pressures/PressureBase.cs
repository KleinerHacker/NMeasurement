using System.Diagnostics.CodeAnalysis;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Forces;
using NMeasurement.Types.Forces.Interfaces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses;
using NMeasurement.Types.Masses.Interfaces;
using NMeasurement.Types.Pressures.Attributes;
using NMeasurement.Types.Pressures.Interfaces;
using NMeasurement.Types.Pressures.Internals;

namespace NMeasurement.Types.Pressures
{
    public abstract class PressureBase<T, TP> : MeasurementBase<T, PressureAttribute, TP, IPrefix>, IPressure<T> where T : IPressureUnitBase where TP : PressureBase<T, TP>
    {
        #region Static Init

        public static TP FromPascal(double value, IPrefix prefix = null) => Create(value, (T) Unit.Pascal, prefix);
        public static TP FromBar(double value, IPrefix prefix = null) => Create(value, (T) Unit.Bar, prefix);
        
        public static TP FromKilogramPerMeterSquareSecond(double value) => Create(value, (T) Unit.KilogramPerMeterSquareSecond);
        public static TP FromNewtonPerSquareMeter(double value) => Create(value, (T) Unit.NewtonPerSquareMeter);

        #endregion

        #region Properties

        public double GetPascal(IPrefix prefix) => GetValue((T) Unit.Pascal, prefix);
        public double Pascal => GetValue((T) Unit.Pascal);
        
        public double GetBar(IPrefix prefix) => GetValue((T) Unit.Bar, prefix);
        public double Bar => GetValue((T) Unit.Bar);
        
        public double KilogramPerMeterSquareSecond => GetValue((T) Unit.KilogramPerMeterSquareSecond);
        public double NewtonPerSquareMeter => GetValue((T) Unit.NewtonPerSquareMeter);

        #endregion
        
        protected internal PressureBase(double value) : base(value)
        {
        }

        protected internal PressureBase(double value, T unit, IPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(Value);

        public override string ToString(T unit, IPrefix prefix) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known pressure units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            /// <summary>
            /// <b>Base unit</b>
            /// </summary>
            public static IPressureUnit Pascal { get; } = new PressureUnit(1_000d, "pa");
            /// <summary>
            /// = 100.000
            /// </summary>
            public static IPressureUnit Bar { get; } = new PressureUnit(100_000_000d, "bar");

            /// <summary>
            /// = 1
            /// </summary>
            public static IPressureUnit KilogramPerMeterSquareSecond { get; } = new PressureUnit((Mass.Unit.Gram, UnitPrefix.Kilo), (Length.Unit.Meter, null), (Duration.Unit.Second, null));
            /// <summary>
            /// = 1
            /// </summary>
            public static IPressureUnit NewtonPerSquareMeter { get; } = new PressureUnit((Force.Unit.Newton, null), (Length.Unit.Meter, null));
            
            /// <summary>
            /// Create a custom pressure unit based on given factor
            /// </summary>
            /// <param name="factor">Factor for custom pressure unit</param>
            /// <param name="abbreviation">Abbreviation for custom pressure unit</param>
            /// <returns>A valid custom pressure unit</returns>
            public static IPressureUnit CreateUnit(double factor, string abbreviation = "<custom>") => new PressureUnit(factor, abbreviation);

            /// <summary>
            /// Create a custom pressure unit based on given elements (m/(l*t²))
            /// </summary>
            /// <param name="massUnit">Mass unit for custom pressure unit</param>
            /// <param name="lengthUnit">Length unit for custom pressure unit</param>
            /// <param name="durationUnit">Square duration unit for custom pressure unit</param>
            /// <param name="abbreviation">Optional abbreviation (overwrite) fir custom pressure unit)</param>
            /// <returns>A valid custom pressure unit</returns>
            public static IPressureUnit CreateUnit((IMassUnit, IPrefix) massUnit, (ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null)
            {
                return new PressureUnit(massUnit, lengthUnit, durationUnit, abbreviation);
            }

            /// <summary>
            /// Create a custom pressure unit based on given elements (f/l²)
            /// </summary>
            /// <param name="forceUnit">Force unit for custom pressure unit</param>
            /// <param name="lengthUnit">Square length unit for custom pressure unit</param>
            /// <param name="abbreviation">Optional abbreviation (overwrite) fir custom pressure unit)</param>
            /// <returns>A valid custom pressure unit</returns>
            public static IPressureUnit CreateUnit((IForceUnit, IPrefix) forceUnit, (ISquareLengthUnit, IPrefix) lengthUnit, string abbreviation = null)
            {
                return new PressureUnit(forceUnit, lengthUnit, abbreviation);
            }
        }

        #endregion
    }
}