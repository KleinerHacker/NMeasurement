using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NMeasurement
{
    /// <summary>
    /// Base unit interface for all units of all measurement types
    /// </summary>
    public interface IUnit
    {
    }

    /// <summary>
    /// Base unit interface for all units which represent a single unit 
    /// </summary>
    public interface ISingleUnit : IUnit
    {
    }

    #region Combined Units

    /// <summary>
    /// Base unit interface for all units which represent a combined unit
    /// </summary>
    /// <typeparam name="TUN">Unit enum for numerator</typeparam>
    /// <typeparam name="TUD">Unit enum for denominator</typeparam>
    public interface ICombinedUnit<TUN, TUD> : IUnit where TUN : Enum where TUD : Enum
    {
        /// <summary>
        /// Units in level numerator (top of fraction)
        /// </summary>
        IReadOnlyDictionary<TUN, UnitHolder> NumeratorUnits { get; }

        /// <summary>
        /// Units in level denominator (bottom of fraction)
        /// </summary>
        IReadOnlyDictionary<TUD, UnitHolder> DenominatorUnits { get; }

        /// <summary>
        /// Resulting total abbreviation
        /// </summary>
        string TotalAbbreviation { get; }

        /// <summary>
        /// Function to calculate raw value from given specific value. Value is specified with this unit.
        /// </summary>
        /// <param name="value">Specified value, based on this unit</param>
        /// <returns>Calculated raw value</returns>
        double CalculateToRawValue(double value);

        /// <summary>
        /// Function to calculate unit value from given raw value. Result value is specified with this unit.
        /// </summary>
        /// <param name="rawValue">Raw value to calculate</param>
        /// <returns>Specified value, based on this unit</returns>
        double CalculateFromRawValue(double rawValue);
    }

    public abstract class CombinedUnitBase<TUN, TUD> : ICombinedUnit<TUN, TUD> where TUN : Enum where TUD : Enum
    {
        private readonly Func<IMultiDimensionalUnit, uint> _dimensionResolver;
        protected readonly IDictionary<TUN, UnitHolder> _numeratorUnits;
        protected readonly IDictionary<TUD, UnitHolder> _denominatorUnits;
        private readonly string _abbreviation;

        public IReadOnlyDictionary<TUN, UnitHolder> NumeratorUnits { get; }
        public IReadOnlyDictionary<TUD, UnitHolder> DenominatorUnits { get; }

        public string TotalAbbreviation => _abbreviation ?? string.Join(" ", NumeratorUnits.Values.Select(ExtractAbbreviation)) + "/" +
                                           string.Join(" ", DenominatorUnits.Values.Select(ExtractAbbreviation));

        protected CombinedUnitBase(string abbreviation = null) : this(_ => 1, abbreviation)
        {
        }

        protected CombinedUnitBase(Func<ISingleUnit, uint> dimensionResolver, string abbreviation = null)
        {
            _numeratorUnits = new Dictionary<TUN, UnitHolder>();
            _denominatorUnits = new Dictionary<TUD, UnitHolder>();

            NumeratorUnits = new ReadOnlyDictionary<TUN, UnitHolder>(_numeratorUnits);
            DenominatorUnits = new ReadOnlyDictionary<TUD, UnitHolder>(_denominatorUnits);

            _dimensionResolver = dimensionResolver;
            _abbreviation = abbreviation;
        }

        public virtual double CalculateToRawValue(double value)
        {
            var fraction = CalculateFraction();
            return value * fraction.numerator / fraction.denominator;
        }

        public virtual double CalculateFromRawValue(double rawValue)
        {
            var fraction = CalculateFraction();
            return rawValue * fraction.denominator / fraction.numerator;
        }

        private (double numerator, double denominator) CalculateFraction()
        {
            var numerator = 1d;
            foreach (var v in NumeratorUnits.Values)
            {
                numerator = Multiply(v, numerator);
            }

            var denominator = 1d;
            foreach (var v in DenominatorUnits.Values)
            {
                denominator = Multiply(v, denominator);
            }

            return (numerator, denominator);
        }

        private double Multiply(UnitHolder u, double value)
        {
            if (u.Unit is IStandardUnit)
            {
                var unit = (IStandardUnit) u.Unit;
                return value * unit.CalculateToRawValue(1d) * (u.Prefix?.Factor ?? 1d);
            }
            else if (u.Unit is IMultiDimensionalUnit)
            {
                var unit = (IMultiDimensionalUnit) u.Unit;
                return value * unit.CalculateToRawValue(1d, _dimensionResolver(unit)) * (u.Prefix?.Factor ?? 1d);
            }
            else
                throw new InvalidOperationException("Unknown single unit type: " + u.GetType().FullName);
        }

        private string ExtractAbbreviation(UnitHolder u)
        {
            if (u.Unit is IStandardUnit)
            {
                var unit = (IStandardUnit) u.Unit;
                return (u.Prefix?.Abbreviation ?? "") + unit.Abbreviation;
            }
            else if (u.Unit is IMultiDimensionalUnit)
            {
                var unit = (IMultiDimensionalUnit) u.Unit;
                return (u.Prefix?.Abbreviation ?? "") + unit.Abbreviation(_dimensionResolver(unit));
            }
            else
                throw new InvalidOperationException("Unknown single unit type: " + u.GetType().FullName);
        }
    }

    public abstract class FactorCombinedUnitBase<TUN, TUD> : CombinedUnitBase<TUN, TUD> where TUN : Enum where TUD : Enum
    {
        private readonly double? _factor;

        protected FactorCombinedUnitBase(string abbreviation = null) : base(abbreviation)
        {
        }

        protected FactorCombinedUnitBase(Func<ISingleUnit, uint> dimensionResolver, string abbreviation = null) : base(dimensionResolver, abbreviation)
        {
        }

        protected FactorCombinedUnitBase(double factor, string abbreviation)
        {
            _factor = factor;
        }

        public override double CalculateToRawValue(double value) => value * _factor ?? base.CalculateToRawValue(value);
        public override double CalculateFromRawValue(double rawValue) => rawValue / _factor ?? base.CalculateFromRawValue(rawValue);
    }

    /// <summary>
    /// Holder to store unit with prefix
    /// </summary>
    public sealed class UnitHolder
    {
        /// <summary>
        /// Unit to store
        /// </summary>
        public ISingleUnit Unit { get; }
        /// <summary>
        /// Connected unit prefix
        /// </summary>
        public IPrefixBase Prefix { get; }

        /// <summary>
        /// Create anew unit holder with combined prefix
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="prefix"></param>
        public UnitHolder(ISingleUnit unit, IPrefixBase prefix = null)
        {
            Unit = unit;
            Prefix = prefix;
        }
    }
    
    #endregion

    #region Standard Unit

    /// <summary>
    /// Represent a single standard unit interface with two methods to calculate from / to raw value
    /// </summary>
    public interface IStandardUnit : ISingleUnit
    {
        /// <summary>
        /// Function to calculate raw value from given specific value. Value is specified with this unit.
        /// </summary>
        /// <param name="value">Specified value, based on this unit</param>
        /// <returns>Calculated raw value</returns>
        double CalculateToRawValue(double value);

        /// <summary>
        /// Function to calculate unit value from given raw value. Result value is specified with this unit.
        /// </summary>
        /// <param name="rawValue">Raw value to calculate</param>
        /// <returns>Specified value, based on this unit</returns>
        double CalculateFromRawValue(double rawValue);

        /// <summary>
        /// Abbreviation for this unit
        /// </summary>
        string Abbreviation { get; }
    }

    public abstract class StandardUnitBase : IStandardUnit
    {
        private readonly Func<double, double> _toRawValue;
        private readonly Func<double, double> _fromRawValue;

        public string Abbreviation { get; }

        protected StandardUnitBase(Func<double, double> rawValue, Func<double, double> fromRawValue, string abbreviation)
        {
            _toRawValue = rawValue;
            _fromRawValue = fromRawValue;
            Abbreviation = abbreviation;
        }

        public double CalculateToRawValue(double value) => _toRawValue(value);
        public double CalculateFromRawValue(double rawValue) => _fromRawValue(rawValue);
    }

    public abstract class FactorUnitBase : StandardUnitBase
    {
        public double Factor { get; }

        protected FactorUnitBase(double factor, string abbreviation)
            : base(v => v * factor, v => v / factor, abbreviation)
        {
            Factor = factor;
        }
    }

    #endregion

    #region Multi Dimensional Unit

    /// <summary>
    /// Represent a single multi dimension unit interface with two methods to calculate from / to raw value based on given dimension (1-n)
    /// </summary>
    public interface IMultiDimensionalUnit : ISingleUnit
    {
        /// <summary>
        /// Function to calculate raw value from given specific value. Value is specified with this unit.
        /// </summary>
        /// <param name="value">Specified value, based on this unit</param>
        /// <param name="dimension">Dimension value 1-n</param>
        /// <returns>Calculated raw value</returns>
        double CalculateToRawValue(double value, uint dimension);

        /// <summary>
        /// Function to calculate unit value from given raw value. Result value is specified with this unit.
        /// </summary>
        /// <param name="rawValue">Raw value to calculate</param>
        /// <param name="dimension">Dimension value 1-n</param>
        /// <returns>Specified value, based on this unit</returns>
        double CalculateFromRawValue(double rawValue, uint dimension);

        /// <summary>
        /// Abbreviation for this unit based on given dimension
        /// </summary>
        /// <param name="dimension">Dimension base</param>
        /// <returns>Abbreviation for given dimension</returns>
        string Abbreviation(uint dimension);
    }

    public abstract class MultiDimensionalUnitBase : IMultiDimensionalUnit
    {
        private readonly Func<double, double, double> _toRawValue;
        private readonly Func<double, double, double> _fromRawValue;

        protected readonly string _abbreviation;

        protected MultiDimensionalUnitBase(Func<double, double, double> rawValue, Func<double, double, double> fromRawValue, string abbreviation)
        {
            _toRawValue = rawValue;
            _fromRawValue = fromRawValue;
            _abbreviation = abbreviation;
        }

        public double CalculateToRawValue(double value, uint dimension) => _toRawValue(value, dimension);
        public double CalculateFromRawValue(double rawValue, uint dimension) => _fromRawValue(rawValue, dimension);

        public string Abbreviation(uint dimension)
        {
            switch (dimension)
            {
                case 1:
                    return _abbreviation;
                case 2:
                    return _abbreviation + "²";
                case 3:
                    return _abbreviation + "³";
                default:
                    return _abbreviation + "^" + dimension;
            }
        }
    }

    public abstract class FactorMultiDimensionalUnitBase : MultiDimensionalUnitBase
    {
        public double Factor { get; }

        protected FactorMultiDimensionalUnitBase(double factor, string abbreviation)
            : base((v, d) => v * Math.Pow(factor, d), (v, d) => v / Math.Pow(factor, d), abbreviation)
        {
            Factor = factor;
        }
    }

    #endregion
}