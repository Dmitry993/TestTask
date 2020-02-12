using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Models;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web.Components
{
    [ViewComponent(Name = "PostRating")]
    public class PostRatingComponent : ViewComponent
    {
        private readonly IRatingService _ratingService;
        private readonly IUserService _userService;

        public PostRatingComponent(IRatingService ratingService, IUserService userService)
        {
            _ratingService = ratingService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);
            var userClick = await _ratingService.UserClickedRatingAsync(postId, userId,Rating.Nothing);
            if (userClick.Equals(Rating.Add))
            {
                return Content("You clicked plus;");
            }
            if (userClick.Equals(Rating.Subtract))
            {
                return Content("You clicked minus;");
            }

            return Content(String.Empty);
        }
    }
}
