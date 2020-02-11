using AutoMapper;
using NewsPortal.Data.Repositories;
using NewsPortal.Logic.Models;
using System.Threading.Tasks;

namespace NewsPortal.Logic.Services
{
    public class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepository _repository;
        private readonly IPostRepository _postRepository;

        public RatingService(IRatingRepository repository, IMapper mapper, 
            IPostRepository postRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<Rating> GetPostRatingAsync(int id)
        {
            var rating = await _repository.GetAsync(id);
            return _mapper.Map<Rating>(rating);
        }

        public async Task<Rating> UpPostRatingAsync(int postId)
        {
            var post = await _postRepository.GetAsync(postId);
            var newRating = new Rating()
            { 
                PostId = post.Id,
                Value = post.Rating.Value += 1
            };
            var mappedRating = _mapper.Map<Data.Models.Rating>(newRating);
            await _repository.CreateAsync(mappedRating);
            await _repository.SaveAsync();
            post.RatingId = mappedRating.Id;
            _postRepository.Update(post);
            await _postRepository.SaveAsync();
            return newRating;
        }

         public async Task<Rating> DownPostRatingAsync(int postId)
        {
            var post = await _postRepository.GetAsync(postId);
            var newRating = new Rating()
            {
                PostId = post.Id,
                Value = post.Rating.Value -= 1
            };
            var mappedRating = _mapper.Map<Data.Models.Rating>(newRating);
            await _repository.CreateAsync(mappedRating);
            await _repository.SaveAsync();
            post.RatingId = mappedRating.Id;
            _postRepository.Update(post);
            await _postRepository.SaveAsync();
            return newRating;
        }
    }
}