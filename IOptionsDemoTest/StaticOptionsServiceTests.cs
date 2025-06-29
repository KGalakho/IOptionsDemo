// File: IOptionsDemo.Tests/Services/StaticOptionsServiceTests.cs
using IOptionsDemo.Configuration;
using IOptionsDemo.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace IOptionsDemo.Tests.Services;

public class StaticOptionsServiceTests
{
    [Fact]
    public void GetMessage_ReturnsMessage_WhenFeatureEnabled()
    {
        // Arrange
        var options = Options.Create(new FeatureOptions
        {
            Enabled = true,
            Message = "Feature is enabled!"
        });
        var service = new StaticOptionsService(options);

        // Act
        var result = service.GetMessage();

        // Assert
        Assert.Equal("Feature is enabled!", result);
    }

    [Fact]
    public void GetMessage_ReturnsDisabled_WhenFeatureIsDisabled()
    {
        // Arrange
        var options = Options.Create(new FeatureOptions
        {
            Enabled = false,
            Message = "Should not be shown"
        });
        var service = new StaticOptionsService(options);

        // Act
        var result = service.GetMessage();

        // Assert
        Assert.Equal("Feature disabled", result);
    }
}
