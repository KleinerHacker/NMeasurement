using System;
using NMeasurement.Internals.Attributes;

namespace NMeasurement.Types.Data.Rates.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class DataRateAttribute : MeasurementAttribute
    {
        public DataRateAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}