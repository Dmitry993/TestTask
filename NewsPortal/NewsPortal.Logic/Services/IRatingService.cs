using System.Threading.Tasks;
using NewsPortal.Logic.Models;

namespace NewsPortal.Logic.Services
{
    public interface IRatingService
    {
        Task AddRatingAsync(int postId, int userId, Rating value);

        Task<Rating> UserClickedRatingAsync(int postId, int userId, Rating value);

        Task CancelRatingAsync(int postId, int userId, Rating value);
    }
}
