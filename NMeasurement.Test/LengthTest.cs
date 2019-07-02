using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Lengths;

namespace NMeasurement.Test
{
    [TestClass]
    public class LengthTest
    {
        private const double DoubleDelta = 0.0001d;

        #region Tests for 1D

        [TestMethod]
        public void TestLengths1DValue()
        {
            Assert.AreEqual(10d, Length.FromMeter(10d).Meter, DoubleDelta);

            Assert.AreEqual(10d, Length.FromMile(10d).Mile, DoubleDelta);
            Assert.AreEqual(10d, Length.FromSeaMile(10d).SeaMile, DoubleDelta);
            Assert.AreEqual(10d, Length.FromFeet(10d).Feet, DoubleDelta);
            Assert.AreEqual(10d, Length.FromYard(10d).Yard, DoubleDelta);
        }

        [TestMethod]
        public void TestLengths1DConvert()
        {
            Assert.AreEqual(10.936132983377078d, Length.FromMeter(10d).Yard, DoubleDelta);
        }

        [TestMethod]
        public void TestLengths1DCalculation()
        {
            Assert.AreEqual(10d, (Length.FromMeter(0.3d, UnitPrefix.Centi) + Length.FromMeter(0.007d)).GetMeter(UnitPrefix.Milli), DoubleDelta);
            Assert.AreEqual(10d, (Length.FromMeter(1.7d, UnitPrefix.Centi) - Length.FromMeter(0.007d)).GetMeter(UnitPrefix.Milli), DoubleDelta);
        }

        [TestMethod]
        public void TestLengths1DTo2DCalculation()
        {
            Assert.AreEqual(8d, (Length.FromMeter(0.2d, UnitPrefix.Centi) * Length.FromMeter(0.004d)).GetMeter(UnitPrefix.Milli), DoubleDelta);
        }

        #endregion

        #region Tests for 2D

        [TestMethod]
        public void TestLengths2DValue()
        {
            Assert.AreEqual(10d, SquareLength.FromMeter(10d).Meter, DoubleDelta);

            Assert.AreEqual(10d, SquareLength.FromMile(10d).Mile, DoubleDelta);
            Assert.AreEqual(10d, SquareLength.FromSeaMile(10d).SeaMile, DoubleDelta);
            Assert.AreEqual(10d, SquareLength.FromFeet(10d).Feet, DoubleDelta);
            Assert.AreEqual(10d, SquareLength.FromYard(10d).Yard, DoubleDelta);

            Assert.AreEqual(10d, SquareLength.FromAr(10d).Ar, DoubleDelta);
            Assert.AreEqual(10d, SquareLength.FromHectar(10d).Hectar, DoubleDelta);
        }

        [TestMethod]
        public void TestLengths2DConvert()
        {
            Assert.AreEqual(10d, SquareLength.FromAr(1000d).Hectar, DoubleDelta);
            Assert.AreEqual(100d, SquareLength.FromMeter(1000d).GetHectar(UnitPrefix.Milli), DoubleDelta);
        }

        [TestMethod]
        public void TestLengths2DCalculation()
        {
            Assert.AreEqual(100d, (SquareLength.FromMeter(0.3d, UnitPrefix.Centi) + SquareLength.FromMeter(0.007d, UnitPrefix.Deci)).GetMeter(UnitPrefix.Milli), DoubleDelta);
            Assert.AreEqual(100d, (SquareLength.FromMeter(1.7d, UnitPrefix.Centi) - SquareLength.FromMeter(0.007d, UnitPrefix.Deci)).GetMeter(UnitPrefix.Milli), DoubleDelta);
        }

        [TestMethod]
        public void TestLengths2DTo1DCalculation()
        {
            Assert.AreEqual(30d, (SquareLength.FromMeter(3d, UnitPrefix.Centi) / Length.FromMeter(10, UnitPrefix.Milli)).GetMeter(UnitPrefix.Milli), DoubleDelta);
        }

        [TestMethod]
        public void TestLengths2Dto3DCalculation()
        {
            Assert.AreEqual(3_000d, (SquareLength.FromMeter(3d, UnitPrefix.Centi) * Length.FromMeter(10d, UnitPrefix.Milli)).GetMeter(UnitPrefix.Milli), DoubleDelta);
            Assert.AreEqual(3_000d, (Length.FromMeter(10d, UnitPrefix.Milli) * SquareLength.FromMeter(3d, UnitPrefix.Centi)).GetMeter(UnitPrefix.Milli), DoubleDelta);
        }

        #endregion

        #region Tests for 3D

        [TestMethod]
        public void TestLengths3DValue()
        {
            Assert.AreEqual(10d, CubicLength.FromMeter(10d).Meter, DoubleDelta);

            Assert.AreEqual(10d, CubicLength.FromMile(10d).Mile, DoubleDelta);
            Assert.AreEqual(10d, CubicLength.FromSeaMile(10d).SeaMile, DoubleDelta);
            Assert.AreEqual(10d, CubicLength.FromFeet(10d).Feet, DoubleDelta);
            Assert.AreEqual(10d, CubicLength.FromYard(10d).Yard, DoubleDelta);

            Assert.AreEqual(10d, CubicLength.FromLitre(10d).Litre, DoubleDelta);

            Assert.AreEqual(10d, CubicLength.FromBarrel(10d).Barrel, DoubleDelta);
            Assert.AreEqual(10d, CubicLength.FromCup(10d).Cup, DoubleDelta);
        }

        [TestMethod]
        public void TestLengths3DConvert()
        {
            Assert.AreEqual(250d, CubicLength.FromCup(1d).GetLitre(UnitPrefix.Milli), DoubleDelta);
            Assert.AreEqual(1000d, CubicLength.FromMeter(1d, UnitPrefix.Deci).GetLitre(UnitPrefix.Milli), DoubleDelta);
            Assert.AreEqual(100_000d, CubicLength.FromMeter(0.1d).GetLitre(UnitPrefix.Milli), DoubleDelta);
        }

        [TestMethod]
        public void TestLengths3DCalculation()
        {
            Assert.AreEqual(10000d, (CubicLength.FromMeter(3d, UnitPrefix.Centi) + CubicLength.FromMeter(0.007d, UnitPrefix.Deci)).GetMeter(UnitPrefix.Milli), DoubleDelta);
            Assert.AreEqual(10000d, (CubicLength.FromMeter(17d, UnitPrefix.Centi) - CubicLength.FromMeter(0.007d, UnitPrefix.Deci)).GetMeter(UnitPrefix.Milli), DoubleDelta);
        }

        [TestMethod]
        public void TestLengths3DTo1DCalculation()
        {
            Assert.AreEqual(300d, (CubicLength.FromMeter(3d, UnitPrefix.Centi) / SquareLength.FromMeter(10, UnitPrefix.Milli)).GetMeter(UnitPrefix.Milli), DoubleDelta);
        }

        [TestMethod]
        public void TestLengths3DTo2DCalculation()
        {
            Assert.AreEqual(30d, (CubicLength.FromMeter(3d, UnitPrefix.Centi) / Length.FromMeter(10, UnitPrefix.Centi)).GetMeter(UnitPrefix.Milli), DoubleDelta);
        }

        #endregion
    }
}