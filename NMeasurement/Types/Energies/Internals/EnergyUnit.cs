using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Energies.Internals
{
    internal sealed class EnergyUnit : EnergyUnitBase
    {
        public EnergyUnit((IMassUnit, IPrefix) massUnit, (ISquareLengthUnit, IPrefix) lengthUnit, (IDurationUnit, IPrefix) durationUnit, string abbreviation = null) : base(massUnit, lengthUnit, durationUnit, abbreviation)
        {
        }

        public EnergyUnit(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}