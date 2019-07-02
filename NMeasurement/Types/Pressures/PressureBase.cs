using System.Diagnostics.CodeAnalysis;
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
            public static IPressureUnit Pascal { get; } = new PressureUnit(1d, "pa");
            /// <summary>
            /// = 0.00001
            /// </summary>
            public static IPressureUnit Bar { get; } = new PressureUnit(100_000d, "bar");

            /// <summary>
            /// = 1
            /// </summary>
            public static IPressureUnit KilogramPerMeterSquareSecond { get; } = new PressureUnit(1d, "kg/m s²");
            /// <summary>
            /// = 1
            /// </summary>
            public static IPressureUnit NewtonPerSquareMeter { get; } = new PressureUnit(1d, "N/m²");
            
            /// <summary>
            /// Create a custom pressure unit based on given factor
            /// </summary>
            /// <param name="factor">Factor for custom pressure unit</param>
            /// <param name="abbreviation">Abbreviation for custom pressure unit</param>
            /// <returns>A valid custom pressure unit</returns>
            public static IPressureUnit CreateUnit(double factor, string abbreviation = "<custom>") => new PressureUnit(factor, abbreviation);
        }

        #endregion
    }
}