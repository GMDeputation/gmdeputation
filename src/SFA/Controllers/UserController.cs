using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SFA.Entities;
using SFA.Extensions;
using SFA.Filters;
using SFA.Models;
using SFA.Services;

namespace SFA.Controllers
{
    [Route("user")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService = null;
        private readonly IHostingEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public UserController(IUserService userService, IHostingEnvironment environment, SFADBContext context)
        {
            _userService = userService;
            _environment = environment;
            _context = context;
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

        [Route("changePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("export")]
        [CheckAccess]
        public ActionResult Export()
        {
            return View();
        }

        [Route("api/menus")]
        public async Task<IActionResult> GetMenu()
        {
            var sessionUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var userId = sessionUser.Id;
            var menus = await _userService.GetMenuByUser(userId);
            return Json(menus);
        }

        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return new JsonResult(users);
        }

        [Route("changePass")]
        public async Task<IActionResult> ChangePassword([FromBody] User user)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var users = await _userService.ChangePassword(loggedinUser.Id, user);
            return new JsonResult(users);
        }


        [Route("api/search")]
        public async Task<IActionResult> Search([FromBody]UserQuery query)
        {
            var users = await _userService.Search(query);
            return new JsonResult(users);
        }

        [Route("api/get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetById(id);
            return new JsonResult(user);
        }

        [Route("api/getAllMissionariesUser")]
        public async Task<IActionResult> GetAllMissionariesByDistrict()
        {
            var user = await _userService.GetAllMissionariesUser();
            return new JsonResult(user);
        }

        [HttpPost]
        [Route("api/save")]
        public async Task<IActionResult> Save([FromBody]User user)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var result = await _userService.Save(user, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("export-section")]
        public async Task<IActionResult> ExportSection([FromForm]User user)
        {
            var files = Request.Form.Files;
            var file = files[0];
            var jsonString = "";
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            try
            {

                var extention = (file != null) ? Path.GetExtension(file.FileName) : null;
                if (file != null && (extention.Equals(".xls", StringComparison.OrdinalIgnoreCase) || extention.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLSX", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLS", StringComparison.OrdinalIgnoreCase)))
                {
                    var fileName = file.FileName;

                    var uploadPath = Path.Combine(_environment.WebRootPath, "resources", "users");
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

                    var model = new List<TblUserNta>();
                    var userPassword = new List<TblUserPasswordNta>();
                    FileInfo File = new FileInfo(Path.Combine(uploadPath, saveFileName));
                    using (ExcelPackage package = new ExcelPackage(File))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.End.Row;
                        int ColCount = worksheet.Dimension.Columns;

                        var rawText = string.Empty;
                        var existingEntity = await _context.TblUserNta.Include(m => m.District).ToListAsync();

                        var roleEntities = await _context.TblRoleNta.ToListAsync();
                        var districtEntities = await _context.TblDistrictNta.ToListAsync();
                        var sectionEntities = await _context.TblSectionNta.ToListAsync();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 1].Value != null && worksheet.Cells[row, 3].Value != null && worksheet.Cells[row, 8].Value != null
                                && !worksheet.Cells[row, 8].Value.ToString().Contains("District Code", StringComparison.OrdinalIgnoreCase) && worksheet.Cells[row, 15].Value != null
                                && worksheet.Cells[row, 7].Value != null && worksheet.Cells[row, 13].Value != null && worksheet.Cells[row, 4].Value != null)
                            {

                                if (existingEntity.Select(m => m.UserName.ToLower()).Contains(worksheet.Cells[row, 13].Value.ToString().ToLower()))
                                {
                                    jsonString = "User Email Should be unique Or not right in " + row + " th row of excel sheet. Kindly check User Email";
                                    return Json(jsonString);
                                }

                                if (!roleEntities.Select(m => m.Name).Contains(worksheet.Cells[row, 15].Value.ToString()))
                                {
                                    jsonString = "Role not right in " + row + " th row of excel sheet. Kindly check Role Name";
                                    return Json(jsonString);
                                }

                                if (!districtEntities.Select(m => m.Code).Contains(worksheet.Cells[row, 8].Value.ToString()))
                                {
                                    jsonString = "District Code is not right in " + row + " th row of excel sheet. Kindly check District code";
                                    return Json(jsonString);
                                }

                                int districtId = districtEntities.Where(m => m.Code == worksheet.Cells[row, 8].Value.ToString()).FirstOrDefault().Id;

                                if (worksheet.Cells[row, 9].Value != null && !sectionEntities.Where(m=> m.DistrictId == districtId).Select(m => m.Name).Contains(worksheet.Cells[row, 9].Value.ToString()))
                                {
                                    jsonString = "Section Name is not found under " + worksheet.Cells[row, 8].Value.ToString()+" District " + row + " th row of excel sheet. Kindly check Section Name";
                                    return Json(jsonString);
                                }
                               
                                var formModel = new TblUserNta
                                {                                   
                                    FirstName = worksheet.Cells[row, 1].Value.ToString(),
                                    MiddleName = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString() : null,
                                    LastName = worksheet.Cells[row, 3].Value.ToString(),
                                    Gender = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value?.ToString(): null,
                                    Address = worksheet.Cells[row, 5].Value != null ? worksheet.Cells[row, 5].Value.ToString() : null,
                                    City = worksheet.Cells[row, 6].Value != null ? worksheet.Cells[row, 6].Value.ToString() : null,
                                    Zipcode = worksheet.Cells[row, 7].Value.ToString(),
                                    DistrictId = districtId,
                                    SectionId = (int)(worksheet.Cells[row, 9].Value != null ? sectionEntities.Where(m => m.DistrictId == districtId && m.Name.Contains(worksheet.Cells[row, 9].Value.ToString())).FirstOrDefault().Id : (int?)null),
                                    TelePhoneNo = worksheet.Cells[row, 10].Value != null? worksheet.Cells[row, 10].Value.ToString() : null,
                                    WorkPhoneNo = worksheet.Cells[row, 11].Value != null ? worksheet.Cells[row, 11].Value.ToString() : null,
                                    Phone = worksheet.Cells[row, 12].Value != null ? worksheet.Cells[row, 12].Value.ToString() : null,
                                    Email = worksheet.Cells[row, 13].Value.ToString(),
                                    UserName = worksheet.Cells[row, 13].Value.ToString(),
                                    Status = worksheet.Cells[row, 14].Value != null ? worksheet.Cells[row, 14].Value.ToString() : null,
                                    RoleId = roleEntities.Where(m => m.Name.Contains(worksheet.Cells[row, 15].Value.ToString())).FirstOrDefault().Id,
                                    IsActive = true,
                                    InsertDatetime = DateTime.Now,
                                };

                                formModel.TblUserPasswordNta.Add(new TblUserPasswordNta
                                {                               
                                    //TODO WE NEED TO PULL THIS CODE OUT AND ADD THIS AFTER OR SOMEWHERE ELSE
                                    UserId = 1,
                                    Password = "123",
                                    InsertDatetime = DateTime.Now,                                   
                                });

                                var currentList = model.Where(m => m.Email == formModel.Email).ToList();
                                var existingList = existingEntity.Where(m => m.Email == formModel.Email).ToList();
                                if (currentList.Count > 0 || existingList.Count > 0)
                                {
                                    model.Remove(formModel);
                                }
                                else
                                {
                                    model.Add(formModel);
                                }
                            }
                            else
                            {
                                jsonString = "Excel Format is not right or data are not proper. Kindly upload the right format as per given format";
                                return Json(jsonString);
                            }

                        }
                        _context.TblUserNta.AddRange(model);
                        int numberRecords =  _context.SaveChanges();
                        //await _context.SaveChangesAsync();
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
