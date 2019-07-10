using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Energies.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Powers.Internals
{
    internal sealed class PowerUnit : PowerUnitBase
    {
        public PowerUnit((IEnergyUnit, IPrefix) energyUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(energyUnit, durationUnit, abbreviation)
        {
        }

        public PowerUnit((IMassUnit, IPrefix) massUnit, (ISquareLengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(massUnit, lengthUnit, durationUnit, abbreviation)
        {
        }

        public PowerUnit(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}