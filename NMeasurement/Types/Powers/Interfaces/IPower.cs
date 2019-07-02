namespace NMeasurement.Types.Powers.Interfaces
{
    public interface IPower<in T> : IMeasurement<T, IPrefix> where T : IPowerUnitBase
    {
    }
}