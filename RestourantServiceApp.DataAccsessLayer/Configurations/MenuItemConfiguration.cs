using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.DataAccsessLayer.Configurations
{
	public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
	{
		public void Configure(EntityTypeBuilder<MenuItem> builder)
		{
			builder.ToTable("MenuItems");

			builder.HasKey(o => o.Id);

			builder.Property(o => o.Id)
				.HasDefaultValueSql("NEWSEQUENTIALID()");

			builder
				.Property(mi => mi.Name)
				.HasMaxLength(50)
				.IsRequired();

			builder
				.HasIndex(mi => mi.Name)
				.IsUnique();

			builder
				.Property(o => o.Price)
				.IsRequired()
				.HasPrecision(18, 2);

			builder
				.Property(o => o.Category)
				.IsRequired();

			builder
				.HasOne(o => o.OrderItem)
				.WithOne(oi => oi.MenuItem)
				.HasForeignKey<OrderItem>(oi => oi.MenuItemId);

		}
	}
}
