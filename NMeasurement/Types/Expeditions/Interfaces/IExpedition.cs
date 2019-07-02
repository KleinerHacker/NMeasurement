namespace NMeasurement.Types.Expeditions.Interfaces
{
    /// <summary>
    /// Base interface for all expedition types
    /// </summary>
    /// <typeparam name="T">Type of acceptable expedition units</typeparam>
    public interface IExpedition<in T> : IMeasurement<T, IPrefix> where T : IExpeditionUnitBase
    {
    }
}