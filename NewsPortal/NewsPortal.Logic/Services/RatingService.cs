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

        public async Task<Rating> UserClickedRatingAsync(int postId, int userId, Rating value)
        {
            var rating = await _repository.FindItemByPostId(postId, userId);
            if (rating == null && !value.Equals(Rating.Nothing))
            {
                await AddRatingAsync(postId, userId, value);
            }
            if (rating != null && rating.Value == (int)value)
            {
                await CancelRatingAsync(postId, userId, value);
            }

            var ratingValue = rating != null ? (Rating)rating.Value : Rating.Nothing;
            return ratingValue;
        }

        public async Task CancelRatingAsync(int postId, int userId, Rating value)
        {
            await _repository.DeleteItemByPostId(postId, userId);
            await _repository.SaveAsync();
            var reverseRating = Rating.Add.Equals(value) ? Rating.Subtract : Rating.Add;
            await _postService.UpdatePostRatingAsync(postId, reverseRating);
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
            await _postService.UpdatePostRatingAsync(postId, value);
        }
    }
}