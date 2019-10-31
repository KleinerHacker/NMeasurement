using System;
using NMeasurement.Commons.Attributes;

namespace NMeasurement.Types.Densities.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class DensityAttribute : MeasurementAttribute
    {
        public DensityAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}