using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsPortal.Data.Models;

namespace NewsPortal.Data.Repositories
{
    public interface IRatingRepository : IRepository<PostRating>
    {
        Task<PostRating> FindItemByPostIdAndUserId(int postId, int userId);

        Task DeleteItemByPostIdAndUserId(int postId, int userId);
    }
}
