FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY "GuessTheWord.sln" "GuessTheWord.sln"

COPY "APIGateway/GuessTheWord.API.Gateway/aggregator/GuessTheWord.API.Gateway.csproj" "APIGateway/GuessTheWord.API.Gateway/aggregator/GuessTheWord.API.Gateway.csproj"
COPY "BuildingBlocks/EventBus/EventBus/EventBus.csproj" "BuildingBlocks/EventBus/EventBus/EventBus.csproj"
COPY "BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj" "BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj"
COPY "BuildingBlocks/EventBus/EventBus.Subscriptions.InMemory/EventBus.Subscriptions.InMemory.csproj" "BuildingBlocks/EventBus/EventBus.Subscriptions.InMemory/EventBus.Subscriptions.InMemory.csproj"
COPY "Services/GuessWord.API.Service/GuessWord.API/GuessWord.API.csproj" "Services/GuessWord.API.Service/GuessWord.API/GuessWord.API.csproj"
COPY "Services/GuessWord.Dictionary.Service/GuessWord.Dictionary/GuessWord.Dictionary.csproj" "Services/GuessWord.Dictionary.Service/GuessWord.Dictionary/GuessWord.Dictionary.csproj"
COPY "Services/Identity.API.Service/Identity.API/Identity.API.csproj" "Services/Identity.API.Service/Identity.API/Identity.API.csproj"
COPY "Services/TryGuessTheWord.API.Service/TryGuessTheWord.API/TryGuessTheWord.API.csproj" "Services/TryGuessTheWord.API.Service/TryGuessTheWord.API/TryGuessTheWord.API.csproj"
COPY "Web/WebSPA/WebSPA.csproj" "Web/WebSPA/WebSPA.csproj"
COPY "Web/WebStatus/WebStatus.csproj" "Web/WebStatus/WebStatus.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "NuGet.config"

RUN dotnet restore "GuessTheWord.sln"

COPY . .
WORKDIR /src/APIGateway/GuessTheWord.API.Gateway/aggregator
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GuessTheWord.API.Gateway.dll"]
