using System;
using NMeasurement.Commons.Attributes;

namespace NMeasurement.Types.Powers.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class PowerAttribute : MeasurementAttribute
    {
        public PowerAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}