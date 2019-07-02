using System;
using NMeasurement.Internals.Attributes;

namespace NMeasurement.Types.Pressures.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class PressureAttribute : MeasurementAttribute
    {
        public PressureAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}