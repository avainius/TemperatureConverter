using TemperatureConverter.Converters;
using TemperatureConverter.Enums;
using TemperatureConverter.Exceptions;

namespace TemperatureConverter;

public static class TemperatureConverter
{
    private static readonly Dictionary<TemperatureScale, ConverterBase> TemperatureConvertersByTemperature = new()
    {
        { TemperatureScale.Celsius, new ToCelsiusConverter() },
        { TemperatureScale.Fahrenheit, new ToFahrenheitConverter() },
        { TemperatureScale.Kelvin, new ToKelvinConverter() },
    };

    public static double Convert(double temperature, TemperatureScale convertFrom, TemperatureScale convertTo)
    {
        Validate(convertFrom, convertTo);
        var convertedTemperature = TemperatureConvertersByTemperature[convertTo].Convert(temperature, convertFrom);

        return convertedTemperature;
    }

    private static void Validate(TemperatureScale convertFrom, TemperatureScale convertTo)
    {
        if (!Enum.IsDefined(convertTo))
        {
            throw new ValidationException($"Target scale {convertTo} is not defined");
        }

        if (!Enum.IsDefined(convertFrom))
        {
            throw new ValidationException($"Source scale {convertFrom} is not defined");
        }

        if (!TemperatureConvertersByTemperature.ContainsKey(convertTo))
        {
            throw new ValidationException($"Converting to {convertTo} is not supported");
        }
    }
}