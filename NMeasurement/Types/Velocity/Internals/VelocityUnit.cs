using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;

namespace NMeasurement.Types.Velocity.Internals
{
    internal sealed class VelocityUnit : VelocityUnitBase
    {
        public VelocityUnit((ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, IPrefix) durationUnit, string abbreviation = null) : base(lengthUnit, durationUnit, abbreviation)
        {
        }

        public VelocityUnit(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}