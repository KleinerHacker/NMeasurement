using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Energies.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;
using NMeasurement.Types.Powers.Interfaces;

namespace NMeasurement.Types.Powers.Internals
{
    internal abstract class PowerUnitBase : FactorCombinedUnitBase, IPowerUnit
    {
        protected PowerUnitBase((IEnergyUnit, IPrefix) energyUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(new UnitHolder(energyUnit.Item1, energyUnit.Item2));
            _denominatorUnits.Add(new UnitHolder(durationUnit.Item1, durationUnit.Item2));
        }

        protected PowerUnitBase((IMassUnit, IPrefix) massUnit, (ISquareLengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(new UnitHolder(massUnit.Item1, massUnit.Item2));
            _numeratorUnits.Add(new UnitHolder(lengthUnit.Item1, lengthUnit.Item2, 2));
            _denominatorUnits.Add(new UnitHolder(durationUnit.Item1, durationUnit.Item2, 3));
        }

        protected PowerUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}