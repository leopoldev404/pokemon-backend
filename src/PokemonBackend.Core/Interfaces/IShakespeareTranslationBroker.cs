namespace PokemonBackend.Core.Interfaces;

public interface IShakespeareTranslationBroker
{
    ValueTask<string> GetShakespeareTranslation(string originalText);
}