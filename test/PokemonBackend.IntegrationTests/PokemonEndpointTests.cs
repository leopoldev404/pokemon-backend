using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace backendmvpsat.tests.integration;

public class PokemonEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient httpClient;

    public PokemonEndpointTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        this.httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async ValueTask GetServiceHealthShouldReturnOkAsync()
    {
        var response = await httpClient.GetAsync("/health");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Theory]
    [InlineData("pokemon/pikachu")]
    public async ValueTask GetExistingPokemonShouldReturnTranslatedDescription(string pokemon)
    {
        HttpResponseMessage response = await httpClient.GetAsync(pokemon);
        string responseContent = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(responseContent);
    }

    [Theory]
    [InlineData("pokemon/d")]
    public async ValueTask GetNotExistingPokemonShouldReturnNotFound(string pokemon)
    {
        HttpResponseMessage response = await httpClient.GetAsync(pokemon);
        string responseContent = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}