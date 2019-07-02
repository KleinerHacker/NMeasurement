using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Expeditions.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Expeditions.Internals
{
    internal abstract class ExpeditionUnitBase : FactorCombinedUnitBase<ExpeditionNumeratorUnit, ExpeditionDenominatorUnit>, IExpeditionUnit
    {
        protected ExpeditionUnitBase((ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, IPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(ExpeditionNumeratorUnit.Length, new UnitHolder(lengthUnit.Item1, lengthUnit.Item2));
            _denominatorUnits.Add(ExpeditionDenominatorUnit.SquareDuration, new UnitHolder(durationUnit.Item1, durationUnit.Item2));
        }

        protected ExpeditionUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}