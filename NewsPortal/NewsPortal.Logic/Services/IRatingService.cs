using System.Threading.Tasks;
using NewsPortal.Logic.Enums;

namespace NewsPortal.Logic.Services
{
    public interface IRatingService
    {
        Task AddRatingAsync(int postId, int userId, Rating value);

        Task AddOrCancelRatingAsync(int postId, int userId, Rating value);

        Task<Rating> GetRatingAsync(int postId, int userId);

        Task CancelRatingAsync(int postId, int userId, Rating value);
    }
}
