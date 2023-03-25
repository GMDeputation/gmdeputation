using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
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

	private readonly IWebHostEnvironment _environment;

	private readonly SFADBContext _context;

	public UserController(IUserService userService, IWebHostEnvironment environment, SFADBContext context)
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

	[Route("api/GetAllPastorsByDistrict/{id}")]
	public async Task<IActionResult> GetAllPastorsByDistrict(int id)
	{
		return new JsonResult(await _userService.GetAllPastorsByDistrict(id));
	}

	[HttpPost]
	[Route("api/save")]
	public async Task<IActionResult> Save([FromBody] User user)
	{
		var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
		var result = await _userService.Save(user, loggedinUser);
		string error = "";
		if (result == 3)
        {
			error = "Failed to Save: Google API is down. Please reach out to IT Support";
			return new JsonResult(error);

		}
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
		var latitude = "";
		var longitude = "";
		var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
		StringBuilder Errors = new StringBuilder();
		IFormFileCollection files = base.Request.Form.Files;
		IFormFile formFile = files[0];
		string jsonString2 = "";
		base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		try
		{
			string text = ((formFile != null) ? Path.GetExtension(formFile.FileName) : null);
			//Checking if it is an Excel File
			if (formFile != null && (text.Equals(".xls", StringComparison.OrdinalIgnoreCase) || text.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || text.Equals(".XLSX", StringComparison.OrdinalIgnoreCase) || text.Equals(".XLS", StringComparison.OrdinalIgnoreCase)))
			{
				_ = formFile.FileName;
				string uploadPath = Path.Combine(_environment.WebRootPath, "resources", "users");
				if (!Directory.Exists(uploadPath))
				{
					Directory.CreateDirectory(uploadPath);
				}
				string text2 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + "_" + loggedinUser.Id.ToString() + "_" + loggedinUser.Name;
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
					int numberInserts = 0;
					int numberFailed = 0;
					_ = string.Empty;
					List<TblUserNta> existingEntity = await _context.TblUserNta.Include((TblUserNta m) => m.District).ToListAsync();
					List<TblRoleNta> roleEntities = await _context.TblRoleNta.ToListAsync();
					List<TblDistrictNta> districtEntities = await _context.TblDistrictNta.ToListAsync();
					List<TblSectionNta> sectionEntities = await _context.TblSectionNta.ToListAsync();
					List<TblCountryNta> countryEntities = await _context.TblCountryNta.ToListAsync();
					List<TblStateNta> source = await _context.TblStateNta.ToListAsync();
					for (int row = 2; row <= rowCount; row++)
					{
						//Make sure there is data in the first column -- If there isnt then we will ignore that under the assumption we read in a blank row.
						if (((ExcelRangeBase)worksheet.Cells[row, 1]).Value != null)
						{
							numberInserts++;
							//Doing a check on the data and making sure there is no data in the 15th column. If there is something is off.
							if (((ExcelRangeBase)worksheet.Cells[row, 15]).Value != null)
							{
								Errors.AppendLine("Row Number:" + row + " Excel Format is not right or data are not proper. Kindly upload the right format as per given format");
								//jsonString2 = "Excel Format is not right or data are not proper. Kindly upload the right format as per given format";
								//return Json(jsonString2);
								numberFailed++;
								continue;
							}
							//Makinf ssure the email does not already exists in the database. 
							if (existingEntity.Select((TblUserNta m) => m.Email.ToLower()).Contains(((ExcelRangeBase)worksheet.Cells[row,4]).Value.ToString().ToLower()))
							{
								Errors.AppendLine("Row Number:" + row + " User Email Should be unique Or not right in " + row + " th row of excel sheet. Kindly check User Email");
								//jsonString2 = "User Email Should be unique Or not right in " + row + " th row of excel sheet. Kindly check User Email";
								//return Json(jsonString2);
								numberFailed++;
								continue;
							}							
							if (!roleEntities.Select((TblRoleNta m) => m.Id.ToString()).Contains(((ExcelRangeBase)worksheet.Cells[row, 10]).Value.ToString()))
							{
								Errors.AppendLine("Row Number:" + row + " Role not right in " + row + " th row of excel sheet. Kindly check Role ID");
								//jsonString2 = "Role not right in " + row + " th row of excel sheet. Kindly check Role ID";
								//return Json(jsonString2);
								numberFailed++;
								continue;
							}
							//Looking into Section table for the district ID. If it does not find it district ID will be null
							//Looking into Section table for the district ID. If it does not find it district ID will be null
							int? districtId = null;
							try
							{
								districtId = ((((ExcelRangeBase)worksheet.Cells[row, 8]).Value != null) ? new int?(sectionEntities.Where((TblSectionNta m) => m.Id.ToString() == ((ExcelRangeBase)worksheet.Cells[row, 8]).Value.ToString()).FirstOrDefault().DistrictId) : null);
							}
							catch (Exception ex)
							{
								Errors.AppendLine("Row Number:" + row + " Failed to retirve District ID. Please verify Section ID is valid.");
								numberFailed++;
								Console.WriteLine(ex.Message);
								continue;
							}														

							TblRoleNta tblRole = roleEntities.Where((TblRoleNta m) => m.Id.ToString().Contains(((ExcelRangeBase)worksheet.Cells[row, 10]).Value.ToString())).FirstOrDefault();

							var firstName = ((ExcelRangeBase)worksheet.Cells[row, 1]).Value.ToString();
							var middleName = ((((ExcelRangeBase)worksheet.Cells[row, 2]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 2]).Value.ToString() : null);
							var lastName = ((ExcelRangeBase)worksheet.Cells[row, 3]).Value.ToString();
							var email = ((ExcelRangeBase)worksheet.Cells[row, 4]).Value.ToString();
							if(!IsValidEmail(email))
                            {
								Errors.AppendLine("Row Number:" + row + " " +email + ": Is not valid");
								numberFailed++;
								continue;
							}
							var gender = ((((ExcelRangeBase)worksheet.Cells[row, 5]).Value == null) ? null : ((ExcelRangeBase)worksheet.Cells[row, 5]).Value?.ToString());
							var address = ((((ExcelRangeBase)worksheet.Cells[row, 6]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 6]).Value.ToString() : null);
							if(address != null)
                            {								

								var locationService = new GoogleLocationService("AIzaSyAoL5Cb3GKL803gYag0jud6d3iPHFZmbuI");
								MapPoint point = null;
								try
                                {

									point = locationService.GetLatLongFromAddress(address);
								}								
								catch(Exception ex)
                                {
									Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Address: Please verify address syntax and validity");
									numberFailed++;
									Console.WriteLine(ex.Message);
									continue;
								}

								latitude = point.Latitude.ToString();
								longitude = point.Longitude.ToString();
							}
							var zipCode = ((ExcelRangeBase)worksheet.Cells[row, 7]).Value;
							var sectionId = (((((ExcelRangeBase)worksheet.Cells[row, 8]).Value == null) ? null : ((ExcelRangeBase)worksheet.Cells[row, 8]).Value.ToString()));
							int? sectionIdInt = null;
							if (sectionId != null || sectionId != "")
								sectionIdInt = Convert.ToInt32(sectionId);				
				
							var stateId = ((((ExcelRangeBase)worksheet.Cells[row, 9]).Value != null) ? new int?(source.Where((TblStateNta m) => m.Alias != null && m.Alias.ToString().ToUpper() == ((ExcelRangeBase)worksheet.Cells[row, 9]).Value.ToString().ToUpper()).FirstOrDefault().Id) : null);
							var roleId = roleEntities.Where((TblRoleNta m) => m.Id.ToString().Contains(((ExcelRangeBase)worksheet.Cells[row, 10]).Value.ToString())).FirstOrDefault().Id;
							var city = ((((ExcelRangeBase)worksheet.Cells[row, 11]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 11]).Value.ToString() : null);
							var workPhoneNum = ((((ExcelRangeBase)worksheet.Cells[row, 12]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 12]).Value.ToString() : null);
							var landLine = ((((ExcelRangeBase)worksheet.Cells[row, 13]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 13]).Value.ToString() : null);
							var mobilePhone = ((((ExcelRangeBase)worksheet.Cells[row, 14]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 14]).Value.ToString() : null);
							var countryId = ((((ExcelRangeBase)worksheet.Cells[row, 9]).Value != null) ? new int?(source.Where((TblStateNta m) => m.Alias != null && m.Alias.ToString().ToUpper() == ((ExcelRangeBase)worksheet.Cells[row, 9]).Value.ToString()).FirstOrDefault().CountryId) : null);
							TblUserNta formModel = new TblUserNta
							{

								FirstName = firstName,

								MiddleName = middleName,

								LastName = lastName,

								UserName = email,

								Email = email,

								Gender = gender,

								Address = address,

								Zipcode = zipCode.ToString(),

								SectionId = sectionIdInt,

								StateId = stateId,

								RoleId = roleId,

								City = city,

								WorkPhoneNo = workPhoneNum,

								TelePhoneNo = landLine,

								Phone = mobilePhone,

								//We are looking up the country based on the state ID that is why we are looking at the state value here and extracting the Country ID from that table. 
								CountryId = countryId,

								//This was another lookup that happened previous. We know the district because we know the section
								DistrictId = districtId,

								Lat = latitude,

								Long = longitude,

								IsActive = true,
								InsertDatetime = DateTime.Now,
								InsertUser = loggedinUser.Id.ToString(),
								
							};


							//Adding User into Database Here
							_context.TblUserNta.AddRange(formModel);

							try
							{
								await _context.SaveChangesAsync();
							}
							catch(Exception ex)
                            {
								Errors.AppendLine("Row Number:" + row + " " + ex.InnerException.Message);
								numberFailed++;
								continue;

							}


							//We now need to add the password for that user. 
							TblUserPasswordNta tmpInsert = (new TblUserPasswordNta
							{
								UserId = formModel.Id,
								Password = "123",
								InsertDatetime = DateTime.Now,
								InsertUser = loggedinUser.Id.ToString(),
							});
							//Adding Password for User into Database Here
							_context.TblUserPasswordNta.AddRange(tmpInsert);
							try
                            {
								await _context.SaveChangesAsync();
							}
							catch (Exception ex)
                            {
								Errors.AppendLine("Row Number:" + row + " " + ex.InnerException.Message);
								numberFailed++;
								continue;
                            }
							

						}
						//This means the data import read in a blank link and we are going to ingore
						else
							continue;
					}

                        //StringBuilder stringBuilder = new StringBuilder();
                        //stringBuilder.AppendLine(string.Format("<p> Hi " + firs + " " + item.MiddleName + " " + item.LastName + "</p>"));
                        //stringBuilder.AppendLine(string.Format("<p style='margin-left:30px'>Your can login on UPCI GLOBAL MISSIONS DEPUTATION using the below credentials. </p><p>Email: " + item.Email + "</p><p>Default Password: " + item.TblUserPasswordNta.FirstOrDefault().Password + "<br><p>Kindly click <a href='https://gmdeputation.com/'>UPCI GLOBAL MISSIONS DEPUTATION</a> to login.</p>"));
                        //try
                        //{
                        //    MimeMessage emailMessage = new MimeMessage();
                        //    emailMessage.From.Add((InternetAddress)new MailboxAddress("UPCI GLOBAL MISSIONS DEPUTATION", "notify@realchurch.eu"));
                        //    emailMessage.To.Add((InternetAddress)new MailboxAddress("Verify Email", item.Email));
                        //    emailMessage.Subject = ("New User Creation and Verify Email");
                        //    TextPart val = new TextPart("html");
                        //    val.Text = (stringBuilder.ToString());
                        //    emailMessage.Body = ((MimeEntity)val);
                        //    SmtpClient client = new SmtpClient();
                        //    try
                        //    {
                        //        client.LocalDomain = ("smtp.easyname.com");
                        //        await ((MailService)client).ConnectAsync("smtp.easyname.com", 465, (SecureSocketOptions)1, default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
                        //        await ((MailService)client).AuthenticateAsync((ICredentials)new NetworkCredential("notify@realchurch.eu", "0.etx6z6qcup"), default(CancellationToken));
                        //        await ((MailTransport)client).SendAsync(emailMessage, default(CancellationToken), (ITransferProgress)null).ConfigureAwait(continueOnCapturedContext: false);
                        //        await ((MailService)client).DisconnectAsync(true, default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
                        //    }
                        //    finally
                        //    {
                        //        ((IDisposable)client)?.Dispose();
                        //    }
                        //}
                        //catch (Exception value)
                        //{
                        //    return StatusCode(500, value);
                        //}
                    
                    //_context.TblUserNta.AddRange(model);
                    //await _context.SaveChangesAsync();
					if(Errors.Length==0)
                    {
						return Json(jsonString2);
					}
					else
                    {
						return Json(numberInserts + "Attempted to be added: " + numberFailed + "Failed:Errors are as follows:\n" + Errors);
                    }
                   
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
			val.Dispose();
		
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

