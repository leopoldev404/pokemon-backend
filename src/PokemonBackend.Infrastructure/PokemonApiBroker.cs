using System.Text.Json.Nodes;
using PokemonBackend.Core.Interfaces;

namespace PokemonBackend.Infrastructure;

public class PokemonApiBroker : BaseApiBroker, IPokemonDataBroker
{
    public PokemonApiBroker(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory) {}

    public async ValueTask<string> GetPokemonSpeciesUrl(string pokemon)
    {
        JsonNode? pokemonData = await GetAsync<JsonNode>("pokemonApiClient", $"pokemon/{pokemon}");
        return pokemonData?["species"]?["url"]?.ToString();
    }
    
    public async ValueTask<string> GetPokemonSpeciesDescription(string pokemonSpeciesId)
    {
        JsonNode? pokemonSpeciesData = 
            await GetAsync<JsonNode>("pokemonApiClient", $"pokemon-species/{pokemonSpeciesId}");

        string? pokemonSpeciesDescription =
            pokemonSpeciesData?["flavor_text_entries"]?[0]?["flavor_text"]?.ToString();

        return pokemonSpeciesDescription?.Replace('\n', ' ')?.Replace('\f', ' ');
    }
}