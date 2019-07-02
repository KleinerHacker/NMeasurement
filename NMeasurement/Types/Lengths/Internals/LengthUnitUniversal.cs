using System;
using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Lengths.Internals
{
    /// <summary>
    /// Implementation of length units in all dimensions (depends on used length type)
    /// </summary>
    internal sealed class LengthUnitUniversal : LengthUnitBase, ILengthUnitUniversal
    {
        public override uint DimensionBase { get; } = 0;

        public LengthUnitUniversal(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}