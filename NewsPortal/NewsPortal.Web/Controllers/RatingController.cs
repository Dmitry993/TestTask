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
        private IRatingService _service;

        public RatingController(IRatingService service)
        {
            _service = service;
        }

        public async Task<IActionResult> UpRating(int postId)
        {
            await _service.UpPostRatingAsync(postId);

            return RedirectToAction("GetPostById", "Post", new { id = postId });
        }

        public async Task<IActionResult> DownRating(int postId)
        {
            await _service.DownPostRatingAsync(postId);

            return RedirectToAction("GetPostById", "Post", new { id = postId });
        }
    }
}