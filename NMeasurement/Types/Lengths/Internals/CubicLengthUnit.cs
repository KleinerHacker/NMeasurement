using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Lengths.Internals
{
    /// <summary>
    /// Implementation of cubic length units, dimension 3. <b>Only for straight 3D units</b> 
    /// </summary>
    internal sealed class CubicLengthUnit : LengthUnitBase, ICubicLengthUnit
    {
        public override uint DimensionBase { get; } = 3;

        public CubicLengthUnit(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
        
        public double CalculateToRawValue(double value, uint dimension) => value * Factor; //Ignore dimension
        public double CalculateFromRawValue(double rawValue, uint dimension) => rawValue / Factor; //Ignore dimension

        public string Abbreviation(uint dimension) => _abbreviation; //Ignore dimension
    }
}