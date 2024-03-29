using System;
using NMeasurement.Commons.Attributes;

namespace NMeasurement.Types.Data.Volumes.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class DataVolumeAttribute : MeasurementAttribute
    {
        public DataVolumeAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}