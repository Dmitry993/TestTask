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
        public Task<IViewComponentResult> InvokeAsync(List<Comment> comments, int postId, int? commentId = null)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);

            if (comments != null && comments.Any())
            {
                var comment = comments.FirstOrDefault();

                var replyToComment = new Comment()
                {
                    PostId = comment.PostId,
                    ParentId = comment.Id,
                    UserId = userId
                };
                return Task.FromResult <IViewComponentResult> 
                    (View("/Views/Comment/CommentEditor.cshtml", comment));
            }

            var postComment = new Comment()
            {
                PostId = postId,
                ParentId = commentId,
                UserId = userId
            };

            return Task.FromResult <IViewComponentResult> 
                (View("/Views/Comment/CommentEditor.cshtml", postComment));
        }
    }
}
