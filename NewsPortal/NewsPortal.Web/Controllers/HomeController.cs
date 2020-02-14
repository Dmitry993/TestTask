using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Logic.Enums;
using NewsPortal.Logic.Services;
using NewsPortal.Web.Attributes;

namespace NewsPortal.Web.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {              
        private IUserService _userService;
       
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            ViewData["sort"] = Sort.None;
            ViewData["direction"] = SortDirection.Ascending;
            return View();
        }

        public async Task<IActionResult> GetUser(Sort sort, SortDirection direction)
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            var userId = Int32.Parse(userIdString);

            var user = await _userService.GetUserAsync(userId);
            
            if (user == null)
            {
                return RedirectToAction("GoogleSignOut", "Auth");
            }
            ViewData["sort"] = sort;
            ViewData["direction"] = direction;
            return View("UserProfile", user);
        }
    }
}
