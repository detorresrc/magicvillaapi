using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.Migrations
{
    /// <inheritdoc />
    public partial class VillaNumberLinkToVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5668), new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5669) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5671), new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5672) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5674), new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5674) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5676), new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5676) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5678), new DateTime(2023, 10, 30, 14, 6, 5, 247, DateTimeKind.Utc).AddTicks(5679) });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaID",
                table: "VillaNumbers",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_VillaID",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VillaNumbers");

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
    }
}
