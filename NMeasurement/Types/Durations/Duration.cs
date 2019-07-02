using NMeasurement.Types.Durations.Attributes;
using NMeasurement.Types.Durations.Interfaces;

namespace NMeasurement.Types.Durations
{
    [Duration(typeof(Unit), "Second")]
    public sealed class Duration : DurationBase<IDurationUnit, Duration>
    {
        private Duration(double value) : base(value)
        {
        }

        public Duration(double value, IDurationUnit unit, ISmallPrefix prefix = null) : base(value, unit, prefix)
        {
        }
        
        #region Calculation Operators

        public static Duration operator +(Duration v1, Duration v2) => new Duration(v1.Value + v2.Value);
        public static Duration operator -(Duration v1, Duration v2) => new Duration(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(Duration m1, Duration m2) => object.Equals(m1, m2);
        public static bool operator !=(Duration m1, Duration m2) => !object.Equals(m1, m2);
        
        public static bool operator >(Duration m1, Duration m2) => m1.Value > m2.Value;
        public static bool operator >=(Duration m1, Duration m2) => m1.Value >= m2.Value;
        public static bool operator <(Duration m1, Duration m2) => m1.Value < m2.Value;
        public static bool operator <=(Duration m1, Duration m2) => m1.Value <= m2.Value;

        #endregion
    }
} 