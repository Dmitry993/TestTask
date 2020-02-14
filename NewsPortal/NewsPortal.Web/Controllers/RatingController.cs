using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Enums;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web.Controllers
{
    public class RatingController : Controller
    {
        private readonly IPostRatingService _postRating;
        private readonly ICommentRatingService _commentRating;

        public RatingController(IPostRatingService postRating, 
            ICommentRatingService commentRating)
        {
            _postRating = postRating;
            _commentRating = commentRating;
        }

        public async Task<IActionResult> ChangePostRating(int postId, Rating value)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);
            await _postRating.UpdateRatingAsync(postId, userId, value);
            return RedirectToAction("GetPostById", "Post", new {id = postId});
        }

        public async Task<IActionResult> ChangeCommentRating(int commentId, int postId, Rating value)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);
            await _commentRating.UpdateRatingAsync(commentId, userId, value);
            return RedirectToAction("GetPostById", "Post", new {id = postId});
        }
    }
}