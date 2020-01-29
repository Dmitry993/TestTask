using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Model;
using NewsPortal.Logic.Services;
using NewsPortal.Web.Attributes;

namespace NewsPortal.Web.Controllers
{
    [CustomAuthorize]
    public class PostController : Controller
    {
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Post(UserPost userPost)
        {
            return View("Post", userPost);
        }
        
        public IActionResult CreatePost()
        {
            return View("Create");
        }        

        public async Task<IActionResult> Create(UserPost userPost)
        {
            var stringId = HttpContext.Request.Cookies["UserId"];
            var id = Int32.Parse(stringId);

            userPost.AuthorId = id;
            await _postService.CreatePostAsync(userPost);

            if (userPost == null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", "Home");
        }        
    }
}