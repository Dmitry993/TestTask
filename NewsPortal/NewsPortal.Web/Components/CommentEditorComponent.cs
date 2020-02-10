using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IViewComponentResult> InvokeAsync(List<Comment> comments, int postId, int? commentId = null)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);

            if (comments != null && comments.Any())
            {
                var cooment = comments.FirstOrDefault();

                var replyToComment = new Comment()
                {
                    PostId = cooment.PostId,
                    ParentId = cooment.Id,
                    UserId = userId
                };
                return View("/Views/Comment/CommentEditor.cshtml", cooment);
            }

            var postComment = new Comment()
            {
                PostId = postId,
                ParentId = commentId,
                UserId = userId
            };

            return View("/Views/Comment/CommentEditor.cshtml", postComment);
        }
    }
}
