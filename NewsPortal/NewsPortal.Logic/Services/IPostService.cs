using NewsPortal.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsPortal.Logic.Enums;

namespace NewsPortal.Logic.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();

        Task<Post> GetPostAsync(int id);

        Task IncreaseRatingAsync(int postId);

        Task DecreaseRatingAsync(int postId);

        Task<IEnumerable<Post>> GetUserPostsAsync(int id);

        Task<Post> CreatePostAsync(Post userPost);

        Task<Post> UpdatePostAsync(Post userPost);

        IEnumerable<Post> GetSortedPosts(SortBy sort,
            bool isDescending, int userId);
    }
}
