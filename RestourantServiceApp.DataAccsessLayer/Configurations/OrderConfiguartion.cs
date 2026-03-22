using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.DataAccsessLayer.Configurations
{
	public class OrderConfiguartion : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders");

			builder.HasKey(o => o.Id);

			builder.Property(o => o.Id)
				.HasDefaultValueSql("NEWSEQUENTIALID()");

			builder
				.Property(o => o.TotalAmount)
				.HasPrecision(18, 2)
				.IsRequired();

			builder
				.Property(o => o.Date)
				.IsRequired()
				.HasDefaultValue(DateTime.Now);

			builder
				.HasMany(o => o.OrderItems)
				.WithOne(oi => oi.Order)
				.HasForeignKey(oi => oi.OrderId);
		}
	}
}
