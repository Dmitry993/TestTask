using System;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Services;
using System.Threading.Tasks;
using NewsPortal.Logic.Model;

namespace NewsPortal.Web.Components
{
    [ViewComponent(Name = "AddComment")]
    public class AddCommentComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public AddCommentComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId, int? commentId = null)
        {
            var stringId = HttpContext.Request.Cookies["UserId"];
            var id = Int32.Parse(stringId);

            var user = await _userService.GetUserAsync(id);

            var comment = new UserComment()
            {
                PostId = postId,
                ParentId = commentId,
                UserName = user.UserName
            };

            return View("/Views/Comment/CreateComment.cshtml", comment);
        }
    }
}
