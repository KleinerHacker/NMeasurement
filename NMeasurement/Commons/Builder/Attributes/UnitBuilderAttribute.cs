using System;

namespace NMeasurement.Commons.Builder.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]   
    public sealed class UnitBuilderAttribute : Attribute
    {
        /// <summary>
        /// Type of unit builder interface
        /// </summary>
        public UnitBuilderType Type { get; } = UnitBuilderType.Default;
    }

    /// <summary>
    /// Known types of unit builder
    /// </summary>
    public enum UnitBuilderType
    {
        /// <summary>
        /// Default unit builder (with all entry methods)
        /// </summary>
        Default,
        /// <summary>
        /// Termination (only with build method)
        /// </summary>
        Termination
    }
}