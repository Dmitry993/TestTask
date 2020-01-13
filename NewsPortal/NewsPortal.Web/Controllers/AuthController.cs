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

namespace NewsPortal.Web.Controllers
{
    
    public class AuthController : Controller
    {
        private IConfiguration _config;       

        public AuthController(IConfiguration config)
        {
            _config = config;            
        }

        [HttpGet]        
        public async Task<ActionResult> GoogleSignIn()
        {
            var clientID = _config.GetSection("Authentication:Google:ClientId").Value;
            var clientSecret = _config.GetSection("Authentication:Google:ClientSecret").Value;
            var redirectUri = new Uri(Url.Action("GoogleLoginCallBack", "Auth", null));           
            var googleClient = new GoogleClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration            
            {
                ClientId = clientID?.Trim(),
                ClientSecret = clientSecret?.Trim(),
                RedirectUri = redirectUri.ToString(),
                Scope = "profile email"
            });
            return Redirect(await googleClient.GetLoginLinkUriAsync("SomeStateValueYouWantToUse"));
        }

        //public ActionResult GoogleLoginCallBack()
        //{
        //    var clientID = _config.GetSection("Authentication:Google:ClientId").Value;
        //    var clientSecret = _config.GetSection("Authentication:Google:ClientSecret").Value;
        //    //var code = Request.QueryString["code"];
        //    var redirectUri = new Uri(Url.Action("GoogleSignIn", "Auth", null));
        //    var googleClient = new GoogleClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
        //    {
        //        ClientId = clientID?.Trim(),
        //        ClientSecret = clientSecret?.Trim(),
        //        //RedirectUri = redirectUrl,
        //        Scope = "profile email"
        //    });

        //    try
        //    {
        //        userInfo = oauth.GetUserInfo(new NameValueCollection() { { "code", code } });
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("LoginError", new { error = ex.Message });
        //    }

        //    do your validation and allow the user to proceed
        //    if (_signInManager.IsUserValid(userInfo.Email))
        //    {
        //        _signInManager.Login(userInfo.Email);
        //        return RedirectToAction("Index", "Home", new { error = ex.Message });
        //    }
        //    return Redirect(googleClient"LoginError", new { error = "User does not exists in the system" });
        //}
    }
}