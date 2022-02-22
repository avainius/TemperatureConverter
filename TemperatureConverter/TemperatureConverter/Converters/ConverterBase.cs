using TemperatureConverter.Enums;
using TemperatureConverter.Exceptions;

namespace TemperatureConverter.Converters
{
    public abstract class ConverterBase
    {
        private readonly Temperature _targetTemperature;

        protected abstract Dictionary<Temperature, Func<double, double>> ConvertFunctionBySupportedTemperature { get; }

        public ConverterBase(Temperature targetTemperature)
        {
            _targetTemperature = targetTemperature;
        }

        public double Convert(double temperature, Temperature sourceTemperature)
        {
            Validate(sourceTemperature);
            var convertedTemperature = ConvertFunctionBySupportedTemperature[sourceTemperature](temperature);
            return convertedTemperature;
        }

        private void Validate(Temperature sourceTemperature)
        {
            if (sourceTemperature == _targetTemperature)
            {
                throw new TemperatureConverterValidationException("Converting to the same temperature is rather pointless, no?");
            }

            if (!ConvertFunctionBySupportedTemperature.ContainsKey(sourceTemperature))
            {
                throw new TemperatureConverterValidationException($"Conversion from {sourceTemperature} to {_targetTemperature} is not supported");
            }
        }
    }
}