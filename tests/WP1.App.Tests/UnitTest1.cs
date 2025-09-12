using WP1.App.Services;

namespace WP1.App.Tests;

public class AppServiceTests
{
    [Fact]
    public void GetWelcomeMessage_ShouldReturnWelcomeText()
    {
        // Arrange
        var appService = new AppService();

        // Act
        var result = appService.GetWelcomeMessage();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains("WP1 Deployment Example", result);
    }

    [Fact]
    public void IsHealthy_ShouldReturnTrue()
    {
        // Arrange
        var appService = new AppService();

        // Act
        var result = appService.IsHealthy();

        // Assert
        Assert.True(result);
    }
}