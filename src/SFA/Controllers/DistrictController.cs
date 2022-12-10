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
using System.Text;

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
            StringBuilder Errors = new StringBuilder();
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
                    var fileSequence = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + "_" + loggedinUser.Id.ToString() + "_" + loggedinUser.Name;
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
                        int rowCount = worksheet.Dimension.End.Row;
                        int ColCount = worksheet.Dimension.Columns;
                        int numberInserts = 0;
                        int numberFailed = 0;

                        var rawText = string.Empty;
                        var existingEntity = await _context.TblDistrictNta.ToListAsync();
                        var maxNumber = existingEntity.Count() == 0 ? 1 : existingEntity.OrderByDescending(m => m.CodeVal).FirstOrDefault().CodeVal + 1;
                        var stateCodes = await _context.TblStateNta.ToListAsync();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            
                            if (worksheet.Cells[row, 1].Value != null)
                            {
                                numberInserts++;


                                var states = worksheet.Cells[row, 4].Value;

                                //Grabbing the value and splitting it. We are expecting it to be interger(s) comma seperated
                                List<string> statesSplit = new List<string>();
                                if (states != null)
                                {
                                    statesSplit = states.ToString().Split(',').ToList();
                                }
                                
                                List<int> statesSplitAsInt = new List<int>();

                                //We need to convert string to INT. Wild! 
                                foreach (var state in statesSplit)
                                {

                                    var stateInt = Convert.ToInt32(state);
                                    statesSplitAsInt.Add(stateInt);

                                }

                                if(statesSplit.Count != statesSplit.Count)
                                {
                                    Errors.AppendLine("Row Number:" + row + "State Code has other characters besides for integers  at  " + row + " th row of excel sheet. Kindly check state code");
                                    jsonString = "State Code has other characters besides for integers  at  " + row + " th row of excel sheet. Kindly check state code";
                                    numberFailed++;

                                    continue;              
                                }
                                var name = worksheet.Cells[row, 1].Value.ToString();
                                var alias = worksheet.Cells[row, 2].Value.ToString();
                                var website = worksheet.Cells[row, 3].Value;
                                var formModel = new TblDistrictNta
                                {
                                    InsertDatetime = DateTime.Now,
                                    InsertUser = loggedinUser.Id.ToString(),                                                               
                                    CodeVal = maxNumber,                    
                                    Name = name,
                                    Alias = alias,
                                    Website = website == null? null : website.ToString(),
                                };
                                

                                _context.TblDistrictNta.AddRange(formModel);
                                try
                                {
                                    await _context.SaveChangesAsync();
                                    maxNumber++;
                                }
                                catch(Exception ex)
                                {
                                    Errors.AppendLine("Row Number:" + row + ex.InnerException.Message);
                                    Console.WriteLine(ex.InnerException.Message);
                                    numberFailed++;
                                    continue;
                                }
                                

                                //This is making the correlation for state and district.
                                foreach (var state in statesSplitAsInt)
                                {
                                    var stateDistrictFormModel = new TblStateDistrictNta
                                    {
                                        DistrictId = formModel.Id,
                                        StateId = state,
                                        InsertUser = loggedinUser.Id.ToString(),
                                        InsertDatetime = DateTime.Now

                                    };
                                    _context.TblStateDistrictNta.AddRange(stateDistrictFormModel);
                                    try
                                    {
                                        await _context.SaveChangesAsync();
                                    }
                                    catch (Exception ex)
                                    {
                                        Errors.AppendLine("Row Number:" + row + ex.InnerException.Message + "NOTE:DISTRICT WAS ADDED BUT RELATIONSHIP FOR STATE DISTRICT FAILED");
                                        Console.WriteLine(ex.InnerException.Message);
                                        numberFailed++;
                                        continue;
                                    }

                                }                              
                                     
                            }
                            //This is for when files come in with added rows that the user does not realize they added. Since the value is blank
                            //for the first column we are going to just ignore it. User error
                            else
                            {
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
