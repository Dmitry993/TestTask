using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Models;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web.Components
{
    [ViewComponent(Name = "Comment")]
    public class CommentComponent : ViewComponent
    {
        private ICommentService _commentService;

        public CommentComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId, List<Comment> replies)
        {
            if (replies != null && replies.Any())
            {
                return View("/Views/Comment/Comment.cshtml", replies);
            }

            if (postId == 0)
            {
                return Content("Comments not found");
            }

            var comments = await _commentService.GetPostCommentsAsync(postId);

            return View("/Views/Comment/Comment.cshtml", comments);
        }
    }
}
