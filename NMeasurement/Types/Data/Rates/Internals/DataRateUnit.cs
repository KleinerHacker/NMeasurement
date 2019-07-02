using NMeasurement.Types.Data.Volumes.Interfaces;
using NMeasurement.Types.Durations.Interfaces;

namespace NMeasurement.Types.Data.Rates.Internals
{
    internal sealed class DataRateUnit : DataRateUnitBase
    {
        public DataRateUnit((IDataVolumeUnit, IDataVolumePrefix) dataVolumeUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) : base(dataVolumeUnit, durationUnit, abbreviation)
        {
        }
    }
}