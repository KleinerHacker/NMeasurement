using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Energies.Interfaces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;
using NMeasurement.Types.Velocity.Interfaces;

namespace NMeasurement.Types.Energies.Internals
{
    internal abstract class EnergyUnitBase : FactorCombinedUnitBase, IEnergyUnit
    {
        protected EnergyUnitBase((IMassUnit, IPrefix) massUnit, (ISquareLengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(new UnitHolder(massUnit.Item1, massUnit.Item2));
            _numeratorUnits.Add(new UnitHolder(lengthUnit.Item1, lengthUnit.Item2, 2));
            _denominatorUnits.Add(new UnitHolder(durationUnit.Item1, durationUnit.Item2, 2));
        }

        protected EnergyUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}