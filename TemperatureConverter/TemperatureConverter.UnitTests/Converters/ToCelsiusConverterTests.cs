using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemperatureConverter.Converters;
using TemperatureConverter.Enums;
using TemperatureConverter.Exceptions;

namespace TemperatureConverter.UnitTests.Converters;

[TestClass]
public class ToCelsiusConverterTests
{
    private ToCelsiusConverter _converter = null!;
    private Fixture _fixture = null!;

    [TestInitialize]
    public void SetUp()
    {
        _converter = new ToCelsiusConverter();
        _fixture = new Fixture();
    }

    [TestMethod]
    [DataRow(-4, -20)]
    [DataRow(203, 95)]
    [DataRow(0.5, -17.5)]
    public void Convert_When_FahrenheitValueProvided_Expect_CelsiusReturned(double temperature, double expectedValue)
    {
        var actualValue = _converter.Convert(temperature, TemperatureScale.Fahrenheit);
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    [DataRow(-4, -277.15)]
    [DataRow(14, -259.15)]
    [DataRow(0.5, -272.65)]
    public void Convert_When_KelvinValueProvided_Expect_CelsiusReturned(double temperature, double expectedValue)
    {
        var actualValue = _converter.Convert(temperature, TemperatureScale.Kelvin);
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void Convert_When_SameTemperatureScaleProvided_Expect_ExceptionIsThrown()
    {
        var expectedExceptionMessage = "Converting to the same temperature is rather pointless, no?";
        var actualException = Assert.ThrowsException<TemperatureConverterValidationException>(
            () => _converter.Convert(_fixture.Create<double>(), TemperatureScale.Celsius));

        Assert.AreEqual(expectedExceptionMessage, actualException.Message);
    }

    [TestMethod]
    public void Convert_When_UnsupportedTemperatureScaleProvided_Expect_ExceptionIsThrown()
    {
        var expectedExceptionMessage = $"Conversion from {TemperatureScale.UnsupportedTemperatureScale} to {TemperatureScale.Celsius} is not supported";
        var actualException = Assert.ThrowsException<TemperatureConverterValidationException>(
            () => _converter.Convert(_fixture.Create<double>(), TemperatureScale.UnsupportedTemperatureScale));

        Assert.AreEqual(expectedExceptionMessage, actualException.Message);
    }
}
