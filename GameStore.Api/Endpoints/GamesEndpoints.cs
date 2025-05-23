using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        //GET /games
        group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Games
                                                                    .Include(game => game.Genre)
                                                                    .Select(game => game.ToGameSummaryDto())
                                                                    .AsNoTracking().ToListAsync());

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

        }).WithName(GetGameEndpointName);

        //POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });


        // PUT /games/{id}
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);

            // If the game is not found, return a 404 response
            if (existingGame == null)
            {
                return Results.NotFound();
            }

            // Update the game entity with the new values
            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntityUpdate(id));
            await dbContext.SaveChangesAsync();

            // Return No Content (204) to indicate the update was successful
            return Results.NoContent();
        });

        //DELETE /games/{id}
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var removedCount = await dbContext.Games
                                .Where(game => game.Id == id)
                                .ExecuteDeleteAsync();

            if (removedCount == 0)
            {
                return Results.NotFound(); // Return 404 if no game was found with the given id
            }

            return Results.NoContent(); // Return 204 when the deletion is successful
        });


        return group;
    }
}
