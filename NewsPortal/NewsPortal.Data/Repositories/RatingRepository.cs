﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Models;

namespace NewsPortal.Data.Repositories
{
    public class RatingRepository : Repository<PostRating>, IRatingRepository
    {
        private readonly NewsPortalDbContext _context;

        public RatingRepository(NewsPortalDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PostRating> FindItemByPostIdAndUserId(int postId, int userId)
        {
            return await _context.Ratings
                .Where(rating => rating.PostId == postId && rating.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteItemByPostIdAndUserId(int postId, int userId)
        {
            var rating = await _context.Ratings
                .Where(rating => rating.PostId == postId && rating.UserId == userId)
                .FirstOrDefaultAsync();
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
            }
        }
    }
}
