namespace NMeasurement.Types.Data.Rates.Interfaces
{
    public interface IDataRateUnitBase : ICombinedUnit<DataRateNumeratorUnit, DataRateDenominatorUnit>
    {
    }

    public enum DataRateNumeratorUnit
    {
        DataVolume
    }

    public enum DataRateDenominatorUnit
    {
        Duration
    }
}