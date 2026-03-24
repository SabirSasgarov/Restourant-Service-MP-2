using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.DataAccsessLayer.Configurations.Seeds
{
	public class OrderItemSeed
	{
		public static void OrderItemSeeds(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasData(
				// Order 1
				new OrderItem
				{
					OrderId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
					MenuItemId = Guid.Parse("b3b5e5c3-2f1a-4c8f-9c1a-0b6b1f3a1a01"), // Margherita Pizza
					Count = 2
				},
				new OrderItem
				{
					OrderId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
					MenuItemId = Guid.Parse("c4c6f6d4-3a2b-4d9f-a2b3-1c7c2f4b2b02"), // Cheeseburger
					Count = 1
				},

				// Order 2
				new OrderItem
				{
					OrderId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
					MenuItemId = Guid.Parse("d5d7f7e5-4b3c-5e1f-b3c4-2d8d3f5c3c03"), // Caesar Salad
					Count = 1
				},
				new OrderItem
				{
					OrderId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
					MenuItemId = Guid.Parse("e6e8f8f6-5c4d-6f2f-c4d5-3e9e4f6d4d04"), // Grilled Salmon
					Count = 1
				},

				// Order 3
				new OrderItem
				{
					OrderId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
					MenuItemId = Guid.Parse("f7f9f9f7-6d5e-7a3f-d5e6-4f1f5f7e5e05"), // Spaghetti Bolognese
					Count = 3
				},
				new OrderItem
				{
					OrderId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
					MenuItemId = Guid.Parse("a8a1a1f8-7e6f-8b4f-e6f7-5a2a6a8f6f06"), // Chicken Nuggets
					Count = 2
				},
				new OrderItem
				{
					OrderId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
					MenuItemId = Guid.Parse("a9b2c3d4-8f7e-9c5d-f7e8-6b3b7a9d7d07"), // Chocolate Cake
					Count = 2
				},
				new OrderItem
				{
					OrderId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
					MenuItemId = Guid.Parse("b1c2d3e4-9a8b-1d6e-e8b9-7c4c8b1e8e08"), // Latte
					Count = 2
				}
			);
		}
	}
}
