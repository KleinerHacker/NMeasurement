using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Lengths.Internals
{
    /// <summary>
    /// Implementation of square length units, dimension 2. <b>Only for straight 2D units</b> 
    /// </summary>
    internal sealed class SquareLengthUnit : LengthUnitBase, ISquareLengthUnit
    {
        public override uint DimensionBase { get; } = 2;

        public SquareLengthUnit(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }

        public double CalculateToRawValue(double value, uint dimension) => value * Factor; //Ignore dimension
        public double CalculateFromRawValue(double rawValue, uint dimension) => rawValue / Factor; //Ignore dimension

        public string GetAbbreviation(uint dimension) => _abbreviation; //Ignore dimension
    }
}