using Microsoft.EntityFrameworkCore;
using BlazorWebAppMovies.Models;

namespace BlazorWebAppMovies.Data;

/// <summary>
/// Create default data in the <see cref="Movie"/> model / database table.
/// </summary>
public static class SeedData
{
    private const string SciFiGenre = "Sci-fi (Cyberpunk)";

    /// <summary>
    /// Set up default seed data if no Movies are present.
    /// </summary>
    /// <param name="serviceProvider">A service provider to work with.</param>
    /// <exception cref="InvalidOperationException">Thrown if the database context or the Movie data set are null.</exception>
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new BlazorWebAppMoviesContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<BlazorWebAppMoviesContext>>());

        if (context == null || context.Movie == null)
        {
            throw new InvalidOperationException(
                "Null BlazorWebAppMoviesContext or Movie DbSet");
        }

        if (context.Movie.Any())
        {
            return;
        }

        context.Movie.AddRange(
            new Movie
            {
                Title = "Mad Max",
                ReleaseDate = new DateOnly(1979, 4, 12),
                Genre = SciFiGenre,
                Price = 2.51M,
            },
            new Movie
            {
                Title = "The Road Warrior",
                ReleaseDate = new DateOnly(1981, 12, 24),
                Genre = SciFiGenre,
                Price = 2.78M,
            },
            new Movie
            {
                Title = "Mad Max: Beyond Thunderdome",
                ReleaseDate = new DateOnly(1985, 7, 10),
                Genre = SciFiGenre,
                Price = 3.55M,
            },
            new Movie
            {
                Title = "Mad Max: Fury Road",
                ReleaseDate = new DateOnly(2015, 5, 15),
                Genre = SciFiGenre,
                Price = 8.43M,
            },
            new Movie
            {
                Title = "Furiosa: A Mad Max Saga",
                ReleaseDate = new DateOnly(2024, 5, 24),
                Genre = SciFiGenre,
                Price = 13.49M,
            });

        context.SaveChanges();
    }
}