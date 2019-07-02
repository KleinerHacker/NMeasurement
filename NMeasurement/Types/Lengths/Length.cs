using System;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Lengths.Attributes;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Velocity;

namespace NMeasurement.Types.Lengths
{
    /// <summary>
    /// Length implementation for dimension 1 (simple)
    /// </summary>
    [Length(typeof(Unit), "Meter", 1)]
    public sealed class Length : LengthBase<ILengthUnit, Length>
    {
        private Length(double rawValue) : base(rawValue)
        {
        }

        public Length(double value, ILengthUnit unit, IPrefix prefix = null) : base(value, unit, prefix)
        {
        }

        #region Calculation Operators

        public static Length operator +(Length m1, Length m2) => new Length(m1.Value + m2.Value);
        public static Length operator -(Length m1, Length m2) => new Length(m1.Value - m2.Value);
        
        public static SquareLength operator *(Length m1, Length m2) => SquareLength.FromMeter(m1.Meter * m2.Meter);

        #endregion

        #region Boolean Operators

        public static bool operator ==(Length m1, Length m2) => object.Equals(m1, m2);
        public static bool operator !=(Length m1, Length m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Length m1, Length m2) => m1.Value > m2.Value;
        public static bool operator >=(Length m1, Length m2) => m1.Value >= m2.Value;
        public static bool operator <(Length m1, Length m2) => m1.Value < m2.Value;
        public static bool operator <=(Length m1, Length m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Velocity.Velocity operator /(Length m, Duration d) => Velocity.Velocity.FromMeterPerSecond(m.Meter / d.Second);
        public static Velocity.Velocity operator /(Duration d, Length m) => Velocity.Velocity.FromMeterPerSecond(m.Meter / d.Second);

        #endregion
    }
}