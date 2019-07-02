using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Expeditions;
using NMeasurement.Types.Velocity;

namespace NMeasurement.Test
{
    [TestClass]
    public class ExpeditionTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestExpeditionValue()
        {
            Assert.AreEqual(10d, Expedition.FromGravity(10d).Gravity, DoubleDelta);
            Assert.AreEqual(10d, Expedition.FromMetersPerSquareSecond(10d).MetersPerSquareSecond, DoubleDelta);
        }

        [TestMethod]
        public void TestExpeditionConvert()
        {
            Assert.AreEqual(1.0197162129779282d, Expedition.FromMetersPerSquareSecond(10d).Gravity, DoubleDelta);
        }

        [TestMethod]
        public void TestExpeditionCalculation()
        {
            Assert.AreEqual(9.4901418935110359d, (Expedition.FromGravity(10d) - Expedition.FromMetersPerSquareSecond(5d)).Gravity, DoubleDelta);
            Assert.AreEqual(10.509858106489d, (Expedition.FromGravity(10d) + Expedition.FromMetersPerSquareSecond(5d)).Gravity, DoubleDelta);
        }

        [TestMethod]
        public void TestExpeditionCrossCalculation()
        {
            Assert.AreEqual(2d, (Velocity.FromMeterPerSecond(10d) / Duration.FromSecond(5)).MetersPerSquareSecond, DoubleDelta);
            Assert.AreEqual(50d, (Expedition.FromMetersPerSquareSecond(10d) * Duration.FromSecond(5)).MeterPerSecond, DoubleDelta);
        }
    }
}