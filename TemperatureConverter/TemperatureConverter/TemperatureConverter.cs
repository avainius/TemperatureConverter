using TemperatureConverter.Converters;
using TemperatureConverter.Enums;
using TemperatureConverter.Exceptions;

namespace TemperatureConverter
{
    public static class TemperatureConverter
	{
		private static Dictionary<Temperature, ConverterBase> _temperatureConvertersByTemperature = new Dictionary<Temperature, ConverterBase>()
		{
			{ Temperature.Celsius, new ToCelsiusConverter() },
			{ Temperature.Fahrenheit, new ToFahrenheitConverter() },
			{ Temperature.Kelvin, new ToKelvinConverter() },
		};

		public static double Convert(double temperature, Temperature convertFrom, Temperature convertTo)
		{
			Validate(convertFrom, convertTo);
			var result = _temperatureConvertersByTemperature[convertTo].Convert(temperature, convertFrom);

			return result;
		}

		private static void Validate(Temperature convertFrom, Temperature convertTo)
        {
			if (!Enum.IsDefined(convertTo))
			{
				throw new TemperatureConverterValidationException($"Conversion target {convertTo} is not a supported");
			}

			if (!Enum.IsDefined(convertFrom))
			{
				throw new TemperatureConverterValidationException($"Conversion source {convertTo} is not a supported");
			}

			if (!_temperatureConvertersByTemperature.ContainsKey(convertTo))
			{
				throw new TemperatureConverterValidationException($"Converting to {convertTo} is not supported");
			}
		}
	}
}