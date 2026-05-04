using Agri.Processing.Domain.Common;
using FluentAssertions;

namespace Agri.Processing.Domain.Tests;

public class ThresholdPolicyTests
{
    private readonly ThresholdPolicy _policy = new(Min: 10, WarningLow: 20, WarningHigh: 30, Max: 40);

    [Theory]
    [InlineData(9.9, SensorStatus.TooLow)]
    [InlineData(10, SensorStatus.WarningLow)]
    [InlineData(19.9, SensorStatus.WarningLow)]
    [InlineData(20, SensorStatus.Ok)]
    [InlineData(30, SensorStatus.Ok)]
    [InlineData(30.1, SensorStatus.WarningHigh)]
    [InlineData(40, SensorStatus.WarningHigh)]
    [InlineData(40.1, SensorStatus.TooHigh)]
    public void Evaluate_ShouldReturnCorrectStatus_ForGivenValue(double value, SensorStatus expectedStatus)
    {
        // Act
        var status = _policy.Evaluate(value);

        // Assert
        status.Should().Be(expectedStatus);
    }
}
