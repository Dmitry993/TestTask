using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly NewsPortalDbContext _context;

        public PostRepository(NewsPortalDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> FindPostsByUserId(int id)
        {
            var allPosts = await _context.Posts.Where(post => post.AuthorId == id).ToListAsync();
            return allPosts;
        }

        public IEnumerable<Post> GetSortedPosts<T>(int userId, Func<Post, T> expression, bool isDescending)
        {
            var sortedPosts = isDescending
                ? _context.Posts.Where(post => post.AuthorId == userId || userId == 0).ToList()
                    .OrderByDescending(expression)
                : _context.Posts.Where(post => post.AuthorId == userId || userId == 0).ToList()
                    .OrderBy(expression);
            return sortedPosts;
        }
    }
}