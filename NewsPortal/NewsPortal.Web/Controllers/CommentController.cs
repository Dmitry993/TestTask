using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Model;
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

        public async Task<IActionResult> GetComment(UserComment userComment)
        {
            var comment = await _commentService.CreateCommentAsync(userComment);

            return RedirectToPage("Index", "/Views/Home");
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment(UserComment userComment)
        {
            var comment = await _commentService.CreateCommentAsync(userComment);

            return RedirectToAction("GetPostById","Post", new { id = comment.PostId});
        }
    }
}