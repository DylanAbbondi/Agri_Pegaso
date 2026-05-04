using Agri.Contracts.V1;
using Agri.Processing.Application.Abstraction;
using Agri.Processing.Application.Ph;
using FluentAssertions;
using Moq;

namespace Agri.Test.Agri.Processing.Application.Tests;

public class ProcessPhCommandHandlerTests
{
    private readonly Mock<IBusService> _busServiceMock;
    private readonly ProcessPhCommandHandler _handler;

    public ProcessPhCommandHandlerTests()
    {
        _busServiceMock = new Mock<IBusService>();
        _handler = new ProcessPhCommandHandler(_busServiceMock.Object);
    }

    [Fact]
    public async Task Handle_Should_PublishPhProcessedIntegrationEvent_WhenCommandIsValid()
    {
        // Arrange
        var command = new ProcessPhCommand("test-sensor", 7.0, DateTime.UtcNow);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _busServiceMock.Verify(
            bus => bus.PublishAsync(It.Is<PhProcessedIntegrationEvent>(
                e => e.SensorCode == command.SensorCode &&
                     e.Value == command.Value &&
                     !string.IsNullOrEmpty(e.Status)),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenPhIsInvalid()
    {
        // Arrange
        var command = new ProcessPhCommand("test-sensor", 15, DateTime.UtcNow); // Invalid pH

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        _busServiceMock.Verify(
            bus => bus.PublishAsync(It.IsAny<PhProcessedIntegrationEvent>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
