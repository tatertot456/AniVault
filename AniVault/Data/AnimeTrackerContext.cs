using Microsoft.EntityFrameworkCore;

namespace AniVault.Data
{
    public class AnimeTrackerContext : DbContext
    {
        public AnimeTrackerContext(DbContextOptions<AnimeTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<AnimeEntry> Anime { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimeEntry>(entity =>
            {
                entity.ToTable("Anime", "dbo");

                entity.Property(e => e.WatchStatus)
                    .HasDefaultValue("Plan to Watch");

                entity.Property(e => e.DateAdded)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.IsFavorite)
                    .HasDefaultValue(false);
            });
        }
    }
}