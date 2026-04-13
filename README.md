# AniVault 🗃

A personal anime and manga tracking web application built with Blazor Server and SQL Server. Search for anime using the Jikan API, add them to your vault, and track your watch/read status, ratings, and notes — all in a dark cinematic interface.

---

## Features

### Pages
- **Home** — Hero splash with live vault stats (total, watched, favorites), currently watching section, and recently added grid
- **Vault** — Full card grid with search, sort, Anime/Manga toggle, status filter chips with counts, and genre mood filter
- **Favorites** — Starred anime with sort options and spotlight modal
- **Stats** — 12 stat cards, colored status breakdown bars, genre cloud, and top rated list
- **Activity Log** — Timeline of all vault entries grouped by date with status, rating, liked, and notes
- **Add Anime** — Centered search powered by Jikan API with grid results and full add panel

### Core Features
- **Global Navbar Search** — Live search across your entire vault from any page with dropdown results
- **Spotlight Modal** — Click any card to preview details, synopsis, genre tags, and ratings without leaving the page
- **Card Hover Info** — Hover over any card to see description and liked status
- **Inline Editing** — Update status, rating, liked, favorite, and notes directly from the detail page
- **Random Picker** — 🎲 button picks a random anime from your Plan to Watch/Read list
- **Anime/Manga Split** — Toggle between All, Anime, and Manga on the Vault page
- **Genre Mood Filter** — Filter the vault by genre chips
- **Status Pills** — Color-coded status indicators throughout
- **Delete Confirmation** — Safe removal with a confirmation modal
- **Smart Back Button** — Returns you to the correct page depending on where you navigated from
- **Favorites Sorting** — Sort favorites by recently added, A→Z, my rating, or MAL score
- **Custom Cursor** — Gold dot and ring cursor
- **Toast Notifications** — Feedback toasts for adding, editing, and removing entries

### Status System
| Status | Color | Type |
|---|---|---|
| Watched | Teal | Anime |
| Watching | Gold | Anime |
| Plan to Watch | Amber | Anime |
| Read | Blue | Manga |
| Reading | Purple | Manga |
| Plan to Read | Amber | Manga |
| Dropped | Red | Both |
| On-Hold | Orange | Both |

---

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | Blazor Server (.NET 10) |
| Backend | ASP.NET Core |
| Database | SQL Server (local) |
| ORM | Entity Framework Core 10 |
| External API | Jikan API v4 (MyAnimeList) |
| IDE | Visual Studio Community 2022 |
| DB Tool | SQL Server Management Studio 2022 |

---

## Project Structure

```
AniVault/
├── AniVault.sln
└── AniVault/
    ├── AniVault.csproj
    ├── Program.cs
    ├── appsettings.json
    ├── Data/
    │   ├── AnimeEntry.cs               # EF Core model — maps to dbo.Anime
    │   └── AnimeTrackerContext.cs      # DbContext
    ├── Services/
    │   ├── AnimeService.cs             # CRUD operations against SQL Server
    │   ├── JikanService.cs             # Jikan API search and metadata fetch
    │   └── ToastService.cs             # In-app toast notifications
    ├── Components/
    │   ├── App.razor
    │   ├── Routes.razor
    │   ├── _Imports.razor
    │   ├── Layout/
    │   │   └── MainLayout.razor        # Navbar, shell, toast container
    │   ├── Shared/
    │   │   └── NavSearch.razor         # Global navbar search component
    │   └── Pages/
    │       ├── Home.razor              # Hero splash, currently watching, recently added
    │       ├── Vault.razor             # Full grid with search, sort, filters
    │       ├── AnimeDetail.razor       # Full entry view with inline editing
    │       ├── Favorites.razor         # Starred anime with sort options
    │       ├── Stats.razor             # Stats dashboard
    │       ├── ActivityLog.razor       # Timeline activity log
    │       └── AddAnime.razor          # Jikan search + add to vault
    └── wwwroot/
        ├── app.css                     # Global dark cinematic styles
        ├── favicon.svg                 # Gold diamond AniVault icon
        └── js/
            └── app.js                  # Custom cursor
```

---

## Database Setup

The app connects to a local SQL Server instance. The database and table must exist before running the app — Entity Framework does **not** auto-create them.

### 1. Create the database

```sql
CREATE DATABASE AnimeTracker;
```

### 2. Create the table

```sql
USE AnimeTracker;
GO

CREATE TABLE dbo.Anime (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    Title          NVARCHAR(255) NOT NULL,
    CoverImageUrl  NVARCHAR(500) NULL,
    Description    NVARCHAR(MAX) NULL,
    Genre          NVARCHAR(255) NULL,
    EpisodeCount   INT NULL,
    AverageRating  DECIMAL(3,1) NULL,
    MyRating       DECIMAL(3,1) NULL,
    Liked          BIT NULL,
    WatchStatus    NVARCHAR(50) NOT NULL DEFAULT 'Plan to Watch',
    DateAdded      DATETIME NOT NULL DEFAULT GETDATE(),
    Notes          NVARCHAR(MAX) NULL,
    TrailerUrl     NVARCHAR(500) NULL,
    IsFavorite     BIT NOT NULL DEFAULT 0,
    CONSTRAINT CK_Anime_WatchStatus CHECK (WatchStatus IN (
        'Watched', 'Watching', 'Plan to Watch', 'Plan to Read',
        'Reading', 'Read', 'Dropped', 'On-Hold'
    ))
);
```

### 3. Update the connection string

In `appsettings.json`, update the server name to match your local SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AnimeTracker;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

## Getting Started

### Prerequisites

- [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/community/) with ASP.NET workload
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer edition)
- [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

### Running the app

1. Clone the repository
2. Set up the database using the SQL scripts above
3. Update `appsettings.json` with your SQL Server instance name
4. Open `AniVault.sln` in Visual Studio
5. Hit **F5** or click the green **https** play button

The app will open in your browser at `https://localhost:{port}`.

---

## External API

AniVault uses the [Jikan API v4](https://docs.api.jikan.moe/) — a free, unofficial MyAnimeList API. No API key is required. The app respects Jikan's rate limit with a 1 second delay between requests.

---

## Potential Future Features

- Import from MyAnimeList
- PWA support for mobile installation
- Public shareable profile page
- Edit from the spotlight modal

---

## License

This is a personal project and is not licensed for distribution.