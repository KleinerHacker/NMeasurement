using System.Security;
using NMeasurement.Types.Durations.Interfaces;
using NMeasurement.Types.Energies.Interfaces;
using NMeasurement.Types.Lengths.Interfaces;
using NMeasurement.Types.Masses.Interfaces;
using NMeasurement.Types.Powers.Interfaces;
using NMeasurement.Types.Powers.Internals;

namespace NMeasurement.Types.Powers
{
    #region Combined Builder

    public sealed class PowerUnitCombinedBuilder
    {
        internal PowerUnitCombinedBuilder()
        {
        }

        public PowerUnitMassBuilder WithMass(IMassUnit unit, IPrefix prefix = null)
        {
            return new PowerUnitMassBuilder(new UnitData<IMassUnit, IPrefix>(unit, prefix));
        }

        public PowerUnitEnergyBuilder WithEnergy(IEnergyUnit unit, IPrefix prefix = null)
        {
            return new PowerUnitEnergyBuilder(new UnitData<IEnergyUnit, IPrefix>(unit, prefix));
        }

        public sealed class PowerUnitEnergyBuilder
        {
            private readonly UnitData<IEnergyUnit, IPrefix> _energyUnit;

            internal PowerUnitEnergyBuilder(UnitData<IEnergyUnit, IPrefix> energyUnit)
            {
                _energyUnit = energyUnit;
            }

            public PowerUnitDurationBuilder DivideByDuration(IDurationUnit unit, ISmallPrefix prefix = null)
            {
                return new PowerUnitDurationBuilder(_energyUnit, new UnitData<IDurationUnit, ISmallPrefix>(unit, prefix));
            }

            public sealed class PowerUnitDurationBuilder
            {
                private readonly UnitData<IEnergyUnit, IPrefix> _energyUnit;
                private readonly UnitData<IDurationUnit, ISmallPrefix> _durationUnit;

                internal PowerUnitDurationBuilder(UnitData<IEnergyUnit, IPrefix> energyUnit, UnitData<IDurationUnit, ISmallPrefix> durationUnit)
                {
                    _energyUnit = energyUnit;
                    _durationUnit = durationUnit;
                }

                public IPowerUnit Build(string abbreviation = null)
                {
                    return new PowerUnit((_energyUnit.Unit, _energyUnit.Prefix), (_durationUnit.Unit, _durationUnit.Prefix), abbreviation);
                }
            }
        }

        public sealed class PowerUnitMassBuilder
        {
            private readonly UnitData<IMassUnit, IPrefix> _massData;

            internal PowerUnitMassBuilder(UnitData<IMassUnit, IPrefix> massData)
            {
                _massData = massData;
            }

            public PowerUnitLengthBuilder MultiplyWithSquareLength(ISquareLengthUnit unit, IPrefix prefix = null)
            {
                return new PowerUnitLengthBuilder(_massData, new UnitData<ISquareLengthUnit, IPrefix>(unit, prefix));
            }

            public sealed class PowerUnitLengthBuilder
            {
                private readonly UnitData<IMassUnit, IPrefix> _massData;
                private readonly UnitData<ISquareLengthUnit, IPrefix> _lengthData;

                internal PowerUnitLengthBuilder(UnitData<IMassUnit, IPrefix> massData, UnitData<ISquareLengthUnit, IPrefix> lengthData)
                {
                    _massData = massData;
                    _lengthData = lengthData;
                }

                public PowerUnitDurationBuilder DivideByCubicDuration(IDurationUnit unit, ISmallPrefix prefix = null)
                {
                    return new PowerUnitDurationBuilder(_massData, _lengthData, new UnitData<IDurationUnit, ISmallPrefix>(unit, prefix));
                }

                public sealed class PowerUnitDurationBuilder
                {
                    private readonly UnitData<IMassUnit, IPrefix> _massData;
                    private readonly UnitData<ISquareLengthUnit, IPrefix> _lengthData;
                    private readonly UnitData<IDurationUnit, ISmallPrefix> _durationData;

                    internal PowerUnitDurationBuilder(UnitData<IMassUnit, IPrefix> massData, UnitData<ISquareLengthUnit, IPrefix> lengthData, UnitData<IDurationUnit, ISmallPrefix> durationData)
                    {
                        _massData = massData;
                        _lengthData = lengthData;
                        _durationData = durationData;
                    }

                    public IPowerUnit Build(string abbreviation = null)
                    {
                        return new PowerUnit((_massData.Unit, _massData.Prefix), (_lengthData.Unit, _lengthData.Prefix), (_durationData.Unit, _durationData.Prefix), abbreviation);
                    }
                }
            }
        }
    }

    #endregion

    internal sealed class UnitData<TU, TP> where TU : IUnit where TP : IPrefixBase
    {
        public TU Unit { get; }
        public TP Prefix { get; }

        public UnitData(TU unit, TP prefix)
        {
            Unit = unit;
            Prefix = prefix;
        }
    }
}