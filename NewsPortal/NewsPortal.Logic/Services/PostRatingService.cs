using System;
using AutoMapper;
using NewsPortal.Data.Repositories;
using System.Threading.Tasks;
using NewsPortal.Data.Models;
using NewsPortal.Logic.Enums;

namespace NewsPortal.Logic.Services
{
    public class PostRatingService : IPostRatingService
    {
        private readonly IPostRatingRepository _repository;
        private readonly IPostService _postService;

        public PostRatingService(IPostRatingRepository repository, IPostService postService)
        {
            _repository = repository;
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
            var postRating = new PostRating()
            {
                PostId = postId,
                UserId = userId,
                Value = (int)value,
                Created = DateTime.UtcNow
            };

            await _repository.CreateAsync(postRating);
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