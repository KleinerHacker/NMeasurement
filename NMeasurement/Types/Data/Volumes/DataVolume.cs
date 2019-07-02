using NMeasurement.Types.Data.Rates;
using NMeasurement.Types.Data.Rates.Attributes;
using NMeasurement.Types.Data.Volumes.Attributes;
using NMeasurement.Types.Data.Volumes.Interfaces;
using NMeasurement.Types.Durations;

namespace NMeasurement.Types.Data.Volumes
{
    [DataVolume(typeof(Unit), "Bit")]
    public sealed class DataVolume : DataVolumeBase<IDataVolumeUnit, DataVolume>
    {
        internal DataVolume(double value) : base(value)
        {
        }

        public DataVolume(double value, IDataVolumeUnit unit, IDataVolumePrefix prefix = null) : base(value, unit, prefix)
        {
        }

        public DataVolume(double value, IDataVolumeUnit unit, IBigPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Calculation Operators

        public static DataVolume operator +(DataVolume v1, DataVolume v2) => new DataVolume(v1.Value + v2.Value);
        public static DataVolume operator -(DataVolume v1, DataVolume v2) => new DataVolume(v1.Value - v2.Value);

        #endregion
        
        #region Boolean Operators

        public static bool operator ==(DataVolume m1, DataVolume m2) => object.Equals(m1, m2);
        public static bool operator !=(DataVolume m1, DataVolume m2) => !object.Equals(m1, m2);
        
        public static bool operator >(DataVolume m1, DataVolume m2) => m1.Value > m2.Value;
        public static bool operator >=(DataVolume m1, DataVolume m2) => m1.Value >= m2.Value;
        public static bool operator <(DataVolume m1, DataVolume m2) => m1.Value < m2.Value;
        public static bool operator <=(DataVolume m1, DataVolume m2) => m1.Value <= m2.Value;

        #endregion

        #region Cross Calculation Operators

        public static DataRate operator /(DataVolume dv, Duration d) => DataRate.FromBitPerSecond(dv.Bit / d.Second);

        #endregion
    }
}