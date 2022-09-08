using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThemeWebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultTextColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultNavMenuTextColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultNavMenuBackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultAppBackGroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "DataTableCellColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DefaultBoxShadowColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultBorderColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultInputTextColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultEditColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataTableCellColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataTableCellColors_Themes_ParentName",
                        column: x => x.ParentName,
                        principalTable: "Themes",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "DropdownButtonColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DefaultBarrierColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultIconEnableColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultIconDisabledColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropdownButtonColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DropdownButtonColors_Themes_ParentName",
                        column: x => x.ParentName,
                        principalTable: "Themes",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "FloatingBoxColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DefaultShadowColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultBackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnlineColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfflineColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloatingBoxColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FloatingBoxColors_Themes_ParentName",
                        column: x => x.ParentName,
                        principalTable: "Themes",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataTableCellColors_ParentName",
                table: "DataTableCellColors",
                column: "ParentName",
                unique: true,
                filter: "[ParentName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DropdownButtonColors_ParentName",
                table: "DropdownButtonColors",
                column: "ParentName",
                unique: true,
                filter: "[ParentName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FloatingBoxColors_ParentName",
                table: "FloatingBoxColors",
                column: "ParentName",
                unique: true,
                filter: "[ParentName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataTableCellColors");

            migrationBuilder.DropTable(
                name: "DropdownButtonColors");

            migrationBuilder.DropTable(
                name: "FloatingBoxColors");

            migrationBuilder.DropTable(
                name: "Themes");
        }
    }
}
