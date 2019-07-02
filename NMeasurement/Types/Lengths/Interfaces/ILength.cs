namespace NMeasurement.Types.Lengths.Interfaces
{
    /// <summary>
    /// Base interface for all length types
    /// </summary>
    /// <typeparam name="T">Type of acceptable length unit</typeparam>
    public interface ILength<in T> : IMeasurement<T, IPrefix> where T : ILengthUnitBase
    {
    }
}