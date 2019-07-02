using NMeasurement.Types.Powers.Interfaces;

namespace NMeasurement.Types.Powers.Internals
{
    internal abstract class PowerUnitBase : FactorUnitBase, IPowerUnit
    {
        protected PowerUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}