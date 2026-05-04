using Agri.Contracts.V1;
using Agri.Processing.Application.Abstraction;
using Agri.Processing.Application.Humidity;
using FluentAssertions;
using Moq;

namespace Agri.Test.Agri.Processing.Application.Tests;

public class ProcessHumidityCommandHandlerTests
{
    private readonly Mock<IBusService> _busServiceMock;
    private readonly ProcessHumidityCommandHandler _handler;

    public ProcessHumidityCommandHandlerTests()
    {
        _busServiceMock = new Mock<IBusService>();
        _handler = new ProcessHumidityCommandHandler(_busServiceMock.Object);
    }

    [Fact]
    public async Task Handle_Should_PublishHumidityProcessedIntegrationEvent_WhenCommandIsValid()
    {
        // Arrange
        var command = new ProcessHumidityCommand("test-sensor", 50.0, DateTime.UtcNow);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _busServiceMock.Verify(
            bus => bus.PublishAsync(It.Is<HumidityProcessedIntegrationEvent>(
                e => e.SensorCode == command.SensorCode &&
                     e.Value == command.Value &&
                     !string.IsNullOrEmpty(e.Status)),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenHumidityIsInvalid()
    {
        // Arrange
        var command = new ProcessHumidityCommand("test-sensor", 110, DateTime.UtcNow); // Invalid humidity

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        _busServiceMock.Verify(
            bus => bus.PublishAsync(It.IsAny<HumidityProcessedIntegrationEvent>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
