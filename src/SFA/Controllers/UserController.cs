using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SFA.Entities;
using SFA.Extensions;
using SFA.Filters;
using SFA.Models;
using SFA.Services;
[Route("user")]
[Authorize]
public class UserController : Controller
{
	private readonly IUserService _userService;

	private readonly IHostingEnvironment _environment;

	private readonly SFADBContext _context;

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
		base.ViewBag.Id = id;
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

	[Route("changeDistrict")]
	[CheckAccess]
	public ActionResult DistrictChange()
	{
		return View();
	}

	[Route("api/menus")]
	public async Task<IActionResult> GetMenu()
	{
		User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		int id = user.Id;
		return Json(await _userService.GetMenuByUser(id));
	}

	[Route("api/all")]
	public async Task<IActionResult> GetAll()
	{
		return new JsonResult(await _userService.GetAll());
	}

	[Route("changePass")]
	public async Task<IActionResult> ChangePassword([FromBody] User user)
	{
		User user2 = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		return new JsonResult(await _userService.ChangePassword(user2.Id, user));
	}

	[Route("api/search")]
	public async Task<IActionResult> Search([FromBody] UserQuery query)
	{
		return new JsonResult(await _userService.Search(query));
	}

	[Route("api/get/{id}")]
	public async Task<IActionResult> Get(int id)
	{
		return new JsonResult(await _userService.GetById(id));
	}

	[Route("api/getAllMissionariesUser")]
	public async Task<IActionResult> GetAllMissionariesByDistrict()
	{
		return new JsonResult(await _userService.GetAllMissionariesUser());
	}

	[HttpPost]
	[Route("api/save")]
	public async Task<IActionResult> Save([FromBody] User user)
	{
		var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
		var result = await _userService.Save(user, loggedinUser);
		return new JsonResult(result);
	}

	[HttpPost]
	[Route("api/updateDistrictAndSection")]
	public async Task<IActionResult> UpdateDistrictAndSection([FromBody] List<User> users)
	{
		return new JsonResult(await _userService.UpdateDistrictAndSection(users));
	}

	[HttpPost]
	[Route("export-section")]
	public async Task<IActionResult> ExportSection([FromForm] User user)
	{
		IFormFileCollection files = base.Request.Form.Files;
		IFormFile formFile = files[0];
		string jsonString2 = "";
		base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		try
		{
			string text = ((formFile != null) ? Path.GetExtension(formFile.FileName) : null);
			if (formFile != null && (text.Equals(".xls", StringComparison.OrdinalIgnoreCase) || text.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || text.Equals(".XLSX", StringComparison.OrdinalIgnoreCase) || text.Equals(".XLS", StringComparison.OrdinalIgnoreCase)))
			{
				_ = formFile.FileName;
				string uploadPath = Path.Combine(_environment.WebRootPath, "resources", "users");
				if (!Directory.Exists(uploadPath))
				{
					Directory.CreateDirectory(uploadPath);
				}
				string text2 = DateTime.Now.Ticks.ToString();
				string saveFileName = text2 + Path.GetExtension(formFile.FileName);
				using (FileStream fileStream = new FileStream(Path.Combine(uploadPath, saveFileName), FileMode.Create))
				{
					await formFile.CopyToAsync(fileStream);
				}
				List<TblUserNta> model = new List<TblUserNta>();
				new List<TblUserPasswordNta>();
				FileInfo fileInfo = new FileInfo(Path.Combine(uploadPath, saveFileName));
				ExcelPackage package = new ExcelPackage(fileInfo);
				try
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					int rowCount = worksheet.Dimension.Rows;
					_ = string.Empty;
					List<TblUserNta> existingEntity = await _context.TblUserNta.Include((TblUserNta m) => m.District).ToListAsync();
					List<TblRoleNta> roleEntities = await _context.TblRoleNta.ToListAsync();
					List<TblDistrictNta> districtEntities = await _context.TblDistrictNta.ToListAsync();
					List<TblSectionNta> sectionEntities = await _context.TblSectionNta.ToListAsync();
					List<TblCountryNta> countryEntities = await _context.TblCountryNta.ToListAsync();
					List<TblStateNta> source = await _context.TblStateNta.ToListAsync();
					for (int row = 2; row <= rowCount; row++)
					{
						if (((ExcelRangeBase)worksheet.Cells[row, 1]).Value != null || ((ExcelRangeBase)worksheet.Cells[row, 3]).Value != null || ((ExcelRangeBase)worksheet.Cells[row, 4]).Value != null || ((ExcelRangeBase)worksheet.Cells[row, 7]).Value != null || ((ExcelRangeBase)worksheet.Cells[row, 13]).Value != null || ((ExcelRangeBase)worksheet.Cells[row, 15]).Value != null || ((ExcelRangeBase)worksheet.Cells[row, 18]).Value != null)
						{
							if (((ExcelRangeBase)worksheet.Cells[row, 1]).Value == null || ((ExcelRangeBase)worksheet.Cells[row, 3]).Value == null || ((ExcelRangeBase)worksheet.Cells[row, 4]).Value == null || ((ExcelRangeBase)worksheet.Cells[row, 7]).Value == null || ((ExcelRangeBase)worksheet.Cells[row, 13]).Value == null || ((ExcelRangeBase)worksheet.Cells[row, 15]).Value == null || ((ExcelRangeBase)worksheet.Cells[row, 18]).Value == null)
							{
								jsonString2 = "Excel Format is not right or data are not proper. Kindly upload the right format as per given format";
								return Json(jsonString2);
							}
							if (existingEntity.Select((TblUserNta m) => m.UserName.ToLower()).Contains(((ExcelRangeBase)worksheet.Cells[row, 13]).Value.ToString().ToLower()))
							{
								jsonString2 = "User Email Should be unique Or not right in " + row + " th row of excel sheet. Kindly check User Email";
								return Json(jsonString2);
							}
							if (!roleEntities.Select((TblRoleNta m) => m.Name).Contains(((ExcelRangeBase)worksheet.Cells[row, 15]).Value.ToString()))
							{
								jsonString2 = "Role not right in " + row + " th row of excel sheet. Kindly check Role Name";
								return Json(jsonString2);
							}
							if (((ExcelRangeBase)worksheet.Cells[row, 8]).Value != null && !districtEntities.Select((TblDistrictNta m) => m.Code).Contains(((ExcelRangeBase)worksheet.Cells[row, 8]).Value.ToString()))
							{
								jsonString2 = "District Code is not right in " + row + " th row of excel sheet. Kindly check District code";
								return Json(jsonString2);
							}
							int? districtId = ((((ExcelRangeBase)worksheet.Cells[row, 8]).Value != null) ? new int?(districtEntities.Where((TblDistrictNta m) => m.Code == ((ExcelRangeBase)worksheet.Cells[row, 8]).Value.ToString()).FirstOrDefault().Id) : null);
							if (((ExcelRangeBase)worksheet.Cells[row, 9]).Value != null && districtId.HasValue && districtId != 0 && !(from m in sectionEntities.Where(delegate (TblSectionNta m)
							{
								int districtId3 = m.DistrictId;
								int? guid3 = districtId;
								return districtId3 == guid3;
							})
																																	   select m.Name).Contains(((ExcelRangeBase)worksheet.Cells[row, 9]).Value.ToString()))
							{
								jsonString2 = "Section Name is not found under " + ((ExcelRangeBase)worksheet.Cells[row, 8]).Value.ToString() + " District " + row + " th row of excel sheet. Kindly check Section Name";
								return Json(jsonString2);
							}
							if (((ExcelRangeBase)worksheet.Cells[row, 16]).Value != null && !countryEntities.Select((TblCountryNta m) => m.Alpha2Code).Contains(((ExcelRangeBase)worksheet.Cells[row, 16]).Value.ToString()))
							{
								jsonString2 = "Country Alpha Code is not right in " + row + " th row of excel sheet. Kindly check Country Alpha Code";
								return Json(jsonString2);
							}
							if (((ExcelRangeBase)worksheet.Cells[row, 17]).Value != null && !source.Select((TblStateNta m) => m.Alias).Contains(((ExcelRangeBase)worksheet.Cells[row, 17]).Value.ToString()))
							{
								jsonString2 = "State Code is not right in " + row + " th row of excel sheet. Kindly check State Code";
								return Json(jsonString2);
							}

							TblRoleNta tblRole = roleEntities.Where((TblRoleNta m) => m.Name.Contains(((ExcelRangeBase)worksheet.Cells[row, 15]).Value.ToString())).FirstOrDefault();
							TblUserNta formModel = new TblUserNta
							{

								FirstName = ((ExcelRangeBase)worksheet.Cells[row, 1]).Value.ToString(),
								MiddleName = ((((ExcelRangeBase)worksheet.Cells[row, 2]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 2]).Value.ToString() : null),
								LastName = ((ExcelRangeBase)worksheet.Cells[row, 3]).Value.ToString(),
								Gender = ((((ExcelRangeBase)worksheet.Cells[row, 4]).Value == null) ? null : ((ExcelRangeBase)worksheet.Cells[row, 4]).Value?.ToString()),
								Address = ((((ExcelRangeBase)worksheet.Cells[row, 5]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 5]).Value.ToString() : null),
								City = ((((ExcelRangeBase)worksheet.Cells[row, 6]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 6]).Value.ToString() : null),
								Zipcode = ((ExcelRangeBase)worksheet.Cells[row, 7]).Value.ToString(),
								DistrictId = ((((ExcelRangeBase)worksheet.Cells[row, 8]).Value != null) ? districtId : null),
								SectionId = ((((ExcelRangeBase)worksheet.Cells[row, 9]).Value != null) ? new int?(sectionEntities.Where(delegate (TblSectionNta m)
								{
									int districtId2 = m.DistrictId;
									int? guid2 = districtId;
									return districtId2 == guid2 && m.Name.Contains(((ExcelRangeBase)worksheet.Cells[row, 9]).Value.ToString());
								}).FirstOrDefault().Id) : null),
								TelePhoneNo = ((((ExcelRangeBase)worksheet.Cells[row, 10]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 10]).Value.ToString() : null),
								WorkPhoneNo = ((((ExcelRangeBase)worksheet.Cells[row, 11]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 11]).Value.ToString() : null),
								Phone = ((((ExcelRangeBase)worksheet.Cells[row, 12]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 12]).Value.ToString() : null),
								Email = ((ExcelRangeBase)worksheet.Cells[row, 13]).Value.ToString(),
								UserName = ((ExcelRangeBase)worksheet.Cells[row, 13]).Value.ToString(),
								RoleId = roleEntities.Where((TblRoleNta m) => m.Name.Contains(((ExcelRangeBase)worksheet.Cells[row, 15]).Value.ToString())).FirstOrDefault().Id,
								CountryId = ((((ExcelRangeBase)worksheet.Cells[row, 16]).Value != null) ? new int?(countryEntities.Where((TblCountryNta m) => m.Alpha2Code != null && m.Alpha2Code == ((ExcelRangeBase)worksheet.Cells[row, 16]).Value.ToString()).FirstOrDefault().Id) : null),
								StateId = ((((ExcelRangeBase)worksheet.Cells[row, 17]).Value != null) ? new int?(source.Where((TblStateNta m) => m.Alias != null && m.Alias == ((ExcelRangeBase)worksheet.Cells[row, 17]).Value.ToString()).FirstOrDefault().Id) : null),
								IsActive = ((((ExcelRangeBase)worksheet.Cells[row, 14]).Value != null && ((ExcelRangeBase)worksheet.Cells[row, 14]).Value.ToString() == "Active") ? true : false),
								InsertDatetime = DateTime.Now
							};
							formModel.TblUserPasswordNta.Add(new TblUserPasswordNta
							{
								UserId = formModel.Id,
								Password = "123",
								InsertDatetime = DateTime.Now,
							});
							List<TblUserNta> list = model.Where((TblUserNta m) => m.Email == formModel.Email).ToList();
							List<TblUserNta> list2 = existingEntity.Where((TblUserNta m) => m.Email == formModel.Email).ToList();
							if (list.Count > 0 || list2.Count > 0)
							{
								model.Remove(formModel);
							}
							else
							{
								model.Add(formModel);
							}
						}
					}
					foreach (TblUserNta item in model)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine(string.Format("<p> Hi " + item.FirstName + " " + item.MiddleName + " " + item.LastName + "</p>"));
						stringBuilder.AppendLine(string.Format("<p style='margin-left:30px'>Your can login on UPCI GLOBAL MISSIONS DEPUTATION using the below credentials. </p><p>Email: " + item.Email + "</p><p>Default Password: " + item.TblUserPasswordNta.FirstOrDefault().Password + "<br><p>Kindly click <a href='https://gmdeputation.com/'>UPCI GLOBAL MISSIONS DEPUTATION</a> to login.</p>"));
						try
						{
							MimeMessage emailMessage = new MimeMessage();
							emailMessage.From.Add((InternetAddress)new MailboxAddress("UPCI GLOBAL MISSIONS DEPUTATION", "notify@realchurch.eu"));
							emailMessage.To.Add((InternetAddress)new MailboxAddress("Verify Email", item.Email));
							emailMessage.Subject = ("New User Creation and Verify Email");
							TextPart val = new TextPart("html");
							val.Text = (stringBuilder.ToString());
							emailMessage.Body = ((MimeEntity)val);
							SmtpClient client = new SmtpClient();
							try
							{
								client.LocalDomain = ("smtp.easyname.com");
								await ((MailService)client).ConnectAsync("smtp.easyname.com", 465, (SecureSocketOptions)1, default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
								await ((MailService)client).AuthenticateAsync((ICredentials)new NetworkCredential("notify@realchurch.eu", "0.etx6z6qcup"), default(CancellationToken));
								await ((MailTransport)client).SendAsync(emailMessage, default(CancellationToken), (ITransferProgress)null).ConfigureAwait(continueOnCapturedContext: false);
								await ((MailService)client).DisconnectAsync(true, default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
							}
							finally
							{
								((IDisposable)client)?.Dispose();
							}
						}
						catch (Exception value)
						{
							return StatusCode(500, value);
						}
					}
					_context.TblUserNta.AddRange(model);
					await _context.SaveChangesAsync();
					return Json(jsonString2);
				}
				finally
				{
					((IDisposable)package)?.Dispose();
				}
			}
			return Json("Extension is not match");
		}
		catch (Exception value2)
		{
			return StatusCode(500, value2);
		}
	}

	[Route("exportListData")]
	public async Task<IActionResult> ExportListData()
	{
		List<User> list = await _userService.GetAll();
		string path = "Users.xlsx";
		string text = Path.Combine(_environment.WebRootPath, "resources", "reports", path);
		FileInfo fileInfo = new FileInfo(Path.Combine(text));
		if (fileInfo.Exists)
		{
			fileInfo.Delete();
			fileInfo = new FileInfo(Path.Combine(text));
		}
		new MemoryStream();
		ExcelPackage val = new ExcelPackage(fileInfo);
		try
		{
			ExcelWorksheet val2 = val.Workbook.Worksheets.Add("User");
			((ExcelRangeBase)val2.Cells[1, 1]).Value = ((object)"User Id");
			((ExcelRangeBase)val2.Cells[1, 2]).Value = ((object)"Name");
			((ExcelRangeBase)val2.Cells[1, 3]).Value = ((object)"Role Name");
			((ExcelRangeBase)val2.Cells[1, 4]).Value = ((object)"Email");
			((ExcelRangeBase)val2.Cells[1, 5]).Value = ((object)"Phone No");
			((ExcelRangeBase)val2.Cells[1, 6]).Value = ((object)"District Name");
			((ExcelRangeBase)val2.Cells[1, 7]).Value = ((object)"Section Name");
			int num = 1;
			val2.Row(num).Style.Font.Bold = true;
			((ExcelRangeBase)val2.Cells[num, 1, num, 1]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
			((ExcelRangeBase)val2.Cells[num, 1, num, 1]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
			((ExcelRangeBase)val2.Cells[num, 2, num, 2]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
			((ExcelRangeBase)val2.Cells[num, 2, num, 2]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
			((ExcelRangeBase)val2.Cells[num, 3, num, 3]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
			((ExcelRangeBase)val2.Cells[num, 3, num, 3]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
			((ExcelRangeBase)val2.Cells[num, 4, num, 4]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
			((ExcelRangeBase)val2.Cells[num, 4, num, 4]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
			((ExcelRangeBase)val2.Cells[num, 5, num, 5]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
			((ExcelRangeBase)val2.Cells[num, 5, num, 5]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
			((ExcelRangeBase)val2.Cells[num, 6, num, 6]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
			((ExcelRangeBase)val2.Cells[num, 6, num, 6]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
			((ExcelRangeBase)val2.Cells[num, 7, num, 7]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
			((ExcelRangeBase)val2.Cells[num, 7, num, 7]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
			num++;
			if (list != null && list.Count > 0)
			{
				foreach (User item in list)
				{
					((ExcelRangeBase)val2.Cells[num, 1, num, 1]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
					((ExcelRangeBase)val2.Cells[num, 1, num, 1]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
					((ExcelRangeBase)val2.Cells[num, 2, num, 2]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
					((ExcelRangeBase)val2.Cells[num, 2, num, 2]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
					((ExcelRangeBase)val2.Cells[num, 3, num, 3]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
					((ExcelRangeBase)val2.Cells[num, 3, num, 3]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
					((ExcelRangeBase)val2.Cells[num, 4, num, 4]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
					((ExcelRangeBase)val2.Cells[num, 4, num, 4]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
					((ExcelRangeBase)val2.Cells[num, 5, num, 5]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
					((ExcelRangeBase)val2.Cells[num, 5, num, 5]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
					((ExcelRangeBase)val2.Cells[num, 6, num, 6]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
					((ExcelRangeBase)val2.Cells[num, 6, num, 6]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
					((ExcelRangeBase)val2.Cells[num, 7, num, 7]).Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
					((ExcelRangeBase)val2.Cells[num, 7, num, 7]).Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
					((ExcelRangeBase)val2.Cells[num, 1]).Value = ((object)item.Id);
					((ExcelRangeBase)val2.Cells[num, 2]).Value = ((object)item.Name);
					((ExcelRangeBase)val2.Cells[num, 3]).Value = ((object)item.RoleName);
					((ExcelRangeBase)val2.Cells[num, 4]).Value = ((object)item.Email);
					((ExcelRangeBase)val2.Cells[num, 5]).Value = ((object)item.Phone);
					((ExcelRangeBase)val2.Cells[num, 6]).Value = ((object)item.DistrictName);
					((ExcelRangeBase)val2.Cells[num, 7]).Value = ((object)item.SectionName);
					num++;
				}
			}
			else
			{
				((ExcelRangeBase)val2.Cells[5, 4]).Value = ((object)"No Data Found");
			}
			val.Save();
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		PhysicalFileResult result = PhysicalFile(Path.Combine(text), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
		base.Response.Headers.Add("x-filename", fileInfo.Name);
		base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
		{
			FileName = fileInfo.Name
		}.ToString();
		return result;
	}
}

