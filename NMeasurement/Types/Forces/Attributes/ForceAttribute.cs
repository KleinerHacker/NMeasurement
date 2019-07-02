using System;
using NMeasurement.Internals.Attributes;

namespace NMeasurement.Types.Forces.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ForceAttribute : MeasurementAttribute
    {
        public ForceAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}