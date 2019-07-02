using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Energies.Interfaces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Energies.Internals
{
    internal abstract class EnergyUnitBase : FactorCombinedUnitBase<EnergyNumeratorUnit, EnergyDenominatorUnit>, IEnergyUnit
    {
        protected EnergyUnitBase((IMassUnit, IPrefix) massUnit, (ISquareLengthUnit, IPrefix) lengthUnit, (IDurationUnit, IPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(EnergyNumeratorUnit.Mass, new UnitHolder(massUnit.Item1, massUnit.Item2));
            _numeratorUnits.Add(EnergyNumeratorUnit.SquareLength, new UnitHolder(lengthUnit.Item1, lengthUnit.Item2));
            _denominatorUnits.Add(EnergyDenominatorUnit.SquareDuration, new UnitHolder(durationUnit.Item1, durationUnit.Item2));
        }

        protected EnergyUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}