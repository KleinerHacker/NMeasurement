using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Expeditions.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Expeditions.Internals
{
    internal abstract class ExpeditionUnitBase : FactorCombinedUnitBase, IExpeditionUnit
    {
        protected ExpeditionUnitBase((ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(new UnitHolder(lengthUnit.Item1, lengthUnit.Item2));
            _denominatorUnits.Add(new UnitHolder(durationUnit.Item1, durationUnit.Item2, 2));
        }

        protected ExpeditionUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}