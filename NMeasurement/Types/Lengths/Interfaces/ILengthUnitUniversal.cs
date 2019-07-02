namespace NMeasurement.Types.Lengths.Interfaces
{
    /// <summary>
    /// Base interface for all length units in all dimensions (depends on used length type)
    /// </summary>
    public interface ILengthUnitUniversal : ILengthUnitBase, ILengthUnit, ISquareLengthUnit, ICubicLengthUnit
    {
    }
}