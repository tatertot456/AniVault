using System.ComponentModel.DataAnnotations;

namespace AniVault.Data;

public class AnimeStatus
{
    public int Id { get; set; }
    public int AnimeId { get; set; }

    [MaxLength(50)]
    public string StatusType { get; set; } = "";

    // Navigation property back to the anime
    public AnimeEntry Anime { get; set; } = null!;
}