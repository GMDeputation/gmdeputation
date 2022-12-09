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
    [Route("countries")]
    [Authorize]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService = null;
        private readonly IWebHostEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public CountryController(ICountryService countryService, IWebHostEnvironment environment, SFADBContext context)
        {
            _countryService = countryService;
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
            var countries = await _countryService.GetAll();
            return new JsonResult(countries);
        }
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var country = await _countryService.GetById(id);
            return new JsonResult(country);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var result = await _countryService.Delete(id,loggedinUser.Id);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]CountryQuery query)
        {
            var countries = await _countryService.Search(query);
            return new JsonResult(countries);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]Country country)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            country.CreatedBy = loggedinUser.Id;

            var result = await _countryService.Add(country,loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody]Country country)
        {
            if (id != country.Id)
            {
                return BadRequest("Country Id not matched");
            }
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            country.UpdateUser = loggedinUser.Id.ToString();
            
            var result = await _countryService.Edit(country, loggedinUser);
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

                    var uploadPath = Path.Combine(_environment.WebRootPath, "resources", "countries");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    //if (file.Length > 0)
                    //{
                    var fileSequence = DateTime.Now.ToString().Replace("/","").Replace(":","").Replace(" ","") + "_" + loggedinUser.Id.ToString() + "_" + loggedinUser.Name;
                    var saveFileName = fileSequence + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, saveFileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //banner.FileName = fileName;
                    //banner.FileSequence = fileSequence;
                    //}
                    //var path = fileName1;
                    var model = new List<TblCountryNta>();
                    FileInfo File = new FileInfo(Path.Combine(uploadPath, saveFileName));
                    using (ExcelPackage package = new ExcelPackage(File))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;

                        var rawText = string.Empty;
                        var existingEntity = await _context.TblCountryNta.ToListAsync();
                        var maxNumber = existingEntity.Count() == 0 ? 1 : existingEntity.OrderByDescending(m => m.CodeVal).FirstOrDefault().CodeVal + 1;
                       // var countryCodes = await _context.TblCountry.ToListAsync();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 1].Value != null)
                            {
                                //if (!countryCodes.Select(m => m.Code).Contains(worksheet.Cells[row, 1].Value.ToString()))
                                //{
                                //    jsonString = "Country Code is not right in " + row + " th row of excel sheet. Kindly check country code";
                                //    return Json(jsonString);
                                //}

                                var formModel = new TblCountryNta
                                {
                                    InsertDatetime = DateTime.Now,
                                    InsertUser = loggedinUser.Id.ToString(),                  
                                    //CountryId = countryCodes.Where(m => m.Code == worksheet.Cells[row, 1].Value.ToString()).FirstOrDefault().Id,
                                    CodeVal = maxNumber,                 
                                    //Alias = worksheet.Cells[row, 2].Value.ToString(),
                                    Name = worksheet.Cells[row, 1].Value.ToString(),
                                    FrenchName = worksheet.Cells[row, 2].Value?.ToString(),
                                    Alpha2Code = worksheet.Cells[row, 3].Value?.ToString(),
                                    Alpha3Code = worksheet.Cells[row, 4].Value?.ToString()
                                };

                                //model = formModel;
                                //var currentList = model.Where(m => m.Name == formModel.Name).ToList();
                                //var existingList = existingEntity.Where(m => m.Name == formModel.Name).ToList();
                                //if (currentList.Count > 0 || existingList.Count > 0)
                                //{
                                //    model.Remove(formModel);
                                //}
                                //else
                                //{
                                //    maxNumber++;
                                //    model.Add(formModel);
                                //}
                                _context.TblCountryNta.AddRange(formModel);
                                try
                                {
                                    await _context.SaveChangesAsync();
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                
                                
                            }
                            else
                            {
                                jsonString = "Excel Format is not right. Kindly upload the right format as per given format";
                                return Json(jsonString);
                            }

                        }
                        _context.TblCountryNta.AddRange(model);
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