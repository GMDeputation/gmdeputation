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
    [Route("districts")]
    [Authorize]
    public class DistrictController : Controller
    {
        private readonly IDistrictService _districtService = null;
        private readonly IWebHostEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public DistrictController(IDistrictService districtService, IWebHostEnvironment environment, SFADBContext context)
        {
            _districtService = districtService;
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
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var districts = await _districtService.GetAll();
            return new JsonResult(districts);
        }

        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var district = await _districtService.GetById(id);
            return new JsonResult(district);
        }

        [Route("getDistrictByStateId/{id}")]
        public async Task<IActionResult> GetDistricByStateId(int id)
        {
            var district = await _districtService.GetDistricByStateId(id);
            return new JsonResult(district);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var result = await _districtService.Delete(id, loggedinUser.Id);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]DistrictQuery query)
        {
            var districts = await _districtService.Search(query);
            return new JsonResult(districts);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]District district)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            district.CreatedBy = loggedinUser.Id;

            var result = await _districtService.Add(district, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody]District district)
        {
            if (id != district.Id)
            {
                return BadRequest("Distric Id not matched");
            }
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            district.LastModifiedBy = loggedinUser.Id;

            var result = await _districtService.Edit(district, loggedinUser);
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

                    var uploadPath = Path.Combine(_environment.WebRootPath, "resources", "districts");
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
                    var model = new List<TblDistrictNta>();
                    FileInfo File = new FileInfo(Path.Combine(uploadPath, saveFileName));
                    using (ExcelPackage package = new ExcelPackage(File))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;

                        var rawText = string.Empty;
                        var existingEntity = await _context.TblDistrictNta.ToListAsync();
                        var maxNumber = existingEntity.Count() == 0 ? 1 : existingEntity.OrderByDescending(m => m.CodeVal).FirstOrDefault().CodeVal + 1;
                        var stateCodes = await _context.TblStateNta.ToListAsync();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 1].Value != null && worksheet.Cells[row, 2].Value != null && !worksheet.Cells[row, 1].Value.ToString().Contains("District Name", StringComparison.OrdinalIgnoreCase))
                            {
                                //if (!stateCodes.Select(m => m.Alias).Contains(worksheet.Cells[row, 3].Value.ToString()))
                                //{
                                //    jsonString = "State Code is not right in " + row + " th row of excel sheet. Kindly check state code";
                                //    return Json(jsonString);
                                //}

                                var formModel = new TblDistrictNta
                                {
                                    InsertDatetime = DateTime.Now,
                                    InsertUser = loggedinUser.Id.ToString(),                           
                                    //StateId = stateCodes.Where(m => m.Alias == worksheet.Cells[row, 3].Value.ToString()).FirstOrDefault().Id,
                                    CodeVal = maxNumber,                    
                                    Name = worksheet.Cells[row, 1].Value.ToString(),
                                    Alias = worksheet.Cells[row, 2].Value.ToString()
                                };
                                //maxNumber++;
                                //model = formModel;
                                var currentList = model.Where(m => m.Name == formModel.Name).ToList();
                                var existingList = existingEntity.Where(m => m.Name == formModel.Name).ToList();
                                if (currentList.Count > 0 || existingList.Count > 0)
                                {
                                    model.Remove(formModel);
                                }
                                else
                                {
                                    maxNumber++;
                                    model.Add(formModel);
                                }
                            }
                            else
                            {
                                jsonString = "Excel Format is not right. Kindly upload the right format as per given format";
                                return Json(jsonString);
                            }

                        }
                        _context.TblDistrictNta.AddRange(model);
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
