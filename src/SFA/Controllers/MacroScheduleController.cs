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
using System.Text;

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
            StringBuilder Errors = new StringBuilder();
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

                    var fileSequence = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + "_" + loggedinUser.Id.ToString() + "_" + loggedinUser.Name;
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
           
                        var existingEntity = await _context.TblChurchNta.Include(m => m.Section).ToListAsync();

                        var macroEntities = await _context.TblMacroScheduleNta.ToListAsync();
                        var userEntities = await _context.TblUserNta.ToListAsync();     

                        int numberInserts = 0;
                        int numberFailed = 0;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            numberInserts++;
                            if (worksheet.Cells[row, 2].Value != null)
                            {
                               
                                var districtId = worksheet.Cells[row, 6].Value;
                                var districtIdInt = 0;

                                    try
                                    {
                                        districtIdInt = Convert.ToInt32(districtId);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        Errors.AppendLine("Row Number:" + row + " District ID is not an interger please very user ID");
                                        numberFailed++;
                                        continue;

                                    }

                                

                                var userID = worksheet.Cells[row, 2].Value?.ToString();
                                var userIDInt = 0;

                                if (userID != null || userID != "")
                                {
                                    try
                                    {
                                        userIDInt = Convert.ToInt32(userID);
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        Errors.AppendLine("Row Number:" + row + " User ID is not an interger please very user ID");
                                        numberFailed++;
                                        continue;

                                    }
                                   
                                }
                                int? macroSchedlueId = null;
                                try
                                {
                                    macroSchedlueId = macroEntities.Where(m => m.Description.ToString() == worksheet.Cells[row, 1].Value?.ToString()).FirstOrDefault().Id;
                                }
                                //If we catch an exception here this means that there was no ID that was found. No issues and let's carry on
                                catch(Exception ex)
                                {
                                    Console.WriteLine("Warning:" + ex.Message);

                                }
                                

                                //This means the macro schedule ID Does not exists in the database and we will add it
                                if (macroSchedlueId == null)
                                {
                                    var formModel = new TblMacroScheduleNta
                                    {
                                        Description = worksheet.Cells[row, 1].Value?.ToString(),
                                        EntryDate = DateTime.Now,
                                        IsActive = true,
                                        InsertDatetime = DateTime.Now,
                                        InsertUser = loggedinUser.Id.ToString(),
                                    };

                                    _context.TblMacroScheduleNta.AddRange(formModel);
                                    try
                                    {
                                        await _context.SaveChangesAsync();
                                    }
                                    catch (Exception ex)
                                    {
                                        Errors.AppendLine("Row Number:" + row + " " + ex.InnerException.Message);
                                        numberFailed++;
                                        continue;
                                    }

                                    var details = new TblMacroScheduleDetailsNta
                                    {
                                        MacroScheduleId = formModel.Id,
                                        DistrictId = districtIdInt,
                                        UserId = userIDInt,
                                        StartDate = DateTime.Parse(worksheet.Cells[row, 3].Value.ToString()),
                                        EndDate = DateTime.Parse(worksheet.Cells[row, 4].Value.ToString()),
                                        Notes = worksheet.Cells[row, 5].Value?.ToString(),
                                        IsApproved = false,
                                        IsRejected = false,
                                        InsertUser = loggedinUser.Id.ToString(),
                                        InsertDatetime = DateTime.Now
                                    };

                                    _context.TblMacroScheduleDetailsNta.AddRange(details);
                                    try
                                    {
                                        await _context.SaveChangesAsync();
                                    }
                                    catch(Exception ex)
                                    {
                                        Errors.AppendLine("Row Number:" + row + " " + ex.InnerException.Message);
                                        numberFailed++;
                                        continue;
                                    }
                                  

                                }
                                //This means that the Macro schedule is already in the database and the macro schedule does not need to be added
                                //We have the ID and can make the relationship with the details table
                                else
                                {
                                    var details = new TblMacroScheduleDetailsNta
                                    {
                                        MacroScheduleId = (int)macroSchedlueId,
                                        DistrictId = districtIdInt,
                                        UserId = userIDInt,
                                        StartDate = DateTime.Parse(worksheet.Cells[row, 3].Value.ToString()),
                                        EndDate = DateTime.Parse(worksheet.Cells[row, 4].Value.ToString()),
                                        Notes = worksheet.Cells[row, 5].Value?.ToString(),
                                        IsApproved = false,
                                        IsRejected = false
                                    };
                                    _context.TblMacroScheduleDetailsNta.AddRange(details);
                                    try
                                    {
                                        await _context.SaveChangesAsync();
                                    }
                                    catch(Exception ex)
                                    {
                                        Errors.AppendLine("Row Number:" + row + " " + ex.InnerException.Message);
                                        numberFailed++;
                                        continue;
                                    }                                                                      
                                }                                                                                    

                            }
                            else
                            {
                                Errors.AppendLine("Row Number:" + row + " Excel Format is not right. Kindly upload the right format as per given format");
                                numberFailed++;
                                continue;
                            }

                        }

                        if (Errors.Length == 0)
                        {
                            return Json(jsonString);
                        }
                        else
                        {
                            return Json(numberInserts + "Attempted to be added: " + numberFailed + "Failed:Errors are as follows:\n" + Errors);
                        }
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