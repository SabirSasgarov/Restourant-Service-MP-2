using Microsoft.Extensions.Caching.Memory;
using RestourantServiceApp.Core.Models.Common;
using RestourantServiceApp.DataAccsessLayer.Concretes;
using RestourantServiceApp.DataAccsessLayer.Interfaces;
using RestourantServiceApp.BLogicLayer.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace RestourantServiceApp.BLogicLayer.Chaching
{
	public class CachedRepository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly Repository<T> _repository;
		private readonly IMemoryCache _cache;
		private readonly string _cacheKey;
		private readonly string _cacheByIdPrefix;

		public CachedRepository(Repository<T> repository, IMemoryCache cache)
		{
			_repository = repository;
			_cache = cache;
			_cacheKey = typeof(T).Name + "List";
			_cacheByIdPrefix = typeof(T).Name + "ById_";
		}

		public async Task AddAsync(T entity)
		{
			await _repository.AddAsync(entity);
			_cache.Remove(_cacheKey);
			_cache.Remove(_cacheByIdPrefix + entity.Id);
		}

		public void Delete(T entity)
		{
			_repository.Delete(entity);
			_cache.Remove(_cacheKey);
			_cache.Remove(_cacheByIdPrefix + entity.Id);
		}

		public IQueryable<T> GetAll()
		{
			return _repository.GetAll();
		}

		public IQueryable<T> GetAll(bool isTracking = false, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
		{
			return _repository.GetAll(isTracking, filter, includes, orderBy);
		}

		public async Task<T?> GetByIdAsync(Guid id)
		{
			var key = _cacheByIdPrefix + id;

			if (_cache.TryGetValue(key, out T? cachedEntity))
				return cachedEntity;

			var entity = await _repository.GetByIdAsync(id);
			if (entity != null)
				_cache.Set(key, entity, TimeSpan.FromMinutes(5));

			return entity;
		}

		public async Task SaveChangesAsync()
		{
			await _repository.SaveChangesAsync();
		}

		public void Update(T entity)
		{
			_repository.Update(entity);
			_cache.Remove(_cacheKey);
			_cache.Remove(_cacheByIdPrefix + entity.Id);
		}
	}
}