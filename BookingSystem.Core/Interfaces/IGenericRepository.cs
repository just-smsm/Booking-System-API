using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;

namespace BookingSystem.Core.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
