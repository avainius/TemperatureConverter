using TemperatureConverter.Enums;

namespace TemperatureConverter.Converters
{
    public class ToCelsiusConverter : ConverterBase
    {
        public ToCelsiusConverter() : base(Temperature.Celsius) { }

        protected override Dictionary<Temperature, Func<double, double>> ConvertFunctionBySupportedTemperature => new()
        {
            { Temperature.Fahrenheit, FromFahrenheit },
            { Temperature.Kelvin, FromKelvin }
        };

        protected double FromFahrenheit(double fahrenheit)
        {
            var celsius = (fahrenheit - 32) * 5 / 9;
            return celsius;
        }

        protected double FromKelvin(double kelvin)
        {
            var celsius = kelvin - 273.15;
            return celsius;
        }
    }
}