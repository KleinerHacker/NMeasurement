namespace NMeasurement.Types.Lengths.Interfaces
{
    /// <summary>
    /// Base interface for all length units
    /// </summary>
    public interface ILengthUnitBase : IMultiDimensionalUnit
    {
        /// <summary>
        /// Basic dimension value (1, 2, 3) to define its own basic dimension, e. g. litre (3), Hectare (2), ...
        /// </summary>
        uint DimensionBase { get; }
    }
}