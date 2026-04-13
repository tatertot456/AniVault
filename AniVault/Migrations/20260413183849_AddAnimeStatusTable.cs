using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniVault.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimeStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EpisodeCount = table.Column<int>(type: "int", nullable: true),
                    AverageRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MyRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Liked = table.Column<bool>(type: "bit", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    TrailerUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false),
                    AiringStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: true),
                    MediaType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Studio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TitleEnglish = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TitleJapanese = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    StatusType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeStatus_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeStatus_AnimeId_StatusType",
                table: "AnimeStatus",
                columns: new[] { "AnimeId", "StatusType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeStatus");

            migrationBuilder.DropTable(
                name: "Anime");
        }
    }
}
