using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemperatureConverter.Converters;
using TemperatureConverter.Enums;
using TemperatureConverter.Exceptions;

namespace TemperatureConverter.UnitTests.Converters;

[TestClass]
public class ToFahrenheitConverterTests
{
    private ToFahrenheitConverter _converter = null!;
    private Fixture _fixture = null!;

    [TestInitialize]
    public void SetUp()
    {
        _converter = new ToFahrenheitConverter();
        _fixture = new Fixture();
    }

    [TestMethod]
    [DataRow(-5, 23)]
    [DataRow(0, 32)]
    [DataRow(10.5, 50.9)]
    public void Convert_When_CelsiusValueProvided_Expect_FahrenheitReturned(double temperature, double expectedValue)
    {
        var actualValue = _converter.Convert(temperature, TemperatureScale.Celsius);
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    [DataRow(-5, -468.67)]
    [DataRow(0, -459.67)]
    [DataRow(500.5, 441.23)]
    public void Convert_When_KelvinValueProvided_Expect_FahrenheitReturned(double temperature, double expectedValue)
    {
        var actualValue = _converter.Convert(temperature, TemperatureScale.Kelvin);
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void Convert_When_SameTemperatureScaleProvided_Expect_ExceptionIsThrown()
    {
        var expectedExceptionMessage = "Converting to the same temperature is rather pointless, no?";
        var actualException = Assert.ThrowsException<ValidationException>(
            () => _converter.Convert(_fixture.Create<double>(), TemperatureScale.Fahrenheit));

        Assert.AreEqual(expectedExceptionMessage, actualException.Message);
    }

    [TestMethod]
    public void Convert_When_UnsupportedTemperatureScaleProvided_Expect_ExceptionIsThrown()
    {
        var expectedExceptionMessage = $"Conversion from {TemperatureScale.UnsupportedTemperatureScale} to {TemperatureScale.Fahrenheit} is not supported";
        var actualException = Assert.ThrowsException<ValidationException>(
            () => _converter.Convert(_fixture.Create<double>(), TemperatureScale.UnsupportedTemperatureScale));

        Assert.AreEqual(expectedExceptionMessage, actualException.Message);
    }
}
