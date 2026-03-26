using RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos;
using RestourantServiceApp.Core.Enums;

namespace RestourantServiceApp.BLogicLayer.Interfaces
{
	public interface IMenuItemService
	{
		Task AddMenuItem(MenuItemCreateDto menuItemCreateDto);
		Task EditMenuItem(MenuItemReturnDto menuItem, string name, decimal price);
		Task<List<MenuItemReturnDto>> GetMenuItems();
		Task<List<MenuItemReturnDto>> GetMenuItemsByCategory(Category category);
		Task<List<MenuItemReturnDto>> GetMenuItemsBySearch(string search);
		Task<List<MenuItemReturnDto>> GetMenuItemsInRange(decimal startPrice, decimal finalPrice);
		Task RemoveMenuItem(MenuItemReturnDto menuItemReturnDto);
	}
}