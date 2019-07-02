using NMeasurement.Types.Forces.Interfaces;

namespace NMeasurement.Types.Forces.Internals
{
    internal abstract class ForceUnitBase : FactorUnitBase, IForceUnit
    {
        protected ForceUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}