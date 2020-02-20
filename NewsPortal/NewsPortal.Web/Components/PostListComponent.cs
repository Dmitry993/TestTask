using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Services;
using System.Threading.Tasks;
using NewsPortal.Logic.Enums;
using X.PagedList;

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

        public async Task<IViewComponentResult> InvokeAsync(int userId, SortBy sort, bool isDescending, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            if (sort == SortBy.None)
            {
                var posts = userId == 0
                ? await _postService.GetAllPostsAsync()
                : await _postService.GetUserPostsAsync(userId);

                return View("/Views/Post/PostList.cshtml", posts.ToPagedList(pageNumber, pageSize));
            }

            var sortedPosts = _postService.GetSortedPosts(sort, isDescending, userId);

            return View("/Views/Post/PostList.cshtml", sortedPosts.ToPagedList(pageNumber, pageSize));
        }
    }
}
