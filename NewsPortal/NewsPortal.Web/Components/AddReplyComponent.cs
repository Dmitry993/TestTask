using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Model;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web.Components
{
    [ViewComponent(Name = "ReplyToComment")]
    public class AddReplyComponent : ViewComponent
    {
        private ICommentService _commentService;

        public AddReplyComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }
               
        public async Task<IViewComponentResult> InvokeAsync(int commentId, string userName)
        {
            var comment = new UserComment()
            {
                ParentId = commentId,
                UserName = userName
            };

            var result = View("/Views/Comment/Reply.cshtml", comment);
            return await Task.FromResult<IViewComponentResult>(result);
        }
    }
}
