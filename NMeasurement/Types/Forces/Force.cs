using NMeasurement.Types.Energies;
using NMeasurement.Types.Expeditions;
using NMeasurement.Types.Forces.Attributes;
using NMeasurement.Types.Forces.Interfaces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Masses;
using NMeasurement.Types.Pressures;

namespace NMeasurement.Types.Forces
{
    [Force(typeof(Unit), "Newton")]
    public sealed class Force : ForceBase<IForceUnit, Force>
    {
        private Force(double value) : base(value)
        {
        }

        public Force(double value, IForceUnit unit, IPrefix prefix = null) : base(value, unit, prefix)
        {
        }
        
        #region Calculation Operators

        public static Force operator +(Force v1, Force v2) => new Force(v1.Value + v2.Value);
        public static Force operator -(Force v1, Force v2) => new Force(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(Force m1, Force m2) => object.Equals(m1, m2);
        public static bool operator !=(Force m1, Force m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Force m1, Force m2) => m1.Value > m2.Value;
        public static bool operator >=(Force m1, Force m2) => m1.Value >= m2.Value;
        public static bool operator <(Force m1, Force m2) => m1.Value < m2.Value;
        public static bool operator <=(Force m1, Force m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Expedition operator /(Force f, Mass w) => Expedition.FromMetersPerSquareSecond(f.Newton / w.GetGram(UnitPrefix.Kilo));
        
        public static Energy operator *(Force f, Length m) => Energy.FromJoule(f.Newton * m.Meter);
        public static Energy operator *(Length m, Force f) => Energy.FromJoule(f.Newton * m.Meter);

        public static Pressure operator /(Force f, SquareLength l) => Pressure.FromPascal(f.Newton / l.Meter);

        #endregion
    }
}