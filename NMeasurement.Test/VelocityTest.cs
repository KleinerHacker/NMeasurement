using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Durations;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Velocity;

namespace NMeasurement.Test
{
    [TestClass]
    public class VelocityTest
    {
        private const double DoubleDelta = 0.0001d;
        
        [TestMethod]
        public void TestVelocityValue()
        {
            Assert.AreEqual(10d, Velocity.FromKilometersPerHour(10d).KilometerPerHour, DoubleDelta);
            Assert.AreEqual(10d, Velocity.FromMilePerHour(10d).MilePerHour, DoubleDelta);
            Assert.AreEqual(10d, Velocity.FromMeterPerSecond(10d).MeterPerSecond, DoubleDelta);
            Assert.AreEqual(10d, Velocity.FromFeetPerSecond(10d).FeetPerSecond, DoubleDelta);
            Assert.AreEqual(10d, Velocity.FromKnot(10d).Knot, DoubleDelta);
        }

        [TestMethod]
        public void TestVelocityConvert()
        {
            Assert.AreEqual(2.77778d, Velocity.FromKilometersPerHour(10d).MeterPerSecond, DoubleDelta);
            Assert.AreEqual(19.438612860586268d, Velocity.FromMeterPerSecond(10d).Knot, DoubleDelta);
        }

        [TestMethod]
        public void TestVelocityCalculation()
        {
            Assert.AreEqual(5.98998d, (Velocity.FromMilePerHour(10d) - Velocity.FromKilometersPerHour(5d)).Knot, DoubleDelta);
            Assert.AreEqual(28.9728d, (Velocity.FromFeetPerSecond(10d) + Velocity.FromMeterPerSecond(5d)).KilometerPerHour, DoubleDelta);
        }

        [TestMethod]
        public void TestCrossCalculation()
        {
            Assert.AreEqual(37d, (Length.FromMeter(37, UnitPrefix.Kilo) / Duration.FromHour(1)).KilometerPerHour, DoubleDelta);
            Assert.AreEqual(604.384783510608d, (Length.FromSeaMile(23) / Duration.FromSecond(137)).Knot, DoubleDelta);
            
            Assert.AreEqual(10d, (Velocity.FromKilometersPerHour(100d) * Duration.FromMinute(6d)).GetMeter(UnitPrefix.Kilo), DoubleDelta);
            Assert.AreEqual(0.1d, (Velocity.FromKilometersPerHour(100d) / Length.FromMeter(10d, UnitPrefix.Kilo)).Hour, DoubleDelta);
        }
    }
}