
using RestourantServiceApp.Core.Models.Common;
using RestourantServiceApp.DataAccsessLayer.Interfaces;

namespace RestourantServiceApp.DataAccsessLayer.Concretes
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
		//private readonly AppDbContext _context;
		//
		public Task AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<T>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task Update(T entity)
		{
			throw new NotImplementedException();
		}
	}
}
