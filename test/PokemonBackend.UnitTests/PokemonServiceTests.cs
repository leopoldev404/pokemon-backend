using Moq;
using PokemonBackend.Core.Interfaces;
using PokemonBackend.Core.Models;
using PokemonBackend.Core.Services;
using Xunit;

namespace PokemonBackend.UnitTests;

public class PokemonServiceTests
{
    private readonly IPokemonService pokemonService;
    private readonly Mock<IPokemonDataBroker> pokemonDataBrokerMock;
    private readonly Mock<IShakespeareTranslationBroker> shakespeareTranslationBrokerMock;

    public PokemonServiceTests()
    {
        pokemonDataBrokerMock = new();
        shakespeareTranslationBrokerMock = new();

        pokemonService = new PokemonService(
            pokemonDataBrokerMock.Object,
            shakespeareTranslationBrokerMock.Object);
    }

    [Fact]
    public async void GivenRightPokemonNameShouldRetrieveAllInfo()
    {
        string pokemon = "pikachu";
        string expectedSpeciesUrl = "piackhu/Species/Urls/34/";
        string speciesUrlId = "34";
        string expectedSpeciesDescription = "thunder";

        pokemonDataBrokerMock.Setup(pokemonDataBrokerMock =>
            pokemonDataBrokerMock.GetPokemonSpeciesUrl(pokemon))
                .ReturnsAsync(expectedSpeciesUrl);

        pokemonDataBrokerMock.Setup(pokemonDataBrokerMock =>
            pokemonDataBrokerMock.GetPokemonSpeciesDescription(speciesUrlId))
                .ReturnsAsync(expectedSpeciesDescription);

        shakespeareTranslationBrokerMock.Setup(shakespeareTranslationBrokerMock =>
            shakespeareTranslationBrokerMock.GetShakespeareTranslation(expectedSpeciesDescription))
                .ReturnsAsync(It.IsAny<string>());

        PokemonApiResponse pokemonApiResponse =
            await pokemonService.GetPokemonShakespearTranslation(pokemon);

        pokemonDataBrokerMock.Verify(pokemonDataBrokerMock =>
            pokemonDataBrokerMock.GetPokemonSpeciesUrl(pokemon), Times.Once());

        pokemonDataBrokerMock.Verify(pokemonDataBrokerMock =>
            pokemonDataBrokerMock.GetPokemonSpeciesDescription(speciesUrlId), Times.Once());

        shakespeareTranslationBrokerMock.Verify(shakespeareTranslationBrokerMock =>
            shakespeareTranslationBrokerMock.GetShakespeareTranslation(expectedSpeciesDescription), Times.Once());
    }

    [Fact]
    public async void GivenWrongPokemonNameShouldExitAndNotMakingOtherCalls()
    {
        string pokemon = "sdgsgsdffgsf";

        pokemonDataBrokerMock.Setup(pokemonDataBrokerMock =>
            pokemonDataBrokerMock.GetPokemonSpeciesUrl(pokemon))
                .ReturnsAsync((string)null);

        PokemonApiResponse pokemonApiResponse =
            await pokemonService.GetPokemonShakespearTranslation(pokemon);

        pokemonDataBrokerMock.Verify(pokemonDataBrokerMock =>
            pokemonDataBrokerMock.GetPokemonSpeciesUrl(pokemon), Times.Once());

        pokemonDataBrokerMock.VerifyNoOtherCalls();
        shakespeareTranslationBrokerMock.VerifyNoOtherCalls();
    }
}