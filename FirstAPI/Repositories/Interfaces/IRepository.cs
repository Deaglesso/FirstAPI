using FirstAPI.Entities;
using FirstAPI.Entities.Base;
using System.Linq.Expressions;

namespace FirstAPI.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>> expression = null, Expression<Func<T, object>> orderExpression = null, bool isDescending = false, int skip = 0, int limit = 0, bool isTracked = false, params string[] includes);

        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();


        
    }
}
