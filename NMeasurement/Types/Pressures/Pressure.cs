using NMeasurement.Types.Forces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Pressures.Attributes;
using NMeasurement.Types.Pressures.Interfaces;

namespace NMeasurement.Types.Pressures
{
    [Pressure(typeof(Unit), "Pascal")]
    public sealed class Pressure : PressureBase<IPressureUnit, Pressure>
    {
        internal Pressure(double value) : base(value)
        {
        }

        public Pressure(double value, IPressureUnit unit, IPrefix prefix = null) : base(value, unit, prefix)
        {
        }
        
        #region Calculation Operators

        public static Pressure operator +(Pressure v1, Pressure v2) => new Pressure(v1.Value + v2.Value);
        public static Pressure operator -(Pressure v1, Pressure v2) => new Pressure(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(Pressure m1, Pressure m2) => object.Equals(m1, m2);
        public static bool operator !=(Pressure m1, Pressure m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Pressure m1, Pressure m2) => m1.Value > m2.Value;
        public static bool operator >=(Pressure m1, Pressure m2) => m1.Value >= m2.Value;
        public static bool operator <(Pressure m1, Pressure m2) => m1.Value < m2.Value;
        public static bool operator <=(Pressure m1, Pressure m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Force operator *(Pressure p, SquareLength l) => Force.FromNewton(p.NewtonPerSquareMeter * l.Meter);
        public static Force operator *(SquareLength l, Pressure p) => Force.FromNewton(p.NewtonPerSquareMeter * l.Meter);

        /// <summary>
        /// Calculates reciprocal: 1 / (p / f)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static SquareLength operator /(Pressure p, Force f) => SquareLength.FromMeter(1d / (p.NewtonPerSquareMeter / f.Newton));

        #endregion
    }
}