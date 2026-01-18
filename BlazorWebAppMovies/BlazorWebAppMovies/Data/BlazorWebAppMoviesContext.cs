using Microsoft.EntityFrameworkCore;

namespace BlazorWebAppMovies.Data
{
    /// <summary>
    /// Database context.
    /// </summary>
    /// <param name="options"></param>
    public class BlazorWebAppMoviesContext(DbContextOptions<BlazorWebAppMoviesContext> options) : DbContext(options)
    {
        /// <summary>
        /// A data set for Movies.
        /// </summary>
        public DbSet<Models.Movie> Movie { get; set; } = default!;
    }
}
