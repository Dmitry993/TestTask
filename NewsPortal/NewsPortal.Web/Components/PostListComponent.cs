using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Models;
using NewsPortal.Logic.Services;
using System.Collections.Generic;
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
            var posts = new List<Post>();

            if (id == 0)
            {
                posts = (List<Post>) await _postService.GetAllPostsAsync();
            }
            else
            {
                posts = (List<Post>) await _postService.GetUserPostsAsync(id);               
            }

            return View("/Views/Post/PostList.cshtml", posts); 
        }
    }
}
