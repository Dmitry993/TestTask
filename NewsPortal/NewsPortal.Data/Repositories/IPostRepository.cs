using NewsPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> FindPostsByUserId(int id);

        IEnumerable<Post> GetSortedPosts<T>(int userId, Func<Post, T> expression, bool isDescending);
    }
}