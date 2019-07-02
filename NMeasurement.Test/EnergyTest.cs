using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Energies;
using NMeasurement.Types.Forces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Masses;
using NMeasurement.Types.Powers;

namespace NMeasurement.Test
{
    [TestClass]
    public class EnergyTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestEnergyValue()
        {
            Assert.AreEqual(10d, Energy.FromJoule(10d).Joule, DoubleDelta);
            Assert.AreEqual(10d, Energy.FromKilogramSquareMeterPerSquareSecond(10d).KilogramSquareMeterPerSquareSecond, DoubleDelta);
        }

        [TestMethod]
        public void TestEnergyConvert()
        {
            Assert.AreEqual(10d, Energy.FromJoule(10d).KilogramSquareMeterPerSquareSecond, DoubleDelta);
            Assert.AreEqual(10_000d, Energy.FromJoule(10d).GetValue(Energy.Unit.CreateUnit((Mass.Unit.Gram, null), (Length.Unit.Meter, null), (Duration.Unit.Second, null))), DoubleDelta);
        }

        [TestMethod]
        public void TestEnergyCalculation()
        {
            Assert.AreEqual(5d, (Energy.FromJoule(10d) - Energy.FromKilogramSquareMeterPerSquareSecond(5d)).Joule, DoubleDelta);
            Assert.AreEqual(15d, (Energy.FromJoule(10d) + Energy.FromKilogramSquareMeterPerSquareSecond(5d)).Joule, DoubleDelta);
        }

        [TestMethod]
        public void TestCrossCalculation()
        {
            Assert.AreEqual(5d, (Force.FromNewton(10d) * Length.FromMeter(5, UnitPrefix.Deci)).Joule);
            Assert.AreEqual(10d, (Power.FromWatt(5d) * Duration.FromSecond(2d)).Joule);
        }
    }
}