using NewsPortal.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
    }
}
