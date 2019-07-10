using System.Diagnostics.CodeAnalysis;
using NMeasurement.Types.Data.Rates.Attributes;
using NMeasurement.Types.Data.Rates.Interfaces;
using NMeasurement.Types.Data.Rates.Internals;
using NMeasurement.Types.Data.Volumes;
using NMeasurement.Types.Data.Volumes.Interfaces;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Velocity.Interfaces;

namespace NMeasurement.Types.Data.Rates
{
    public abstract class DataRateBase<T, TR> : MeasurementBase<T, DataRateAttribute, TR>, IDataRate<T> where T : IDataRateUnitBase where TR : DataRateBase<T, TR>
    {
        #region Static Init

        public static TR FromBitPerSecond(double value) => Create(value, (T) Unit.BitPerSecond);
        public static TR FromBytePerSecond(double value) => Create(value, (T) Unit.BytePerSecond);

        #endregion

        #region Properties

        public double BitPerSecond => GetValue((T) Unit.BitPerSecond);
        public double BytePerSecond => GetValue((T) Unit.BytePerSecond);

        #endregion
        
        protected internal DataRateBase(double value) : base(value)
        {
        }

        protected internal DataRateBase(double value, T unit) : base(value, unit)
        {
        }
        
        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(rawValue);

        public override string ToString(T unit) => $"{GetValue(unit)} {unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known data rate units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            public static IDataRateUnit BitPerSecond { get; } = new DataRateUnit((DataVolume.Unit.Bit, null), (Duration.Unit.Second, null));
            public static IDataRateUnit BytePerSecond { get; } = new DataRateUnit((DataVolume.Unit.Byte, null), (Duration.Unit.Second, null));
            
            /// <summary>
            /// Create a custom data rate unit based on combined units.
            /// </summary>
            /// <param name="lengthUnit">Numerator data volume unit</param>
            /// <param name="durationUnit">Denominator duration unit (time)</param>
            /// <param name="abbreviation">Optional overwrite of abbreviation</param>
            /// <returns>A valid custom data rate unit</returns>
            public static IDataRateUnit CreateUnit((IDataVolumeUnit, IDataVolumePrefix) lengthUnit, (IDurationUnit, ISmallPrefix) durationUnit, string abbreviation = null) => new DataRateUnit(lengthUnit, durationUnit, abbreviation);
        }

        #endregion
    }
}