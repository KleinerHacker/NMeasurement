using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NMeasurement.Internals.Attributes;

namespace NMeasurement
{
    /// <summary>
    /// Base interface for all measurement types
    /// </summary>
    public interface IMeasurement
    {
    }
    
    /// <summary>
    /// Base interface for all measurement types
    /// </summary>
    /// <typeparam name="T">Acceptable unit types</typeparam>
    public interface IMeasurement<in T> : IMeasurement where T : IUnit
    {
        /// <summary>
        /// Returns the contained value, calculated in given unit.
        /// </summary>
        /// <param name="unit">Unit to calculate value to</param>
        /// <returns>Calculated unit based value</returns>
        double GetValue(T unit);

        /// <summary>
        /// Returns the string representation of this object instance based on given unit
        /// </summary>
        /// <param name="unit">Unit to return content on this base</param>
        /// <returns>string representation of this object instance</returns>
        string ToString(T unit);
    }
    
    /// <summary>
    /// Base interface for all measurement types
    /// </summary>
    /// <typeparam name="T">Acceptable unit types</typeparam>
    /// <typeparam name="TPf">Acceptable prefix types</typeparam>
    public interface IMeasurement<in T, in TPf> : IMeasurement<T> where T : IUnit where TPf : class, IPrefixBase
    {
        /// <summary>
        /// Returns the contained value, calculated in given unit.
        /// </summary>
        /// <param name="unit">Unit to calculate value to</param>
        /// <param name="prefix">Optional unit prefix to multiply value</param>
        /// <returns>Calculated unit based value</returns>
        double GetValue(T unit, TPf prefix);

        /// <summary>
        /// Returns the string representation of this object instance based on given unit
        /// </summary>
        /// <param name="unit">Unit to return content on this base</param>
        /// <param name="prefix">Optional unit prefix to multiply value</param>
        /// <returns>string representation of this object instance</returns>
        string ToString(T unit, TPf prefix);
    }

    /// <summary>
    /// Base class for all measurement sub classes. Contains a double value and need always a <see cref="MeasurementAttribute"/> based attribute.
    /// </summary>
    /// <typeparam name="T">Type of acceptable units</typeparam>
    /// <typeparam name="TA">Type of measurement attribute</typeparam>
    /// <typeparam name="TS">Type of concrete implementation of itself</typeparam>
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public abstract class MeasurementBase<T, TA, TS> : IMeasurement<T> where T : IUnit where TA : MeasurementAttribute where TS : MeasurementBase<T, TA, TS>
    {
        /// <summary>
        /// Raw value of measurement (base unit)
        /// </summary>
        protected double Value { get; set; }
        private readonly TA _attribute;

        protected MeasurementBase()
        {
            _attribute = GetType().GetCustomAttribute<TA>();
            if (_attribute == null)
                throw new InvalidOperationException("Unable to find needed attribute " + typeof(TA).FullName + " on class " + GetType().FullName);
            HandleAttribute(_attribute);

            Value = 0;
        }

        protected internal MeasurementBase(double value) : this()
        {
            Value = value;
        }

        protected internal MeasurementBase(double value, T unit) : this()
        {
            Value = CalculateToRawValue(value, unit);
        }

        /// <summary>
        /// Returns the contained value of measurement object, based on given unit
        /// </summary>
        /// <param name="unit">Target unit</param>
        /// <returns>Value based on given unit</returns>
        public double GetValue(T unit) =>  CalculateFromRawValue(Value, unit);

        #region Equals / Hashcode

        protected bool Equals(MeasurementBase<T, TA, TS> other)
        {
            return Value.Equals(other.Value);
        }

        public sealed override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((MeasurementBase<T, TA, TS>) obj);
        }

        public sealed override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion

        #region ToString

        /// <summary>
        /// Default ToString (with base unit, no prefix)
        /// </summary>
        /// <returns></returns>
        public sealed override string ToString() => ToString((T) _attribute.BaseUnit);

        /// <summary>
        /// Returns the measurement as string in form &lt;value> &lt;unit>
        /// </summary>
        /// <param name="unit">Unit to use</param>
        /// <returns></returns>
        public abstract string ToString(T unit);

        #endregion

        /// <summary>
        /// Calculates from value (given unit) to raw value (base unit)
        /// </summary>
        /// <param name="value">Input value (given unit)</param>
        /// <param name="unit">Unit of input value</param>
        /// <returns>Raw value (base unit)</returns>
        protected abstract double CalculateToRawValue(double value, T unit);
        /// <summary>
        /// Calculates from raw value (base unit) to value (given unit)
        /// </summary>
        /// <param name="rawValue">Raw value (base unit)</param>
        /// <param name="unit">Target unit for resulting value</param>
        /// <returns>Value based on given unit</returns>
        protected abstract double CalculateFromRawValue(double rawValue, T unit);

        /// <summary>
        /// Handle read measurement based attribute
        /// </summary>
        /// <param name="attribute">Attribute was read from class</param>
        protected virtual void HandleAttribute(TA attribute)
        {
        }

        #region Instance Area Additional Calculation

        public TS Multiply(TS m) => Multiply((TS) this, m);
        public TS Divide(TS m) => Divide((TS) this, m);

        #endregion

        #region Static Area Additional Calculation

        public static TS Multiply(TS m1, TS m2) => Create(() => m1.Value / m2.Value);
        public static TS Divide(TS m1, TS m2) => Create(() => m1.Value / m2.Value);

        #endregion
        
        /// <summary>
        /// Creates measurement object instance from default raw value constructor (one parameter of type double) 
        /// </summary>
        /// <param name="getter">Getter method to supply raw value</param>
        /// <returns>Created object instance</returns>
        /// <exception cref="InvalidOperationException">If constructor was not found</exception>
        protected static TS Create(DoubleGetter getter)
        {
            var constructorInfo = typeof(TS).GetConstructor(new[] {typeof(double)});
            if (constructorInfo == null)
                throw new InvalidOperationException("Unable to find raw value constructor (one parameter of type double)");
            
            return (TS) constructorInfo.Invoke(new object[] {getter.Invoke()});
        }
        
        /// <summary>
        /// Creates measurement object instance from default value constructor (three parameter of type double, T (unit) and TPf (IPrefixBase)) 
        /// </summary>
        /// <param name="value">value to put into</param>
        /// <param name="unit">Unit of measurement</param>
        /// <returns>Created object instance</returns>
        /// <exception cref="InvalidOperationException">If constructor was not found</exception>
        protected static TS Create(double value, T unit)
        {
            var constructorInfo = typeof(TS).GetConstructor(new[] {typeof(double), typeof(T)});
            if (constructorInfo == null)
            {
                throw new InvalidOperationException("Unable to find value constructor (two parameter, one of type double, one of type T)");
            }

            return (TS) constructorInfo.Invoke(new object[] {value, unit});
        } 

        /// <summary>
        /// Double getter (supplier)
        /// </summary>
        protected delegate double DoubleGetter();
    }

    /// <summary>
    /// Base class for all measurement sub classes. Contains a double value and need always a <see cref="MeasurementAttribute"/> based attribute.
    /// </summary>
    /// <typeparam name="T">Type of acceptable units</typeparam>
    /// <typeparam name="TA">Type of measurement attribute</typeparam>
    /// <typeparam name="TS">Type of concrete implementation of itself</typeparam>
    /// <typeparam name="TPf">Type of acceptable prefixes</typeparam>
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public abstract class MeasurementBase<T, TA, TS, TPf> : MeasurementBase<T, TA, TS>, IMeasurement<T, TPf> where T : IUnit where TA : MeasurementAttribute where TS : MeasurementBase<T, TA, TS, TPf> where TPf : class, IPrefixBase
    {
        protected MeasurementBase()
        {
        }

        protected internal MeasurementBase(double value) : base(value)
        {
        }

        protected internal MeasurementBase(double value, T unit, TPf prefix) 
        {
            Value = CalculateToRawValue(value * RecalculateFactor(prefix?.Factor ?? 1d, unit), unit);
        }

        /// <summary>
        /// Returns the contained value of measurement object, based on given unit and its prefix
        /// </summary>
        /// <param name="unit">Target unit</param>
        /// <param name="prefix">Target unit prefix</param>
        /// <returns>Value based on given unit and its prefix</returns>
        public double GetValue(T unit, TPf prefix) => CalculateFromRawValue(Value, unit) / RecalculateFactor(prefix?.Factor ?? 1d, unit);

        #region ToString

        public abstract string ToString(T unit, TPf prefix);
        public override string ToString(T unit) => ToString(unit, null);

        #endregion
        
        /// <summary>
        /// Override to change prefix calculation factor e. g. for multiple dimension support
        /// </summary>
        /// <param name="factor">Original factor</param>
        /// <param name="unit">Unit (calculation base)</param>
        /// <returns>Changed (recalculated) factor</returns>
        protected virtual double RecalculateFactor(double factor, T unit)
        {
            return factor;
        }
        
        /// <summary>
        /// Creates measurement object instance from default value constructor (three parameter of type double, T (unit) and TPf (IPrefixBase)) 
        /// </summary>
        /// <param name="value">value to put into</param>
        /// <param name="unit">Unit of measurement</param>
        /// <param name="prefix">Prefix of measurement</param>
        /// <returns>Created object instance</returns>
        /// <exception cref="InvalidOperationException">If constructor was not found</exception>
        protected static TS Create(double value, T unit, TPf prefix = null)
        {
            var constructorInfo = typeof(TS).GetConstructor(new[] {typeof(double), typeof(T), typeof(TPf)});
            if (constructorInfo == null)
            {
                throw new InvalidOperationException("Unable to find value constructor (three parameter, one of type double, one of type T, one of type IPrefixBase)");
            }

            return (TS) constructorInfo.Invoke(new object[] {value, unit, prefix});
        }
    }

    /// <summary>
    /// Base class for all measurement sub classes. Contains a double value and need always a <see cref="MeasurementAttribute"/> based attribute.
    /// </summary>
    /// <typeparam name="T">Type of acceptable units</typeparam>
    /// <typeparam name="TA">Type of measurement attribute</typeparam>
    /// <typeparam name="TS">Type of concrete implementation of itself</typeparam>
    /// <typeparam name="TPf1">Type of acceptable prefixes</typeparam>
    /// <typeparam name="TPf2">Type of acceptable prefixes</typeparam>
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public abstract class MeasurementBase<T, TA, TS, TPf1, TPf2> : MeasurementBase<T, TA, TS, TPf1>, IMeasurement<T, TPf1> where T : IUnit where TA : MeasurementAttribute where TS : MeasurementBase<T, TA, TS, TPf1, TPf2> where TPf1 : class, IPrefixBase where TPf2 : class, IPrefixBase
    {
        protected internal MeasurementBase(double value) : base(value)
        {
        }

        protected internal MeasurementBase(double value, T unit, TPf1 prefix) : base(value, unit, prefix)
        {
        }
        
        protected internal MeasurementBase(double value, T unit, TPf2 prefix) 
        {
            Value = CalculateToRawValue(value * RecalculateFactor(prefix?.Factor ?? 1d, unit), unit);
        }
        
        /// <summary>
        /// Returns the contained value of measurement object, based on given unit and its prefix
        /// </summary>
        /// <param name="unit">Target unit</param>
        /// <param name="prefix">Target unit prefix</param>
        /// <returns>Value based on given unit and its prefix</returns>
        public double GetValue(T unit, TPf2 prefix = null) => CalculateFromRawValue(Value, unit) / RecalculateFactor(prefix?.Factor ?? 1d, unit);
        
        /// <summary>
        /// Returns the measurement as string in form &lt;value> &lt;prefix>&lt;unit>
        /// </summary>
        /// <param name="unit">Unit to use</param>
        /// <param name="prefix">Prefix to use</param>
        /// <returns></returns>
        public abstract string ToString(T unit, TPf2 prefix = null);
        
        /// <summary>
        /// Creates measurement object instance from default value constructor (three parameter of type double, T (unit) and TPf (IPrefixBase)) 
        /// </summary>
        /// <param name="value">value to put into</param>
        /// <param name="unit">Unit of measurement</param>
        /// <param name="prefix">Prefix of measurement</param>
        /// <returns>Created object instance</returns>
        /// <exception cref="InvalidOperationException">If constructor was not found</exception>
        protected static TS Create(double value, T unit, TPf2 prefix = null)
        {
            var constructorInfo = typeof(TS).GetConstructor(new[] {typeof(double), typeof(T), typeof(TPf2)});
            if (constructorInfo == null)
            {
                throw new InvalidOperationException("Unable to find value constructor (three parameter, one of type double, one of type T, one of type IPrefixBase)");
            }

            return (TS) constructorInfo.Invoke(new object[] {value, unit, prefix});
        }
    }
}