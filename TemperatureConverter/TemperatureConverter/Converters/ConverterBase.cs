using TemperatureConverter.Enums;
using TemperatureConverter.Exceptions;

namespace TemperatureConverter.Converters;

public abstract class ConverterBase
{
    private readonly TemperatureScale _targetTemperature;

    protected abstract Dictionary<TemperatureScale, Func<double, double>> ConvertFunctionBySupportedTemperature { get; }

    protected ConverterBase(TemperatureScale targetTemperature)
    {
        _targetTemperature = targetTemperature;
    }

    public double Convert(double temperature, TemperatureScale sourceTemperature)
    {
        Validate(sourceTemperature);
        var convertedTemperature = ConvertFunctionBySupportedTemperature[sourceTemperature](temperature);
        return Math.Round(convertedTemperature, 2);
    }

    private void Validate(TemperatureScale sourceTemperature)
    {
        if (sourceTemperature == _targetTemperature)
        {
            throw new ValidationException("Converting to the same temperature is rather pointless, no?");
        }

        if (!ConvertFunctionBySupportedTemperature.ContainsKey(sourceTemperature))
        {
            throw new ValidationException($"Conversion from {sourceTemperature} to {_targetTemperature} is not supported");
        }
    }
}