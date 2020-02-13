using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly NewsPortalDbContext _context;

        public CommentRepository(NewsPortalDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int postId)
        {
            var comments = await _context.Comments
                .Include(comment=>comment.Author)
                .Where(comment => comment.PostId == postId)
                .ToListAsync();
            return comments;
        }
    }
}
