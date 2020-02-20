using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbWorker.Migrations
{
    public partial class AddHashEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FolderHashCods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderHashCods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TnpaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TnpaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tnpas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PutIntoOperation = table.Column<DateTime>(nullable: false),
                    Cancelled = table.Column<DateTime>(nullable: false),
                    Registered = table.Column<DateTime>(nullable: false),
                    NumberRegistered = table.Column<int>(nullable: false),
                    IsReal = table.Column<bool>(nullable: false),
                    TnpaTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tnpas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tnpas_TnpaTypes_TnpaTypeId",
                        column: x => x.TnpaTypeId,
                        principalTable: "TnpaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Change",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(nullable: false),
                    PutIntoOperation = table.Column<DateTime>(nullable: false),
                    Registered = table.Column<DateTime>(nullable: false),
                    TnpaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Change", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Change_Tnpas_TnpaId",
                        column: x => x.TnpaId,
                        principalTable: "Tnpas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataFileInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Path = table.Column<string>(nullable: true),
                    HashCode = table.Column<int>(nullable: false),
                    TnpaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFileInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataFileInfo_Tnpas_TnpaId",
                        column: x => x.TnpaId,
                        principalTable: "Tnpas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Change_TnpaId",
                table: "Change",
                column: "TnpaId");

            migrationBuilder.CreateIndex(
                name: "IX_DataFileInfo_TnpaId",
                table: "DataFileInfo",
                column: "TnpaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tnpas_TnpaTypeId",
                table: "Tnpas",
                column: "TnpaTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Change");

            migrationBuilder.DropTable(
                name: "DataFileInfo");

            migrationBuilder.DropTable(
                name: "FolderHashCods");

            migrationBuilder.DropTable(
                name: "Tnpas");

            migrationBuilder.DropTable(
                name: "TnpaTypes");
        }
    }
}
