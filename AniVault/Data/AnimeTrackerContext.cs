using Microsoft.EntityFrameworkCore;

namespace AniVault.Data;

public class AnimeTrackerContext : DbContext
{
    public AnimeTrackerContext(DbContextOptions<AnimeTrackerContext> options) : base(options) { }

    public DbSet<AnimeEntry> Anime { get; set; }
    public DbSet<AnimeStatus> AnimeStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AnimeStatus>(entity =>
        {
            entity.ToTable("AnimeStatus");

            entity.HasOne(s => s.Anime)
                  .WithMany(a => a.Statuses)
                  .HasForeignKey(s => s.AnimeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(s => new { s.AnimeId, s.StatusType })
                  .IsUnique();
        });

        modelBuilder.Entity<AnimeEntry>(entity =>
        {
            entity.ToTable("Anime");
        });
    }
}