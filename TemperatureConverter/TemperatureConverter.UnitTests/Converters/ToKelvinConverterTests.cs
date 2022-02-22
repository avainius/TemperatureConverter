using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemperatureConverter.Converters;
using TemperatureConverter.Enums;
using TemperatureConverter.Exceptions;

namespace TemperatureConverter.UnitTests.Converters;

[TestClass]
public class ToKelvinConverterTests
{
    private ToKelvinConverter _converter = null!;
    private Fixture _fixture = null!;

    [TestInitialize]
    public void SetUp()
    {
        _converter = new ToKelvinConverter();
        _fixture = new Fixture();
    }

    [TestMethod]
    [DataRow(-5, 268.15)]
    [DataRow(0, 273.15)]
    [DataRow(10.5, 283.65)]
    public void Convert_When_CelsiusValueProvided_Expect_KelvinReturned(double temperature, double expectedValue)
    {
        var actualValue = _converter.Convert(temperature, TemperatureScale.Celsius);
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    [DataRow(-5, 252.59)]
    [DataRow(0, 255.37)]
    [DataRow(500.5, 533.43)]
    public void Convert_When_FahrenheitValueProvided_Expect_KelvinReturned(double temperature, double expectedValue)
    {
        var actualValue = _converter.Convert(temperature, TemperatureScale.Fahrenheit);
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void Convert_When_SameTemperatureScaleProvided_Expect_ExceptionIsThrown()
    {
        var expectedExceptionMessage = "Converting to the same temperature is rather pointless, no?";
        var actualException = Assert.ThrowsException<TemperatureConverterValidationException>(
            () => _converter.Convert(_fixture.Create<double>(), TemperatureScale.Kelvin));

        Assert.AreEqual(expectedExceptionMessage, actualException.Message);
    }

    [TestMethod]
    public void Convert_When_UnsupportedTemperatureScaleProvided_Expect_ExceptionIsThrown()
    {
        var expectedExceptionMessage = $"Conversion from {TemperatureScale.UnsupportedTemperatureScale} to {TemperatureScale.Kelvin} is not supported";
        var actualException = Assert.ThrowsException<TemperatureConverterValidationException>(
            () => _converter.Convert(_fixture.Create<double>(), TemperatureScale.UnsupportedTemperatureScale));

        Assert.AreEqual(expectedExceptionMessage, actualException.Message);
    }
}
