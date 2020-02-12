using NewsPortal.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Logic.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();

        Task<Post> GetPostAsync(int id);

        Task UpdatePostRatingAsync(int postId, Rating value);

        Task<IEnumerable<Post>> GetUserPostsAsync(int id);

        Task<Post> CreatePostAsync(Post userPost);

        Task<Post> UpdatePostAsync(Post userPost);
    }
}
