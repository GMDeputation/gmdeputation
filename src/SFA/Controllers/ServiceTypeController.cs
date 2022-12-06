using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.Models;
using SFA.Extensions;
using Microsoft.AspNetCore.Authorization;
using SFA.Filters;
using Microsoft.AspNetCore.Hosting;
using SFA.Entities;
using System.IO;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;

namespace SFA.Controllers
{
    //[Authorize]
    [Route("serviceType")]
    [Authorize]
    public class ServiceTypeController : Controller
    {
        private readonly IServiceTypeService _serviceTypeService = null;
        private readonly IWebHostEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public ServiceTypeController(IServiceTypeService serviceTypeService, IWebHostEnvironment environment, SFADBContext context)
        {
            _serviceTypeService = serviceTypeService;
            _environment = environment;
            _context = context;
        }

        [Route("")]
        [CheckAccess]
        public IActionResult Index()
        {
            return View();
        }

        [Route("detail/{id}")]
        [CheckAccess]
        public IActionResult Detail(int? id)
        {
            ViewBag.Id = id;
            return View();
        }

        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var serviceType = await _serviceTypeService.GetAll();
            return new JsonResult(serviceType);
        }
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var serviceType = await _serviceTypeService.GetById(id);
            return new JsonResult(serviceType);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var result = await _serviceTypeService.Delete(id, loggedinUser.Id);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]ServiceTypeQuery query)
        {
            var serviceType = await _serviceTypeService.Search(query);
            return new JsonResult(serviceType);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody]ServiceType serviceType)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");


            var result = await _serviceTypeService.Save(serviceType, loggedinUser);
            return new JsonResult(result);
        }
    }
}