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
    [Route("menu")]
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService = null;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
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
            var menus = await _menuService.GetAll();
            return Json(menus);
        }

        [Route("api/get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var menu = await _menuService.GetById(id);
            return Json(menu);
        }

        [HttpPost]
        [Route("api/search")]
        public async Task<IActionResult> Search([FromBody][Bind("Name,Group,Limit,Order,Page")]MenuQuery query)
        {
            var searchResult = await _menuService.Search(query);
            return Json(searchResult);
        }

        [HttpPost]
        [Route("api/save")]
        public async Task<IActionResult> Save([FromBody]Menu menu)
        {
            var sessionUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var userId = sessionUser.Id;
            menu.InsertUser = userId.ToString();
            menu.UpdateUser = userId.ToString();
            var saveResult = await _menuService.Save(menu);
            return Json(saveResult);
        }

        
    }
}
