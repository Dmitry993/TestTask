using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var userClicked = await _ratingService.UserClickedRatingAsync(postId, userId);
            if (userClicked == true)
            {
                return Content("You clicked plus;");
            }
            if (userClicked == false)
            {
                return Content("You clicked minus;");
            }

            return Content(String.Empty);
        }
    }
}
