using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.BLogicLayer.Interfaces
{
	public interface IOrderService
	{
		Task AddOrder(List<(Guid MenuItemId, int Count)> orderItems);
		Task<Order> GetOrderByNo(Guid id);
		Task<List<Order>> GetOrders();
		Task<List<Order>> GetOrdersByDate(DateTime date);
		Task<List<Order>> GetOrdersByDatesInterval(DateTime firstDate, DateTime lastDate);
		Task<List<Order>> GetOrdersByPriceInterval(decimal startPrice, decimal finalPrice);
		Task RemoveOrder(Guid orderId);
	}
}