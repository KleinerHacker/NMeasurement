namespace NMeasurement.Types.Durations.Interfaces
{
    public interface IDuration<in T> : IMeasurement<T, ISmallPrefix> where T : IDurationUnitBase
    {
    }
}