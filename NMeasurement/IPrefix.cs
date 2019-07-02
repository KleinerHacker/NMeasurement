using System;

namespace NMeasurement
{
    /// <summary>
    /// Base for all (custom too) unit prefixes
    /// </summary>
    public interface IPrefixBase
    {
        /// <summary>
        /// Factor of prefix
        /// </summary>
        double Factor { get; }
        /// <summary>
        /// Abbreviation for prefix
        /// </summary>
        string Abbreviation { get; }
    }
    
    /// <summary>
    /// Interface for a prefix for a unit
    /// </summary>
    public interface IPrefix : IPrefixBase
    {
    }

    /// <summary>
    /// Marker interface for big prefixes > 1
    /// </summary>
    public interface IBigPrefix : IPrefix
    {
    }

    /// <summary>
    /// Marker interface for small prefixes &lt; 1
    /// </summary>
    public interface ISmallPrefix : IPrefix
    {
    }
    
    internal abstract class PrefixBase : IPrefixBase
    {
        public double Factor { get; }
        public string Abbreviation { get; }

        public PrefixBase(double factor, string abbreviation)
        {
            Factor = factor;
            Abbreviation = abbreviation;
        }
    }

    internal sealed class BigPrefix : PrefixBase, IBigPrefix
    {
        public BigPrefix(double factor, string abbreviation) : base(factor, abbreviation)
        {
            if (factor <= 1d)
                throw new ArgumentException("factor must be > 1");
        }
    }

    internal sealed class SmallPrefix : PrefixBase, ISmallPrefix
    {
        public SmallPrefix(double factor, string abbreviation) : base(factor, abbreviation)
        {
            if (factor >= 1d)
                throw new ArgumentException("factor must be < 1");
        }
    }

    /// <summary>
    /// Contains all known prefixes
    /// </summary>
    public static class UnitPrefix
    {
        public static IBigPrefix Terra { get; } = new BigPrefix(1_000_000_000_000d, "T");
        public static IBigPrefix Giga { get; } = new BigPrefix(1_000_000_000d, "G");
        public static IBigPrefix Mega { get; } = new BigPrefix(1_000_000d, "M");
        public static IBigPrefix Kilo { get; } = new BigPrefix(1_000d, "k");
        
        public static ISmallPrefix Deci { get; } = new SmallPrefix(0.1d, "d");
        public static ISmallPrefix Centi { get; } = new SmallPrefix(0.01d, "c");
        public static ISmallPrefix Milli { get; } = new SmallPrefix(0.001d, "m");
        public static ISmallPrefix Micro { get; } = new SmallPrefix(0.000001d, "µ");
        public static ISmallPrefix Nano { get; } = new SmallPrefix(0.000000001d, "n");
        public static ISmallPrefix Pico { get; } = new SmallPrefix(0.000000000001d, "p");

        /// <summary>
        /// Create a custom unit big prefix based on given factor (must be > 1)
        /// </summary>
        /// <param name="factor">Factor for custom big prefix (> 1)</param>
        /// <param name="abbreviation">Abbreviation for custom big prefix</param>
        /// <returns>A valid custom big prefix</returns>
        public static IBigPrefix CreateBigPrefix(double factor, string abbreviation = "<custom>") => new BigPrefix(factor, abbreviation);
        
        /// <summary>
        /// Create a custom unit small prefix based on given factor (must be &lt; 1)
        /// </summary>
        /// <param name="factor">Factor for custom small prefix (&lt; 1)</param>
        /// <param name="abbreviation">Abbreviation for custom small prefix</param>
        /// <returns>A valid custom small prefix</returns>
        public static ISmallPrefix CreateSmallPrefix(double factor, string abbreviation = "<custom>") => new SmallPrefix(factor, abbreviation);
    }
}