using RestourantServiceApp.Core.Models.Common;

namespace RestourantServiceApp.DataAccsessLayer.Interfaces
{
	public interface IRepository<T> where T : BaseEntity
	{
		IQueryable<T> GetAll();
		Task<T?> GetByIdAsync(Guid id);
		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		Task SaveChangesAsync();

	}
}
