using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Web.Views.Components
{
    public class PostListViewComponent : ViewComponent
    {
        private IPostService _postService;

        public PostListViewComponent(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await _postService.GetAllPostsAsync();

            return View("AllPosts", posts); 
        }
    }
}
