using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.DataAccsessLayer.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("OrderItems");

			builder
				.HasKey(oi => new { oi.OrderId, oi.MenuItemId });

			builder
				.Property(oi => oi.Count)
				.IsRequired();

			builder
				.HasOne(oi => oi.Order)
				.WithMany(o => o.OrderItems)
				.HasForeignKey(oi => oi.OrderId);

			builder
				.HasOne(oi => oi.MenuItem)
				.WithOne(mi => mi.OrderItem)
				.HasForeignKey<OrderItem>(oi => oi.MenuItemId);
		}
	}
}