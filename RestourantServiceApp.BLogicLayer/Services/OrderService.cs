using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.BLogicLayer.Interfaces;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Contexts;

namespace RestourantServiceApp.BLogicLayer.Services
{
	public class OrderService(RestourantDbContext context) : IOrderService
	{
		public async Task<List<Order>> GetOrders()
			=> await context.Orders
				.AsNoTracking()
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.ToListAsync();

		public async Task AddOrder(List<(Guid MenuItemId, int Count)> orderItems)
		{
			var newOrder = new Order
			{
				//Date = DateTime.Now,
				OrderItems = new List<OrderItem>()
			};

			foreach (var (menuItemId, count) in orderItems)
			{
				var menuItem = await context.MenuItems
					.FirstOrDefaultAsync(mi => mi.Id == menuItemId);
				if (menuItem == null)
					throw new InvalidOperationException($"Menu item with ID {menuItemId} not found.");

				newOrder.OrderItems.Add(new OrderItem
				{
					MenuItemId = menuItemId,
					MenuItem = menuItem,
					Count = count
				});
			}

			newOrder.TotalAmount = newOrder.OrderItems
				.Sum(oi => oi.MenuItem.Price * oi.Count);

			context.Orders.Add(newOrder);
			await context.SaveChangesAsync();
		}

		public async Task RemoveOrder(Guid orderId)
		{
			var order = await context.Orders
				.FirstOrDefaultAsync(o => o.Id == orderId);

			if (order == null)
				throw new InvalidOperationException("Order not found.");

			context.Orders.Remove(order);
			await context.SaveChangesAsync();
		}

		public async Task<List<Order>> GetOrdersByDatesInterval(DateTime firstDate, DateTime lastDate)
		{
			var orders = await context.Orders
				.Where(o => o.Date.Day >= firstDate.Day && o.Date.Day <= lastDate.Day)
				.AsNoTracking()
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.ToListAsync();

			if (orders == null || orders.Count == 0)
				throw new InvalidOperationException("No orders found for the specified date interval.");

			return orders;
		}

		public async Task<List<Order>> GetOrdersByDate(DateTime date)
		{
			var orders = await context.Orders
				.Where(o => o.Date.Date == date.Date)
				.AsNoTracking()
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.ToListAsync();

			if (orders == null || orders.Count == 0)
				throw new InvalidOperationException("No orders found for the specified date.");

			return orders;
		}

		public async Task<Order> GetOrderByNo(Guid id)
		{
			var order = await context.Orders
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.FirstOrDefaultAsync(o => o.Id == id);

			if (order == null)
				throw new InvalidOperationException("Order not found.");

			//order.OrderItems = order.OrderItems
			//	.Select(oi =>
			//	{
			//		oi.MenuItem = context.MenuItems
			//		
			//		.FirstOrDefault(mi => mi.Id == oi.MenuItemId);
			//		return oi;
			//	})
			//	.ToList();

			return order;
		}


		public async Task<List<Order>> GetOrdersByPriceInterval(decimal startPrice, decimal finalPrice)
		{
			var orders = await context.Orders
				.Where(o => o.TotalAmount >= startPrice && o.TotalAmount <= finalPrice)
				.AsNoTracking()
				.ToListAsync();

			if (orders == null || orders.Count == 0)
				throw new InvalidOperationException("No orders found for the specified price interval.");

			return orders;
		}
	}
}
