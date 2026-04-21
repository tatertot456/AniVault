# AniVault 🗃

A personal anime and manga tracking web application built with Blazor Server and SQL Server. Search for anime using the Jikan API, add them to your vault, and track your watch/read status, ratings, and notes — all in a dark cinematic interface. Supports multiple users with isolated vaults.

---

## Features

### Pages
- **Home** — Hero splash with live vault stats (total, watched, favorites), currently watching section, and recently added grid
- **Vault** — Full card grid with search, sort, Anime/Manga toggle, status filter chips with counts, and genre mood filter
- **Favorites** — Starred anime with sort options and spotlight modal
- **Stats** — 12 stat cards, colored status breakdown bars, genre cloud, and top rated list
- **Activity Log** — Timeline of all vault entries grouped by date with status, rating, liked, and notes
- **Add Anime** — Centered search powered by Jikan API with grid results and full add panel
- **Login** — Email and password sign in
- **Register** — New account creation

### Core Features
- **Multi-Status Support** — Assign multiple statuses to a single title (e.g. both Watched and Read for a series with an anime and manga)
- **User Accounts** — Each user has their own isolated vault; entries are never shared between accounts
- **Auth Protection** — All pages require login; unauthenticated users are redirected to the login page
- **Global Navbar Search** — Live search across your entire vault from any page with dropdown results
- **Spotlight Modal** — Click any card to preview details, synopsis, genre tags, and ratings without leaving the page
- **Card Hover Info** — Hover over any card to see description and liked status
- **Inline Editing** — Update status, rating, liked, favorite, and notes directly from the detail page
- **Random Picker** — 🎲 button picks a random anime from your Plan to Watch/Read list
- **Anime/Manga Split** — Toggle between All, Anime, and Manga on the Vault page
- **Genre Mood Filter** — Filter the vault by genre chips
- **Status Pills** — Color-coded status indicators throughout; multiple pills shown when multiple statuses are assigned
- **Delete Confirmation** — Safe removal with a confirmation modal
- **Smart Back Button** — Returns you to the correct page depending on where you navigated from
- **Favorites Sorting** — Sort favorites by recently added, A→Z, my rating, or MAL score
- **Custom Cursor** — Gold dot and ring cursor
- **Toast Notifications** — Feedback toasts for adding, editing, and removing entries

### Status System
| Status | Color | Type |
|---|---|---|
| Watched | Teal | Anime |
| Watching | Blue | Anime |
| Plan to Watch | Amber | Anime |
| Read | Blue-Gray | Manga |
| Reading | Purple | Manga |
| Plan to Read | Amber | Manga |
| Dropped | Red | Both |
| On-Hold | Orange | Both |

Multiple statuses can be assigned to the same title. For example, a title with both an anime and manga adaptation can be marked as both Watched and Read simultaneously.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | Blazor Server (.NET 10) |
| Backend | ASP.NET Core |
| Auth | ASP.NET Core Identity |
| Database | SQL Server (local) |
| ORM | Entity Framework Core 10 |
| External API | Jikan API v4 (MyAnimeList) |
| IDE | Visual Studio Community 2026 |
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
    │   ├── AnimeStatus.cs              # EF Core model — maps to dbo.AnimeStatus
    │   └── AnimeTrackerContext.cs      # IdentityDbContext
    ├── Services/
    │   ├── AnimeService.cs             # CRUD operations scoped to logged-in user
    │   ├── JikanService.cs             # Jikan API search and metadata fetch
    │   └── ToastService.cs             # In-app toast notifications
    ├── Components/
    │   ├── App.razor                   # CascadingAuthenticationState wrapper
    │   ├── Routes.razor
    │   ├── _Imports.razor
    │   ├── Layout/
    │   │   └── MainLayout.razor        # Navbar, shell, toast container, sign out button
    │   ├── Shared/
    │   │   └── NavSearch.razor         # Global navbar search component
    │   └── Pages/
    │       ├── Home.razor              # Hero splash, currently watching, recently added
    │       ├── Vault.razor             # Full grid with search, sort, filters
    │       ├── AnimeDetail.razor       # Full entry view with inline editing
    │       ├── Favorites.razor         # Starred anime with sort options
    │       ├── Stats.razor             # Stats dashboard
    │       ├── ActivityLog.razor       # Timeline activity log
    │       ├── AddAnime.razor          # Jikan search + add to vault
    │       ├── Login.razor             # Sign in page
    │       └── Register.razor          # Account creation page
    └── wwwroot/
        ├── app.css                     # Global dark cinematic styles
        ├── favicon.svg                 # Gold diamond AniVault icon
        └── js/
            └── app.js                  # Custom cursor
```

---

## Database Setup

The app connects to a local SQL Server instance. Run EF Core migrations to set up the schema — do **not** manually create tables.

### 1. Create the database in SSMS

```sql
CREATE DATABASE AnimeTracker;
```

### 2. Update the connection string

In `appsettings.json`, update the server name to match your local SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AnimeTracker;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Apply migrations

In Visual Studio Package Manager Console:

```
Update-Database
```

This creates all tables including `dbo.Anime`, `dbo.AnimeStatus`, and the full ASP.NET Core Identity tables (`AspNetUsers`, `AspNetRoles`, etc.).

### Database Schema

**dbo.Anime** — core entry table with a `UserId` foreign key linking each entry to its owner in `AspNetUsers`.

**dbo.AnimeStatus** — many-to-one status table allowing multiple statuses per anime entry. Each row has an `AnimeId` and a `StatusType` string. A unique constraint prevents duplicate status types per entry.

---

## Getting Started

### Prerequisites

- [Visual Studio Community 2026](https://visualstudio.microsoft.com/vs/community/) with ASP.NET workload
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer edition)
- [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

### Running the app

1. Clone the repository
2. Create the `AnimeTracker` database in SSMS
3. Update `appsettings.json` with your SQL Server instance name
4. Open `AniVault.sln` in Visual Studio
5. Run `Update-Database` in Package Manager Console
6. Hit **F5** or click the green **https** play button
7. Navigate to `/register` to create your account
8. Start adding anime from the **＋ Add Anime** page

The app will open in your browser at `https://localhost:{port}`.

---

## External API

AniVault uses the [Jikan API v4](https://docs.api.jikan.moe/) — a free, unofficial MyAnimeList API. No API key is required. The app respects Jikan's rate limit with a 1 second delay between requests.

---

## Branch Strategy

| Branch | Purpose |
|---|---|
| `main` | Stable, production-ready code |
| `develop` | Active development |
| `feature/*` | Individual features branched from develop |

New features are developed on `feature/` branches, merged into `develop` when complete, and merged into `main` when stable.

---

## Potential Future Features

- DevMenu — admin dashboard for bulk editing and database health checks
- Import from MyAnimeList
- PWA support for mobile installation
- Public shareable profile page
- Edit from the spotlight modal

---

## License

This is a personal project and is not licensed for distribution.
