using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Energies;
using NMeasurement.Types.Expeditions;
using NMeasurement.Types.Forces;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Masses;

namespace NMeasurement.Test
{
    [TestClass]
    public class ForceTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestForceValue()
        {
            Assert.AreEqual(10d, Force.FromNewton(10d).Newton, DoubleDelta);
            Assert.AreEqual(10d, Force.FromKilogramMeterPerSquareSecond(10d).KilogramMeterPerSquareSecond, DoubleDelta);
        }

        [TestMethod]
        public void TestForceConvert()
        {
            Assert.AreEqual(10d, Force.FromNewton(10d).KilogramMeterPerSquareSecond, DoubleDelta);
        }

        [TestMethod]
        public void TestForceCalculation()
        {
            Assert.AreEqual(5d, (Force.FromNewton(10d) - Force.FromKilogramMeterPerSquareSecond(5d)).Newton, DoubleDelta);
            Assert.AreEqual(15d, (Force.FromNewton(10d) + Force.FromKilogramMeterPerSquareSecond(5d)).Newton, DoubleDelta);
        }

        [TestMethod]
        public void TestCrossCalculation()
        {
            Assert.AreEqual(30d, (Expedition.FromMetersPerSquareSecond(10d) * Mass.FromGram(3, UnitPrefix.Kilo)).Newton);
            Assert.AreEqual(2d, (Force.FromNewton(10d) / Mass.FromGram(5, UnitPrefix.Kilo)).MetersPerSquareSecond);
            Assert.AreEqual(10d, (Energy.FromJoule(20d) / Length.FromMeter(2d)).Newton);
            Assert.AreEqual(10d, (Force.FromNewton(20d) / SquareLength.FromMeter(2d)).Pascal);
        }
    }
}