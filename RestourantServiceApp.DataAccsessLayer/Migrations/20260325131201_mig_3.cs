using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestourantServiceApp.DataAccsessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "MenuItemId", "OrderId" },
                keyValues: new object[] { new Guid("b3b5e5c3-2f1a-4c8f-9c1a-0b6b1f3a1a01"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "MenuItemId", "OrderId" },
                keyValues: new object[] { new Guid("c4c6f6d4-3a2b-4d9f-a2b3-1c7c2f4b2b02"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "MenuItemId", "OrderId" },
                keyValues: new object[] { new Guid("d5d7f7e5-4b3c-5e1f-b3c4-2d8d3f5c3c03"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "MenuItemId", "OrderId" },
                keyValues: new object[] { new Guid("e6e8f8f6-5c4d-6f2f-c4d5-3e9e4f6d4d04"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "MenuItemId", "OrderId" },
                keyValues: new object[] { new Guid("a8a1a1f8-7e6f-8b4f-e6f7-5a2a6a8f6f06"), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "MenuItemId", "OrderId" },
                keyValues: new object[] { new Guid("a9b2c3d4-8f7e-9c5d-f7e8-6b3b7a9d7d07"), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "MenuItemId", "OrderId" },
                keyValues: new object[] { new Guid("b1c2d3e4-9a8b-1d6e-e8b9-7c4c8b1e8e08"), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "MenuItemId", "OrderId" },
                keyValues: new object[] { new Guid("f7f9f9f7-6d5e-7a3f-d5e6-4f1f5f7e5e05"), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "Count", "MenuItemId", "OrderId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 2, new Guid("b3b5e5c3-2f1a-4c8f-9c1a-0b6b1f3a1a01"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 1, new Guid("c4c6f6d4-3a2b-4d9f-a2b3-1c7c2f4b2b02"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 1, new Guid("d5d7f7e5-4b3c-5e1f-b3c4-2d8d3f5c3c03"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 1, new Guid("e6e8f8f6-5c4d-6f2f-c4d5-3e9e4f6d4d04"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 3, new Guid("f7f9f9f7-6d5e-7a3f-d5e6-4f1f5f7e5e05"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), 2, new Guid("a8a1a1f8-7e6f-8b4f-e6f7-5a2a6a8f6f06"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), 2, new Guid("a9b2c3d4-8f7e-9c5d-f7e8-6b3b7a9d7d07"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), 2, new Guid("b1c2d3e4-9a8b-1d6e-e8b9-7c4c8b1e8e08"), new Guid("33333333-3333-3333-3333-333333333333") }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                columns: new[] { "OrderId", "MenuItemId" });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "MenuItemId", "OrderId", "Count" },
                values: new object[,]
                {
                    { new Guid("b3b5e5c3-2f1a-4c8f-9c1a-0b6b1f3a1a01"), new Guid("11111111-1111-1111-1111-111111111111"), 2 },
                    { new Guid("c4c6f6d4-3a2b-4d9f-a2b3-1c7c2f4b2b02"), new Guid("11111111-1111-1111-1111-111111111111"), 1 },
                    { new Guid("d5d7f7e5-4b3c-5e1f-b3c4-2d8d3f5c3c03"), new Guid("22222222-2222-2222-2222-222222222222"), 1 },
                    { new Guid("e6e8f8f6-5c4d-6f2f-c4d5-3e9e4f6d4d04"), new Guid("22222222-2222-2222-2222-222222222222"), 1 },
                    { new Guid("a8a1a1f8-7e6f-8b4f-e6f7-5a2a6a8f6f06"), new Guid("33333333-3333-3333-3333-333333333333"), 2 },
                    { new Guid("a9b2c3d4-8f7e-9c5d-f7e8-6b3b7a9d7d07"), new Guid("33333333-3333-3333-3333-333333333333"), 2 },
                    { new Guid("b1c2d3e4-9a8b-1d6e-e8b9-7c4c8b1e8e08"), new Guid("33333333-3333-3333-3333-333333333333"), 2 },
                    { new Guid("f7f9f9f7-6d5e-7a3f-d5e6-4f1f5f7e5e05"), new Guid("33333333-3333-3333-3333-333333333333"), 3 }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Date",
                value: new DateTime(2026, 3, 22, 13, 0, 41, 597, DateTimeKind.Local).AddTicks(5937));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Date",
                value: new DateTime(2026, 3, 23, 13, 0, 41, 597, DateTimeKind.Local).AddTicks(5959));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Date",
                value: new DateTime(2026, 3, 24, 13, 0, 41, 597, DateTimeKind.Local).AddTicks(5961));
        }
    }
}
