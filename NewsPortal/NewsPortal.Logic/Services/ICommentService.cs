using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsPortal.Logic.Model;

namespace NewsPortal.Logic.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<UserComment>> GetAllCommentsAsync();

        Task<UserComment> GetCommentAsync(int id);

        Task<UserComment> CreateCommentAsync(UserComment userPost);

        Task<IEnumerable<UserComment>> GetPostCommentsAsync(int id);
    }
}
