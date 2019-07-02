using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Data;
using NMeasurement.Types.Data.Volumes;
using NMeasurement.Types.Data.Volumes.Attributes;
using NMeasurement.Types.Durations;

namespace NMeasurement.Test
{
    [TestClass]
    public class DataVolumeTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestDataVolumeValue()
        {
            Assert.AreEqual(10d, DataVolume.FromBit(10d).Bit, DoubleDelta);
            Assert.AreEqual(10d, DataVolume.FromByte(10d).Byte, DoubleDelta);
            Assert.AreEqual(10d, DataVolume.FromOctet(10d).Octet, DoubleDelta);
            Assert.AreEqual(10d, DataVolume.FromWord(10d).Word, DoubleDelta);
        }

        [TestMethod]
        public void TestDataVolumeConvert()
        {
            Assert.AreEqual(1.25d, DataVolume.FromBit(10d).Byte, DoubleDelta);
            Assert.AreEqual(5d, DataVolume.FromByte(10d).Word, DoubleDelta);
        }

        [TestMethod]
        public void TestDataVolumePrefixConvert()
        {
            Assert.AreEqual(1d, DataVolume.FromByte(1000).GetByte(UnitPrefix.Kilo), DoubleDelta);
            Assert.AreEqual(1d, DataVolume.FromByte(1024).GetByte(DataVolumeUnitPrefix.Kibi), DoubleDelta);
            
            Assert.AreEqual(5120d, DataVolume.FromByte(10d, DataVolumeUnitPrefix.Mebi).GetWord(DataVolumeUnitPrefix.Kibi), DoubleDelta);
        }

        [TestMethod]
        public void TestDataVolumeCalculation()
        { 
            Assert.AreEqual(4.6875d, (DataVolume.FromByte(10d) - DataVolume.FromBit(5d)).Word, DoubleDelta);
            Assert.AreEqual(200d, (DataVolume.FromWord(10d) + DataVolume.FromByte(5d)).Bit, DoubleDelta);
        }

        [TestMethod]
        public void TestDataVolumeCrossCalculation()
        {
            Assert.AreEqual(10_000d, (DataVolume.FromBit(10d) / Duration.FromSecond(1d, UnitPrefix.Milli)).BitPerSecond);
        }
    }
}