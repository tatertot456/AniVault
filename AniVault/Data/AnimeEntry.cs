using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AniVault.Data;

public class AnimeEntry
{
    public int Id { get; set; }

    [MaxLength(500)]
    public string Title { get; set; } = "";

    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    [MaxLength(5000)]
    public string? Description { get; set; }

    [MaxLength(500)]
    public string? Genre { get; set; }

    public int? EpisodeCount { get; set; }
    public decimal? AverageRating { get; set; }
    public decimal? MyRating { get; set; }
    public bool? Liked { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;

    [MaxLength(2000)]
    public string? Notes { get; set; }

    [MaxLength(500)]
    public string? TrailerUrl { get; set; }

    public bool IsFavorite { get; set; } = false;

    [MaxLength(100)]
    public string? AiringStatus { get; set; }

    public DateTime? LastModified { get; set; }
    public int? MalId { get; set; }

    [MaxLength(50)]
    public string? MediaType { get; set; }

    [MaxLength(200)]
    public string? Studio { get; set; }

    [MaxLength(500)]
    public string? TitleEnglish { get; set; }

    [MaxLength(500)]
    public string? TitleJapanese { get; set; }

    public int? Year { get; set; }

    // Navigation property for statuses
    public List<AnimeStatus> Statuses { get; set; } = new();

}