using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QueReal.DAL.Interfaces;

namespace QueReal.DAL.EF
{
    internal class GenericRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly DbSet<T> dbSet;

        public GenericRepository(DbContext dbContext)
        {
            dbSet = dbContext.Set<T>();
        }

        public Task<Guid> CreateAsync(T entity)
        {
            var id = Guid.NewGuid();
            entity.Id = id;

            dbSet.Add(entity);

            return Task.FromResult(id);
        }

        public Task DeleteAsync(T entity)
        {
            entity.DeletedTime = DateTime.UtcNow;

            dbSet.Update(entity);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);

            return Task.CompletedTask;
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, int skipCount = 0, int takeCount = 0)
        {
            var queryable = GetConfiguredQueryable(predicate, null, skipCount, takeCount);

            return queryable.CountAsync();
        }

        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = null,
            int skipCount = 0,
            int takeCount = 0)
        {
            var queryable = GetConfiguredQueryable(predicate, orderFunc, skipCount, takeCount);

            return queryable.ToListAsync().ContinueWith(x => (IEnumerable<T>)x.Result);

        }

        public Task<T> GetAsync(Guid id)
        {
            return GetAsync(x => x.Id == id);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(x => x.DeletedTime == null).FirstOrDefaultAsync(predicate);
        }

        private IQueryable<T> GetConfiguredQueryable(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = null,
            int skipCount = 0, 
            int takeCount = 0)
        {
            IQueryable<T> result = dbSet;

            result = result.Where(x => x.DeletedTime == null);

            if (predicate != null)
            {
                result = result.Where(predicate);
            }
            if (orderFunc != null)
            {
                result = orderFunc(result);
            }
            if (skipCount > 0)
            {
                result = result.Skip(skipCount);
            }
            if (takeCount > 0)
            {
                result = result.Take(takeCount);
            }

            return result;
        }
    }
}
