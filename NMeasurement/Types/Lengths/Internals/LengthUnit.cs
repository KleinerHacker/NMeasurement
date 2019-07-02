using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Lengths.Internals
{
    /// <summary>
    /// Implementation of length units, dimension 1
    /// </summary>
    internal sealed class LengthUnit : LengthUnitBase, ILengthUnit
    {
        public override uint DimensionBase { get; } = 1;

        public LengthUnit(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}