namespace NMeasurement.Types.Temperatures.Interfaces
{
    /// <summary>
    /// Base interface for all temperature types
    /// </summary>
    /// <typeparam name="T">Type of acceptable temperature units</typeparam>
    public interface ITemperature<in T> : IMeasurement<T> where T : ITemperatureUnitBase
    {
    }
}