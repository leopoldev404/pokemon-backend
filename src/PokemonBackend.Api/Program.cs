using PokemonBackend.Core.Interfaces;
using PokemonBackend.Core.Services;
using PokemonBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

CreatePokemonHttpClient();
CreateShakespeareHttpClient();

builder.Services.AddSingleton<IPokemonService, PokemonService>();
builder.Services.AddSingleton<IPokemonDataBroker, PokemonApiBroker>();
builder.Services.AddSingleton<IShakespeareTranslationBroker, ShakespeareApiBroker>();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(cors =>
    cors.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.MapControllers();
app.MapHealthChecks("/health");
await app.RunAsync();

void CreatePokemonHttpClient() =>
    builder.Services.AddHttpClient("pokemonApiClient", httpClient =>
    {
        httpClient.BaseAddress = new Uri(builder.Configuration["pokemonApi"]);
        httpClient.DefaultRequestVersion = new Version("2.0");
    });

void CreateShakespeareHttpClient() =>
    builder.Services.AddHttpClient("shakespeareApiClient", httpClient =>
    {
        httpClient.BaseAddress = new Uri(builder.Configuration["shakespeareApi"]);
        httpClient.DefaultRequestVersion = new Version("2.0");
    });

public partial class Program {}