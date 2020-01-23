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
using AutoMapper;

namespace NewsPortal.Web.Controllers
{
    public class AuthController : Controller
    {
        public const string AUTHORIZATION_TOKEN = "AuthorizationToken";
        public const string USER_ID = "UserId";

        private IConfiguration _config;
        private IUserService _userService;
        private IMapper _mapper;

        public AuthController(IConfiguration config, IUserService userService, IMapper mapper)
        {
            _config = config;
            _userService = userService;
            _mapper = mapper;
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
            var userId = "";
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

            if (userInfo.Id != null)
            {
                var user = _mapper.Map<ApplicationUser>(userInfo);
                var id = await _userService.GetUserIdAsync(user);
                userId = id;
            }

            HttpContext.Response.Cookies.Append(
                AUTHORIZATION_TOKEN,
                googleClient.AccessToken,
                new CookieOptions { HttpOnly = false });

            HttpContext.Response.Cookies.Append(
                USER_ID,
                userId,
                new CookieOptions { HttpOnly = false });

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GoogleSignOut()
        {

            Response.Cookies.Delete(AUTHORIZATION_TOKEN);
            Response.Cookies.Delete(USER_ID);

            return View("Login");
        }
    }
}