using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos;
using RestourantServiceApp.BLogicLayer.Dtos.OrderDtos;
using RestourantServiceApp.BLogicLayer.Exceptions;
using RestourantServiceApp.BLogicLayer.Interfaces;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Interfaces;

namespace RestourantServiceApp.BLogicLayer.Services
{
	public class OrderService : IOrderService
	{
		private readonly IRepository<Order> _orderRepository;
		private readonly IRepository<MenuItem> _menuItemRepository;
		private readonly IMapper _mapper;

		public OrderService(IRepository<Order> orderRepository, IRepository<MenuItem> menuItemRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_menuItemRepository = menuItemRepository;
			_mapper = mapper;
		}

		public async Task<List<OrderReturnDto>> GetOrders()
		{
			var orders = await _orderRepository.GetAll(
				false,
				null,
				q => q.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem))
				.OrderBy(o => o.Date)
				.ToListAsync();

			return _mapper.Map<List<OrderReturnDto>>(orders);
		}

		public async Task AddOrder(List<(MenuItemReturnDto menuItemReturnDto, int Count)> orderItems)
		{
			if (orderItems == null || orderItems.Count == 0)
				throw new MenuItemWrongValue("Order must contain at least one menu item.");

			var newOrder = new Order
			{
				Id = Guid.NewGuid(),
				Date = DateTime.Now,
				OrderItems = new List<OrderItem>(),
				TotalAmount = 0
			};

			foreach (var (menuItemDto, count) in orderItems)
			{
				var menuItem = await _menuItemRepository
					.GetAll(false, mi => mi.Name.ToLower() == menuItemDto.Name.ToLower())
					.FirstOrDefaultAsync();

				if (menuItem == null)
					throw new MenuItemNotFound($"Menu item with name '{menuItemDto.Name}' not found.");

				newOrder.OrderItems.Add(new OrderItem
				{
					Id = Guid.NewGuid(),
					OrderId = newOrder.Id,
					MenuItemId = menuItem.Id,
					Count = count
				});
				newOrder.TotalAmount += count * menuItem.Price;
			}

			await _orderRepository.AddAsync(newOrder);
			await _orderRepository.SaveChangesAsync();
		}

		public async Task RemoveOrder(OrderReturnDto orderReturnDto)
		{
			var order = await GetOrderEntity(orderReturnDto, true);
			_orderRepository.Delete(order);
			await _orderRepository.SaveChangesAsync();
		}

		public async Task<List<OrderReturnDto>> GetOrdersByDatesInterval(DateTime firstDate, DateTime lastDate)
		{
			var orders = await _orderRepository.GetAll(
				false,
				o => o.Date.Day >= firstDate.Day && o.Date.Day <= lastDate.Day,
				q => q.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem))
				.ToListAsync();

			if (orders.Count == 0)
				throw new OrderNotFound("No orders found for the specified date interval.");

			return _mapper.Map<List<OrderReturnDto>>(orders);
		}

		public async Task<List<OrderReturnDto>> GetOrdersByDate(DateTime date)
		{
			var orders = await _orderRepository.GetAll(
				false,
				o => o.Date.Date == date.Date,
				q => q.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem))
				.ToListAsync();

			if (orders.Count == 0)
				throw new OrderNotFound("No orders found for the specified date.");

			return _mapper.Map<List<OrderReturnDto>>(orders);
		}

		public async Task<OrderReturnDto> GetOrderByNo(OrderReturnDto orderReturnDto)
		{
			var order = await GetOrderEntity(orderReturnDto, false);
			return _mapper.Map<OrderReturnDto>(order);
		}

		public async Task<List<OrderReturnDto>> GetOrdersByPriceInterval(decimal startPrice, decimal finalPrice)
		{
			var orders = await _orderRepository.GetAll(
				false,
				o => o.TotalAmount >= startPrice && o.TotalAmount <= finalPrice,
				q => q.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem))
				.ToListAsync();

			if (orders.Count == 0)
				throw new OrderNotFound("No orders found for the specified price interval.");

			return _mapper.Map<List<OrderReturnDto>>(orders);
		}

		private async Task<Order> GetOrderEntity(OrderReturnDto orderReturnDto, bool isTracking)
		{
			var orders = await _orderRepository.GetAll(
				isTracking,
				o => o.Date == orderReturnDto.Date && o.TotalAmount == orderReturnDto.TotalAmount,
				q => q.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem))
				.ToListAsync();

			var order = orders.FirstOrDefault(o =>
				o.OrderItems.Count == orderReturnDto.OrderItems.Count &&
				o.OrderItems.All(oi => orderReturnDto.OrderItems.Any(dtoOi =>
					dtoOi.MenuItemName.ToLower() == oi.MenuItem.Name.ToLower() &&
					dtoOi.MenuItemPrice == oi.MenuItem.Price &&
					dtoOi.Count == oi.Count)));

			if (order == null)
				throw new OrderNotFound("Order not found.");

			return order;
		}
	}
}
