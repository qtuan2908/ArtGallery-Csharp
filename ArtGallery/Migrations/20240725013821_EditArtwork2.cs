using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGallery.Migrations
{
    /// <inheritdoc />
    public partial class EditArtwork2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExhibitionId",
                table: "Artworks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_ExhibitionId",
                table: "Artworks",
                column: "ExhibitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Exhibitions_ExhibitionId",
                table: "Artworks",
                column: "ExhibitionId",
                principalTable: "Exhibitions",
                principalColumn: "ExhibitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Exhibitions_ExhibitionId",
                table: "Artworks");

            migrationBuilder.DropIndex(
                name: "IX_Artworks_ExhibitionId",
                table: "Artworks");

            migrationBuilder.DropColumn(
                name: "ExhibitionId",
                table: "Artworks");
        }
    }
}
