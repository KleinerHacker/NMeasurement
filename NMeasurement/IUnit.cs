using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NMeasurement.Utils;

namespace NMeasurement
{
    /// <summary>
    /// Base unit interface for all units of all measurement types
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// Function to calculate raw value from given specific value. Value is specified with this unit.
        /// </summary>
        /// <param name="value">Specified value, based on this unit</param>
        /// <param name="dimension">Dimension value 1-n</param>
        /// <returns>Calculated raw value</returns>
        double CalculateToRawValue(double value, uint dimension = 1);

        /// <summary>
        /// Function to calculate unit value from given raw value. Result value is specified with this unit.
        /// </summary>
        /// <param name="rawValue">Raw value to calculate</param>
        /// <param name="dimension">Dimension value 1-n</param>
        /// <returns>Specified value, based on this unit</returns>
        double CalculateFromRawValue(double rawValue, uint dimension = 1);
        
        /// <summary>
        /// Abbreviation for this unit
        /// </summary>
        string Abbreviation { get; }
        
        /// <summary>
        /// Abbreviation for this unit based on given dimension
        /// </summary>
        /// <param name="dimension">Dimension base</param>
        /// <returns>Abbreviation for given dimension</returns>
        string GetAbbreviation(uint dimension);
    }

    #region Combined Units

    /// <summary>
    /// Base unit interface for all units which represent a combined unit
    /// </summary>
    public interface ICombinedUnit : IUnit
    {
        /// <summary>
        /// Units in level numerator (top of fraction)
        /// </summary>
        UnitHolder[] NumeratorUnits { get; }

        /// <summary>
        /// Units in level denominator (bottom of fraction)
        /// </summary>
        UnitHolder[] DenominatorUnits { get; }
    }

    public abstract class CombinedUnitBase : ICombinedUnit
    {
        protected readonly IList<UnitHolder> _numeratorUnits;
        protected readonly IList<UnitHolder> _denominatorUnits;
        private readonly string _abbreviation;

        public string Abbreviation => GetAbbreviation(1);

        public UnitHolder[] NumeratorUnits => _numeratorUnits.ToArray();
        public UnitHolder[] DenominatorUnits => _denominatorUnits.ToArray();

        protected CombinedUnitBase(string abbreviation = null)
        {
            _numeratorUnits = new List<UnitHolder>();
            _denominatorUnits = new List<UnitHolder>();

            _abbreviation = abbreviation;
        }

        public virtual double CalculateToRawValue(double value, uint dimension = 1)
        {
            var fraction = CalculateFraction(dimension);
            return value * fraction.numerator / fraction.denominator;
        }

        public virtual double CalculateFromRawValue(double rawValue, uint dimension = 1)
        {
            var fraction = CalculateFraction(dimension);
            return rawValue * fraction.denominator / fraction.numerator;
        }

        private (double numerator, double denominator) CalculateFraction(uint dimension)
        {
            var numerator = 1d;
            foreach (var v in NumeratorUnits)
            {
                numerator = Multiply(v, numerator, dimension);
            }

            var denominator = 1d;
            foreach (var v in DenominatorUnits)
            {
                denominator = Multiply(v, denominator, dimension);
            }

            return (numerator, denominator);
        }

        private double Multiply(UnitHolder u, double value, uint dimension)
        {
            return value * u.Unit.CalculateToRawValue(1d, u.Dimension * dimension) * (u.Prefix?.Factor ?? 1d);
        }

        private string ExtractAbbreviation(UnitHolder u)
        {
            return (u.Prefix?.Abbreviation ?? "") + u.Unit.GetAbbreviation(u.Dimension);
        }
        
        public string GetAbbreviation(uint dimension)
        {
            if (!string.IsNullOrEmpty(_abbreviation))
                return _abbreviation;

            var sb = new StringBuilder();
            if (dimension != 1)
            {
                sb.Append("(");
            }

            sb.Append(string.Join(" ", NumeratorUnits.Select(ExtractAbbreviation)))
                .Append("/")
                .Append(string.Join(" ", DenominatorUnits.Select(ExtractAbbreviation)));

            if (dimension != 1)
            {
                sb.Append(")");
                switch (dimension)
                {
                    case 2:
                        sb.Append("²");
                        break;
                    case 3:
                        sb.Append("³");
                        break;
                    default:
                        sb.Append("^").Append(dimension);
                        break;
                }
            }

            return sb.ToString();
        }
    }

    public abstract class FactorCombinedUnitBase : CombinedUnitBase
    {
        private readonly double? _factor;

        protected FactorCombinedUnitBase(string abbreviation = null) : base(abbreviation)
        {
        }

        protected FactorCombinedUnitBase(double factor, string abbreviation) : base(abbreviation)
        {
            _factor = factor;
        }

        public override double CalculateToRawValue(double value, uint dimension = 1) => value * _factor ?? base.CalculateToRawValue(value, dimension);
        public override double CalculateFromRawValue(double rawValue, uint dimension = 1) => rawValue / _factor ?? base.CalculateFromRawValue(rawValue, dimension);
    }

    /// <summary>
    /// Holder to store unit with prefix
    /// </summary>
    public sealed class UnitHolder
    {
        /// <summary>
        /// Unit to store
        /// </summary>
        public IUnit Unit { get; }

        /// <summary>
        /// Connected unit prefix
        /// </summary>
        public IPrefixBase Prefix { get; }

        /// <summary>
        /// Dimension of this unit.
        /// </summary>
        public uint Dimension { get; }

        /// <summary>
        /// Create anew unit holder with combined prefix
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="prefix"></param>
        /// <param name="dimension"></param>
        public UnitHolder(IUnit unit, IPrefixBase prefix = null, uint dimension = 1)
        {
            Unit = unit;
            Prefix = prefix;
            Dimension = dimension;
        }
    }

    #endregion

    #region Single Unit

    /// <summary>
    /// Base unit interface for all units which represent a single unit 
    /// </summary>
    public interface ISingleUnit : IUnit
    {
    }

    public abstract class SingleUnitBase : ISingleUnit
    {
        private readonly Func<double, uint, double> _toRawValue;
        private readonly Func<double, uint, double> _fromRawValue;

        protected readonly string _abbreviation;

        protected SingleUnitBase(Func<double, uint, double> toRawValue, Func<double, uint, double> fromRawValue, string abbreviation)
        {
            _toRawValue = toRawValue;
            _fromRawValue = fromRawValue;
            _abbreviation = abbreviation;
        }

        public double CalculateToRawValue(double value, uint dimension = 1) => _toRawValue(value, dimension);
        public double CalculateFromRawValue(double rawValue, uint dimension = 1) => _fromRawValue(rawValue, dimension);

        public string GetAbbreviation(uint dimension)
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

        public string Abbreviation => GetAbbreviation(1);
    }

    public abstract class FactorSingleUnitBase : SingleUnitBase
    {
        public double Factor { get; }

        protected FactorSingleUnitBase(double factor, string abbreviation)
            : base((v, d) => ExtendedMathUtils.CalculateToRawValue(v, factor, d), (v, d) => ExtendedMathUtils.CalculateFromRawValue(v, factor, d), abbreviation)
        {
            Factor = factor;
        }
    }

    #endregion
}