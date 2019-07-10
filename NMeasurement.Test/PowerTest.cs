using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Energies;
using NMeasurement.Types.Powers;

namespace NMeasurement.Test
{
    [TestClass]
    public class PowerTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestPowerValue()
        {
            Assert.AreEqual(10d, Power.FromWatt(10d).Watt, DoubleDelta);
            Assert.AreEqual(10d, Power.FromJoulePerSecond(10d).JoulePerSecond, DoubleDelta);
            Assert.AreEqual(10d, Power.FromKilogramSquareMeterPerCubicSecond(10d).KilogramSquareMeterPerCubicSecond, DoubleDelta);
        }

        [TestMethod]
        public void TestPowerConvert()
        {
            Assert.AreEqual(10d, Power.FromWatt(10d).KilogramSquareMeterPerCubicSecond, DoubleDelta);

            Power.Unit.CreateCombinedUnit()
                .WithEnergy(Energy.Unit.Joule)
                .DivideByDuration(Duration.Unit.Second)
                .Build();
        }

        [TestMethod]
        public void TestPowerCalculation()
        {
            Assert.AreEqual(5d, (Power.FromWatt(10d) - Power.FromKilogramSquareMeterPerCubicSecond(5d)).JoulePerSecond, DoubleDelta);
            Assert.AreEqual(15d, (Power.FromWatt(10d) + Power.FromKilogramSquareMeterPerCubicSecond(5d)).JoulePerSecond, DoubleDelta);
        }

        [TestMethod]
        public void TestCrossCalculation()
        {
            Assert.AreEqual(10d, (Energy.FromJoule(50d) / Duration.FromSecond(5)).Watt);
        }
    }
}