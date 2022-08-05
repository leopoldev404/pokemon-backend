using PokemonBackend.Core.Interfaces;
using PokemonBackend.Core.Models;

namespace PokemonBackend.Core.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonDataBroker pokemonDataBroker;
    private readonly IShakespeareTranslationBroker shakespearTranslatorBroker;

    public PokemonService(
        IPokemonDataBroker pokemonDataBroker,
        IShakespeareTranslationBroker shakespearTranslatorBroker)
    {
        this.pokemonDataBroker = pokemonDataBroker;
        this.shakespearTranslatorBroker = shakespearTranslatorBroker;
    }

    public async ValueTask<PokemonApiResponse> GetPokemonShakespearTranslation(string pokemon)
    {
        string pokemonSpeciesUrl = await pokemonDataBroker.GetPokemonSpeciesUrl(pokemon);

        if (pokemonSpeciesUrl == null)
        {
            return null;
        }
        
        string pokemonSpeciesDescription = await pokemonDataBroker
            .GetPokemonSpeciesDescription(GetSpeciesUrlId(pokemonSpeciesUrl));
            
        string shakespeareDescriptionTranslation = await shakespearTranslatorBroker
            .GetShakespeareTranslation(pokemonSpeciesDescription) ?? pokemonSpeciesDescription;

        PokemonApiResponse pokemonApiResponse = new(pokemon, shakespeareDescriptionTranslation);
        return pokemonApiResponse;
    }

    private string GetSpeciesUrlId(string pokemonSpeciesUrl)
    {
        string[] parameters = pokemonSpeciesUrl.Split('/');
        return parameters[parameters.Length - 2];
    }
}