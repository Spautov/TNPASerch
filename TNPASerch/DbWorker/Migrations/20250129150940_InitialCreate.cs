using Microsoft.EntityFrameworkCore.Migrations;

namespace DbWorker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IDGLOBAL",
                table: "Tnpas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RN",
                table: "Tnpas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDGLOBAL",
                table: "Tnpas");

            migrationBuilder.DropColumn(
                name: "RN",
                table: "Tnpas");
        }
    }
}
