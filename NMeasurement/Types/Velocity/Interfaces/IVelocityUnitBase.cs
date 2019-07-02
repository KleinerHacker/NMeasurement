namespace NMeasurement.Types.Velocity.Interfaces
{
    public interface IVelocityUnitBase : ICombinedUnit<VelocityNumeratorUnit, VelocityDenominatorUnit>
    {
    }

    public enum VelocityNumeratorUnit
    {
        Length
    }

    public enum VelocityDenominatorUnit
    {
        Duration
    }
}