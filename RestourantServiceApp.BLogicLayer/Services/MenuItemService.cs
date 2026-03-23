using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.BLogicLayer.Interfaces;
using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Contexts;
using System.ComponentModel;

namespace RestourantServiceApp.BLogicLayer.Services
{
	public class MenuItemService(RestourantDbContext context) : IMenuItemService
	{
		public async Task<List<MenuItem>> GetMenuItems()
		=> await context.MenuItems
				.AsNoTracking()
				.ToListAsync();

		public async Task AddMenuItem(string name, decimal price, Category category)
		{
			var newMenuItem = new MenuItem
			{
				Name = name,
				Price = price,
				Category = category
			};
			context.MenuItems.Add(newMenuItem);
			await context.SaveChangesAsync();
		}

		public async Task RemoveMenuItem(Guid menuItemId)
		{
			var menuItem = await context.MenuItems
				.FirstOrDefaultAsync(mi => mi.Id == menuItemId);

			if (menuItem == null)
				throw new InvalidOperationException("Menu item not found.");

			context.MenuItems.Remove(menuItem);
			await context.SaveChangesAsync();
		}

		public async Task EditMenuItem(Guid menuItemId, string name, decimal price)
		{
			var menuItem = await context.MenuItems
				.FirstOrDefaultAsync(mi => mi.Id == menuItemId);

			if (menuItem == null)
				throw new InvalidOperationException("Menu item not found.");

			menuItem.Name = name;
			menuItem.Price = price;

			context.MenuItems.Update(menuItem);
			await context.SaveChangesAsync();
		}

		public async Task<List<MenuItem>> GetMenuItemsByCategory(Category category)
		=> await context.MenuItems
			.Where(mi => mi.Category == category)
			.AsNoTracking()
			.ToListAsync();

		public async Task<List<MenuItem>> GetMenuItemsInRange(decimal startPrice, decimal finalPrice)
		=> await context.MenuItems
			.Where(mi => mi.Price >= startPrice && mi.Price <= finalPrice)
			.AsNoTracking()
			.ToListAsync();

		public async Task<List<MenuItem>> GetMenuItemsBySearch(string search)
		{
			if (string.IsNullOrWhiteSpace(search))
				return await context.MenuItems
					.AsNoTracking()
					.ToListAsync();

			search = search.ToLower();
			var menuItems = await context.MenuItems
				.Where(mi => mi.Name.ToLower().Contains(search))
				.AsNoTracking()
				.ToListAsync();

			return menuItems;
		}

	}
}
