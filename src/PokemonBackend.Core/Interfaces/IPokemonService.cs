using PokemonBackend.Core.Models;

namespace PokemonBackend.Core.Interfaces;

public interface IPokemonService
{
    ValueTask<PokemonApiResponse> GetPokemonShakespearTranslation(string pokemon);
}