using Agri.Contracts.V1;
using Agri.Processing.Application.Abstraction;
using Agri.Processing.Application.Temperature;
using FluentAssertions;
using Moq;

namespace Agri.Test.Agri.Processing.Application.Tests;

public class ProcessTemperatureCommandHandlerTests
{
    private readonly Mock<IBusService> _busServiceMock;
    private readonly ProcessTemperatureCommandHandler _handler;

    public ProcessTemperatureCommandHandlerTests()
    {
        _busServiceMock = new Mock<IBusService>();
        _handler = new ProcessTemperatureCommandHandler(_busServiceMock.Object);
    }

    [Fact]
    public async Task Handle_Should_PublishTemperatureProcessedIntegrationEvent_WhenCommandIsValid()
    {
        // Arrange
        var command = new ProcessTemperatureCommand("test-sensor", 25.0, DateTime.UtcNow);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _busServiceMock.Verify(
            bus => bus.PublishAsync(It.Is<TemperatureProcessedIntegrationEvent>(
                e => e.SensorCode == command.SensorCode &&
                     e.Value == command.Value &&
                     !string.IsNullOrEmpty(e.Status)),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenTemperatureIsInvalid()
    {
        // Arrange
        var command = new ProcessTemperatureCommand("test-sensor", -300, DateTime.UtcNow); // Invalid temperature

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        _busServiceMock.Verify(
            bus => bus.PublishAsync(It.IsAny<TemperatureProcessedIntegrationEvent>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
