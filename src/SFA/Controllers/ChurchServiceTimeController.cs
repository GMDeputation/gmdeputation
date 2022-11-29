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
    [Route("churchServiceTime")]
    [Authorize]
    public class ChurchServiceTimeController : Controller
    {
        private readonly IChurchServiceTimeService _churchServiceTimeService = null;
        private readonly IHostingEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public ChurchServiceTimeController(IChurchServiceTimeService churchServiceTimeService, IHostingEnvironment environment, SFADBContext context)
        {
            _churchServiceTimeService = churchServiceTimeService;
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
            var churchServiceTime = await _churchServiceTimeService.GetAll();
            return new JsonResult(churchServiceTime);
        }
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var churchServiceTime = await _churchServiceTimeService.GetById(id);
            return new JsonResult(churchServiceTime);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]ChurchServiceTimeQuery query)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var churchServiceTime = await _churchServiceTimeService.Search(query, loggedinUser.DataAccessCode, loggedinUser.Id);
            return new JsonResult(churchServiceTime);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody]ChurchServiceTime churchServiceTime)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            churchServiceTime.CreatedBy = loggedinUser.Id;
            churchServiceTime.ModifiedBy = loggedinUser.Id;

            var result = await _churchServiceTimeService.Save(churchServiceTime);
            return new JsonResult(result);
        }

        [Route("getTimeByChurch/{churchId}/{day}")]
        public async Task<IActionResult> GetTimeByChurch(int churchId, string day)
        {
            var times = await _churchServiceTimeService.GetTimeByChurch(churchId, day);
            return new JsonResult(times);
        }
    }
}