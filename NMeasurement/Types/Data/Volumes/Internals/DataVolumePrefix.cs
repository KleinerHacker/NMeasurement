using NMeasurement.Types.Data.Volumes.Interfaces;

namespace NMeasurement.Types.Data.Volumes.Internals
{
    internal sealed class DataVolumePrefix : PrefixBase, IDataVolumePrefix
    {
        public DataVolumePrefix(double factor, string abbreviation) : base(factor, abbreviation)
        {
        }
    }
}