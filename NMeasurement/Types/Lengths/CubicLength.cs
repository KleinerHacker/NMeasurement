using NMeasurement.Types.Lengths.Attributes;
using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Lengths
{
    /// <summary>
    /// Length implementation for dimension 3 (cubic length)
    /// </summary>
    [Length(typeof(Unit), "Meter",3)]
    public sealed class CubicLength : LengthBase<ICubicLengthUnit, CubicLength>
    {
        #region Static Init

        public static CubicLength FromLitre(double value, IPrefix prefix = null) => new CubicLength(value, Unit.Cubic.Litre, prefix);
        
        public static CubicLength FromBarrel(double value, IPrefix prefix = null) => new CubicLength(value, Unit.Cubic.Barrel, prefix);
        public static CubicLength FromCup(double value, IPrefix prefix = null) => new CubicLength(value, Unit.Cubic.Cup, prefix);

        #endregion

        #region Properties

        public double GetLitre(IPrefix prefix) => GetValue(Unit.Cubic.Litre, prefix);
        public double Litre => GetValue(Unit.Cubic.Litre);
        
        public double GetBarrel(IPrefix prefix) => GetValue(Unit.Cubic.Barrel, prefix);
        public double Barrel => GetValue(Unit.Cubic.Barrel);
        public double GetCup(IPrefix prefix) => GetValue(Unit.Cubic.Cup, prefix);
        public double Cup => GetValue(Unit.Cubic.Cup);

        #endregion
        
        private CubicLength(double rawValue) : base(rawValue)
        {
        }

        public CubicLength(double value, ICubicLengthUnit unit, IPrefix prefix = null) : base(value, unit, prefix)
        {
        }

        #region Calculation Operators

        public static CubicLength operator +(CubicLength m1, CubicLength m2) => new CubicLength(m1.Value + m2.Value);
        public static CubicLength operator -(CubicLength m1, CubicLength m2) => new CubicLength(m1.Value - m2.Value);
        
        public static SquareLength operator /(CubicLength m1, Length m2) => SquareLength.FromMeter(m1.Meter / m2.Meter);
        public static Length operator /(CubicLength m1, SquareLength m2) => Length.FromMeter(m1.Meter / m2.Meter);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(CubicLength m1, CubicLength m2) => object.Equals(m1, m2);
        public static bool operator !=(CubicLength m1, CubicLength m2) => !object.Equals(m1, m2);
        
        public static bool operator >(CubicLength m1, CubicLength m2) => m1.Value > m2.Value;
        public static bool operator >=(CubicLength m1, CubicLength m2) => m1.Value >= m2.Value;
        public static bool operator <(CubicLength m1, CubicLength m2) => m1.Value < m2.Value;
        public static bool operator <=(CubicLength m1, CubicLength m2) => m1.Value <= m2.Value;

        #endregion
    }
}