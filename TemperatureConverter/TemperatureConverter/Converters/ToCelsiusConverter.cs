using TemperatureConverter.Enums;

namespace TemperatureConverter.Converters;

public class ToCelsiusConverter : ConverterBase
{
    public ToCelsiusConverter() : base(TemperatureScale.Celsius)
    {
    }

    protected override Dictionary<TemperatureScale, Func<double, double>> ConvertFunctionBySupportedTemperature =>
        new()
        {
            { TemperatureScale.Fahrenheit, FromFahrenheit },
            { TemperatureScale.Kelvin, FromKelvin }
        };

    private static double FromFahrenheit(double fahrenheit)
    {
        var celsius = (fahrenheit - 32) * 5 / 9;
        return celsius;
    }

    private static double FromKelvin(double kelvin)
    {
        var celsius = kelvin - 273.15;
        return celsius;
    }
}