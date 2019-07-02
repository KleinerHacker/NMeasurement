namespace NMeasurement.Types.Densities.Interfaces
{
    public interface IDensity<in T> : IMeasurement<T> where T : IDensityUnitBase
    {
    }
}