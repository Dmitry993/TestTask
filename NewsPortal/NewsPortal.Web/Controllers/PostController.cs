﻿using System;
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

        public IActionResult GetPost(UserPost userPost)
        {
            return View("Post", userPost);
        }

        public IActionResult CreatePost()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(UserPost userPost)
        {
            var stringId = HttpContext.Request.Cookies["UserId"];
            var id = Int32.Parse(stringId);

            userPost.AuthorId = id;
            var post = await _postService.CreatePostAsync(userPost);

            if (userPost == null)
            {
                return BadRequest();
            }
            return View("Post", post);
        }

        public IActionResult EditPost(UserPost userPost)
        {
            if (UserIsOwner(userPost))
            {
                return View("Edit", userPost);
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(UserPost userPost)
        {
            if (UserIsOwner(userPost))
            {
                var post = await _postService.UpdatePostAsync(userPost);
                return View("Post", post);
            }

            return Forbid();
        }

        private bool UserIsOwner(UserPost userPost)
        {
            var stringId = HttpContext.Request.Cookies["UserId"];
            var id = Int32.Parse(stringId);

            return userPost.AuthorId == id;
        }
    }
}
