using deploymentfall25.Controllers;
using deploymentfall25.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // added for DefaultHttpContext
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace deploymentfall25.Tests.Controllers;

public class HomeControllerTests
{
    private static HomeController CreateController()
    {
        // Use NullLogger to avoid needing a real logger implementation
        var logger = NullLogger<HomeController>.Instance;
        return new HomeController(logger);
    }

    [Fact]
    public void Index_Returns_View()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName); // uses default view name
    }

    [Fact]
    public void Privacy_Returns_View()
    {
        var controller = CreateController();
        var result = controller.Privacy();
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Error_Returns_View_With_ErrorViewModel()
    {
        var controller = CreateController();
        // Provide HttpContext so HttpContext.TraceIdentifier is not null-accessed
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        var result = controller.Error();
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ErrorViewModel>(viewResult.Model);
        Assert.False(string.IsNullOrWhiteSpace(model.RequestId));
    }
}
