using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private bool _disposed = false;
        private readonly NewsPortalDbContext _context;

        public CommentRepository(NewsPortalDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment item)
        {
            await _context.Comments.AddAsync(item);
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
                _context.Comments.Remove(comment);
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetAsync(int id)
        {
           return await _context.Comments.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Comment item)
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
