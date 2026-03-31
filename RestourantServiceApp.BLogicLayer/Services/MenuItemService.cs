using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos;
using RestourantServiceApp.BLogicLayer.Exceptions;
using RestourantServiceApp.BLogicLayer.Interfaces;
using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Interfaces;
using AutoMapper;

namespace RestourantServiceApp.BLogicLayer.Services
{
	public class MenuItemService : IMenuItemService
	{
		private readonly IRepository<MenuItem> _menuItemRepository;
		private readonly IMapper _mapper;

		public MenuItemService(IRepository<MenuItem> menuItemRepository, IMapper mapper)
		{
			_menuItemRepository = menuItemRepository;
			_mapper = mapper;
		}

		public async Task<List<MenuItemReturnDto>> GetMenuItems()
		{
			var menuItems = await _menuItemRepository
				.GetAll(false)
				.ToListAsync();

			return _mapper.Map<List<MenuItemReturnDto>>(menuItems);
		}

		public async Task AddMenuItem(MenuItemCreateDto menuItemCreateDto)
		{
			var newMenuItem = _mapper.Map<MenuItem>(menuItemCreateDto);

			await _menuItemRepository.AddAsync(newMenuItem);
			await _menuItemRepository.SaveChangesAsync();
		}

		public async Task RemoveMenuItem(MenuItemReturnDto menuItemReturnDto)
		{
			var menuItem = await GetMenuItemByName(menuItemReturnDto);

			if (menuItem == null)
				throw new MenuItemNotFound("Menu item not found.");

			_menuItemRepository.Delete(menuItem);
			await _menuItemRepository.SaveChangesAsync();
		}

		public async Task EditMenuItem(MenuItemReturnDto menuItemDto, string name, decimal price)
		{
			var menuItem = await GetMenuItemByName(menuItemDto);

			if (menuItem == null)
				throw new MenuItemNotFound("Menu item not found.");

			if(price == menuItem.Price && name == menuItem.Name)
				throw new MenuItemWrongValue("No changes detected for the menu item.");

			if (!string.IsNullOrWhiteSpace(name))
				menuItem.Name = name;

			if (price > 0 && price != menuItem.Price)
				menuItem.Price = price;			

			_menuItemRepository.Update(menuItem);
			await _menuItemRepository.SaveChangesAsync();
		}

		public async Task<List<MenuItemReturnDto>> GetMenuItemsByCategory(Category category)
		{
			var items = await _menuItemRepository.GetAll(false, mi => mi.Category == category)
				.ToListAsync();

			return _mapper.Map<List<MenuItemReturnDto>>(items);
		}

		public async Task<List<MenuItemReturnDto>> GetMenuItemsInRange(decimal startPrice, decimal finalPrice)
		{
			var items = await _menuItemRepository.GetAll(false, mi => mi.Price >= startPrice && mi.Price <= finalPrice)
				.ToListAsync();

			return _mapper.Map<List<MenuItemReturnDto>>(items);
		}

		public async Task<List<MenuItemReturnDto>> GetMenuItemsBySearch(string search)
		{
			if (string.IsNullOrWhiteSpace(search))
			{
				var allItems = await _menuItemRepository.GetAll(false).ToListAsync();
				return _mapper.Map<List<MenuItemReturnDto>>(allItems);
			}

			search = search.ToLower();
			var menuItems = await _menuItemRepository.GetAll(false, mi => mi.Name.ToLower().Contains(search))
				.ToListAsync();

			return _mapper.Map<List<MenuItemReturnDto>>(menuItems);
		}

		public async Task<MenuItem> GetMenuItemByName(MenuItemReturnDto menuItemReturnDto)
		{
			var menuItem = await _menuItemRepository.GetAll(true, mi => mi.Name.ToLower() == menuItemReturnDto.Name.ToLower())
				.FirstOrDefaultAsync();

			if (menuItem == null)
				throw new MenuItemNotFound($"Menu item with name '{menuItemReturnDto.Name}' not found.");

			return menuItem;
		}
	}
}

