using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestourantServiceApp.DataAccsessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Date",
                value: new DateTime(2026, 3, 23, 17, 29, 21, 804, DateTimeKind.Local).AddTicks(9985));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Date",
                value: new DateTime(2026, 3, 24, 17, 29, 21, 804, DateTimeKind.Local).AddTicks(9993));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Date",
                value: new DateTime(2026, 3, 25, 17, 29, 21, 804, DateTimeKind.Local).AddTicks(9995));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Date",
                value: new DateTime(2026, 3, 23, 17, 12, 1, 134, DateTimeKind.Local).AddTicks(5253));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Date",
                value: new DateTime(2026, 3, 24, 17, 12, 1, 134, DateTimeKind.Local).AddTicks(5263));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Date",
                value: new DateTime(2026, 3, 25, 17, 12, 1, 134, DateTimeKind.Local).AddTicks(5265));
        }
    }
}
