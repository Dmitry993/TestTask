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
