using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Web.Attributes;

namespace NewsPortal.Web.Controllers
{
    
    public class HomeController : Controller
    {
        [CustomAuthorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}