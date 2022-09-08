using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThemeWebApi.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfflineColor",
                table: "FloatingBoxColors");

            migrationBuilder.DropColumn(
                name: "OnlineColor",
                table: "FloatingBoxColors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfflineColor",
                table: "FloatingBoxColors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnlineColor",
                table: "FloatingBoxColors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
