using System;
using NMeasurement.Internals.Attributes;

namespace NMeasurement.Types.Velocity.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class VelocityAttribute : MeasurementAttribute
    {
        public VelocityAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}