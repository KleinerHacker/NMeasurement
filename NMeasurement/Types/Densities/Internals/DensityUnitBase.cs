using NMeasurement.Types.Densities.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Densities.Internals
{
    internal abstract class DensityUnitBase : CombinedUnitBase, IDensityUnit
    {
        protected DensityUnitBase((IMassUnit, IPrefix) massUnit, (ICubicLengthUnit, IPrefix) lengthUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(new UnitHolder(massUnit.Item1, massUnit.Item2));
            _denominatorUnits.Add(new UnitHolder(lengthUnit.Item1, lengthUnit.Item2, 3));
        }
    }
}