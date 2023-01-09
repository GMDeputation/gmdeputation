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
using GoogleMaps.LocationServices;

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


        [Route("GetChurchByPastorID/{id}")]
        public async Task<IActionResult> GetChurchByPastorID(int id)
        {
            var church = await _churchService.GetChurchByPastorID(id);
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
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        [Route("getChurchByDistrictAndMacroSchDtl/{id}/{macroScheduleDetailId}")]
        public async Task<IActionResult> GetChurchByDistrictAndMacroSchDtl(int id, int macroScheduleDetailId)
        {
            Console.WriteLine("we are here");
            return new JsonResult(await _churchService.GetChurchByDistrictAndMacroSchDtl(id, macroScheduleDetailId));
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
                int numberInserts = 0;
                int numberFailed = 0;
                var extention = (file != null) ? Path.GetExtension(file.FileName) : null;
                if (file != null && (extention.Equals(".xls", StringComparison.OrdinalIgnoreCase) || extention.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLSX", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLS", StringComparison.OrdinalIgnoreCase)))
                {
                    var fileName = file.FileName;

                    var uploadPath = Path.Combine(_environment.WebRootPath, "resources", "churchs");
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

                    var model = new List<TblChurchNta>();
                    FileInfo File = new FileInfo(Path.Combine(uploadPath, saveFileName));
                    using (ExcelPackage package = new ExcelPackage(File))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;

                        var rawText = string.Empty;
                        var existingEntity = await _context.TblChurchNta.Include(m => m.Section).ToListAsync();
                        var districtEntities = await _context.TblDistrictNta.ToListAsync();
                        var sectionEntities = await _context.TblSectionNta.ToListAsync();
                        var latitude = "";
                        var longitude = "";
                        for (int row = 2; row <= rowCount; row++)
                        {
                            numberInserts++;
                            if (worksheet.Cells[row, 1].Value != null)
                            {
                                //Making sure that the section ID exists in the database. This is required for there is a FK relationship 
                                if (!sectionEntities.Select(m => m.Id.ToString()).Contains(worksheet.Cells[row, 7].Value.ToString()))
                                {
                                    Errors.AppendLine("Row Number:" + row + "Section Name is not right in " + row + " th row of excel sheet. Kindly check District Name");
                                    jsonString = "Section Name is not right in " + row + " th row of excel sheet. Kindly check District Name";
                                    numberFailed++;
                                    continue;                               
                                }
                                //This will not be null because we know we found a section above. No need to have another check
                                //District ID is a FK of the Section table
                                int districtId = sectionEntities.Where(m => m.Id.ToString() == worksheet.Cells[row, 7].Value.ToString()).FirstOrDefault().DistrictId;

                                var churchIdNumber = worksheet.Cells[row, 1].Value.ToString();
                                var churchName = worksheet.Cells[row, 2].Value.ToString();
                                var address = worksheet.Cells[row, 3].Value?.ToString();
                                if (address != null)
                                {

                                    var locationService = new GoogleLocationService("AIzaSyAoL5Cb3GKL803gYag0jud6d3iPHFZmbuI");
                                    MapPoint point = null;
                                    try
                                    {

                                        point = locationService.GetLatLongFromAddress(address);
                                    }
                                    catch (Exception ex)
                                    {
                                        Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Address: Please verify address syntax and validity");
                                        numberFailed++;
                                        Console.WriteLine(ex.Message);
                                        continue;
                                    }

                                    latitude = point.Latitude.ToString();
                                    longitude = point.Longitude.ToString();
                                }
                                var mailAddress = worksheet.Cells[row, 4].Value?.ToString();
                                var churchType = worksheet.Cells[row, 5].Value?.ToString();
                                var accountNumber = worksheet.Cells[row, 6].Value?.ToString();
                                var sectionID = sectionEntities.Where(m => m.Id.ToString() == worksheet.Cells[row, 7].Value.ToString()).FirstOrDefault().Id;
                                var phone = worksheet.Cells[row, 8].Value?.ToString();
                                var alternatePhone = worksheet.Cells[row, 9].Value?.ToString();
                                var email = worksheet.Cells[row, 10].Value?.ToString();
                                if (!IsValidEmail(email))
                                {
                                    Errors.AppendLine("Row Number:" + row + " " + email + ": Is not valid");
                                    numberFailed++;
                                    continue;
                                }
                                var website = worksheet.Cells[row, 11].Value?.ToString();                                
                                var status = worksheet.Cells[row, 12].Value?.ToString();


                                var formModel = new TblChurchNta
                                {
                                    ChurchIdNo = churchIdNumber,
                                    InsertDatetime = DateTime.Now,
                                    InsertUser = loggedinUser.Id.ToString(),
                                    DistrictId = districtId,
                                    SectionId = sectionID,
                                    IsDelete = false,
                                    ChurchName = churchName,
                                    ChurchType = churchType,
                                    AccountNo = accountNumber,
                                    Address = address,
                                    MailAddress = mailAddress,
                                    Phone = phone,
                                    Phone2 = alternatePhone,
                                    WebSite = website,
                                    Email = email,
                                    Lat = latitude,
                                    Lon = longitude
                                };
                
                                try
                                {
                                    _context.TblChurchNta.AddRange(formModel);
                                    await _context.SaveChangesAsync();
                                }
                                catch(Exception ex)
                                {
                                    Errors.AppendLine("Row Number:" + row + ex.InnerException.Message);
                                    numberFailed++;
                                    continue;
                                }             

                            }
                            else
                            {
                                Errors.AppendLine("Row Number:" + row + " Is blank. Please resubmit that row with relative data");
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
