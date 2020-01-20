using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Data.Repositories
{
    interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetAllList();
        T GetBook(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
