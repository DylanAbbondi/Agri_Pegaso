using Agri.Processing.Application.Common;

namespace Agri.Processing.Application.Ph;

public record ProcessPhCommand(
    string SensorCode,
    double Value,
    DateTime OccuredOnUtc) : ICommand;
