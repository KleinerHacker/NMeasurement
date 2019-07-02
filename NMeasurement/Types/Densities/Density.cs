using NMeasurement.Types.Densities.Attributes;
using NMeasurement.Types.Densities.Interfaces;
using NMeasurement.Types.Densities.Internals;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Masses;

namespace NMeasurement.Types.Densities
{
    [Density(typeof(Unit), "GramPerCubicMeter")]
    public sealed class Density : DensityBase<IDensityUnit, Density>
    {
        private Density(double value) : base(value)
        {
        }

        public Density(double value, IDensityUnit unit) : base(value, unit)
        {
        }
        
        #region Calculation Operators

        public static Density operator +(Density v1, Density v2) => new Density(v1.Value + v2.Value);
        public static Density operator -(Density v1, Density v2) => new Density(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(Density m1, Density m2) => object.Equals(m1, m2);
        public static bool operator !=(Density m1, Density m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Density m1, Density m2) => m1.Value > m2.Value;
        public static bool operator >=(Density m1, Density m2) => m1.Value >= m2.Value;
        public static bool operator <(Density m1, Density m2) => m1.Value < m2.Value;
        public static bool operator <=(Density m1, Density m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Mass operator *(Density d, CubicLength m) => Mass.FromGram(d.GramPerCubicMeter * m.Meter);
        public static Mass operator *(CubicLength m, Density d) => Mass.FromGram(d.GramPerCubicMeter * m.Meter);
        /// <summary>
        /// Calculates the cubic length 1 / (d / w)
        /// </summary>
        /// <param name="d"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public static CubicLength operator /(Density d, Mass w) => CubicLength.FromMeter(1 / (d.GramPerCubicMeter / w.Gram));

        #endregion
    }
}