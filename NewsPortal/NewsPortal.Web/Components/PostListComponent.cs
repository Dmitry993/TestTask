using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Models;
using NewsPortal.Logic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsPortal.Logic.Enums;

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

        public async Task<IViewComponentResult> InvokeAsync(int id, Sort sort, SortDirection direction)
        {
            var posts = id == 0
                ? await _postService.GetAllPostsAsync()
                : await _postService.GetUserPostsAsync(id);
            if (sort.Equals(Sort.None))
            {
                return View("/Views/Post/PostList.cshtml", posts);
            }

            var sortedPosts = _postService.SortPosts(
                (List<Post>)posts, sort, direction);

            return View("/Views/Post/PostList.cshtml", sortedPosts);
        }
    }
}
