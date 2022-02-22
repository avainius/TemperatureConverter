using TemperatureConverter.Enums;

namespace TemperatureConverter.Converters;

public class ToFahrenheitConverter : ConverterBase
{
    public ToFahrenheitConverter() : base(TemperatureScale.Fahrenheit)
    {
    }

    protected override Dictionary<TemperatureScale, Func<double, double>> ConvertFunctionBySupportedTemperature =>
        new()
        {
            { TemperatureScale.Kelvin, FromKelvin },
            { TemperatureScale.Celsius, FromCelsius }
        };

    private static double FromKelvin(double kelvin)
    {
        var fahrenheit = (kelvin - 273.15) * 9 / 5 + 32;
        return fahrenheit;
    }

    private static double FromCelsius(double celsius)
    {
        var fahrenheit = celsius * 9 / 5 + 32;
        return fahrenheit;
    }
}