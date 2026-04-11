using AniVault.Data;
using Microsoft.EntityFrameworkCore;

namespace AniVault.Services
{
    public class AnimeService
    {
        private readonly IDbContextFactory<AnimeTrackerContext> _contextFactory;

        public AnimeService(IDbContextFactory<AnimeTrackerContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<AnimeEntry>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Anime
                .OrderByDescending(a => a.DateAdded)
                .ToListAsync();
        }

        public async Task<AnimeEntry?> GetByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Anime.FindAsync(id);
        }

        public async Task<List<AnimeEntry>> GetFavoritesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Anime
                .Where(a => a.IsFavorite)
                .OrderByDescending(a => a.DateAdded)
                .ToListAsync();
        }

        public async Task AddAsync(AnimeEntry entry)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Anime.Add(entry);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimeEntry entry)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Anime.Update(entry);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var entry = await context.Anime.FindAsync(id);
            if (entry != null)
            {
                context.Anime.Remove(entry);
                await context.SaveChangesAsync();
            }
        }

        public async Task ToggleFavoriteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var entry = await context.Anime.FindAsync(id);
            if (entry != null)
            {
                entry.IsFavorite = !entry.IsFavorite;
                await context.SaveChangesAsync();
            }
        }
    }
}