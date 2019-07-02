using NMeasurement.Types.Densities;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Masses.Attributes;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Masses
{
    [Mass(typeof(Unit), "Gram")]
    public sealed class Mass : MassBase<IMassUnit, Mass>
    {
        private Mass(double rawValue) : base(rawValue)
        {
        }

        public Mass(double value, IMassUnit unit, IPrefix prefix = null) : base(value, unit, prefix)
        {
        }
        
        #region Calculation Operators

        public static Mass operator +(Mass v1, Mass v2) => new Mass(v1.Value + v2.Value);
        public static Mass operator -(Mass v1, Mass v2) => new Mass(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(Mass m1, Mass m2) => object.Equals(m1, m2);
        public static bool operator !=(Mass m1, Mass m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Mass m1, Mass m2) => m1.Value > m2.Value;
        public static bool operator >=(Mass m1, Mass m2) => m1.Value >= m2.Value;
        public static bool operator <(Mass m1, Mass m2) => m1.Value < m2.Value;
        public static bool operator <=(Mass m1, Mass m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Density operator /(Mass w, CubicLength m) => Density.FromGramPerCubicMeter(w.Gram / m.Meter);

        #endregion
    }
}