using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsPortal.Web.Attributes
{
    public class CustomAuthAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public const string SessionKey = "_AccessToken";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userToken = context.HttpContext.User.FindFirst("Access_token");
            var serverToken = context.HttpContext.Session.GetString(SessionKey);

            if (userToken.Equals(serverToken))
            {
                return;
            }

        }
    }
}
