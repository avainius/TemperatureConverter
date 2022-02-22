using TemperatureConverter.Enums;

namespace TemperatureConverter.Converters
{
    public class ToFahrenheitConverter : ConverterBase
    {
        public ToFahrenheitConverter() : base(Temperature.Fahrenheit) { }

        protected override Dictionary<Temperature, Func<double, double>> ConvertFunctionBySupportedTemperature => new()
        {
            { Temperature.Kelvin, FromKelvin },
            { Temperature.Celsius, FromCelsius }
        };

        protected double FromKelvin(double kelvin)
        {
            var fahrenheit = (kelvin - 273.15) * 9 / 5 + 32;
            return fahrenheit;
        }

        protected double FromCelsius(double celsius)
        {
            var fahrenheit = celsius * 9 / 5 + 32;
            return fahrenheit;
        }
    }
}