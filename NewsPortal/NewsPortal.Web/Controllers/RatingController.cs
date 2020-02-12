using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Models;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService _service;

        public RatingController(IRatingService service)
        {
            _service = service;
        }

        public async Task<IActionResult> ChangePostRating(int postId, bool value)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);
            var userClicked = await _service.UserClickedRatingAsync(postId, userId);
            if (userClicked == value)
            {
                await _service.CancelRatingAsync(postId, userId, value);
                return RedirectToAction("GetPostById", "Post", new { id = postId });
            }
            if (userClicked == null)
            {
                await _service.AddRatingAsync(postId, userId, value);
                return RedirectToAction("GetPostById", "Post", new { id = postId });
            }

            return RedirectToAction("GetPostById", "Post", new { id = postId });
        }
    }
}