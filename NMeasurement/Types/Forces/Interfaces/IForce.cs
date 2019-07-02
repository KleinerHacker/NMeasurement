namespace NMeasurement.Types.Forces.Interfaces
{
    public interface IForce<in T> : IMeasurement<T, IPrefix> where T : IForceUnitBase
    {
    }
}