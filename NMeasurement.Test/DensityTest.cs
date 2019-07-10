using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Densities;
using NMeasurement.Types.Lengths;
using NMeasurement.Types.Masses;

namespace NMeasurement.Test
{
    [TestClass]
    public class DensityTest
    {
        private const double DoubleDelta = 0.0001d;

        [TestMethod]
        public void TestDensityValue()
        {
            Assert.AreEqual(10d, Density.FromGramPerCubicMeter(10d).GramPerCubicMeter, DoubleDelta);
            Assert.AreEqual(10d, Density.FromAtomMassPerCubicMeter(10d).AtomMassPerCubicMeter, DoubleDelta);
        }

        [TestMethod]
        public void TestDensityConvert()
        {
            Assert.AreEqual(0.01d, Density.FromGramPerCubicMeter(10d).GramPerLitre, DoubleDelta);

            var unit = Density.Unit.CreateUnit((Mass.Unit.Gram, null), (Length.Unit.Feet, null));
            Assert.AreEqual(0.2831684659199085d, Density.FromGramPerCubicMeter(10d).GetValue(unit), DoubleDelta);
            
            unit = Density.Unit.CreateUnit((Mass.Unit.Gram, null), (Length.Unit.Cubic.Litre, null));
            Assert.AreEqual(0.01d, Density.FromGramPerCubicMeter(10d).GetValue(unit), DoubleDelta);
        }

        [TestMethod]
        public void TestDensityCalculation()
        { 
            Assert.AreEqual(-4.99d, (Density.FromGramPerCubicMeter(10d) - Density.FromGramPerLitre(5d)).GramPerLitre, DoubleDelta);
            Assert.AreEqual(5.01d, (Density.FromGramPerCubicMeter(10d) + Density.FromGramPerLitre(5d)).GramPerLitre, DoubleDelta);
        }

        [TestMethod]
        public void TestCrossCalculation()
        {
            Assert.AreEqual(10d, (Mass.FromGram(20d) / CubicLength.FromMeter(2d)).GramPerCubicMeter);
        }
    }
}