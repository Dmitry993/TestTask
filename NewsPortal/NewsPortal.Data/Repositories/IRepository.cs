using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task<IEnumerable<T>> GetAll();
        T Get(int id);        
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
