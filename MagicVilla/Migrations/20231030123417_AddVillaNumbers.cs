using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.VillaNo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2810), new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2811) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2813), new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2814) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2816), new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2816) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2818), new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2818) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2820), new DateTime(2023, 10, 30, 12, 34, 16, 973, DateTimeKind.Utc).AddTicks(2820) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6806), new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6807) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6809), new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6810) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6811), new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6811) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6813), new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6814) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6815), new DateTime(2023, 10, 29, 12, 32, 17, 507, DateTimeKind.Utc).AddTicks(6816) });
        }
    }
}
