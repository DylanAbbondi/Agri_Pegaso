using Agri.Processing.Domain.Temperature;
using Agri.Shared;
using FluentAssertions;
using System;

namespace Agri.Processing.Domain.Tests;

public class TemperatureValueTests
{
    private const string SensorCode = "test-sensor";
    private readonly DateTime _occuredOn = DateTime.UtcNow;

    [Theory]
    [InlineData(25.0)]
    [InlineData(-10.0)]
    [InlineData(50.0)]
    public void Create_ShouldSucceed_WithValidValue(double value)
    {
        // Act
        var result = TemperatureValue.Create(SensorCode, value, _occuredOn);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Celsius.Should().Be(value);
        result.Value.SensorCode.Should().Be(SensorCode);
    }

    [Theory]
    [InlineData(-50.1)] // Below lower bound
    [InlineData(60.1)]  // Above upper bound
    public void Create_ShouldFail_WhenValueIsOutOfRange(double invalidValue)
    {
        // Act
        var result = TemperatureValue.Create(SensorCode, invalidValue, _occuredOn);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TemperatureErrors.InvalidTemperature);
    }
}
