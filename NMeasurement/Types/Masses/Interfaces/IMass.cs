namespace NMeasurement.Types.Masses.Interfaces
{
    /// <summary>
    /// Base interface for all mass types
    /// </summary>
    /// <typeparam name="T">Type of acceptable mass units</typeparam>
    public interface IMass<in T> : IMeasurement<T, IPrefix> where T : IMassUnitBase
    {
        
    }
}