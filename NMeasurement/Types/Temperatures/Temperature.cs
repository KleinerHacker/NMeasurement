using NMeasurement.Types.Temperatures.Attributes;
using NMeasurement.Types.Temperatures.Interfaces;

namespace NMeasurement.Types.Temperatures
{
    /// <summary>
    /// Temperature implementation
    /// </summary>
    [Temperature(typeof(Unit), "Kelvin")]
    public sealed class Temperature : TemperatureBase<ITemperatureUnit, Temperature>
    {
        private Temperature(double rawValue) : base(rawValue)
        {
        }

        public Temperature(double value, ITemperatureUnit unit) : base(value, unit)
        {
        }

        #region Calculation Operators

        public static Temperature operator +(Temperature v1, Temperature v2) => new Temperature(v1.Value + v2.Value);
        public static Temperature operator -(Temperature v1, Temperature v2) => new Temperature(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(Temperature m1, Temperature m2) => object.Equals(m1, m2);
        public static bool operator !=(Temperature m1, Temperature m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Temperature m1, Temperature m2) => m1.Value > m2.Value;
        public static bool operator >=(Temperature m1, Temperature m2) => m1.Value >= m2.Value;
        public static bool operator <(Temperature m1, Temperature m2) => m1.Value < m2.Value;
        public static bool operator <=(Temperature m1, Temperature m2) => m1.Value <= m2.Value;

        #endregion
    }
}