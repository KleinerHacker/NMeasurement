using NMeasurement.Types.Data.Rates.Interfaces;
using NMeasurement.Types.Data.Volumes;
using NMeasurement.Types.Data.Volumes.Interfaces;
using NMeasurement.Types.Data.Volumes.Internals;
using NMeasurement.Types.Durations.Interfaces;

namespace NMeasurement.Types.Data.Rates.Internals
{
    internal abstract class DataRateUnitBase : CombinedUnitBase, IDataRateUnit 
    {
        protected DataRateUnitBase((IDataVolumeUnit, IDataVolumePrefix) dataVolumeUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(abbreviation)
        {
            _numeratorUnits.Add(new UnitHolder(dataVolumeUnit.Item1, dataVolumeUnit.Item2));
            _denominatorUnits.Add(new UnitHolder(durationUnit.Item1, durationUnit.Item2));
        }
    }
}