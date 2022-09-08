using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThemeWebApi.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultEditColor",
                table: "DataTableCellColors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultEditColor",
                table: "DataTableCellColors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
