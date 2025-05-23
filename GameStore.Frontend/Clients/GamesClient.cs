using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> games =
    [
        new GameSummary
                {
                Id = 1,
                Name = "Street Fighter III",
                Genre = "Fighting",
                Price = 19.99M,
                ReleaseDate = new DateOnly(2012, 7, 15)
                },
                new GameSummary
                {
                Id = 2,
                Name = "The Legend of Zelda",
                Genre = "Adventure",
                Price = 59.99M,
                ReleaseDate = new DateOnly(2017, 3, 3)
                },
                new GameSummary
                {
                Id = 3,
                Name = "Super Mario Odyssey",
                Genre = "Platformer",
                Price = 49.99M,
                ReleaseDate = new DateOnly(2017, 10, 28)
                }
    ];

    private readonly Genre[] genres = new GenresClient().GetGenres();

    public GameSummary[] GetGames()
    {
        return [.. games];
    }

    public void AddGame(GameDetails game)
    {
        // Check if GenreId is null or empty (considering both cases)
        if (string.IsNullOrWhiteSpace(game.GenreId))
        {
            game.GenreId = "Unknown"; // Set to "Unknown" if it's null or empty
        }

        // If GenreId is not "Unknown", try to fetch the genre
        if (game.GenreId != "Unknown")
        {
            // Make sure to parse the GenreId and find the corresponding genre
            if (int.TryParse(game.GenreId, out int genreId))
            {
                var genre = genres.SingleOrDefault(genre => genre.Id == genreId);

                // If the genre is found, proceed
                if (genre != null)
                {
                    var gameSummary = new GameSummary
                    {
                        Id = games.Count + 1,
                        Name = game.Name,
                        Genre = genre.Name,
                        Price = game.Price,
                        ReleaseDate = game.ReleaseDate
                    };
                    games.Add(gameSummary);
                }
            }
            else
            {
                // Handle the case when the genre is unknown, if needed
                var gameSummary = new GameSummary
                {
                    Id = games.Count + 1,
                    Name = game.Name,
                    Genre = game.GenreId, // Use "Unknown" as the genre name
                    Price = game.Price,
                    ReleaseDate = game.ReleaseDate
                };
                games.Add(gameSummary);
            }
        }

    }
}
