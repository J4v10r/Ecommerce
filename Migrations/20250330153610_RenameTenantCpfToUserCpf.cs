using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saas_Sexshop.Migrations
{
    /// <inheritdoc />
    public partial class RenameTenantCpfToUserCpf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantCpf",
                table: "Users",
                newName: "UserCpf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserCpf",
                table: "Users",
                newName: "TenantCpf");
        }
    }
}
