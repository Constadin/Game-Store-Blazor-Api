using System;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto createGameDto)
    {
        return new Game()
        {
            Name = createGameDto.Name,
            GenreId = createGameDto.GenreId,
            Price = createGameDto.Price,
            ReleaseDate = createGameDto.ReleaseDate
        };
    }
    public static Game ToEntityUpdate(this UpdateGameDto updateGameDto, int Id)
    {
        return new Game()
        {
            Id = Id,
            Name = updateGameDto.Name,
            GenreId = updateGameDto.GenreId,
            Price = updateGameDto.Price,
            ReleaseDate = updateGameDto.ReleaseDate
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new GameSummaryDto(
            game.Id,
            game.Name,
            game.Genre?.Name ?? "Unknown",
            game.Price,
            game.ReleaseDate
        );
    }
    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        return new GameDetailsDto(game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate);
    }
}
