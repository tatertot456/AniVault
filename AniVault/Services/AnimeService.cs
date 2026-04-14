using AniVault.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AniVault.Services
{
    public class AnimeService
    {
        private readonly IDbContextFactory<AnimeTrackerContext> _contextFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnimeService(
            IDbContextFactory<AnimeTrackerContext> contextFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _contextFactory = contextFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetUserId() =>
            _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<List<AnimeEntry>> GetAllAsync()
        {
            var userId = GetUserId();
            using var context = _contextFactory.CreateDbContext();
            return await context.Anime
                .Include(a => a.Statuses)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.DateAdded)
                .ToListAsync();
        }

        public async Task<AnimeEntry?> GetByIdAsync(int id)
        {
            var userId = GetUserId();
            using var context = _contextFactory.CreateDbContext();
            return await context.Anime
                .Include(a => a.Statuses)
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        }

        public async Task<List<AnimeEntry>> GetFavoritesAsync()
        {
            var userId = GetUserId();
            using var context = _contextFactory.CreateDbContext();
            return await context.Anime
                .Include(a => a.Statuses)
                .Where(a => a.IsFavorite && a.UserId == userId)
                .OrderByDescending(a => a.DateAdded)
                .ToListAsync();
        }

        public async Task AddAsync(AnimeEntry entry)
        {
            entry.UserId = GetUserId();
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
            var userId = GetUserId();
            using var context = _contextFactory.CreateDbContext();
            var entry = await context.Anime
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (entry != null)
            {
                context.Anime.Remove(entry);
                await context.SaveChangesAsync();
            }
        }

        public async Task ToggleFavoriteAsync(int id)
        {
            var userId = GetUserId();
            using var context = _contextFactory.CreateDbContext();
            var entry = await context.Anime
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (entry != null)
            {
                entry.IsFavorite = !entry.IsFavorite;
                await context.SaveChangesAsync();
            }
        }

        public async Task SetStatusesAsync(int animeId, List<string> statusTypes)
        {
            using var context = _contextFactory.CreateDbContext();
            var existing = await context.AnimeStatuses
                .Where(s => s.AnimeId == animeId)
                .ToListAsync();
            context.AnimeStatuses.RemoveRange(existing);
            foreach (var status in statusTypes)
            {
                context.AnimeStatuses.Add(new AnimeStatus
                {
                    AnimeId = animeId,
                    StatusType = status
                });
            }
            await context.SaveChangesAsync();
        }
    }
}