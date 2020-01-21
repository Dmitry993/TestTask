using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;

namespace NewsPortal.Data.Repositories
{
    public class UserRepository : IRepository<ApplicationUserDb>
    {
        private readonly NewsPortalDbContext _context;        

        public UserRepository(NewsPortalDbContext context)
        {
            _context = context;
        }

        public void Create(ApplicationUserDb item)
        {            
            _context.Users.Add(item);
        }

        public void Delete(int id)
        {
            ApplicationUserDb user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }       

        public ApplicationUserDb Get(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<ApplicationUserDb> GetAll()
        {
            return _context.Users;
        }

        public async void Save()
        {
           await _context.SaveChangesAsync();
        }

        public void Update(ApplicationUserDb item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        private bool disposed = false;

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
