using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        public async Task<IActionResult> GetUser()
        {
            var stringId = HttpContext.Request.Cookies["UserId"];
            var id = Int32.Parse(stringId);

            var user = await _userService.GetUserAsync(id);
            
            if (user == null)
            {
                return RedirectToAction("GoogleSignOut", "Auth");
            }
            return View("UserProfile", user);
        }
    }
}
