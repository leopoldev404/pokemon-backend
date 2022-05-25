using Microsoft.AspNetCore.Mvc;
using PokemonBackend.Core.Interfaces;
using PokemonBackend.Core.Models;

namespace PokemonBackend.Api.Controllers;

[ApiController]
[Route("pokemon")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        this.pokemonService = pokemonService;
    }

    [HttpGet("{pokemon}")]
    public async ValueTask<ActionResult<PokemonApiResponse>> GetPokemonShakespearTranslation(string pokemon)
    {
        PokemonApiResponse response =
            await pokemonService.GetPokemonShakespearTranslation(pokemon);
            
        return response == null ? NotFound() : Ok(response);
    }
}
