using RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos;
using RestourantServiceApp.BLogicLayer.Dtos.OrderDtos;

namespace RestourantServiceApp.BLogicLayer.Interfaces
{
	public interface IOrderService
	{
		Task AddOrder(List<(MenuItemReturnDto menuItemReturnDto, int Count)> orderItems);
		Task<OrderReturnDto> GetOrderByNo(OrderReturnDto orderReturnDto);
		Task<List<OrderReturnDto>> GetOrders();
		Task<List<OrderReturnDto>> GetOrdersByDate(DateTime date);
		Task<List<OrderReturnDto>> GetOrdersByDatesInterval(DateTime firstDate, DateTime lastDate);
		Task<List<OrderReturnDto>> GetOrdersByPriceInterval(decimal startPrice, decimal finalPrice);
		Task RemoveOrder(OrderReturnDto orderReturnDto);
	}
}