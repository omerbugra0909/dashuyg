using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dashuyg.Migrations
{
    /// <inheritdoc />
    public partial class AddSinifOgrenci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Siniflar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siniflar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false),
                    Soyad = table.Column<string>(type: "TEXT", nullable: false),
                    Yas = table.Column<int>(type: "INTEGER", nullable: false),
                    SinifId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Siniflar_SinifId",
                        column: x => x.SinifId,
                        principalTable: "Siniflar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                column: "OlusturmaTarihi",
                value: new DateTime(2026, 3, 12, 10, 33, 48, 923, DateTimeKind.Local).AddTicks(6027));

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                column: "OlusturmaTarihi",
                value: new DateTime(2026, 3, 12, 10, 33, 48, 923, DateTimeKind.Local).AddTicks(6057));

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 3,
                column: "OlusturmaTarihi",
                value: new DateTime(2026, 3, 12, 10, 33, 48, 923, DateTimeKind.Local).AddTicks(6059));

            migrationBuilder.InsertData(
                table: "Siniflar",
                columns: new[] { "Id", "Ad", "OlusturmaTarihi" },
                values: new object[,]
                {
                    { 1, "9-A", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "10-B", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Ogrenciler",
                columns: new[] { "Id", "Ad", "SinifId", "Soyad", "Yas" },
                values: new object[,]
                {
                    { 1, "Ali", 1, "Yilmaz", 15 },
                    { 2, "Zeynep", 2, "Kaya", 16 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_SinifId",
                table: "Ogrenciler",
                column: "SinifId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Siniflar");

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                column: "OlusturmaTarihi",
                value: new DateTime(2026, 3, 9, 12, 33, 40, 962, DateTimeKind.Local).AddTicks(8038));

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                column: "OlusturmaTarihi",
                value: new DateTime(2026, 3, 9, 12, 33, 40, 962, DateTimeKind.Local).AddTicks(8074));

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 3,
                column: "OlusturmaTarihi",
                value: new DateTime(2026, 3, 9, 12, 33, 40, 962, DateTimeKind.Local).AddTicks(8076));
        }
    }
}
