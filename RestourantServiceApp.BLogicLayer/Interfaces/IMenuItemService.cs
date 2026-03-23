using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.BLogicLayer.Interfaces
{
	public interface IMenuItemService
	{
		Task AddMenuItem(string name, decimal price, Category category);
		Task EditMenuItem(Guid menuItemId, string name, decimal price);
		Task<List<MenuItem>> GetMenuItems();
		Task<List<MenuItem>> GetMenuItemsByCategory(Category category);
		Task<List<MenuItem>> GetMenuItemsBySearch(string search);
		Task<List<MenuItem>> GetMenuItemsInRange(decimal startPrice, decimal finalPrice);
		Task RemoveMenuItem(Guid menuItemId);
	}
}