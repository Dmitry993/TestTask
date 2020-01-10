using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OAuth2.Client.Impl;
using OAuth2.Infrastructure;

namespace NewsPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> GoogleSignIn()
        {           
            var redirectUri = new Uri(Url.Action("GoogleLoginCallBack", "Account", null, protocol: Request.Scheme));
            var googleClient = new GoogleClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
            {
                ClientId = "1097896526158 - luavjh43lrku3a8nhpkqii6dgpumt9jj.apps.googleusercontent.com".Trim(),
                ClientSecret = "qbqOZP3Kgj3J-0dXT-F5950h".Trim(),
                RedirectUri = "http://localhost:65277",
                Scope = "profile email"
            });
            return Redirect(await googleClient.GetLoginLinkUriAsync("SomeStateValueYouWantToUse"));
        }

        //public ActionResult GoogleLoginCallBack()
        //{
        //    var code = Request.QueryString["code"];
        //    var redirectUri = new Uri(Url.Action("GoogleLoginCallBack", "Account", null, protocol: Request.Scheme));
        //    var googleClient = new GoogleClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
        //    {
        //        ClientId = "1097896526158 - luavjh43lrku3a8nhpkqii6dgpumt9jj.apps.googleusercontent.com".Trim(),
        //        ClientSecret = "qbqOZP3Kgj3J-0dXT-F5950h".Trim(),
        //        RedirectUri = redirectUrl,
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

        //    // do your validation and allow the user to proceed
        //    if (SignInManager.IsUserValid(userInfo.Email))
        //    {
        //        SignInManager.Login(userInfo.Email);
        //        return RedirectToAction("Index", "Home", new { error = ex.Message });
        //    }
        //    return Redirect(googleClient"LoginError", new { error = "User does not exists in the system" });
        //}
    }
}