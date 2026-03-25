using RestourantServiceApp.Core.Models.Common;

namespace RestourantServiceApp.DataAccsessLayer.Interfaces
{
	public interface IRepository<T> where T : BaseEntity
	{
		Task<IQueryable<T>> GetAllAsync();
		Task<T> GetByIdAsync(Guid id);
		Task AddAsync(T entity);
		Task Update(T entity);
		Task Delete(Guid id);
		

	}
}
