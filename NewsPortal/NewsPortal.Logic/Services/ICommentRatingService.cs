using System.Threading.Tasks;
using AutoMapper;
using NewsPortal.Logic.Enums;

namespace NewsPortal.Logic.Services
{
    public interface ICommentRatingService
    {
        Task AddRatingAsync(int commentId, int userId, Rating value);

        Task UpdateRatingAsync(int commentId, int userId, Rating value);

        Task<Rating> GetRatingAsync(int commentId, int userId);

        Task CancelRatingAsync(int commentId, int userId, Rating value);
    }
}
