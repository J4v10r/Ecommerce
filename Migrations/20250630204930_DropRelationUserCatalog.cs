using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saas.Migrations
{
    /// <inheritdoc />
    public partial class DropRelationUserCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Catalogs_CatalogId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CatalogId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CatalogId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatalogId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CatalogId",
                table: "Users",
                column: "CatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Catalogs_CatalogId",
                table: "Users",
                column: "CatalogId",
                principalTable: "Catalogs",
                principalColumn: "CatalogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
