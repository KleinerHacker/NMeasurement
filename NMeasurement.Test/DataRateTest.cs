using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Data.Rates;
using NMeasurement.Types.Data.Volumes;
using NMeasurement.Types.Durations;

namespace NMeasurement.Test
{
    [TestClass]
    public class DataRateTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestDataRateValue()
        {
            Assert.AreEqual(10d, DataRate.FromBitPerSecond(10d).BitPerSecond, DoubleDelta);
            Assert.AreEqual(10d, DataRate.FromBytePerSecond(10d).BytePerSecond, DoubleDelta);
        }

        [TestMethod]
        public void TestDataRateConvert()
        {
            Assert.AreEqual(1.25d, DataRate.FromBitPerSecond(10d).BytePerSecond, DoubleDelta);
        }

        [TestMethod]
        public void TestDataRateCalculation()
        { 
            Assert.AreEqual(75d, (DataRate.FromBytePerSecond(10d) - DataRate.FromBitPerSecond(5d)).BitPerSecond, DoubleDelta);
        }

        [TestMethod]
        public void TestDataRateCrossCalculation()
        {
            Assert.AreEqual(5d, (DataRate.FromBitPerSecond(10d) / DataVolume.FromBit(2d)).Second);
            Assert.AreEqual(50d, (DataRate.FromBitPerSecond(10d) * Duration.FromSecond(5d)).Bit);
        }
    }
}