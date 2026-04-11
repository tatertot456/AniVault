using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AniVault.Data
{
    [Table("Anime", Schema = "dbo")]
    public class AnimeEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? CoverImageUrl { get; set; }

        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Genre { get; set; }

        public int? EpisodeCount { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal? AverageRating { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal? MyRating { get; set; }

        public bool? Liked { get; set; }

        [MaxLength(50)]
        public string WatchStatus { get; set; } = "Plan to Watch";

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public string? Notes { get; set; }

        [MaxLength(500)]
        public string? TrailerUrl { get; set; }

        public bool IsFavorite { get; set; } = false;
    }
}
