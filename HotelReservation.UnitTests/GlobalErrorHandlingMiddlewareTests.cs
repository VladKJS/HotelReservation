using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;


namespace HotelReservation.UnitTests;

public class GlobalErrorHandlingIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly HttpContext _httpContext;
    public GlobalErrorHandlingIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _httpContext = new DefaultHttpContext().Request.HttpContext;
    }

    [Fact]
    public async Task Get_UnknownRoute_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync("/unknown-route");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
