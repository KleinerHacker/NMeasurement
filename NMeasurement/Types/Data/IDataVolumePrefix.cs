using NMeasurement.Types.Data.Volumes.Internals;

namespace NMeasurement.Types.Data
{
    /// <summary>
    /// Extended data volume unit prefix
    /// </summary>
    public interface IDataVolumePrefix : IPrefixBase
    {
    }

    /// <summary>
    /// Contains all known extended data volume prefixes
    /// </summary>
    public static class DataVolumeUnitPrefix
    {
        /// <summary>
        /// = 1024^1
        /// </summary>
        public static IDataVolumePrefix Kibi { get; } = new DataVolumePrefix(1024d, "Ki");
        /// <summary>
        /// = 1024^2
        /// </summary>
        public static IDataVolumePrefix Mebi { get; } = new DataVolumePrefix(1024d * 1024d, "Mi");
        /// <summary>
        /// = 1024^3
        /// </summary>
        public static IDataVolumePrefix Gigi { get; } = new DataVolumePrefix(1024d * 1024d * 1024d, "Gi");
        /// <summary>
        /// = 1024^4
        /// </summary>
        public static IDataVolumePrefix Tebi { get; } = new DataVolumePrefix(1024d * 1024d * 1024d * 1024d, "Ti");
            
        /// <summary>
        /// Creates a custom data volume prefix based on given factor
        /// </summary>
        /// <param name="factor">Factor for custom data volume prefix</param>
        /// <param name="abbreviation">Abbreviation for custom data volume prefix</param>
        /// <returns>A valid custom data volume prefix</returns>
        public static IDataVolumePrefix CreatePrefix(double factor, string abbreviation = "<custom>") => new DataVolumePrefix(factor, abbreviation);
    }
}