
using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.Core.Models.Common;
using RestourantServiceApp.DataAccsessLayer.Contexts;
using RestourantServiceApp.DataAccsessLayer.Interfaces;

namespace RestourantServiceApp.DataAccsessLayer.Concretes
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly RestourantDbContext _context;
		private DbSet<T> Table { get; set; }
		public Repository()
		{
			_context = new RestourantDbContext();
			Table = _context.Set<T>();
		}

		public async Task AddAsync(T entity)
		{
			await Table.AddAsync(entity);
		}

		public void Delete(T entity)
		{
			Table.Remove(entity);
		}

		public IQueryable<T> GetAll()
		{
			return Table.AsQueryable();
		}

		public async Task<T?> GetByIdAsync(Guid id)
		{
			return await Table.FindAsync(id);
		}

		public void Update(T entity)
		{
			Table.Update(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
