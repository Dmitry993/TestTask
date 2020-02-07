using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace NewsPortal.Web.Attributes
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cookie = context.HttpContext.Request.Cookies;

            var token = cookie["AuthorizationToken"];

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }           
        }
    }
}
