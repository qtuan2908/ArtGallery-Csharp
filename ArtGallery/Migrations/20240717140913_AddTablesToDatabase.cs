using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGallery.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Customers_WinnerId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                table: "Auctions",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_WinnerId",
                table: "Auctions",
                newName: "IX_Auctions_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Customers_CustomerId",
                table: "Auctions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Customers_CustomerId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Auctions",
                newName: "WinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_CustomerId",
                table: "Auctions",
                newName: "IX_Auctions_WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Customers_WinnerId",
                table: "Auctions",
                column: "WinnerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
