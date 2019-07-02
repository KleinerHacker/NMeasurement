namespace NMeasurement.Types.Data.Volumes.Interfaces
{
    public interface IDataVolume<in T> : IMeasurement<T, IDataVolumePrefix> where T : IDataVolumeUnitBase
    {
        
    }
}