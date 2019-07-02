namespace NMeasurement.Types.Energies.Interfaces
{
    public interface IEnergyUnitBase : ICombinedUnit<EnergyNumeratorUnit, EnergyDenominatorUnit>
    {
    }

    public enum EnergyNumeratorUnit
    {
        Mass,
        SquareLength
    }

    public enum EnergyDenominatorUnit
    {
        SquareDuration
    }
}