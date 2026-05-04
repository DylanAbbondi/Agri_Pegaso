using Agri.Processing.Application.Common;

namespace Agri.Processing.Application.Temperature;

public record ProcessTemperatureCommand(
    string SensorCode,
    double Value,
    DateTime OccuredOnUtc) : ICommand;
