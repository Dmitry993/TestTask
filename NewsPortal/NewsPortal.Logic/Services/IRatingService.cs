using System.Threading.Tasks;
using NewsPortal.Logic.Models;

namespace NewsPortal.Logic.Services
{
    public interface IRatingService
    {
        Task AddRatingAsync(int postId, int userId, bool value);

        Task<bool?> UserClickedRatingAsync(int postId, int userId);

        Task CancelRatingAsync(int postId, int userId, bool value);
    }
}
