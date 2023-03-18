using System.Linq.Expressions;

namespace QueReal.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<Guid> CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<T> GetAsync(Guid id);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = null,
            int skipCount = 0,
            int takeCount = 0);

        Task<int> CountAsync(
            Expression<Func<T, bool>> predicate = null,
            int skipCount = 0,
            int takeCount = 0);
    }
}
