using Agri.Processing.Domain.Humidity;
using Agri.Shared;
using FluentAssertions;
using System;

namespace Agri.Processing.Domain.Tests;

public class HumidityValueTests
{
    private const string SensorCode = "test-sensor";
    private readonly DateTime _occuredOn = DateTime.UtcNow;

    [Theory]
    [InlineData(50.0)]
    [InlineData(0.0)]
    [InlineData(100.0)]
    public void Create_ShouldSucceed_WithValidValue(double value)
    {
        // Act
        var result = HumidityValue.Create(SensorCode, value, _occuredOn);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Percentage.Should().Be(value);
        result.Value.SensorCode.Should().Be(SensorCode);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(100.1)]
    public void Create_ShouldFail_WhenValueIsOutOfRange(double invalidValue)
    {
        // Act
        var result = HumidityValue.Create(SensorCode, invalidValue, _occuredOn);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(HumidityErrors.InvalidPercentage);
    }
}
