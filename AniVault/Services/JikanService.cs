using System.Text.Json;
using System.Text.Json.Serialization;

namespace AniVault.Services
{
    public class JikanAnimeResult
    {
        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("title_english")]
        public string? TitleEnglish { get; set; }

        [JsonPropertyName("synopsis")]
        public string? Synopsis { get; set; }

        [JsonPropertyName("score")]
        public decimal? Score { get; set; }

        [JsonPropertyName("episodes")]
        public int? Episodes { get; set; }

        [JsonPropertyName("genres")]
        public List<JikanGenre>? Genres { get; set; }

        [JsonPropertyName("images")]
        public JikanImages? Images { get; set; }

        [JsonPropertyName("trailer")]
        public JikanTrailer? Trailer { get; set; }
    }

    public class JikanGenre
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class JikanImages
    {
        [JsonPropertyName("jpg")]
        public JikanImageSet? Jpg { get; set; }
    }

    public class JikanImageSet
    {
        [JsonPropertyName("large_image_url")]
        public string? LargeImageUrl { get; set; }
    }

    public class JikanTrailer
    {
        [JsonPropertyName("youtube_id")]
        public string? YoutubeId { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("embed_url")]
        public string? EmbedUrl { get; set; }
    }

    public class JikanService
    {
        private readonly HttpClient _http;
        private readonly ILogger<JikanService> _logger;

        public JikanService(HttpClient http, ILogger<JikanService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<JikanAnimeResult>> SearchAsync(string query)
        {
            try
            {
                await Task.Delay(1000); // Respect Jikan rate limit
                var url = $"https://api.jikan.moe/v4/anime?q={Uri.EscapeDataString(query)}&limit=8&sfw=false";

                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Jikan returned {Status} for query: {Query}",
                        response.StatusCode, query);
                    return new List<JikanAnimeResult>();
                }

                var json = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Jikan response: {Json}", json[..Math.Min(200, json.Length)]);

                var doc = JsonDocument.Parse(json);
                var dataElement = doc.RootElement.GetProperty("data");

                var results = JsonSerializer.Deserialize<List<JikanAnimeResult>>(
                    dataElement.GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return results ?? new List<JikanAnimeResult>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Jikan search failed for query: {Query}", query);
                return new List<JikanAnimeResult>();
            }
        }
    }
}