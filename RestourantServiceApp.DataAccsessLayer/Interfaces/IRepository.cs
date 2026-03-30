using Microsoft.EntityFrameworkCore.Query;
using RestourantServiceApp.Core.Models.Common;
using System.Linq.Expressions;

namespace RestourantServiceApp.DataAccsessLayer.Interfaces
{
	public interface IRepository<T> where T : BaseEntity
	{
		IQueryable<T> GetAll();
		IQueryable<T> GetAll(bool isTracking = false,
			Expression<Func<T, bool>>? filter = null, 
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
		Task<T?> GetByIdAsync(Guid id);
		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		Task SaveChangesAsync();

	}
}
