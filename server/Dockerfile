# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /home/app

COPY Directory.Build.props .

COPY ./Dictionaries/GuessTheWord.TextFileDictionary/GuessTheWord.TextFileDictionary.csproj ./Dictionaries/GuessTheWord.TextFileDictionary/
COPY ./GuessTheWord.Abstractions/GuessTheWord.Abstractions.csproj ./GuessTheWord.Abstractions/
COPY ./Games/GuessTheWord.GuessGame/GuessTheWord.GuessGame.csproj ./Games/GuessTheWord.GuessGame/
COPY ./Games/GuessTheWord.TryGuessGame/GuessTheWord.TryGuessGame.csproj ./Games/GuessTheWord.TryGuessGame/
COPY ./Games/GuessTheWord.Words/GuessTheWord.Words.csproj ./Games/GuessTheWord.Words/
COPY ./GuessTheWord.Engine/GuessTheWord.Engine.csproj ./GuessTheWord.Engine/
COPY ./GuessTheWord.Api.Web/GuessTheWord.Api.Web.csproj ./GuessTheWord.Api.Web/
COPY ./GuessTheWord.sln .

RUN dotnet restore GuessTheWord.sln

COPY . .

RUN dotnet publish -c Release ./GuessTheWord.sln -o /publish/


FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS final

WORKDIR /publish
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "GuessTheWord.Api.Web.dll"]