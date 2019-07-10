using System;
using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Lengths.Internals
{
    /// <summary>
    /// Abstract base class for all length units
    /// </summary>
    internal abstract class LengthUnitBase : FactorSingleUnitBase, ILengthUnitBase
    {
        public abstract uint DimensionBase { get; }

        protected LengthUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}