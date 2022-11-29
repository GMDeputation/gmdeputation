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
    [Route("attribute")]
    [Authorize]
    public class AttributeController : Controller
    {
        private readonly IAttributeService _attributeService = null;
        private readonly IHostingEnvironment _environment = null;
        private readonly SFADBContext _context = null;

        public AttributeController(IAttributeService attributeService, IHostingEnvironment environment, SFADBContext context)
        {
            _attributeService = attributeService;
            _environment = environment;
            _context = context;
        }

        [Route("")]
        [CheckAccess]
        public IActionResult Index()
        {
            return View();
        }

        [Route("addAttributeTypes")]
        [CheckAccess]
        public IActionResult AddAttributeTypes()
        {
            return View();
        }

        [Route("attributeDetail/{id?}")]
        [CheckAccess]
        public IActionResult AddAttribute(int? id)
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

        [Route("editAttribute/{id}")]
        [CheckAccess]
        public IActionResult EditAttribute(int id)
        {
            return View();
        }

        [Route("import")]
        [CheckAccess]
        public IActionResult Import(int id)
        {
            return View();
        }

        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var attribute = await _attributeService.GetAll();
            return new JsonResult(attribute);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]AttributeModelQuery query)
        {
            var attribute = await _attributeService.Search(query);
            return new JsonResult(attribute);
        }

        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var attribute = await _attributeService.GetById(id);
            return new JsonResult(attribute);
        }

        [Route("getByAttribute/{id}")]
        public async Task<IActionResult> GetByAttribute(int id)
        {
            var attribute = await _attributeService.GetByAttribute(id);
            return new JsonResult(attribute);
        }

        [Route("getByTypeId/{typeId}")]
        public async Task<IActionResult> GetByTypeId(int typeId)
        {
            var attribute = await _attributeService.GetByTypeId(typeId);
            return new JsonResult(attribute);
        }

        //[Route("delete/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
        //    var result = await _attributeService.Delete(id,loggedinUser.Id);
        //    return new JsonResult(result);
        //}

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]AttributeTypeModel attribute)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            //attribute.CreatedBy = loggedinUser.Id;

            var result = await _attributeService.Add(attribute, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody]AttributeTypeModel attribute)
        {
            if (id != attribute.Id)
            {
                return BadRequest("Attribute Id not matched");
            }
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            //attribute.LastModifiedBy = loggedinUser.Id;

            var result = await _attributeService.Edit(attribute, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("saveAttribute")]
        public async Task<IActionResult> SaveAttribute([FromBody]AttributeModel attribute)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var result = await _attributeService.SaveAttribute(attribute, loggedinUser);
            return new JsonResult(result);
        }


        [HttpPost]
        [Route("import-attribute")]
        public async Task<IActionResult> ImportSection([FromForm]AttributeModel attribute)  
        {

            var files = Request.Form.Files;
            var file = files[0];
            var jsonString = "";
            try
            {

                var extention = (file != null) ? Path.GetExtension(file.FileName) : null;
                if (file != null && (extention.Equals(".xls", StringComparison.OrdinalIgnoreCase) || extention.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLSX", StringComparison.OrdinalIgnoreCase) || extention.Equals(".XLS", StringComparison.OrdinalIgnoreCase)))
                {
                    var fileName = file.FileName;

                    var uploadPath = Path.Combine(_environment.WebRootPath, "resources", "attributes");
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

                    var model = new List<TblAttributeNta>();
                    FileInfo File = new FileInfo(Path.Combine(uploadPath, saveFileName));
                    using (ExcelPackage package = new ExcelPackage(File))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;

                        var existingEntity = await _context.TblAttributeNta.Include(m => m.AttributeType).ToListAsync();
                        var attributeTypes = await _context.TblAttributeTypeNta.ToListAsync();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 1].Value != null && worksheet.Cells[row, 2].Value != null && !worksheet.Cells[row, 1].Value.ToString().Contains("Aattribute Type", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!attributeTypes.Select(m => m.Name).Contains(worksheet.Cells[row, 1].Value.ToString()))
                                {
                                    jsonString = "Attribute Type is not right in " + row + " th row of excel sheet. Kindly check Attribute Type";
                                    return Json(jsonString);
                                }

                                var formModel = new TblAttributeNta
                                {                                                                      
                                    AttributeTypeId = attributeTypes.Where(m => m.Name == worksheet.Cells[row, 1].Value.ToString()).FirstOrDefault().Id,
                                    AttributeName = worksheet.Cells[row, 2].Value.ToString(),
                                    AttributeNotes = worksheet.Cells[row, 3].Value?.ToString()
                                };

                                var currentList = model.Where(m => m.AttributeName == formModel.AttributeName).ToList();
                                var existingList = existingEntity.Where(m => m.AttributeName == formModel.AttributeName).ToList();

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
                                jsonString = "Excel Format is not right. Kindly upload the right format as per given format";
                                return Json(jsonString);
                            }

                        }
                        _context.TblAttributeNta.AddRange(model);
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