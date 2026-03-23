using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.DataAccsessLayer.Contexts
{
	public class RestourantDbContext : DbContext
	{
		public DbSet<Order> Orders { get; set; }
		public DbSet<MenuItem> MenuItems { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }


		override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=.\\MSSQLSERVER01;Initial Catalog=RestourantServiceDb;Integrated Security=True;Trust Server Certificate=True;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestourantDbContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entries = ChangeTracker.Entries<Order>().ToList();

			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.Date = DateTime.Now;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

	}
}
