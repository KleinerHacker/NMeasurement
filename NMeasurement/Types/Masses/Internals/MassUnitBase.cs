using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Masses.Internals
{
    internal abstract class MassUnitBase : FactorUnitBase, IMassUnit
    {
        protected MassUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}