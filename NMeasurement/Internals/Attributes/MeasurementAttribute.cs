using System;
using System.Reflection;
using NMeasurement.Types.Forces.Attributes;

namespace NMeasurement.Internals.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public abstract class MeasurementAttribute : Attribute
    {
        public Type UnitType { get; }
        public string PropertyName { get; }
        
        public IUnit BaseUnit { get; }

        protected MeasurementAttribute(Type unitType, string propertyName)
        {
            UnitType = unitType;
            PropertyName = propertyName;

            var property = unitType.GetProperty(propertyName, BindingFlags.GetProperty | BindingFlags.Static | BindingFlags.Public);
            if (property == null)
                throw new InvalidOperationException("Unable to find type " + unitType.FullName + " or property " + propertyName);
            BaseUnit = (IUnit) property.GetValue(null);
        }
    }
}