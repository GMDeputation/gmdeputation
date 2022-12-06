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
    [Route("churches")]
    [Authorize]
    public class ChurchController : Controller
    {
        private readonly IChurchService _churchService = null;
        private readonly IWebHostEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public ChurchController(IChurchService churchService, IWebHostEnvironment environment, SFADBContext context)
        {
            _churchService = churchService;
            _environment = environment;
            _context = context;
        }
        [Route("")]
        [CheckAccess]
        public IActionResult Index()
        {
            return View();
        }

        [Route("add")]
        [CheckAccess]
        public IActionResult Add()
        {
            return View();
        }

        [Route("edit/{id}")]
        [CheckAccess]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [Route("export")]
        [CheckAccess]
        public ActionResult Export()
        {
            return View();
        }

        [Route("mapView")]
        [CheckAccess]
        public ActionResult MapView()
        {
            return View();
        }

        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var churchs = await _churchService.GetAll();
            return new JsonResult(churchs);
        }

        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var church = await _churchService.GetById(id);
            return new JsonResult(church);
        }

        [Route("GetChurchBySectionId/{id}")]
        public async Task<IActionResult> GetChurchBySectionId(int id)
        {
            var church = await _churchService.GetChurchBySectionId(id);
            return new JsonResult(church);
        }

        [Route("getChurchByDistrict/{id}")]
        public async Task<IActionResult> GetChurchByDistrict(int id)
        {
            var church = await _churchService.GetChurchByDistrict(id);
            return new JsonResult(church);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var result = await _churchService.Delete(id, loggedinUser.Id);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]ChurchQuery query)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var churchs = await _churchService.Search(query, loggedinUser.DataAccessCode, loggedinUser.Id);
            return new JsonResult(churchs);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]Church church)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var result = await _churchService.Add(church, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody]Church church)
        {
            if (id != church.Id)
            {
                return BadRequest("Distric Id not matched");
            }
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");          

            var result = await _churchService.Edit(church, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("export")]
        public async Task<IActionResult> Export(IFormFile file)
        {
            var jsonString = "";
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            try
            {

                var extention = (file != null) ? Path.GetExtension(file.FileName) : null;
                if (file != null && (extention.Equals(".xls", StringComparison.OrdinalIgnoreCase) || extention.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLSX", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLS", StringComparison.OrdinalIgnoreCase)))
                {
                    var fileName = file.FileName;

                    var uploadPath = Path.Combine(_environment.WebRootPath, "resources", "churchs");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    //if (file.Length > 0)
                    //{
                    var fileSequence = DateTime.Now.Ticks.ToString();
                    var saveFileName = fileSequence + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, saveFileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //banner.FileName = fileName;
                    //banner.FileSequence = fileSequence;
                    //}
                    //var path = fileName1;
                    var model = new List<TblChurchNta>();
                    FileInfo File = new FileInfo(Path.Combine(uploadPath, saveFileName));
                    using (ExcelPackage package = new ExcelPackage(File))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;

                        var rawText = string.Empty;
                        var existingEntity = await _context.TblChurchNta.Include(m => m.Section).ToListAsync();
                        //var maxNumber = existingEntity.Count() == 0 ? 1 : existingEntity.OrderByDescending(m => m.CodeVal).FirstOrDefault().CodeVal + 1;
                        var districtEntities = await _context.TblDistrictNta.ToListAsync();
                        var sectionEntities = await _context.TblSectionNta.ToListAsync();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 1].Value != null && worksheet.Cells[row, 2].Value != null && !worksheet.Cells[row, 2].Value.ToString().Contains("Section Description", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!districtEntities.Select(m => m.Name).Contains(worksheet.Cells[row, 1].Value.ToString()))
                                {
                                    jsonString = "District Name is not right in " + row + " th row of excel sheet. Kindly check District Name";
                                    return Json(jsonString);
                                }

                                int districtId = districtEntities.Where(m => m.Name == worksheet.Cells[row, 1].Value.ToString()).FirstOrDefault().Id;

                                if (worksheet.Cells[row, 2].Value != null && !sectionEntities.Where(m => m.DistrictId == districtId).Select(m => m.Name).Contains(worksheet.Cells[row, 2].Value.ToString()))
                                {
                                    jsonString = "Section Name is not found under " + worksheet.Cells[row, 8].Value.ToString() + " District " + row + " th row of excel sheet. Kindly check Section Name";
                                    return Json(jsonString);
                                }

                                var formModel = new TblChurchNta
                                {
                                    InsertDatetime = DateTime.Now,
                                    InsertUser = loggedinUser.Id.ToString(),                                 
                                    DistrictId = districtId,
                                    SectionId = sectionEntities.Where(m => m.Name == worksheet.Cells[row, 2].Value.ToString()).FirstOrDefault().Id,
                                    IsDelete = false,
                                    ChurchName = worksheet.Cells[row, 3].Value.ToString(),
                                    AccountNo = worksheet.Cells[row, 4].Value?.ToString(),
                                    ChurchType = worksheet.Cells[row, 5].Value?.ToString(),
                                    Directory = worksheet.Cells[row, 6].Value?.ToString(),
                                    Address = worksheet.Cells[row, 7].Value?.ToString(),
                                    MailAddress = worksheet.Cells[row, 8].Value?.ToString(),
                                    Phone = worksheet.Cells[row, 9].Value?.ToString(),
                                    Phone2 = worksheet.Cells[row, 10].Value?.ToString(),
                                    WebSite = worksheet.Cells[row, 11].Value?.ToString(),
                                    Email = worksheet.Cells[row, 12].Value?.ToString(),
                                };
                                //maxNumber++;
                                //model = formModel;
                                //var currentList = model.Where(m => m.ChurchName == formModel.ChurchName).ToList();
                                //var existingList = existingEntity.Where(m => m.ChurchName == formModel.ChurchName).ToList();
                                //if (currentList.Count > 0 || existingList.Count > 0)
                                //{
                                //    model.Remove(formModel);
                                //}
                                //else
                                //{
                                    model.Add(formModel);
                                //}
                            }
                            else
                            {
                                jsonString = "Excel Format is not right. Kindly upload the right format as per given format";
                                return Json(jsonString);
                            }

                        }
                        _context.TblChurchNta.AddRange(model);
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
