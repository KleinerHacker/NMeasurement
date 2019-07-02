namespace NMeasurement.Types.Data.Rates.Interfaces
{
    public interface IDataRate<in T> : IMeasurement<T> where T : IDataRateUnitBase
    {
    }
}