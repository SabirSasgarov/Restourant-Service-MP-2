using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestourantServiceApp.DataAccsessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_seeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("a8a1a1f8-7e6f-8b4f-e6f7-5a2a6a8f6f06"), 10, "Chicken Nuggets", 7.20m },
                    { new Guid("a9b2c3d4-8f7e-9c5d-f7e8-6b3b7a9d7d07"), 13, "Chocolate Cake", 6.50m },
                    { new Guid("b1c2d3e4-9a8b-1d6e-e8b9-7c4c8b1e8e08"), 15, "Latte", 4.30m },
                    { new Guid("b3b5e5c3-2f1a-4c8f-9c1a-0b6b1f3a1a01"), 4, "Margherita Pizza", 12.50m },
                    { new Guid("c4c6f6d4-3a2b-4d9f-a2b3-1c7c2f4b2b02"), 5, "Cheeseburger", 10.00m },
                    { new Guid("d5d7f7e5-4b3c-5e1f-b3c4-2d8d3f5c3c03"), 2, "Caesar Salad", 8.75m },
                    { new Guid("e6e8f8f6-5c4d-6f2f-c4d5-3e9e4f6d4d04"), 6, "Grilled Salmon", 18.90m },
                    { new Guid("f7f9f9f7-6d5e-7a3f-d5e6-4f1f5f7e5e05"), 3, "Spaghetti Bolognese", 13.40m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "TotalAmount" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 3, 22, 13, 0, 41, 597, DateTimeKind.Local).AddTicks(5937), 35.00m },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 3, 23, 13, 0, 41, 597, DateTimeKind.Local).AddTicks(5959), 27.65m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 3, 24, 13, 0, 41, 597, DateTimeKind.Local).AddTicks(5961), 76.20m }
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("a8a1a1f8-7e6f-8b4f-e6f7-5a2a6a8f6f06"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("a9b2c3d4-8f7e-9c5d-f7e8-6b3b7a9d7d07"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("b1c2d3e4-9a8b-1d6e-e8b9-7c4c8b1e8e08"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("b3b5e5c3-2f1a-4c8f-9c1a-0b6b1f3a1a01"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("c4c6f6d4-3a2b-4d9f-a2b3-1c7c2f4b2b02"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("d5d7f7e5-4b3c-5e1f-b3c4-2d8d3f5c3c03"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("e6e8f8f6-5c4d-6f2f-c4d5-3e9e4f6d4d04"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("f7f9f9f7-6d5e-7a3f-d5e6-4f1f5f7e5e05"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));
        }
    }
}
