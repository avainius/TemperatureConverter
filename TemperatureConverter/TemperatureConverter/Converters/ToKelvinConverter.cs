using TemperatureConverter.Enums;

namespace TemperatureConverter.Converters
{
    public class ToKelvinConverter : ConverterBase
    {
        public ToKelvinConverter() : base(Temperature.Kelvin) { }

        protected override Dictionary<Temperature, Func<double, double>> ConvertFunctionBySupportedTemperature => new()
        {
            { Temperature.Fahrenheit, FromFahrenheit },
            { Temperature.Celsius, FromCelsius }
        };

        protected double FromFahrenheit(double fahrenheit)
        {
            var kelvin = 273.5 + (fahrenheit - 32) * (5 / 9);
            return kelvin;
        }

        protected double FromCelsius(double celsius)
        {
            var kelvin = celsius + 273.15;
            return kelvin;
        }
    }
}