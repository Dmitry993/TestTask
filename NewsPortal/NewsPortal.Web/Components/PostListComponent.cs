using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Model;
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
            var posts = new List<UserPost>();

            if (id == 0)
            {
                posts = (List<UserPost>) await _postService.GetAllPostsAsync();
            }
            else
            {
                posts = (List<UserPost>) await _postService.GetUserPostsAsync(id);               
            }

            return View("/Views/Post/AllPosts.cshtml", posts); 
        }
    }
}
