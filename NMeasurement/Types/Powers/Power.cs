using System;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Energies;
using NMeasurement.Types.Powers.Attributes;
using NMeasurement.Types.Powers.Interfaces;

namespace NMeasurement.Types.Powers
{
    [Power(typeof(Unit), "Watt")]
    public sealed class Power : PowerBase<IPowerUnit, Power>
    {
        private Power(double value) : base(value)
        {
        }

        public Power(double value, IPowerUnit unit, IPrefix prefix = null) : base(value, unit, prefix)
        {
        }
        
        #region Calculation Operators

        public static Power operator +(Power v1, Power v2) => new Power(v1.Value + v2.Value);
        public static Power operator -(Power v1, Power v2) => new Power(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(Power m1, Power m2) => object.Equals(m1, m2);
        public static bool operator !=(Power m1, Power m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Power m1, Power m2) => m1.Value > m2.Value;
        public static bool operator >=(Power m1, Power m2) => m1.Value >= m2.Value;
        public static bool operator <(Power m1, Power m2) => m1.Value < m2.Value;
        public static bool operator <=(Power m1, Power m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Energy operator *(Power p, Duration t) => Energy.FromKilogramSquareMeterPerSquareSecond(p.KilogramSquareMeterPerCubicSecond * t.Second);
        public static Energy operator *(Duration t, Power p) => Energy.FromKilogramSquareMeterPerSquareSecond(p.KilogramSquareMeterPerCubicSecond * t.Second);

        #endregion
    }
}