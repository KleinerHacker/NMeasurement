using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMeasurement.Types.Temperatures;

namespace NMeasurement.Test
{
    [TestClass]
    public class TemperatureTest
    {
        private const double DoubleDelta = 0.0001d;
        
        [TestMethod]
        public void TestTemperatureValue()
        {
            Assert.AreEqual(10d, Temperature.FromCelsius(10d).Celsius, DoubleDelta);
            Assert.AreEqual(10d, Temperature.FromFahrenheit(10d).Fahrenheit, DoubleDelta);
            Assert.AreEqual(10d, Temperature.FromKelvin(10d).Kelvin, DoubleDelta);
        }

        [TestMethod]
        public void TestTemperatureConvert()
        {
            Assert.AreEqual(-12.2222d, Temperature.FromFahrenheit(10d).Celsius, DoubleDelta);
            Assert.AreEqual(260.9277d, Temperature.FromFahrenheit(10d).Kelvin, DoubleDelta);
        }

        [TestMethod]
        public void TestTemperatureCalculation()
        {
            Assert.AreEqual(0d, (Temperature.FromFahrenheit(50d) - Temperature.FromKelvin(10d)).Celsius, DoubleDelta);
            Assert.AreEqual(20d, (Temperature.FromKelvin(10d) + Temperature.FromFahrenheit(50d)).Celsius, DoubleDelta);
        }
    }
}