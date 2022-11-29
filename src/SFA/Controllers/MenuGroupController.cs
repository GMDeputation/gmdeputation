using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFA.Filters;
using SFA.Models;
using SFA.Services;

namespace SFA.Controllers
{
    [Route("menu-group")]
    [Authorize]
    public class MenuGroupController : Controller
    {
        private readonly IMenuGroupService _menuGroupService = null;

        public MenuGroupController(IMenuGroupService menuGroupService)
        {
            _menuGroupService = menuGroupService;
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
            var menuGroups = await _menuGroupService.GetAll();
            return Json(menuGroups);
        }

        [Route("api/allActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var menuGroups = await _menuGroupService.GetAllActive();
            return Json(menuGroups);
        }

        [Route("api/allCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var results = await _menuGroupService.GetAllCategory();
            return new JsonResult(results);
        }

        [Route("api/getByCategory/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var results = await _menuGroupService.GetByCategory(category);
            return new JsonResult(results);
        }

        [Route("api/get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var menuGroup = await _menuGroupService.GetById(id);
            return Json(menuGroup);
        }

        [HttpPost]
        [Route("api/save")]
        public async Task<IActionResult> Save([FromBody]MenuGroup menuGroup)
        {
            var saveResult = await _menuGroupService.Save(menuGroup);
            return Json(saveResult);
        }
    }
}
