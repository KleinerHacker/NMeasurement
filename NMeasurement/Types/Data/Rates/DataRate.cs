using NMeasurement.Types.Data.Rates.Attributes;
using NMeasurement.Types.Data.Rates.Interfaces;
using NMeasurement.Types.Data.Volumes;
using NMeasurement.Types.Data.Volumes.Attributes;
using NMeasurement.Types.Durations;

namespace NMeasurement.Types.Data.Rates
{
    [DataRate(typeof(Unit), "BitPerSecond")]
    public sealed class DataRate : DataRateBase<IDataRateUnit, DataRate>
    {
        internal DataRate(double value) : base(value)
        {
        }

        public DataRate(double value, IDataRateUnit unit) : base(value, unit)
        {
        }
        
        #region Calculation Operators

        public static DataRate operator +(DataRate v1, DataRate v2) => new DataRate(v1.Value + v2.Value);
        public static DataRate operator -(DataRate v1, DataRate v2) => new DataRate(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(DataRate m1, DataRate m2) => object.Equals(m1, m2);
        public static bool operator !=(DataRate m1, DataRate m2) => !object.Equals(m1, m2);
        
        public static bool operator >(DataRate m1, DataRate m2) => m1.Value > m2.Value;
        public static bool operator >=(DataRate m1, DataRate m2) => m1.Value >= m2.Value;
        public static bool operator <(DataRate m1, DataRate m2) => m1.Value < m2.Value;
        public static bool operator <=(DataRate m1, DataRate m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static Duration operator /(DataRate dr, DataVolume dv) => Duration.FromSecond(dr.BitPerSecond / dv.Bit);
        
        public static DataVolume operator *(DataRate dr, Duration d) => DataVolume.FromBit(dr.BitPerSecond * d.Second);
        public static DataVolume operator *(Duration d, DataRate dr) => DataVolume.FromBit(dr.BitPerSecond * d.Second);

        #endregion
    }
}