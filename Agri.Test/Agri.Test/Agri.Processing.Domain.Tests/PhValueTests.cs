using Agri.Processing.Domain.Ph;
using Agri.Shared;
using FluentAssertions;
using System;

namespace Agri.Processing.Domain.Tests;

public class PhValueTests
{
    private const string SensorCode = "test-sensor";
    private readonly DateTime _occuredOn = DateTime.UtcNow;

    [Theory]
    [InlineData(7.0)]
    [InlineData(0.0)]
    [InlineData(14.0)]
    public void Create_ShouldSucceed_WithValidValue(double value)
    {
        // Act
        var result = PhValue.Create(SensorCode, value, _occuredOn);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(value);
        result.Value.SensorCode.Should().Be(SensorCode);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(14.1)]
    public void Create_ShouldFail_WhenValueIsOutOfRange(double invalidValue)
    {
        // Act
        var result = PhValue.Create(SensorCode, invalidValue, _occuredOn);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(PhErrors.InvalidRange);
    }
}
