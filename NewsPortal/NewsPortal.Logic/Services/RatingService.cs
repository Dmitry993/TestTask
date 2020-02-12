using AutoMapper;
using NewsPortal.Data.Repositories;
using NewsPortal.Logic.Models;
using System.Threading.Tasks;
using NewsPortal.Data.Models;

namespace NewsPortal.Logic.Services
{
    public class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepository _repository;
        private readonly IPostService _postService;

        public RatingService(IRatingRepository repository, IMapper mapper, 
            IPostService postService)
        {
            _repository = repository;
            _mapper = mapper;
            _postService = postService;
        }

        public async Task<bool?> UserClickedRatingAsync(int postId, int userId)
        {
            var rating = await _repository.FindItemByPostIdAndUserId(postId, userId);
            return rating?.Value;
        }

        public async Task CancelRatingAsync(int postId, int userId, bool value)
        {
            await _repository.DeleteItemByPostIdAndUserId(postId, userId);
            await _repository.SaveAsync();
            await _postService.UpdatePostRatingAsync(postId, null, value);
        }

        public async Task AddRatingAsync(int postId, int userId, bool value)
        {
            var rating = new PostRating()
            {
                PostId = postId,
                UserId = userId,
                Value = value
            };

            await _repository.CreateAsync(rating);
            await _repository.SaveAsync();
            await _postService.UpdatePostRatingAsync(postId, value, null);
        }
    }
}