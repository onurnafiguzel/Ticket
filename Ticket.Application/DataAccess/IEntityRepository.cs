using System.Linq.Expressions;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Application.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null, int pageNumber = 0, int pageSize = 0, int limit = 0);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
