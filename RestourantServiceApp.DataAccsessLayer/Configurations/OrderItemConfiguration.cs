using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Configurations.Seeds;

namespace RestourantServiceApp.DataAccsessLayer.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("OrderItems");

			builder.HasKey(oi => oi.Id);

			//builder.Property(oi => oi.Id)
			//	.HasDefaultValueSql("NEWSEQUENTIALID()");

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

			builder
				.HasIndex(oi => oi.MenuItemId)
				.IsUnique(false);

			OrderItemSeed.OrderItemSeeds(builder);
		}
	}
}