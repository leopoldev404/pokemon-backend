using System.Net;
using System.Text.Json;

namespace PokemonBackend.Infrastructure;

public class BaseApiBroker
{
    private readonly IHttpClientFactory httpClientFactory;

    public BaseApiBroker(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async ValueTask<T?> GetAsync<T>(string httpClientFactoryName, string url)
    {
        HttpClient httpClient = httpClientFactory.CreateClient(httpClientFactoryName);
        HttpResponseMessage response = await httpClient.GetAsync(url);

        return response.StatusCode == HttpStatusCode.OK
            ? await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync())
            : default;
    }
}