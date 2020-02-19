using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Services;
using System.ComponentModel;
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

        public async Task<IViewComponentResult> InvokeAsync(int userId, SortBy sort, ListSortDirection direction)
        {
            if (sort == SortBy.None)
            {
                var posts = userId == 0
                ? await _postService.GetAllPostsAsync()
                : await _postService.GetUserPostsAsync(userId);
            
                return View("/Views/Post/PostList.cshtml", posts);
            }

            var isDescending = direction == ListSortDirection.Descending;

            var sortedPosts = _postService.GetSortedPosts(sort, isDescending, userId);

            return View("/Views/Post/PostList.cshtml", sortedPosts);
        }
    }
}
