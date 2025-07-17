using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saas_Sexshop.Migrations
{
    /// <inheritdoc />
    public partial class FixTenantPlanForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Plans_TenantPlanPlanId",
                table: "Tenants");

            migrationBuilder.RenameColumn(
                name: "TenantPlanPlanId",
                table: "Tenants",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Tenants_TenantPlanPlanId",
                table: "Tenants",
                newName: "IX_Tenants_PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Plans_PlanId",
                table: "Tenants",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Plans_PlanId",
                table: "Tenants");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "Tenants",
                newName: "TenantPlanPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Tenants_PlanId",
                table: "Tenants",
                newName: "IX_Tenants_TenantPlanPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Plans_TenantPlanPlanId",
                table: "Tenants",
                column: "TenantPlanPlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
