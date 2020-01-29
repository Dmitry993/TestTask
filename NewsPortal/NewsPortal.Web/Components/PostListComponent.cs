using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Web.Views.Components
{
    [ViewComponent(Name = "PostList")]
    public class PostListComponent : ViewComponent
    {
        private IPostService _postService;

        public PostListComponent(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var posts = await _postService.GetAllPostsAsync();

            if (id != 0)
            {
                var userPosts = posts.Where(post => post.AuthorId == id).ToList();
                return View("/Views/Post/AllPosts.cshtml", userPosts);
            }

            return View("/Views/Post/AllPosts.cshtml", posts); 
        }
    }
}
