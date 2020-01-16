using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OAuth2.Client.Impl;
using OAuth2.Infrastructure;
using NewsPortal.Logic.Model;
using RestSharp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NewsPortal.Web.Attributes;
using OAuth2.Models;

namespace NewsPortal.Web.Controllers
{

    public class AuthController : Controller
    {
        public const string SessionKey = "_AccessToken";

        private IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [CustomAuth]
        public ActionResult Index()
        {
            return RedirectToPage("/Index");
        }

        [HttpGet]
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

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKey)))
            {
                HttpContext.Session.SetString(SessionKey, googleClient.AccessToken);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userInfo.FirstName),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim("Access_token", googleClient.AccessToken)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);            

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));           

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> GoogleSignOut()
        {            
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login");
        }
    }
}