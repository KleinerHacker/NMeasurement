using NMeasurement.Types.Data.Volumes.Interfaces;

namespace NMeasurement.Types.Data.Volumes.Internals
{
    internal abstract class DataVolumeUnitBase : FactorSingleUnitBase, IDataVolumeUnit
    {
        protected DataVolumeUnitBase(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}