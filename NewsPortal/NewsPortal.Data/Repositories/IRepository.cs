using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);        
        Task<T> CreateAsync(T item);
        void Update(T item);
        Task UpdateAndSaveAsync(T item);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
