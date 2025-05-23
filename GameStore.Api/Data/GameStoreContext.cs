using System;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options)
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Adventure" },
            new Genre { Id = 3, Name = "Kids & Family" },
            new Genre { Id = 4, Name = "Strategy" },
            new Genre { Id = 5, Name = "Fight" },
            new Genre { Id = 6, Name = "Horror" },
            new Genre { Id = 7, Name = "Puzzle" },
            new Genre { Id = 8, Name = "Racing" },
            new Genre { Id = 9, Name = "Shooter" },
            new Genre { Id = 10, Name = "Simulation" }
        );
    }
}
