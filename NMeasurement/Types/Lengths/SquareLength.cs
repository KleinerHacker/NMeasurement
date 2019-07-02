using NMeasurement.Types.Lengths.Attributes;
using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Lengths
{
    /// <summary>
    /// Length implementation for dimension 2 (square length)
    /// </summary>
    [Length(typeof(Unit), "Meter",2)]
    public sealed class SquareLength : LengthBase<ISquareLengthUnit, SquareLength>
    {
        #region Static Init

        public static SquareLength FromHectar(double value, IPrefix prefix = null) => new SquareLength(value, Unit.Square.Hectare, prefix);
        public static SquareLength FromAr(double value, IPrefix prefix = null) => new SquareLength(value, Unit.Square.Ar, prefix);

        #endregion

        #region Properties

        public double GetHectar(IPrefix prefix) => GetValue(Unit.Square.Hectare, prefix);
        public double Hectar => GetValue(Unit.Square.Hectare);
        public double GetAr(IPrefix prefix) => GetValue(Unit.Square.Ar, prefix);
        public double Ar => GetValue(Unit.Square.Ar);

        #endregion
        
        private SquareLength(double value) : base(value)
        {
        }

        public SquareLength(double value, ISquareLengthUnit unit, IPrefix prefix = null) : base(value, unit, prefix)
        {
        }

        #region Calculation Operators

        public static SquareLength operator +(SquareLength m1, SquareLength m2) => new SquareLength(m1.Value + m2.Value);
        public static SquareLength operator -(SquareLength m1, SquareLength m2) => new SquareLength(m1.Value - m2.Value);
        
        public static CubicLength operator *(SquareLength m1, Length m2) => CubicLength.FromMeter(m1.Meter * m2.Meter);
        public static CubicLength operator *(Length m1, SquareLength m2) => CubicLength.FromMeter(m1.Meter * m2.Meter);
        public static Length operator /(SquareLength m1, Length m2) => Length.FromMeter(m1.Meter / m2.Meter);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(SquareLength m1, SquareLength m2) => object.Equals(m1, m2);
        public static bool operator !=(SquareLength m1, SquareLength m2) => !object.Equals(m1, m2);
        
        public static bool operator >(SquareLength m1, SquareLength m2) => m1.Value > m2.Value;
        public static bool operator >=(SquareLength m1, SquareLength m2) => m1.Value >= m2.Value;
        public static bool operator <(SquareLength m1, SquareLength m2) => m1.Value < m2.Value;
        public static bool operator <=(SquareLength m1, SquareLength m2) => m1.Value <= m2.Value;

        #endregion
    }
}