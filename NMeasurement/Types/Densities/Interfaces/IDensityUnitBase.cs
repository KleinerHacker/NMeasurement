namespace NMeasurement.Types.Densities.Interfaces
{
    public interface IDensityUnitBase : ICombinedUnit<DensityNumeratorUnit, DensityDenominatorUnit>
    {
    }

    public enum DensityNumeratorUnit
    {
        Mass
    }

    public enum DensityDenominatorUnit
    {
        CubicLength
    }
}