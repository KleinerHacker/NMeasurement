using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Forces.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Pressures.Internals
{
    internal sealed class PressureUnit : PressureUnitBase
    {
        public PressureUnit((IForceUnit, IPrefix) forceUnit, (ISquareLengthUnit, IPrefix) lengthUnit, string abbreviation = null) : base(forceUnit, lengthUnit, abbreviation)
        {
        }

        public PressureUnit((IMassUnit, IPrefix) massUnit, (ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(massUnit, lengthUnit, durationUnit, abbreviation)
        {
        }

        public PressureUnit(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}