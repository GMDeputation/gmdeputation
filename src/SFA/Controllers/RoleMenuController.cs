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
    [Route("roleMenu")]
    [Authorize]
    public class RoleMenuController : Controller
    {
        private readonly IRoleMenuService _roleMenuService = null;

        public RoleMenuController(IRoleMenuService roleMenuService)
        {
            _roleMenuService = roleMenuService;
        }

        [Route("")] 
        [CheckAccess]
        public IActionResult Index()
        {
            return View();
        }

        [Route("api/getMenuByRole/{roleId}")]
        public async Task<IActionResult> GetMenuByRole(int roleId)
        {
            var menus = await _roleMenuService.GetMenuByRole(roleId);
            return new JsonResult(menus);
        }

        [HttpPost]
        [Route("api/saveRoleMenu")]
        public async Task<IActionResult> SaveRoleMenu([FromBody]RoleWiseMenu roleMenu)
        {
            var result = await _roleMenuService.SaveRoleMenu(roleMenu);
            return new JsonResult(result);
        }

    }
}
