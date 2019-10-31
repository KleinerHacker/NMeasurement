using System;
using NMeasurement.Commons.Attributes;

namespace NMeasurement.Types.Temperatures.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class TemperatureAttribute : MeasurementAttribute
    {
        public TemperatureAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}