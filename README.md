# Pokemon Backend Service

## Introduction
This is a simple backend service which given a pokemon name it returns his description translated to shakespeare language

**NB: The service required [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) for being running**

## Example
Request:

`GET http://localhost:5000/pokemon/pikachu`

Response:

```
{
  "name": "pikachu",
  "translation": "At which hour several of these pokémon gather,  their electricity couldst buildeth and cause lightning storms."
}
```

## Run Backend Service

```git clone https://github.com/LeozzzDev/pokemon-backend.git```

```cd pokemon-backend/src/PokemonBackend.Api```

```dotnet run``` (it should start on port 5000)

## Integration Test Backend Service

```cd pokemon-backend/test/PokemonBackend.IntegrationTests```

```dotnet test```

## Unit Test Backend Service

```cd pokemon-backend/test/PokemonBackend.UnitTests```

```dotnet test```

## Features

- Project structure following [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) principles
- Based on .NET 6 for Long Term Support
- Resilient `HttpClient` instances using [IHttpClientFactory](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0)
- Testing: separate projects for Unit and Integration tests (libraries used: xUnit, FluentAssertions)
- Docker support: [Dockerfile](docker/dockerfile)