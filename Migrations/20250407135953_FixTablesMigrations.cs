using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saas_Sexshop.Migrations
{
    /// <inheritdoc />
    public partial class FixTablesMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<string>(
                name: "HtmlStructure",
                table: "Templates",
                type: "text",
                nullable: true);

         
            migrationBuilder.AddColumn<string>(
                name: "DefaultCss",
                table: "Templates",
                type: "text",
                nullable: true);

        
            migrationBuilder.AddColumn<string>(
                name: "CustomizableAreas",
                table: "Templates",
                type: "json",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        
            migrationBuilder.DropColumn(
                name: "HtmlStructure",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "DefaultCss",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CustomizableAreas",
                table: "Templates");
        }
    }
}
