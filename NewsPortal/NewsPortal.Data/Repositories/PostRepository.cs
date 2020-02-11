using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private bool _disposed = false;
        private readonly NewsPortalDbContext _context;

        public PostRepository(NewsPortalDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreateAsync(Post item)
        {
            await _context.Posts.AddAsync(item);
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetAsync(int id)
        {
            return await _context.Posts
                .Include(post => post.Rating)
                .Where(post => post.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> FindPostsByUserId(int id)
        {
            var allPosts = await _context.Posts.Where(post => post.AuthorId == id).ToListAsync();
            return allPosts;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Post item)
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
