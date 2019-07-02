using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Forces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Pressures;

namespace NMeasurement.Test
{
    [TestClass]
    public class PressureTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestPressureValue()
        {
            Assert.AreEqual(10d, Pressure.FromPascal(10d).Pascal, DoubleDelta);
            Assert.AreEqual(10d, Pressure.FromBar(10d).Bar, DoubleDelta);
        }

        [TestMethod]
        public void TestPressureConvert()
        {
            Assert.AreEqual(0.0001d, Pressure.FromPascal(10d).Bar, DoubleDelta);
        }

        [TestMethod]
        public void TestPressureCalculation()
        {
            Assert.AreEqual(-499990d, (Pressure.FromPascal(10d) - Pressure.FromBar(5d)).Pascal, DoubleDelta);
            Assert.AreEqual(500010d, (Pressure.FromPascal(10d) + Pressure.FromBar(5d)).Pascal, DoubleDelta);
        }

        [TestMethod]
        public void TestCrossCalculation()
        {
            Assert.AreEqual(30d, (Pressure.FromPascal(10d) * SquareLength.FromMeter(3d)).Newton);
            Assert.AreEqual(0.2d, (Pressure.FromPascal(10d) / Force.FromNewton(2d)).Meter);
        }
    }
}