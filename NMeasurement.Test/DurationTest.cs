using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Durations;

namespace NMeasurement.Test
{
    [TestClass]
    public class DurationTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestDurationValue()
        {
            Assert.AreEqual(10d, Duration.FromDay(10d).Day, DoubleDelta);
            Assert.AreEqual(10d, Duration.FromHour(10d).Hour, DoubleDelta);
            Assert.AreEqual(10d, Duration.FromMinute(10d).Minute, DoubleDelta);
            Assert.AreEqual(10d, Duration.FromSecond(10d).Second, DoubleDelta);
        }

        [TestMethod]
        public void TestDurationConvert()
        {
            Assert.AreEqual(600d, Duration.FromHour(10d).Minute, DoubleDelta);
        }

        [TestMethod]
        public void TestDurationCalculation()
        {
            Assert.AreEqual(60d * 60d * 10d - 60d * 5d, (Duration.FromHour(10d) - Duration.FromMinute(5d)).Second, DoubleDelta);
            Assert.AreEqual(60d * 60d * 10d + 60d * 5d, (Duration.FromHour(10d) + Duration.FromMinute(5d)).Second, DoubleDelta);
        }
    }
}