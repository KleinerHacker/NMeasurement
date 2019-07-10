using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Forces.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Forces.Internals
{
    internal abstract class ForceUnitBase : FactorCombinedUnitBase, IForceUnit
    {
        protected ForceUnitBase((IMassUnit, IPrefix) massUnit, (ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(new UnitHolder(massUnit.Item1, massUnit.Item2));
            _numeratorUnits.Add(new UnitHolder(lengthUnit.Item1, lengthUnit.Item2));
            _denominatorUnits.Add(new UnitHolder(durationUnit.Item1, durationUnit.Item2, 2));
        }

        protected ForceUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}