using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Models;
using NewsPortal.Logic.Services;
using NewsPortal.Web.Attributes;

namespace NewsPortal.Web.Controllers
{
    [CustomAuthorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostAsync(id);

            return View("Post", post);
        }

        public IActionResult CreatePost()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post userPost)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);

            if (userPost == null)
            {
                return BadRequest();
            }

            userPost.AuthorId = userId;
            var post = await _postService.CreatePostAsync(userPost);

            return View("Post", post);
        }

        public IActionResult EditPost(Post userPost)
        {
            if (UserIsOwner(userPost))
            {
                return View("Edit", userPost);
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(Post userPost)
        {
            if (UserIsOwner(userPost))
            {
                var post = await _postService.UpdatePostAsync(userPost);
                return View("Post", post);
            }

            return Forbid();
        }

        private bool UserIsOwner(Post userPost)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);

            return userPost.AuthorId == userId;
        }
    }
}
