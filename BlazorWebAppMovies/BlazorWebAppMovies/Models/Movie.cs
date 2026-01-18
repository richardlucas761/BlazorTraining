using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppMovies.Models;

/// <summary>
/// Information about a Movie.
/// </summary>
public class Movie
{
    [Key]
    /// <summary>
    /// Unique identifier of this Movie.
    /// </summary>
    /// <remarks>This was 'Id' in the sample code but an identifier that includes the class / table name is preferable.</remarks>
    public int MovieId { get; set; }

    [MaxLength(255)]
    /// <summary>
    /// Title of the Movie.
    /// </summary>
    /// <remarks>The sample code has this property as nullable but a Movie must have a title.</remarks>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// When this Movie was released.
    /// </summary>
    public DateOnly ReleaseDate { get; set; }

    [MaxLength(50)]
    /// <summary>
    /// Genre of the Movie.
    /// </summary>
    /// <remarks>The sample code had this property as nullable but lets force this to be entered instead.</remarks>
    public string Genre { get; set; } = string.Empty;

    /// <summary>
    /// Currency cost of this Movie.
    /// </summary>
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
}
