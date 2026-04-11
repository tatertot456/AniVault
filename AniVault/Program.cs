using AniVault.Data;
using AniVault.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Database
builder.Services.AddDbContextFactory<AnimeTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// App Services
builder.Services.AddScoped<AnimeService>();
builder.Services.AddSingleton<ToastService>();

// Jikan HTTP Client
builder.Services.AddHttpClient<JikanService>(client =>
{
    client.BaseAddress = new Uri("https://api.jikan.moe/v4/");
    client.DefaultRequestHeaders.Add("User-Agent", "AniVault/1.0");
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<AniVault.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();