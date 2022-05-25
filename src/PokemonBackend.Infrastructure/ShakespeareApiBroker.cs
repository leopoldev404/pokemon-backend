using System.Text.Json.Nodes;
using PokemonBackend.Core.Interfaces;

namespace PokemonBackend.Infrastructure;

public class ShakespeareApiBroker : BaseApiBroker, IShakespeareTranslationBroker
{
    public ShakespeareApiBroker(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory) { }

    public async ValueTask<string> GetShakespeareTranslation(string originalText)
    {
        JsonNode? shakespeareTranslationData =
            await GetAsync<JsonNode>("shakespeareApiClient", $"?text={originalText}");

        return shakespeareTranslationData?["contents"]?["translated"]?.ToString();
    }
}