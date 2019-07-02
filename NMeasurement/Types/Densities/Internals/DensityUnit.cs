using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;

namespace NMeasurement.Types.Densities.Internals
{
    internal sealed class DensityUnit : DensityUnitBase
    {
        public DensityUnit((IMassUnit, IPrefix) massUnit, (ICubicLengthUnit, IPrefix) lengthUnit, string abbreviation = null) : base(massUnit, lengthUnit, abbreviation)
        {
        }
    }
}