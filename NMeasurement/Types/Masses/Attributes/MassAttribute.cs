using System;
using NMeasurement.Commons.Attributes;

namespace NMeasurement.Types.Masses.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class MassAttribute : MeasurementAttribute
    {
        public MassAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}