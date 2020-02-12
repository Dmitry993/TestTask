using Microsoft.EntityFrameworkCore;
using NewsPortal.Data.Context;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly NewsPortalDbContext _context;

        public UserRepository(NewsPortalDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<User> FindUserByGoogleIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.GoogleId == id);
            return user;
        }

        public async Task<User> GetUserWithPostsAsync(int id)
        {            
            return await _context.Users.Include(user => user.Posts)
                .Where(user => user.Id == id).FirstOrDefaultAsync();
        }
    }
}
