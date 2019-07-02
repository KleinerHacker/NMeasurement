using System.Diagnostics.CodeAnalysis;
using NMeasurement.Types.Data.Volumes.Attributes;
using NMeasurement.Types.Data.Volumes.Interfaces;
using NMeasurement.Types.Data.Volumes.Internals;

namespace NMeasurement.Types.Data.Volumes
{
    public abstract class DataVolumeBase<T, TD> : MeasurementBase<T, DataVolumeAttribute, TD, IDataVolumePrefix, IBigPrefix>, IDataVolume<T> where T : IDataVolumeUnitBase where TD : DataVolumeBase<T, TD>
    {
        #region Static Init

        public static TD FromBit(double value, IBigPrefix prefix) => Create(value, (T) Unit.Bit, prefix); 
        public static TD FromBit(double value, IDataVolumePrefix prefix) => Create(value, (T) Unit.Bit, prefix);
        public static TD FromBit(double value) => Create(value, (T) Unit.Bit);
        
        public static TD FromByte(double value, IBigPrefix prefix) => Create(value, (T) Unit.Byte, prefix); 
        public static TD FromByte(double value, IDataVolumePrefix prefix) => Create(value, (T) Unit.Byte, prefix);
        public static TD FromByte(double value) => Create(value, (T) Unit.Byte);
        
        public static TD FromOctet(double value, IBigPrefix prefix) => Create(value, (T) Unit.Octet, prefix); 
        public static TD FromOctet(double value, IDataVolumePrefix prefix) => Create(value, (T) Unit.Octet, prefix);
        public static TD FromOctet(double value) => Create(value, (T) Unit.Octet);
        
        public static TD FromWord(double value, IBigPrefix prefix) => Create(value, (T) Unit.Word, prefix); 
        public static TD FromWord(double value, IDataVolumePrefix prefix) => Create(value, (T) Unit.Word, prefix);
        public static TD FromWord(double value) => Create(value, (T) Unit.Word);

        #endregion

        #region Properties

        public double GetBit(IBigPrefix prefix) => GetValue((T) Unit.Bit, prefix);
        public double GetBit(IDataVolumePrefix prefix) => GetValue((T) Unit.Bit, prefix);
        public double Bit => GetValue((T) Unit.Bit);
        
        public double GetByte(IBigPrefix prefix) => GetValue((T) Unit.Byte, prefix);
        public double GetByte(IDataVolumePrefix prefix) => GetValue((T) Unit.Byte, prefix);
        public double Byte => GetValue((T) Unit.Byte);
        
        public double GetOctet(IBigPrefix prefix) => GetValue((T) Unit.Octet, prefix);
        public double GetOctet(IDataVolumePrefix prefix) => GetValue((T) Unit.Octet, prefix);
        public double Octet => GetValue((T) Unit.Octet);
        
        public double GetWord(IBigPrefix prefix) => GetValue((T) Unit.Word, prefix);
        public double GetWord(IDataVolumePrefix prefix) => GetValue((T) Unit.Word, prefix);
        public double Word => GetValue((T) Unit.Word);

        #endregion
        
        protected internal DataVolumeBase(double value) : base(value)
        {
        }

        protected internal DataVolumeBase(double value, T unit, IDataVolumePrefix prefix) : base(value, unit, prefix)
        {
        }

        protected internal DataVolumeBase(double value, T unit, IBigPrefix prefix) : base(value, unit, prefix)
        {
        }

        #region Abstract Implementation

        protected override double CalculateToRawValue(double value, T unit) => unit.CalculateToRawValue(value);
        protected override double CalculateFromRawValue(double rawValue, T unit) => unit.CalculateFromRawValue(rawValue);

        public override string ToString(T unit, IDataVolumePrefix prefix = null) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.Abbreviation}";
        public override string ToString(T unit, IBigPrefix prefix = null) => $"{GetValue(unit)} {prefix?.Abbreviation ?? ""}{unit.Abbreviation}";

        #endregion

        #region Units

        /// <summary>
        /// Contains all known data volume units
        /// </summary>
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Unit
        {
            /// <summary>
            /// <b>Base Unit</b>
            /// </summary>
            public static IDataVolumeUnit Bit { get; } = new DataVolumeUnit(1d, "b");
            /// <summary>
            /// = 8
            /// </summary>
            public static IDataVolumeUnit Byte { get; } = new DataVolumeUnit(8d, "B");
            /// <summary>
            /// = 8
            /// </summary>
            public static IDataVolumeUnit Octet { get; } = new DataVolumeUnit(8d, "o");
            /// <summary>
            /// = 16
            /// </summary>
            public static IDataVolumeUnit Word { get; } = new DataVolumeUnit(16d, "w");
            
            /// <summary>
            /// Creates a custom data volume unit based on given factor
            /// </summary>
            /// <param name="factor">Factor for custom data volume unit</param>
            /// <param name="abbreviation">Abbreviation for custom data volume unit</param>
            /// <returns>A valid custom data volume unit</returns>
            public static IDataVolumeUnit CreateUnit(double factor, string abbreviation = "<custom>") => new DataVolumeUnit(factor, abbreviation);
        }

        #endregion
    }
}