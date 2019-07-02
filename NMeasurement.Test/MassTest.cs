using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Densities;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Masses;

namespace NMeasurement.Test
{
    [TestClass]
    public class MassTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestMassValue()
        {
            Assert.AreEqual(10d, Mass.FromGram(10d).Gram, DoubleDelta);
            Assert.AreEqual(10d, Mass.FromTonne(10d).Tonne, DoubleDelta);

            Assert.AreEqual(10d, Mass.FromCentner(10d).Centner, DoubleDelta);
            Assert.AreEqual(10d, Mass.FromMeterCentner(10d).MeterCentner, DoubleDelta);
            Assert.AreEqual(10d, Mass.FromOunce(10d).Ounce, DoubleDelta);
            Assert.AreEqual(10d, Mass.FromEnglishPound(10d).EnglishPound, DoubleDelta);
            Assert.AreEqual(10d, Mass.FromPound(10d).Pound, DoubleDelta);
            
            Assert.AreEqual(10d, Mass.FromAtomMass(10d).AtomMass, DoubleDelta);
        }

        [TestMethod]
        public void TestMassConvert()
        {
            Assert.AreEqual(0.625d, Mass.FromOunce(10d).EnglishPound, DoubleDelta);
            Assert.AreEqual(0.5d, Mass.FromCentner(10d).Tonne, DoubleDelta);
        }

        [TestMethod]
        public void TestMassCalculation()
        {
            Assert.AreEqual(4.394172d, (Mass.FromEnglishPound(10d) - Mass.FromOunce(5d)).GetGram(UnitPrefix.Kilo), DoubleDelta);
            Assert.AreEqual(4.6776675d, (Mass.FromEnglishPound(10d) + Mass.FromOunce(5d)).GetGram(UnitPrefix.Kilo), DoubleDelta);
        }

        [TestMethod]
        public void TestMassCrossCalculation()
        {
            Assert.AreEqual(10d, (Mass.FromGram(20d) / CubicLength.FromMeter(2d)).GramPerCubicMeter);
        }

        [TestMethod]
        public void TestMassCondition()
        {
            Assert.IsTrue(Mass.FromCentner(1d) == Mass.FromCentner(1d));
            Assert.IsTrue(Mass.FromCentner(1d) == Mass.FromMeterCentner(0.5d));
            Assert.IsFalse(Mass.FromCentner(1d) == Mass.FromGram(1d, UnitPrefix.Kilo));
            
            Assert.IsTrue(Mass.FromCentner(1d) != Mass.FromOunce(1d));
            Assert.IsFalse(Mass.FromCentner(1d) != Mass.FromCentner(1d));
            Assert.IsFalse(Mass.FromCentner(1d) != Mass.FromMeterCentner(0.5d));
            
            Assert.IsTrue(Mass.FromGram(1d) < Mass.FromGram(1d, UnitPrefix.Kilo));
            Assert.IsTrue(Mass.FromGram(1d) <= Mass.FromGram(1d, UnitPrefix.Kilo));
            Assert.IsTrue(Mass.FromGram(1000d) <= Mass.FromGram(1d, UnitPrefix.Kilo));
            Assert.IsFalse(Mass.FromTonne(1d) < Mass.FromGram(1d, UnitPrefix.Kilo));
            Assert.IsFalse(Mass.FromTonne(1d) <= Mass.FromGram(1d, UnitPrefix.Kilo));
            
            Assert.IsTrue(Mass.FromGram(1d, UnitPrefix.Kilo) > Mass.FromGram(1d));
            Assert.IsTrue(Mass.FromGram(1d, UnitPrefix.Kilo) >= Mass.FromGram(1d));
            Assert.IsTrue(Mass.FromGram(1d, UnitPrefix.Kilo) >= Mass.FromGram(1000d));
            Assert.IsFalse(Mass.FromGram(1d, UnitPrefix.Kilo) > Mass.FromTonne(1d));
            Assert.IsFalse(Mass.FromGram(1d, UnitPrefix.Kilo) >= Mass.FromTonne(1d));
        }
    }
}