using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFA.Extensions;
using SFA.Filters;
using SFA.Models;
using SFA.Services;

namespace SFA.Controllers
{
    [Authorize]
    [Route("nav")]
    public class NavigationController : Controller
    {
        [Route("{category}")]
        public IActionResult Index(string category)
        {
            ViewBag.Category = category;
            return View();
        }
    }
}
