using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Enums;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web.Components
{
    [ViewComponent(Name = "PostRating")]
    public class PostRatingComponent : ViewComponent
    {
        private readonly IRatingService _ratingService;

        public PostRatingComponent(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);
            var userClick = await _ratingService.GetRatingAsync(postId, userId);
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