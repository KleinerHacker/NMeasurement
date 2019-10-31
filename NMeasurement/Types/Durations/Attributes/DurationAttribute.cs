using System;
using NMeasurement.Commons.Attributes;

namespace NMeasurement.Types.Durations.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class DurationAttribute : MeasurementAttribute
    {
        public DurationAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}