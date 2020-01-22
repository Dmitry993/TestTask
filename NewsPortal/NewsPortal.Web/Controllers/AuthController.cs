using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OAuth2.Client.Impl;
using OAuth2.Infrastructure;
using OAuth2.Models;
using NewsPortal.Logic.Services;
using NewsPortal.Logic.Model;

namespace NewsPortal.Web.Controllers
{
    public class AuthController : Controller
    {
        public const string AuthorizationToken = "AuthorizationToken";

        private IConfiguration _config;
        private IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService)
        {            
            _config = config;
            _userService = userService;
        }        

        public IActionResult Login()
        {
            return View("Login");
        }        
       
        public async Task<ActionResult> GoogleSignIn()
        {
            var clientID = _config.GetSection("Authentication:Google:ClientId").Value;
            var clientSecret = _config.GetSection("Authentication:Google:ClientSecret").Value;
            var redirectUri = new Uri(Url.Action("GoogleLoginCallBack", "Auth", null, "https"));
            var googleClient = new GoogleClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
            {
                ClientId = clientID?.Trim(),
                ClientSecret = clientSecret?.Trim(),
                RedirectUri = redirectUri.ToString(),
                Scope = "profile email"
            });

            return Redirect(await googleClient.GetLoginLinkUriAsync());
        }


        public async Task<ActionResult> GoogleLoginCallBack()
        {
            var code = HttpContext.Request.Query["code"];
            var userInfo = new UserInfo();
            var clientID = _config.GetSection("Authentication:Google:ClientId").Value;
            var clientSecret = _config.GetSection("Authentication:Google:ClientSecret").Value;
            var redirectUri = new Uri(Url.Action("GoogleLoginCallBack", "Auth", null, "https"));

            var googleClient = new GoogleClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
            {
                ClientId = clientID?.Trim(),
                ClientSecret = clientSecret?.Trim(),
                RedirectUri = redirectUri.ToString(),
                Scope = "profile email"
            });

            try
            {
                userInfo = await googleClient.GetUserInfoAsync(new NameValueCollection() { { "code", code } });
            }
            catch (Exception ex)
            {
                return RedirectToAction("LoginError", new { error = ex.Message });
            }

            if (userInfo.Id != null && !(await _userService.UserExist(userInfo.Id)))
            {
                _userService.CreateUser(CreateNewUser(userInfo));
            }

            HttpContext.Response.Cookies.Append(
                AuthorizationToken, 
                googleClient.AccessToken, 
                new CookieOptions { HttpOnly = false });

            return RedirectToAction("Index", "Home");
        }
        
        public ApplicationUser CreateNewUser(UserInfo userInfo)
        {            
            string userName = userInfo.Email.Split('@')[0];

            return new ApplicationUser()
            {
                GoogleId = userInfo.Id,
                Email = userInfo.Email.ToString(),
                UserName = userName,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName
            };
        }
        
        public ActionResult GoogleSignOut()
        {

            Response.Cookies.Delete(AuthorizationToken);

            return View("Login");
        }
    }
}