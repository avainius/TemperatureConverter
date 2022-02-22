using TemperatureConverter.Enums;

namespace TemperatureConverter.Converters;

public class ToKelvinConverter : ConverterBase
{
    public ToKelvinConverter() : base(TemperatureScale.Kelvin)
    {
    }

    protected override Dictionary<TemperatureScale, Func<double, double>> ConvertFunctionBySupportedTemperature =>
        new()
        {
            { TemperatureScale.Fahrenheit, FromFahrenheit },
            { TemperatureScale.Celsius, FromCelsius }
        };

    private static double FromFahrenheit(double fahrenheit)
    {
        var kelvin = (fahrenheit + 459.67) * (5.0 / 9.0);
        return kelvin;
    }

    private static double FromCelsius(double celsius)
    {
        var kelvin = celsius + 273.15;
        return kelvin;
    }
}