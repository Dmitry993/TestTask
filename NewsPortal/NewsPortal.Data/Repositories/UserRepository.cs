using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private bool _disposed = false;
        private readonly NewsPortalDbContext _context;

        public UserRepository(NewsPortalDbContext context)
        {
            _context = context;
        }

        public async Task<User> FindUserByGoogleIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(b =>b.GoogleId == id);
            return user;
        }

        public async Task<User> CreateAsync(User item)
        {
            await _context.Users.AddAsync(item);
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            User user = await _context.Users.FindAsync(id);
            if (user != null)
                _context.Users.Remove(user);
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(User item)
        {            
            _context.Entry(item).State = EntityState.Modified;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
