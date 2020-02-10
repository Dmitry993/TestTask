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

        public Task<IViewComponentResult> InvokeAsync(List<Comment> replies)
        {
            if (replies == null)
            {
                return Task.FromResult <IViewComponentResult> 
                    (Content("Comments not found"));
            }

            return Task.FromResult <IViewComponentResult> 
                (View("/Views/Comment/Comment.cshtml", replies));
        }
    }
}
