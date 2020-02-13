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
    }
}