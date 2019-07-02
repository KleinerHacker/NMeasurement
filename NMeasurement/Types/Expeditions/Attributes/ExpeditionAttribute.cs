using System;
using NMeasurement.Internals.Attributes;

namespace NMeasurement.Types.Expeditions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ExpeditionAttribute : MeasurementAttribute
    {
        public ExpeditionAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}