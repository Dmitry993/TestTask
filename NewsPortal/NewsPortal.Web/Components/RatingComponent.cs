using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Enums;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web.Components
{
    [ViewComponent(Name = "Rating")]
    public class RatingComponent : ViewComponent
    {
        private readonly IPostRatingService _postRating;
        private readonly ICommentRatingService _commentRating;

        public RatingComponent(IPostRatingService postRating, ICommentRatingService commentRating)
        {
            _postRating = postRating;
            _commentRating = commentRating;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId, int commentId)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);
            var userClick = postId == 0
                ? await _commentRating.GetRatingAsync(commentId, userId)
                : await _postRating.GetRatingAsync(postId, userId);
            switch (userClick)
            {
                case Rating.Plus:
                    return Content("You clicked plus;");
                case Rating.Minus:
                    return Content("You clicked minus;");
                default:
                    return Content(String.Empty);
            }
        }
    }
}