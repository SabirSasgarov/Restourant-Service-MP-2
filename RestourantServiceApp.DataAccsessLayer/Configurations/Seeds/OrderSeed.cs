using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.DataAccsessLayer.Configurations.Seeds
{
	public class OrderSeed
	{
		public static void OrderSeeds(EntityTypeBuilder<Order> builder)
		{
			builder.HasData(
				new Order
				{
					Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
					Date = DateTime.Now.AddDays(-2),
					TotalAmount = 35.00m,
				},
				new Order
				{
					Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
					Date = DateTime.Now.AddDays(-1),
					TotalAmount = 27.65m,
				},
				new Order
				{
					Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
					Date = DateTime.Now,
					TotalAmount = 76.20m,
				}
			);
		}
	}
}
