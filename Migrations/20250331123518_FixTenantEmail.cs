using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saas_Sexshop.Migrations
{
    /// <inheritdoc />
    public partial class FixTenantEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantEmail",
                table: "Tenants",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantEmail",
                table: "Tenants");
        }
    }
}
