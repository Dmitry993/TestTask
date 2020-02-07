using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsPortal.Logic.Models;

namespace NewsPortal.Logic.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();

        Task<Comment> GetCommentAsync(int id);

        Task<Comment> CreateCommentAsync(Comment post);

        Task<IEnumerable<Comment>> GetPostCommentsAsync(int id);
    }
}
