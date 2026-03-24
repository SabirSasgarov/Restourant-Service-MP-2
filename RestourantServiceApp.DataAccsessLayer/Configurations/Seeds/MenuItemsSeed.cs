using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.DataAccsessLayer.Configurations.Seeds
{
	public class MenuItemsSeed
	{
		public static void MenuItemsSeeds(EntityTypeBuilder<MenuItem> builder)
		{
			builder.HasData(
				new MenuItem
				{
					Id = Guid.Parse("b3b5e5c3-2f1a-4c8f-9c1a-0b6b1f3a1a01"),
					Name = "Margherita Pizza",
					Price = 12.50m,
					Category = Category.Pizza
				},
				new MenuItem
				{
					Id = Guid.Parse("c4c6f6d4-3a2b-4d9f-a2b3-1c7c2f4b2b02"),
					Name = "Cheeseburger",
					Price = 10.00m,
					Category = Category.Burger
				},
				new MenuItem
				{
					Id = Guid.Parse("d5d7f7e5-4b3c-5e1f-b3c4-2d8d3f5c3c03"),
					Name = "Caesar Salad",
					Price = 8.75m,
					Category = Category.Salad
				},
				new MenuItem
				{
					Id = Guid.Parse("e6e8f8f6-5c4d-6f2f-c4d5-3e9e4f6d4d04"),
					Name = "Grilled Salmon",
					Price = 18.90m,
					Category = Category.Seafood
				},
				new MenuItem
				{
					Id = Guid.Parse("f7f9f9f7-6d5e-7a3f-d5e6-4f1f5f7e5e05"),
					Name = "Spaghetti Bolognese",
					Price = 13.40m,
					Category = Category.Pasta
				},
				new MenuItem
				{
					Id = Guid.Parse("a8a1a1f8-7e6f-8b4f-e6f7-5a2a6a8f6f06"),
					Name = "Chicken Nuggets",
					Price = 7.20m,
					Category = Category.FastFood
				},
				new MenuItem
				{
					Id = Guid.Parse("a9b2c3d4-8f7e-9c5d-f7e8-6b3b7a9d7d07"),
					Name = "Chocolate Cake",
					Price = 6.50m,
					Category = Category.Dessert
				},
				new MenuItem
				{
					Id = Guid.Parse("b1c2d3e4-9a8b-1d6e-e8b9-7c4c8b1e8e08"),
					Name = "Latte",
					Price = 4.30m,
					Category = Category.HotDrink
				}
			);
		}
	}
}
