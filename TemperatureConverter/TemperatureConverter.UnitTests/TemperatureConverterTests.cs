using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TemperatureConverter.Enums;
using TemperatureConverter.Exceptions;

namespace TemperatureConverter.UnitTests;

[TestClass]
public class TemperatureConverterTests
{
    private Fixture _fixture = null!;

    [TestInitialize]
    public void SetUp()
    {
        _fixture = new Fixture();
    }

    [TestMethod]
    [DataRow(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 1, 33.8)]
    [DataRow(TemperatureScale.Celsius, TemperatureScale.Kelvin, 1, 274.15)]
    [DataRow(TemperatureScale.Kelvin, TemperatureScale.Fahrenheit, 1, -457.87)]
    [DataRow(TemperatureScale.Kelvin, TemperatureScale.Celsius, 1, -272.15)]
    [DataRow(TemperatureScale.Fahrenheit, TemperatureScale.Celsius, 1, -17.22)]
    [DataRow(TemperatureScale.Fahrenheit, TemperatureScale.Kelvin, 1, 255.93)]
    public void Convert_When_ValidConversionDataProvided_Expect_ConvertedTemperatureReturned(
        TemperatureScale convertFrom, TemperatureScale convertTo, int sourceTemperature, double expectedTemperature)
    {
        var actualValue = TemperatureConverter.Convert(sourceTemperature, convertFrom, convertTo);

        Assert.AreEqual(expectedTemperature, actualValue);
    }

    [TestMethod]
    public void Convert_When_UndefinedTemperatureSourceScaleProvided_Expect_ExceptionIsThrown()
    {
        var undefinedTemperatureValue = Enum.Parse<TemperatureScale>("-1");
        var expectedExceptionMessage = $"Temperature {undefinedTemperatureValue} is not defined";
        var actualException = Assert.ThrowsException<TemperatureConverterValidationException>(() =>
            TemperatureConverter.Convert(
                _fixture.Create<double>(),
                undefinedTemperatureValue,
                _fixture.Create<TemperatureScale>()));

        Assert.AreEqual(expectedExceptionMessage, actualException.Message);
    }

    [TestMethod]
    public void Convert_When_UndefinedTemperatureTargetScaleProvided_Expect_ExceptionIsThrown()
    {
        var undefinedTemperatureValue = Enum.Parse<TemperatureScale>("-1");
        var expectedExceptionMessage = $"Temperature {undefinedTemperatureValue} is not defined";
        var actualException = Assert.ThrowsException<TemperatureConverterValidationException>(() =>
            TemperatureConverter.Convert(
                _fixture.Create<double>(),
                _fixture.Create<TemperatureScale>(),
                undefinedTemperatureValue));

        Assert.AreEqual(expectedExceptionMessage, actualException.Message);
    }

    [TestMethod]
    public void Convert_When_UnsupportedTemperatureScaleProvided_Expect_ExceptionIsThrown()
    {
        var expectedExceptionMessage = $"Converting to {TemperatureScale.UnsupportedTemperatureScale} is not supported";
        var actualException = Assert.ThrowsException<TemperatureConverterValidationException>(() =>
            TemperatureConverter.Convert(
                _fixture.Create<double>(),
                _fixture.Create<TemperatureScale>(),
                TemperatureScale.UnsupportedTemperatureScale));

        Assert.AreEqual(expectedExceptionMessage, actualException.Message);
    }
}