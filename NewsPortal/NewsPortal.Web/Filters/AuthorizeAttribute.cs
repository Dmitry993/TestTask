using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NewsPortal.Web.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public const string SessionKey = "_AccessToken";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userToken = context.HttpContext.User.FindFirst("Access_token");
            var serverToken = context.HttpContext.Session.GetString(SessionKey);

            if (userToken == null | serverToken == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }          
            
            if (!serverToken.Equals(userToken.Value))
            {
                context.Result = new BadRequestObjectResult("User is not valid");
                Thread.Sleep(3000);
                context.Result = new RedirectToActionResult("GoogleSignIn", "Auth", null);
            }
        }
    }
}
