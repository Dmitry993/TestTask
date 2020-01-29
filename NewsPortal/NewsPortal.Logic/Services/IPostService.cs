using NewsPortal.Logic.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Logic.Services
{
    public interface IPostService
    {
        Task<IEnumerable<UserPost>> GetAllPostsAsync();

        Task<UserPost> GetPostAsync(int id);

        Task<IEnumerable<UserPost>> GetUserPostsAsync(int id);

        Task<UserPost> CreatePostAsync(UserPost userPost);       
    }
}
