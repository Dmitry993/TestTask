using System;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Services;
using System.Threading.Tasks;
using NewsPortal.Logic.Models;

namespace NewsPortal.Web.Components
{
    [ViewComponent(Name = "CommentEditor")]
    public class CommentEditorComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public CommentEditorComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId, int? commentId = null)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);

            var comment = new Comment()
            {
                PostId = postId,
                ParentId = commentId,
                UserId = userId
            };

            return View("/Views/Comment/CommentEditor.cshtml", comment);
        }
    }
}
