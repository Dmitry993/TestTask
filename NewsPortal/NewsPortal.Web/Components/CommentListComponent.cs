using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            if (postId == 0)
            {
                return Content("Comments not found");
            }

            var comments = await _commentService.GetPostCommentsAsync(postId);

            return View("/Views/Comment/CommentList.cshtml", comments);
        }
    }
}
