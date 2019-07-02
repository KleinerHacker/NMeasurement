namespace NMeasurement.Types.Expeditions.Interfaces
{
    /// <summary>
    /// Basic interface for all expedition units
    /// </summary>
    public interface IExpeditionUnitBase : ICombinedUnit<ExpeditionNumeratorUnit, ExpeditionDenominatorUnit>
    {
    }

    public enum ExpeditionNumeratorUnit
    {
        Length
    }

    public enum ExpeditionDenominatorUnit
    {
        SquareDuration
    }
}