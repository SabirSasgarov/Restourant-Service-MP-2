using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.BLogicLayer.Exceptions;
using RestourantServiceApp.BLogicLayer.Interfaces;
using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Contexts;

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
				throw new MenuItemNotFound("Menu item not found.");

			context.MenuItems.Remove(menuItem);
			await context.SaveChangesAsync();
		}

		public async Task EditMenuItem(Guid menuItemId, string name, decimal price)
		{
			var menuItem = await context.MenuItems
				.FirstOrDefaultAsync(mi => mi.Id == menuItemId);

			if (menuItem == null)
				throw new MenuItemNotFound("Menu item not found.");

			menuItem.Name = name;
			menuItem.Price = price;

			context.MenuItems.Update(menuItem);
			await context.SaveChangesAsync();
		}

		public async Task<List<MenuItem>> GetMenuItemsByCategory(Category category)
		{
		    var items =	await context.MenuItems
			.Where(mi => mi.Category == category)
			.AsNoTracking()
			.ToListAsync();
		
			if(items.Count == 0)
				throw new MenuItemNotFound($"No menu items found in the category: {category}.");

			return items;
		}
		public async Task<List<MenuItem>> GetMenuItemsInRange(decimal startPrice, decimal finalPrice)
		{
			var items = await context.MenuItems
			.Where(mi => mi.Price >= startPrice && mi.Price <= finalPrice)
			.AsNoTracking()
			.ToListAsync();

			if(items.Count == 0)
				throw new MenuItemNotFound("No menu items found in the specified price range.");

			return items;
		}
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

			if(menuItems.Count == 0)
				throw new MenuItemNotFound("No menu items found matching the search criteria.");

			return menuItems;
		}

	}
}
