using System.Threading.Tasks;
using NewsPortal.Logic.Models;

namespace NewsPortal.Logic.Services
{
    public interface IRatingService
    {
        Task<Rating> GetPostRatingAsync(int id);
        Task<Rating> UpPostRatingAsync(int postId);
        Task<Rating> DownPostRatingAsync(int postId);
    }
}
