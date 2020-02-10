using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Models;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            var newComment = await _commentService.CreateCommentAsync(comment);

            return RedirectToAction("GetPostById","Post", new { id = comment.PostId});
        }
    }
}