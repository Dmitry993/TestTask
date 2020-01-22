using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private bool disposed = false;
        private readonly NewsPortalDbContext _context;        

        public UserRepository(NewsPortalDbContext context)
        {
            _context = context;
        }

        public void Create(User item)
        {            
           _context.Users.Add(item);            
        }

        public void Delete(int id)
        {
            User user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }       

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }       

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public void Save()
        {
           _context.SaveChanges();
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
