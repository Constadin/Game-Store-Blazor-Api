using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GenresClient
{
    private readonly Genre[] genres =
    [
        new Genre { Id = 1, Name = "Action" },
        new Genre { Id = 2, Name = "Adventure" },
        new Genre { Id = 3, Name = "RPG" },
        new Genre { Id = 4, Name = "Simulation" },
        new Genre { Id = 5, Name = "Strategy" },
        new Genre { Id = 6, Name = "Sports" },
        new Genre { Id = 7, Name = "Puzzle" },
        new Genre { Id = 8, Name = "Idle" }
    ];

    public Genre[] GetGenres() => genres;
}
