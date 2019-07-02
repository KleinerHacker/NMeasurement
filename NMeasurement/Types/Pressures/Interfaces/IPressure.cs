namespace NMeasurement.Types.Pressures.Interfaces
{
    public interface IPressure<in T> : IMeasurement<T, IPrefix> where T : IPressureUnitBase
    {
    }
}