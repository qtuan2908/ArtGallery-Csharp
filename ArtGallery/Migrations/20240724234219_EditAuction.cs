using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGallery.Migrations
{
    /// <inheritdoc />
    public partial class EditAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Customers_CustomerId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Auctions",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_CustomerId",
                table: "Auctions",
                newName: "IX_Auctions_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Accounts_AccountId",
                table: "Auctions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Accounts_AccountId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Auctions",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_AccountId",
                table: "Auctions",
                newName: "IX_Auctions_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Customers_CustomerId",
                table: "Auctions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }
    }
}
