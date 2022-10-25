using System.Linq.Expressions;

namespace QueReal.DAL.Interfaces
{
    internal interface IRepository<T>
    {
        public Task<Guid> CreateAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);

        public Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = null,
            int skipCount = 0,
            int takeCount = 0);

        public Task<int> CountAsync(
            Expression<Func<T, bool>> predicate = null,
            int skipCount = 0,
            int takeCount = 0);
    }
}
