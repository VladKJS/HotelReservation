using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;


namespace HotelReservation.UnitTests;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
}

