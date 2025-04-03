using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace ArchBackend.Core.Service
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id, bool tracking = true);
        Task<T> GetByIdIncludeThenIncludeAsync<TKey>(TKey id, bool tracking = true,
                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<IEnumerable<T>> GetAllAsync(bool tracking = true);
        Task<List<T>> GetAllIncludeThenIncludeAsync(bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IQueryable<T> Where(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        int Count();
    }
}
