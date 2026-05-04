using Agri.Processing.Application.Common;

namespace Agri.Processing.Application.Humidity;

public record ProcessHumidityCommand(
    string SensorCode,
    double Value,
    DateTime OccuredOnUtc) : ICommand;
