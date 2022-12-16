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
    //[Authorize]
    [Route("accommodations")]
    [Authorize]
    public class ChurchAccommodationController : Controller
    {
        private readonly IChurchAccommodationService _accommodationService = null;
        private readonly IWebHostEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public ChurchAccommodationController(IChurchAccommodationService accommodationService, IWebHostEnvironment environment, SFADBContext context)
        {
            _accommodationService = accommodationService;
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

        [Route("import")]
        [CheckAccess]
        public IActionResult Import()
        {
            return View();
        }

        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var accommodation = await _accommodationService.GetAll();
            return new JsonResult(accommodation);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]ChurchAccommodationQuery query)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var accommodation = await _accommodationService.Search(query, loggedinUser.DataAccessCode, loggedinUser.Id);
            return new JsonResult(accommodation);
        }

        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var accommodation = await _accommodationService.GetById(id);
            return new JsonResult(accommodation);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody]ChurchAccommodation accommodation) 
        {
            var result = await _accommodationService.Save(accommodation);
            return new JsonResult(result);
        }


        [HttpPost]
        [Route("import-accommodation")]
        public async Task<IActionResult> ImportSection([FromForm]AttributeModel accommodation)  
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var files = Request.Form.Files;
            var file = files[0];
            var jsonString = "";
            try
            {

                var extention = (file != null) ? Path.GetExtension(file.FileName) : null;
                if (file != null && (extention.Equals(".xls", StringComparison.OrdinalIgnoreCase) || extention.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLSX", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLS", StringComparison.OrdinalIgnoreCase)))
                {
                    var fileName = file.FileName;

                    var uploadPath = Path.Combine(_environment.WebRootPath, "resources", "accommodations");
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

                    var model = new List<TblChurchAccommodationNta>();
                    FileInfo File = new FileInfo(Path.Combine(uploadPath, saveFileName));
                    using (ExcelPackage package = new ExcelPackage(File))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;

                        var existingEntity = await _context.TblChurchAccommodationNta.Include(m => m.Church).ToListAsync();
                        var churches = await _context.TblChurchNta.ToListAsync();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 1].Value != null )
                            {
                                if (!churches.Select(m => m.Id.ToString()).Contains(worksheet.Cells[row, 1].Value.ToString()))
                                {
                                    jsonString = "Church ID does not exist  in database for " + row + " th row of excel sheet. Kindly check Church ID";
                                    return Json(jsonString);
                                }
                                var churchID = worksheet.Cells[row, 1].Value?.ToString();
                                int ChurchIDAsInt = 0;
                                if (churchID != null || churchID != "")
                                    ChurchIDAsInt = Convert.ToInt32(churchID);

                                var formModel = new TblChurchAccommodationNta
                                {
                                    ChurchId = ChurchIDAsInt,
                                    AccomType = worksheet.Cells[row, 2].Value?.ToString(),
                                    AccomNotes = worksheet.Cells[row, 3].Value?.ToString(),
                                    EqaccomAddress = worksheet.Cells[row, 4].Value?.ToString(),
                                    InsertUser = loggedinUser.Id.ToString(),
                                    InsertDatetime = DateTime.Now

                                };

                                //This line will not allow duplicates
                                //var currentList = model.Where(m => m.AccomType == formModel.AccomType).ToList();
                                //var existingList = existingEntity.Where(m => m.AccomType == formModel.AccomType).ToList();

                                //if (currentList.Count > 0 || existingList.Count > 0)
                               // {
                               //     model.Remove(formModel);
                               // }
                               // else
                               // {
                                    model.Add(formModel);
                               // }
                            }
                            else
                            {
                                jsonString = "Excel Format is not right. Kindly upload the right format as per given format";
                                return Json(jsonString);
                            }

                        }
                        _context.TblChurchAccommodationNta.AddRange(model);
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