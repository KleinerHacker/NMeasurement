namespace NMeasurement.Types.Velocity.Interfaces
{
    public interface IVelocity<in T> : IMeasurement<T> where T : IVelocityUnitBase
    {
    }
}