using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.BLogicLayer.Exceptions;
using RestourantServiceApp.BLogicLayer.Interfaces;
using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Interfaces;

namespace RestourantServiceApp.BLogicLayer.Services
{
	public class MenuItemService : IMenuItemService
	{
		private readonly IRepository<MenuItem> _menuItemRepository;
		public MenuItemService(IRepository<MenuItem> menuItemRepository)
		{
			_menuItemRepository = menuItemRepository;
		}

		public async Task<List<MenuItem>> GetMenuItems()
		=> await _menuItemRepository
				.GetAll()
				.ToListAsync();

		public async Task AddMenuItem(string name, decimal price, Category category)
		{
			var newMenuItem = new MenuItem
			{
				Name = name,
				Price = price,
				Category = category
			};
			await _menuItemRepository.AddAsync(newMenuItem);
			await _menuItemRepository.SaveChangesAsync();
		}

		public async Task RemoveMenuItem(Guid menuItemId)
		{
			var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId);

			if (menuItem == null)
				throw new MenuItemNotFound("Menu item not found.");

			_menuItemRepository.Delete(menuItem);
			await _menuItemRepository.SaveChangesAsync();
		}

		public async Task EditMenuItem(Guid menuItemId, string name, decimal price)
		{
			var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId);

			if (menuItem == null)
				throw new MenuItemNotFound("Menu item not found.");

			menuItem.Name = name;
			menuItem.Price = price;

			_menuItemRepository.Update(menuItem);
			await _menuItemRepository.SaveChangesAsync();
		}

		public async Task<List<MenuItem>> GetMenuItemsByCategory(Category category)
		{
			var items = await _menuItemRepository.GetAll()
			.Where(mi => mi.Category == category)
			.AsNoTracking()
			.ToListAsync();

			if (items.Count == 0)
				throw new MenuItemNotFound($"No menu items found in the category: {category}.");

			return items;
		}
		public async Task<List<MenuItem>> GetMenuItemsInRange(decimal startPrice, decimal finalPrice)
		{
			var items = await _menuItemRepository.GetAll()
			.Where(mi => mi.Price >= startPrice && mi.Price <= finalPrice)
			.AsNoTracking()
			.ToListAsync();

			if (items.Count == 0)
				throw new MenuItemNotFound("No menu items found in the specified price range.");

			return items;
		}
		public async Task<List<MenuItem>> GetMenuItemsBySearch(string search)
		{
			if (string.IsNullOrWhiteSpace(search))
				return await _menuItemRepository.GetAll()
					.AsNoTracking()
					.ToListAsync();

			search = search.ToLower();
			var menuItems = await _menuItemRepository.GetAll()
				.Where(mi => mi.Name.ToLower().Contains(search))
				.AsNoTracking()
				.ToListAsync();

			if (menuItems.Count == 0)
				throw new MenuItemNotFound("No menu items found matching the search criteria.");

			return menuItems;
		}

	}
}
