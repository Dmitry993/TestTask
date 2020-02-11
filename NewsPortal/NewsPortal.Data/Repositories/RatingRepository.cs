using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Models;

namespace NewsPortal.Data.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private bool _disposed = false;
        private readonly NewsPortalDbContext _context;

        public RatingRepository(NewsPortalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating> GetAsync(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task<Rating> CreateAsync(Rating item)
        {
            await _context.Ratings.AddAsync(item);
            return item;
        }

        public void Update(Rating item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
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
