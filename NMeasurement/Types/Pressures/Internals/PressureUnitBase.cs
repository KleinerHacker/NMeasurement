using NMeasurement.Types.Pressures.Interfaces;

namespace NMeasurement.Types.Pressures.Internals
{
    internal abstract class PressureUnitBase : FactorUnitBase, IPressureUnit
    {
        protected PressureUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}