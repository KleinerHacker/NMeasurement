using System.Collections.Generic;
using System.Net.Http.Headers;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Velocity.Interfaces;

namespace NMeasurement.Types.Velocity.Internals
{
    internal abstract class VelocityUnitBase : FactorCombinedUnitBase<VelocityNumeratorUnit, VelocityDenominatorUnit>, IVelocityUnit
    {
        protected VelocityUnitBase((ILengthUnit, IPrefix) lengthUnit, (IDurationUnit, IPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(VelocityNumeratorUnit.Length, new UnitHolder(lengthUnit.Item1, lengthUnit.Item2));
            _denominatorUnits.Add(VelocityDenominatorUnit.Duration, new UnitHolder(durationUnit.Item1, durationUnit.Item2));
        }

        protected VelocityUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}