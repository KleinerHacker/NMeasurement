using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Expeditions.Internals
{
    internal sealed class ExpeditionUnit : ExpeditionUnitBase
    {
        public ExpeditionUnit((ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, IPrefix) durationUnit, string abbreviation = null) : base(lengthUnit, durationUnit, abbreviation)
        {
        }

        public ExpeditionUnit(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}