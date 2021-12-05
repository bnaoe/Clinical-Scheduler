using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataAccess.Repository.IRepository
{
    public interface IRepositoryAsync<T> where T : class
    {
        //T - CodeSet
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter,string? includeProperties=null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>?, IOrderedQueryable<T>>? orderBy = null, string ? includeProperties = null);
        Task  AddAsync(T entity);
        Task  RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity);

    }
}
