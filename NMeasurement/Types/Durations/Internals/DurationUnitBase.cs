using NMeasurement.Types.Durations.Interfaces;

namespace NMeasurement.Types.Durations.Internals
{
    internal abstract class DurationUnitBase : FactorUnitBase, IDurationUnit
    {
        protected DurationUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}