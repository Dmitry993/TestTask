using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Models;

namespace NewsPortal.Data.Repositories
{
    public class CommentRatingRepository : Repository<CommentRating>, ICommentRatingRepository
    {
        private readonly NewsPortalDbContext _context;

        public CommentRatingRepository(NewsPortalDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CommentRating> FindItem(int commentId, int userId)
        {
            return await _context.CommentRatings
                .Where(rating => rating.CommentId == commentId && rating.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteItem(int commentId, int userId)
        {
            var rating = await _context.CommentRatings
                .Where(rating => rating.CommentId == commentId && rating.UserId == userId)
                .FirstOrDefaultAsync();
            if (rating != null)
            {
                _context.CommentRatings.Remove(rating);
            }
        }
    }
}
