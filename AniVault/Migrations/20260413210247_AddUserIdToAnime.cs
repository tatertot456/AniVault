using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniVault.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToAnime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Anime",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Anime_UserId",
                table: "Anime",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anime_AspNetUsers_UserId",
                table: "Anime",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anime_AspNetUsers_UserId",
                table: "Anime");

            migrationBuilder.DropIndex(
                name: "IX_Anime_UserId",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Anime");
        }
    }
}
