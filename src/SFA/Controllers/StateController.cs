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
using OfficeOpenXml;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;

namespace SFA.Controllers
{
    [Route("states")]
    [Authorize]
    public class StateController : Controller
    {
        private readonly IStateService _stateService = null;
        private readonly IWebHostEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public StateController(IStateService stateService, IWebHostEnvironment environment, SFADBContext context)
        {
            _stateService = stateService;
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
            //var Id = int.Newint();
            //var user = HttpContext.Session.GetString("USERNAME");
            //if (user == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            return View();

        }

        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var states = await _stateService.GetAll();
            return new JsonResult(states);
        }

        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var state = await _stateService.GetById(id);
            return new JsonResult(state);
        }

        [Route("getStateByCountryId/{id}")]
        public async Task<IActionResult> GetStateByCountryId(int id)
        {
            var state = await _stateService.GetStateByCountryId(id);
            return new JsonResult(state);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var result = await _stateService.Delete(id, loggedinUser.Id);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]StateQuery query)
        {
            var states = await _stateService.Search(query);
            return new JsonResult(states);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]State state)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            state.CreatedBy = loggedinUser.Id;

            var result = await _stateService.Add(state, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody]State state)
        {
            if (id != state.Id)
            {
                return BadRequest("State Id not matched");
            }

            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            state.LastModifiedBy = loggedinUser.Id;

            var result = await _stateService.Edit(state, loggedinUser);
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

                    var uploadPath = Path.Combine(_environment.WebRootPath, "resources", "states");
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
                    var model = new List<TblStateNta>();
                    FileInfo File = new FileInfo(Path.Combine(uploadPath, saveFileName));
                    using (ExcelPackage package = new ExcelPackage(File))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;

                        var rawText = string.Empty;
                        var existingEntity = await _context.TblStateNta.Include(m=>m.Country).ToListAsync();
                        var maxNumber = existingEntity.Count() == 0 ? 1 : existingEntity.OrderByDescending(m => m.CodeVal).FirstOrDefault().CodeVal + 1;
                        var countryCodes = await _context.TblCountryNta.ToListAsync();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 1].Value != null)
                            {
                                var countryID = worksheet.Cells[row, 3].Value.ToString();
                                int countryIDInt = 0;
                                if (countryID != null )
                                    countryIDInt = Convert.ToInt32(countryID);

                                var formModel = new TblStateNta
                                {
                                    InsertDatetime = DateTime.Now,
                                    InsertUser = loggedinUser.Id.ToString(),                                  
                                    CountryId = countryIDInt,
                                    CodeVal = maxNumber,                                  
                                    Alias = worksheet.Cells[row, 1].Value.ToString(),
                                    Name = worksheet.Cells[row, 2].Value.ToString()
                                };

                                _context.TblStateNta.AddRange(formModel);
                                try
                                {
                                    await _context.SaveChangesAsync();
                                    maxNumber++;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.InnerException.Message);
                                }
                                
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
                            }
                            else
                            {
                                jsonString = "Excel Format is not right. Kindly upload the right format as per given format";
                                return Json(jsonString);
                            }

                        }
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
