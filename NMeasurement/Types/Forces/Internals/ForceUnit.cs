using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Forces.Internals
{
    internal sealed class ForceUnit : ForceUnitBase
    {
        public ForceUnit((IMassUnit, IPrefix) massUnit, (ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(massUnit, lengthUnit, durationUnit, abbreviation)
        {
        }

        public ForceUnit(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}