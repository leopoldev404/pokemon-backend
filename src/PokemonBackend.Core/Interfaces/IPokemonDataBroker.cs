namespace PokemonBackend.Core.Interfaces;

public interface IPokemonDataBroker
{
    ValueTask<string> GetPokemonSpeciesUrl(string pokemon);
    ValueTask<string> GetPokemonSpeciesDescription(string pokemonSpeciesUrl);
}