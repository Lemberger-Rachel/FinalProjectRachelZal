using Microsoft.EntityFrameworkCore.Migrations;

namespace BrixBank.Data.Migrations
{
    public partial class EigMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsManager",
                table: "Rules",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "Rules");
        }
    }
}
