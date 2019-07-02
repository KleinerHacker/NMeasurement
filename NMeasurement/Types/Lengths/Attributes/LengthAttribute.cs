using System;
using NMeasurement.Internals.Attributes;

namespace NMeasurement.Types.Lengths.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class LengthAttribute : MeasurementAttribute
    {
        public uint Dimension { get; }

        public LengthAttribute(Type unitType, string propertyName, uint dimension) : base(unitType, propertyName)
        {
            Dimension = dimension;
        }
    }
}