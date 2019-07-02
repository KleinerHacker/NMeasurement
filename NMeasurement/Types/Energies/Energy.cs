using System;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Energies.Attributes;
using NMeasurement.Types.Energies.Interfaces;
using NMeasurement.Types.Forces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Powers;

namespace NMeasurement.Types.Energies
{
    [Energy(typeof(Unit), "Joule")]
    public sealed class Energy : EnergyBase<IEnergyUnit, Energy>
    {
        private Energy(double value) : base(value)
        {
        }

        public Energy(double value, IEnergyUnit unit, IPrefix prefix) : base(value, unit, prefix)
        {
        }
        
        #region Calculation Operators

        public static Energy operator +(Energy v1, Energy v2) => new Energy(v1.Value + v2.Value);
        public static Energy operator -(Energy v1, Energy v2) => new Energy(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(Energy m1, Energy m2) => object.Equals(m1, m2);
        public static bool operator !=(Energy m1, Energy m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Energy m1, Energy m2) => m1.Value > m2.Value;
        public static bool operator >=(Energy m1, Energy m2) => m1.Value >= m2.Value;
        public static bool operator <(Energy m1, Energy m2) => m1.Value < m2.Value;
        public static bool operator <=(Energy m1, Energy m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Force operator /(Energy e, Length m) => Force.FromKilogramMeterPerSquareSecond(e.KilogramSquareMeterPerSquareSecond / m.Meter);
        public static Power operator /(Energy e, Duration t) => Power.FromKilogramSquareMeterPerCubicSecond(e.KilogramSquareMeterPerSquareSecond / t.Second);

        #endregion
    }
}