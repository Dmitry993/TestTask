using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsPortal.Data.Models;

namespace NewsPortal.Data.Repositories
{
    public interface IRatingRepository : IRepository<PostRating>
    {
        Task<PostRating> FindItem(int postId, int userId);

        Task DeleteItem(int postId, int userId);
    }
}
