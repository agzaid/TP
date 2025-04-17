using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false);
        Task <T> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        void Add(T entity);
        bool Any(Expression<Func<T, bool>> filter);
        void Remove(T entity);
    }
}
