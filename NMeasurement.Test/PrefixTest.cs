using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Lengths;

namespace NMeasurement.Test
{
    [TestClass]
    public class PrefixTest
    {
        private const double DoubleDelta = 0.0001d;
        
        [TestMethod]
        public void TestWithoutChange()
        {
            Assert.AreEqual(0.1d, Length.FromMeter(0.1d).Meter, DoubleDelta);
            Assert.AreEqual(0.1d, Length.FromMeter(0.1d, UnitPrefix.Milli).GetMeter(UnitPrefix.Milli), DoubleDelta);
        }
        
        [TestMethod]
        public void TestChange()
        {
            Assert.AreEqual(100d, Length.FromMeter(0.1d).GetMeter(UnitPrefix.Milli), DoubleDelta);
            Assert.AreEqual(0.1d, Length.FromMeter(10d, UnitPrefix.Milli).GetMeter(UnitPrefix.Deci), DoubleDelta);
        }
    }
}