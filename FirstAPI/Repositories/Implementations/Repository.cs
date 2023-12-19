using FirstAPI.DAL;
using FirstAPI.Entities;
using FirstAPI.Entities.Base;
using FirstAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FirstAPI.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity 
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _table;


        public Repository(AppDbContext db)
        {
            _db = db;
            _table = _db.Set<T>();  
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public IQueryable<T> GetAllAsync(Expression<Func<T,bool>> expression = null, Expression<Func<T, object>> orderExpression = null,bool isDescending = false,int skip = 0,int limit = 0,bool isTracked = false , params string[] includes)
        {

            IQueryable<T> query = _table;

            

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (orderExpression is not null)
            {
                if (isDescending == false)
                {
                    query = query.OrderBy(orderExpression);
                }
                else
                {
                    query = query.OrderByDescending(orderExpression);
                }
            }

            if (skip != 0) query = query.Skip(skip);
            if (limit != 0) query = query.Take(limit);

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            return isTracked?query:query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _table.FirstOrDefaultAsync(c => c.Id == id);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }
    }
}
