using System;
using System.Threading.Tasks;
using NewsPortal.Data.Models;
using NewsPortal.Data.Repositories;
using NewsPortal.Logic.Enums;

namespace NewsPortal.Logic.Services
{
    public class CommentRatingService : ICommentRatingService
    {
        private readonly ICommentRatingRepository _repository;
        private readonly ICommentService _commentService;

        public CommentRatingService(ICommentRatingRepository repository, ICommentService commentService)
        {
            _repository = repository;
            _commentService = commentService;
        }

        public async Task AddRatingAsync(int commentId, int userId, Rating value)
        {
            var commentRating = new CommentRating()
            {
                CommentId = commentId,
                UserId = userId,
                Value = (int)value,
                Created = DateTime.UtcNow
            };

            await _repository.CreateAsync(commentRating);
            await _repository.SaveAsync();
            switch (value)
            {
                case Rating.Plus:
                    await _commentService.IncreaseRatingAsync(commentId);
                    break;
                case Rating.Minus:
                    await _commentService.DecreaseRatingAsync(commentId);
                    break;
            }
        }

        public async Task UpdateRatingAsync(int commentId, int userId, Rating value)
        {
            var commentRating = await _repository.FindItem(commentId, userId);
            if (commentRating == null && !value.Equals(Rating.None))
            {
                await AddRatingAsync(commentId, userId, value);
            }
            if (commentRating != null && commentRating.Value == (int)value)
            {
                await CancelRatingAsync(commentId, userId, value);
            }
        }

        public async Task<Rating> GetRatingAsync(int commentId, int userId)
        {
            var commentRating = await _repository.FindItem(commentId, userId);
            return commentRating == null ? Rating.None : (Rating)commentRating.Value;
        }

        public async Task CancelRatingAsync(int commentId, int userId, Rating value)
        {
            await _repository.DeleteItem(commentId, userId);
            await _repository.SaveAsync();
            switch (value)
            {
                case Rating.Plus:
                    await _commentService.DecreaseRatingAsync(commentId);
                    break;
                case Rating.Minus:
                    await _commentService.IncreaseRatingAsync(commentId);
                    break;
            }
        }
    }
}
