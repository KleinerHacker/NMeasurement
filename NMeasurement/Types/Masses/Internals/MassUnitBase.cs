using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Masses.Internals
{
    internal abstract class MassUnitBase : FactorSingleUnitBase, IMassUnit
    {
        protected MassUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}