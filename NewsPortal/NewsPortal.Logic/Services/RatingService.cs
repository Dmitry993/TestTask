using AutoMapper;
using NewsPortal.Data.Repositories;
using NewsPortal.Logic.Models;
using System.Threading.Tasks;
using NewsPortal.Data.Models;
using NewsPortal.Logic.Enums;

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

        public async Task UpdateRatingAsync(int postId, int userId, Rating value)
        {
            var postRating = await _repository.FindItem(postId, userId);
            if (postRating == null && !value.Equals(Rating.None))
            {
                await AddRatingAsync(postId, userId, value);
            }
            if (postRating != null && postRating.Value == (int)value)
            {
                await CancelRatingAsync(postId, userId, value);
            }
        }

        public async Task<Rating> GetRatingAsync(int postId, int userId)
        {
            var postRating = await _repository.FindItem(postId, userId);
            return postRating == null ? Rating.None : (Rating)postRating.Value;
        }

        public async Task CancelRatingAsync(int postId, int userId, Rating value)
        {
            await _repository.DeleteItem(postId, userId);
            await _repository.SaveAsync();
            switch (value)
            {
                case Rating.Plus:
                    await _postService.DecreaseRatingAsync(postId);
                    break;
                case Rating.Minus:
                    await _postService.IncreaseRatingAsync(postId);
                    break;
            }
        }

        public async Task AddRatingAsync(int postId, int userId, Rating value)
        {
            var rating = new PostRating()
            {
                PostId = postId,
                UserId = userId,
                Value = (int)value
            };

            await _repository.CreateAsync(rating);
            await _repository.SaveAsync();
            switch (value)
            {
                case Rating.Plus:
                    await _postService.IncreaseRatingAsync(postId);
                    break;
                case Rating.Minus:
                    await _postService.DecreaseRatingAsync(postId);
                    break;
            }
        }
    }
}