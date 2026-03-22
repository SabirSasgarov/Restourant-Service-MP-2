using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Contexts;

namespace RestourantServiceApp.BLogicLayer.Services
{
	public class OrderService(RestourantDbContext context)
	{
		public async Task<List<Order>> GetOrders()
			=> await context.Orders
				.AsNoTracking()
				.ToListAsync();

		public

	}
}
