using System;
using NMeasurement.Internals.Attributes;

namespace NMeasurement.Types.Energies.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class EnergyAttribute : MeasurementAttribute
    {
        public EnergyAttribute(Type unitType, string propertyName) : base(unitType, propertyName)
        {
        }
    }
}