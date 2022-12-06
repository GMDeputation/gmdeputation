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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SFA.Entities;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;

namespace SFA.Controllers
{
    [Route("macroSchedule")]
    [Authorize]
    public class MacroScheduleController : Controller
    {
        private readonly IMacroScheduleService _macroScheduleService = null;
        private readonly IWebHostEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public MacroScheduleController(IMacroScheduleService macroScheduleService, IWebHostEnvironment environment, SFADBContext context)
        {
            _macroScheduleService = macroScheduleService;
            _environment = environment;
            _context = context;
        }

        [Route("")]
        [CheckAccess]
        public IActionResult Index()
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            ViewBag.AccessCode = loggedinUser.DataAccessCode;
            return View();
        }

        [Route("approvedSchedule")]
        public IActionResult ApprovedSchedule()
        {
            return View();
        }

        [Route("detail/{id}")]
        [CheckAccess]
        public IActionResult Detail(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [Route("edit/{id}")]
        [CheckAccess]
        public IActionResult Edit(int id) 
        {
            return View();
        }

        [Route("calender")]
        [CheckAccess]
        public IActionResult CalenderView()
        {
            return View();
        }

        [Route("import")]
        [CheckAccess]
        public ActionResult Import() 
        {
            return View();
        }

        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var macroSchedules = await _macroScheduleService.GetAll(loggedinUser.DataAccessCode, loggedinUser.Id);
            return new JsonResult(macroSchedules);
        }

        [HttpPost]
        [Route("getAllapproved")]
        public async Task<IActionResult> GetAllapproved([FromBody]MacroScheduleQuery query)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            TimeZoneInfo timeZone = TimeZoneInfo.Local;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZone.Id);

            if (query.FromDate != null)
            {
                query.FromDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)query.FromDate, tzi);
            }
            if (query.ToDate != null)
            {
                query.ToDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)query.ToDate, tzi);
            }
            if (query.FromEntryDate != null)
            {
                query.FromEntryDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)query.FromEntryDate, tzi);
            }
            if (query.ToEntryDate != null)
            {
                query.ToEntryDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)query.ToEntryDate, tzi);
            }

            var macroSchedules = await _macroScheduleService.GetAllapproved(query, loggedinUser.Id, loggedinUser.IsSuperAdmin);
            return new JsonResult(macroSchedules);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]MacroScheduleQuery query)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine("Valid");
            }
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            TimeZoneInfo timeZone = TimeZoneInfo.Local;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZone.Id);

            if (query.FromDate != null)
            {
                query.FromDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)query.FromDate, tzi);
            }
            if (query.ToDate != null)
            {
                query.ToDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)query.ToDate, tzi);
            }
            if (query.FromEntryDate != null)
            {
                query.FromEntryDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)query.FromEntryDate, tzi);
            }
            if (query.ToEntryDate != null)
            {
                query.ToEntryDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)query.ToEntryDate, tzi);
            }

            var macroSchedules = await _macroScheduleService.Search(query, loggedinUser.Id);
            return new JsonResult(macroSchedules);
        }

        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var macroSchedule = await _macroScheduleService.GetById(id);
            return new JsonResult(macroSchedule);
        }

        [Route("getMacroScheduleDetailsById/{id}")]
        public async Task<IActionResult> GetMacroScheduleDetailsById(int id)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var macroSchedule = await _macroScheduleService.GetMacroScheduleDetailsById(id, loggedinUser.DataAccessCode);
            return new JsonResult(macroSchedule);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody]MacroSchedule macroSchedule)
        { 
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var result = await _macroScheduleService.Save(macroSchedule, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromBody]MacroScheduleDetails macroScheduleDetails)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var result = await _macroScheduleService.Edit(macroScheduleDetails, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("approved")]
        public async Task<IActionResult> Approved([FromBody]MacroScheduleDetails macroScheduleDetails)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var macroSchedule = await _macroScheduleService.Approved(macroScheduleDetails, loggedinUser.Id);
            return new JsonResult(macroSchedule);
        }

        [HttpPost]
        [Route("rejected")]
        public async Task<IActionResult> Rejected([FromBody]MacroScheduleDetails macroScheduleDetails)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var macroSchedule = await _macroScheduleService.Rejected(macroScheduleDetails, loggedinUser.Id);
            return new JsonResult(macroSchedule);
        }

        [HttpPost]
        [Route("approvedMacroSchedulesIds")]
        public async Task<IActionResult> ApprovedMacroSchedulesIds([FromBody]List<int> selectSchedule)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var macroSchedule = await _macroScheduleService.ApprovedMacroSchedulesIds(selectSchedule, loggedinUser.Id);
            return new JsonResult(macroSchedule);
        }

        [HttpPost]
        [Route("rejectMacroSchedulesIds")]
        public async Task<IActionResult> RejectMacroSchedulesIds([FromBody]List<int> selectSchedule) 
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var macroSchedule = await _macroScheduleService.RejectMacroSchedulesIds(selectSchedule, loggedinUser.Id);
            return new JsonResult(macroSchedule);
        }

        [HttpPost]
        [Route("importData")]
        public async Task<IActionResult> ImportData(IFormFile file) 
        {
            var jsonString = "";
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            try
            {

                var extention = (file != null) ? Path.GetExtension(file.FileName) : null;
                if (file != null && (extention.Equals(".xls", StringComparison.OrdinalIgnoreCase) || extention.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLSX", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLS", StringComparison.OrdinalIgnoreCase)))
                {
                    var fileName = file.FileName;

                    var uploadPath = Path.Combine(_environment.WebRootPath, "resources", "macroSchedules");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var fileSequence = DateTime.Now.Ticks.ToString();
                    var saveFileName = fileSequence + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, saveFileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var model = new List<TblMacroScheduleNta>();
                    FileInfo File = new FileInfo(Path.Combine(uploadPath, saveFileName));
                    using (ExcelPackage package = new ExcelPackage(File))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;

                        var rawText = string.Empty;
                        var existingEntity = await _context.TblChurchNta.Include(m => m.Section).ToListAsync();

                        var districtEntities = await _context.TblDistrictNta.ToListAsync();
                        var userEntities = await _context.TblUserNta.ToListAsync();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 2].Value != null && !worksheet.Cells[row, 2].Value.ToString().Contains("Entry Date", StringComparison.OrdinalIgnoreCase) 
                                && worksheet.Cells[row, 3].Value != null && worksheet.Cells[row, 4].Value != null && worksheet.Cells[row, 5].Value != null && worksheet.Cells[row, 6].Value != null)
                            {

                                if (!userEntities.Select(m => m.Email).Contains(worksheet.Cells[row, 3].Value.ToString()))
                                {
                                    jsonString = "Missionary User EmailId is not right in " + row + " th row of excel sheet. Kindly check Missionary User EmailId";
                                    return Json(jsonString);
                                }
                                int userId = userEntities.Where(m => m.Email == worksheet.Cells[row, 3].Value.ToString()).FirstOrDefault().Id;

                                if (!districtEntities.Select(m => m.Name).Contains(worksheet.Cells[row, 4].Value.ToString()))
                                {
                                    jsonString = "District Name is not right in " + row + " th row of excel sheet. Kindly check District Name";
                                    return Json(jsonString);
                                }
                                int districtId = districtEntities.Where(m => m.Name == worksheet.Cells[row, 4].Value.ToString()).FirstOrDefault().Id;
                            
                                var formModel = new TblMacroScheduleNta
                                {                                                                      
                                    Description = worksheet.Cells[row, 1].Value?.ToString(),
                                    EntryDate = DateTime.Parse(worksheet.Cells[row, 2].Value.ToString()),
                                    IsActive = true,
                                    InsertDatetime = DateTime.Now,
                                    InsertUser = loggedinUser.Id.ToString(),
                                };
                                var details = new TblMacroScheduleDetailsNta
                                {                                                             
                                    DistrictId = districtId,
                                    UserId = userId,
                                    StartDate = DateTime.Parse(worksheet.Cells[row, 5].Value.ToString()),
                                    EndDate = DateTime.Parse(worksheet.Cells[row, 6].Value.ToString()),
                                    Notes = worksheet.Cells[row, 7].Value?.ToString(),
                                    IsApproved = false,
                                    IsRejected = false
                                };
                                formModel.TblMacroScheduleDetailsNta.Add(details);
                                model.Add(formModel);
                            }
                            else
                            {
                                jsonString = "Excel Format is not right. Kindly upload the right format as per given format";
                                return Json(jsonString);
                            }

                        }
                        _context.TblMacroScheduleNta.AddRange(model);
                        await _context.SaveChangesAsync();
                        return Json(jsonString);
                    }

                }
                return Json("Extension is not match");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}