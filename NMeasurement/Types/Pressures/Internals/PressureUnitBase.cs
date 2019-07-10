using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Forces.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;
using NMeasurement.Types.Pressures.Interfaces;

namespace NMeasurement.Types.Pressures.Internals
{
    internal abstract class PressureUnitBase : FactorCombinedUnitBase, IPressureUnit
    {
        protected PressureUnitBase((IForceUnit, IPrefix) forceUnit, (ISquareLengthUnit, IPrefix) lengthUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(new UnitHolder(forceUnit.Item1, forceUnit.Item2));
            _denominatorUnits.Add(new UnitHolder(lengthUnit.Item1, lengthUnit.Item2, 2));
        }

        protected PressureUnitBase((IMassUnit, IPrefix) massUnit, (ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(new UnitHolder(massUnit.Item1, massUnit.Item2));
            _denominatorUnits.Add(new UnitHolder(lengthUnit.Item1, lengthUnit.Item2));
            _denominatorUnits.Add(new UnitHolder(durationUnit.Item1, durationUnit.Item2, 2));
        }

        protected PressureUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}