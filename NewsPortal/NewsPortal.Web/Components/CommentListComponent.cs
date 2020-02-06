using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Model;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web.Components
{
    [ViewComponent(Name = "CommentList")]
    public class CommentListComponent : ViewComponent
    {
        private ICommentService _commentService;

        public CommentListComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId, List<UserComment> reply, int commentId)
        {
            if (postId == 0 & commentId == 0)
            {
                return View("/Views/Home/Index.cshtml");
            }

            if (reply != null)
            {
                return View("/Views/Comment/ReplyToComment.cshtml", reply);
            }

            var comments = await _commentService.GetPostCommentsAsync(postId);

            return View("/Views/Comment/Comment.cshtml", comments);
        }
    }
}
