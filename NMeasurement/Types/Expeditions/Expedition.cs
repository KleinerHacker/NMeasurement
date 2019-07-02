using System;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Expeditions.Attributes;
using NMeasurement.Types.Expeditions.Interfaces;
using NMeasurement.Types.Forces;
using NMeasurement.Types.Masses;
using NMeasurement.Types.Velocity;

namespace NMeasurement.Types.Expeditions
{
    /// <summary>
    /// Implementation of expedition
    /// </summary>
    [Expedition(typeof(Unit), "Gravity")]
    public sealed class Expedition : ExpeditionBase<IExpeditionUnit, Expedition>
    {
        private Expedition(double value) : base(value)
        {
        }

        public Expedition(double value, IExpeditionUnit unit, IPrefix prefix = null) : base(value, unit, prefix)
        {
        }

        #region Calculation Operators

        public static Expedition operator +(Expedition v1, Expedition v2) => new Expedition(v1.Value + v2.Value);
        public static Expedition operator -(Expedition v1, Expedition v2) => new Expedition(v1.Value - v2.Value);

        #endregion

        #region Boolean Operators

        public static bool operator ==(Expedition m1, Expedition m2) => object.Equals(m1, m2);
        public static bool operator !=(Expedition m1, Expedition m2) => !object.Equals(m1, m2);

        public static bool operator >(Expedition m1, Expedition m2) => m1.Value > m2.Value;
        public static bool operator >=(Expedition m1, Expedition m2) => m1.Value >= m2.Value;
        public static bool operator <(Expedition m1, Expedition m2) => m1.Value < m2.Value;
        public static bool operator <=(Expedition m1, Expedition m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Velocity.Velocity operator *(Expedition e, Duration t) => Velocity.Velocity.FromMeterPerSecond(e.MetersPerSquareSecond * t.Second);
        public static Velocity.Velocity operator *(Duration t, Expedition e) => Velocity.Velocity.FromMeterPerSecond(e.MetersPerSquareSecond * t.Second);

        public static Force operator *(Expedition e, Mass w) => Force.FromKilogramMeterPerSquareSecond(w.GetGram(UnitPrefix.Kilo) * e.MetersPerSquareSecond);
        public static Force operator *(Mass w, Expedition e) => Force.FromKilogramMeterPerSquareSecond(w.GetGram(UnitPrefix.Kilo) * e.MetersPerSquareSecond);

        #endregion
    }
}