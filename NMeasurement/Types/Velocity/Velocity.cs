using System;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Expeditions;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Velocity.Attributes;
using NMeasurement.Types.Velocity.Interfaces;

namespace NMeasurement.Types.Velocity
{
    /// <summary>
    /// Velocity implementation
    /// </summary>
    [Velocity(typeof(Unit), "KilometerPerHour")]
    public sealed class Velocity : VelocityBase<IVelocityUnit, Velocity>
    {
        private Velocity(double rawValue) : base(rawValue)
        {
        }

        public Velocity(double value, IVelocityUnit unit) : base(value, unit)
        {
        }
        
        #region Calculation Operators

        public static Velocity operator +(Velocity v1, Velocity v2) => new Velocity(v1.Value + v2.Value);
        public static Velocity operator -(Velocity v1, Velocity v2) => new Velocity(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(Velocity m1, Velocity m2) => object.Equals(m1, m2);
        public static bool operator !=(Velocity m1, Velocity m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Velocity m1, Velocity m2) => m1.Value > m2.Value;
        public static bool operator >=(Velocity m1, Velocity m2) => m1.Value >= m2.Value;
        public static bool operator <(Velocity m1, Velocity m2) => m1.Value < m2.Value;
        public static bool operator <=(Velocity m1, Velocity m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Length operator *(Velocity s, Duration t) => Length.FromMeter(s.KilometerPerHour * t.Hour, UnitPrefix.Kilo);
        public static Length operator *(Duration t, Velocity s) => Length.FromMeter(s.KilometerPerHour * t.Hour, UnitPrefix.Kilo);
        
        /// <summary>
        /// Calculates the time span for the given velocity and given distance covered length.<br/>
        /// <b>Calculates 1 / (v / l)</b>
        /// </summary>
        /// <param name="s">Velocity value</param>
        /// <param name="m">Length value</param>
        /// <returns>Time span</returns>
        public static Duration operator /(Velocity s, Length m) => Duration.FromHour(1 / (s.KilometerPerHour / m.GetMeter(UnitPrefix.Kilo)));
        public static Expedition operator /(Velocity s, Duration t) => Expedition.FromMetersPerSquareSecond(s.MeterPerSecond / t.Second);

        #endregion
    }
}