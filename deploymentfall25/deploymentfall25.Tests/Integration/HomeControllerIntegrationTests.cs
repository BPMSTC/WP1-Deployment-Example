using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace deploymentfall25.Tests.Integration;

// Integration tests spin up the full in-memory host (routing, middleware, views)
// Trait Category=Integration so we can filter separately.
public class HomeControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HomeControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory; // default factory boots the real Program pipeline
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task Get_Index_ReturnsSuccessAndHtml()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var contentType = response.Content.Headers.ContentType?.MediaType;
        Assert.Equal("text/html", contentType);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task Get_Privacy_ReturnsSuccessAndHtml()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Home/Privacy");
        response.EnsureSuccessStatusCode();
        var contentType = response.Content.Headers.ContentType?.MediaType;
        Assert.Equal("text/html", contentType);
    }
}
