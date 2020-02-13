using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Models;

namespace NewsPortal.Data.Repositories
{
    public class PostRatingRepository : Repository<PostRating>, IPostRatingRepository
    {
        private readonly NewsPortalDbContext _context;

        public PostRatingRepository(NewsPortalDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PostRating> FindItem(int postId, int userId)
        {
            return await _context.PostRatings
                .Where(rating => rating.PostId == postId && rating.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteItem(int postId, int userId)
        {
            var rating = await _context.PostRatings
                .Where(rating => rating.PostId == postId && rating.UserId == userId)
                .FirstOrDefaultAsync();
            if (rating != null)
            {
                _context.PostRatings.Remove(rating);
            }
        }

    }
}
