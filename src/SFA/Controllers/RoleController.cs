using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.Extensions;
using SFA.Filters;
using SFA.Models;
using SFA.Services;

namespace SFA.Controllers
{
    [Route("role")]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService = null;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Route("")]
        [CheckAccess]
        public IActionResult Index()
        {
            return View();
        }

        [Route("detail/{id?}")]
        [CheckAccess]
        public IActionResult Detail(int? id)
        {
            ViewBag.Id = id;
            return View();
        }

        [Route("api/all")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            
            return Json(roles);
        }

        [Route("api/get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetById(id);
            return Json(role);
        }

        [HttpPost]
        [Route("api/search")]
        public async Task<IActionResult> Search([FromBody][Bind("Name,Group,Limit,Order,Page")]RoleQuery query)
        {
            var searchResult = await _roleService.Search(query);
            return Json(searchResult);
        }

        [HttpPost]
        [Route("api/save")]
        public async Task<IActionResult> Save([FromBody]Role role)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var saveResult = await _roleService.Save(role, loggedinUser);
            return Json(saveResult);
        }

    }
}
