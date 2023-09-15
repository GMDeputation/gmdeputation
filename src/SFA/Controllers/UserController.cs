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
			error = "Failed to Save: Google API is down Or address is invalid. Please reach out to IT Support if issue continues";
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
		var Pastorlatitude = "";
		var Pastorlongitude = "";

		var Churchlatitude = "";
		var Churchlongitude = "";

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
					
					List<TblRoleNta> roleEntities = await _context.TblRoleNta.ToListAsync();
					List<TblDistrictNta> districtEntities = await _context.TblDistrictNta.ToListAsync();
					List<TblSectionNta> sectionEntities = await _context.TblSectionNta.ToListAsync();
					List<TblCountryNta> countryEntities = await _context.TblCountryNta.ToListAsync();
					List<TblStateNta> source = await _context.TblStateNta.ToListAsync();
					for (int row = 2; row <= rowCount; row++)
					{
						List<TblUserNta> existingEntity = await _context.TblUserNta.Include((TblUserNta m) => m.District).ToListAsync();
						//Make sure there is data in the 15th column(5) -- If there isnt then we will ignore that under the assumption we read in a blank row.
						if (((ExcelRangeBase)worksheet.Cells[row, 5]).Value != null)
						{
							numberInserts++;
							//Doing a check on the data and making sure there is no data in the 15th column. If there is something is off.
							if (((ExcelRangeBase)worksheet.Cells[row, 32]).Value != null)
							{
								Errors.AppendLine("Row Number:" + row + " Excel Format is not right or data are not proper. Kindly upload the right format as per given format");
								//jsonString2 = "Excel Format is not right or data are not proper. Kindly upload the right format as per given format";
								//return Json(jsonString2);
								numberFailed++;
								continue;
							}
					
							TblRoleNta tblRole = roleEntities.Where((TblRoleNta m) => m.Id.ToString().Contains(((ExcelRangeBase)worksheet.Cells[row, 10]).Value.ToString())).FirstOrDefault();
	
							//Disting Name and Section Name we will Ignore in the import file. It is just for the user 
							//to have a visual that the ID's match the name
							//Looking into Section table for the district ID. If it does not find it district ID will be null old Logic it is being given now. 
							//Leaving code here in case mind is changed later. 
							//int? districtId = null;
							//try
							//{
							//	districtId = ((((ExcelRangeBase)worksheet.Cells[row, 2]).Value != null) ? new int?(sectionEntities.Where((TblSectionNta m) => m.Id.ToString() == ((ExcelRangeBase)worksheet.Cells[row, 2]).Value.ToString()).FirstOrDefault().DistrictId) : null);
							//}
							//catch (Exception ex)
							//{
							//	Errors.AppendLine("Row Number:" + row + " Failed to retirve District ID. Please verify Section ID is valid.");
							//	numberFailed++;
							//	Console.WriteLine(ex.Message);
							//	continue;
							//}
							//Column 2
							var districtIDString = (((((ExcelRangeBase)worksheet.Cells[row, 2]).Value == null) ? null : ((ExcelRangeBase)worksheet.Cells[row, 2]).Value.ToString()));
							int districtID = 0;
							Int32.TryParse(districtIDString, out districtID);
							//Column 3 is ignored. 
							//Column 4
							var sectionIdString = (((((ExcelRangeBase)worksheet.Cells[row, 4]).Value == null) ? null : ((ExcelRangeBase)worksheet.Cells[row, 4]).Value.ToString()));
							int sectionId = 0;
							Int32.TryParse(sectionIdString, out sectionId);
							//column 5
							var PastorfirstName = ((((ExcelRangeBase)worksheet.Cells[row, 5]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 5]).Value.ToString() : null);
							//column 6
							var PastorlastName = ((((ExcelRangeBase)worksheet.Cells[row, 6]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 6]).Value.ToString() : null);
							//Column 7
							var PastorGender = ((((ExcelRangeBase)worksheet.Cells[row, 7]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 7]).Value.ToString() : null);
							//Column 8
							var churchName = ((((ExcelRangeBase)worksheet.Cells[row, 8]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 8]).Value.ToString() : null);
							//Column 9
							var churchStreet = ((((ExcelRangeBase)worksheet.Cells[row, 9]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 9]).Value.ToString() : null);
							//Column 10
							var churchCity = ((((ExcelRangeBase)worksheet.Cells[row, 10]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 10]).Value.ToString() : null);
							//Column 11
							var churchState = ((((ExcelRangeBase)worksheet.Cells[row, 11]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 11]).Value.ToString() : null);
							//Column 12
							var churchPostalCode = ((((ExcelRangeBase)worksheet.Cells[row, 12]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 12]).Value.ToString() : null);
							//Column 13
							var churchCountryAlpha2 = ((((ExcelRangeBase)worksheet.Cells[row, 13]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 13]).Value.ToString() : null);
							//Column 14
							var churchFullAddress = ((((ExcelRangeBase)worksheet.Cells[row, 14]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 14]).Value.ToString() : null);
							//Column 15
							var email = ((((ExcelRangeBase)worksheet.Cells[row, 15]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 15]).Value.ToString() : null);
							//We need to make sure the email is formatted correctly and looks like a valid email
							//If it is not we are done reading in this row and will move to the next one
							if (email != null)
                            {
								if (!IsValidEmail(email))
								{
									Errors.AppendLine("Row Number:" + row + " " + email + ": Is not valid");
									numberFailed++;
									continue;
								}
							}
							else
                            {
								Errors.AppendLine("Row Number:" + row + " Email is a required field");
								numberFailed++;
								continue;
							}


							
							//Making ssure the email does not already exists in the database. 
							if (existingEntity.Select((TblUserNta m) => m.Email.ToLower()).Contains(email.ToLower()))
							{
								Errors.AppendLine("Row Number:" + row + " User Email Should be unique Or not right in " + row + " th row of excel sheet. Kindly check User Email");
								//jsonString2 = "User Email Should be unique Or not right in " + row + " th row of excel sheet. Kindly check User Email";
								//return Json(jsonString2);
								numberFailed++;
								continue;
							}
							//Column 16
							var phone = ((((ExcelRangeBase)worksheet.Cells[row, 16]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 16]).Value.ToString() : null);
							//Stripping all characters from phone number that isnt a number so a - or / etc
							if(phone != null)
								phone = GetNumbersOnly(phone);
							//Column 17
							var homePhone = ((((ExcelRangeBase)worksheet.Cells[row, 17]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 17]).Value.ToString() : null);
							//Stripping all characters from phone number that isnt a number so a - or / etc
							if (homePhone != null)
								homePhone = GetNumbersOnly(homePhone);
							//Column 18
							var workPhone = ((((ExcelRangeBase)worksheet.Cells[row, 18]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 18]).Value.ToString() : null);
							//Stripping all characters from phone number that isnt a number so a - or / etc
							if (workPhone != null)
								workPhone = GetNumbersOnly(workPhone);
							//Column 19
							var mobilePhone = ((((ExcelRangeBase)worksheet.Cells[row, 19]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 19]).Value.ToString() : null);
							//Stripping all characters from phone number that isnt a number so a - or / etc
							if (mobilePhone != null)
								mobilePhone = GetNumbersOnly(mobilePhone);
							//Column 20
							var PastorFullAddress = ((((ExcelRangeBase)worksheet.Cells[row, 20]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 20]).Value.ToString() : null);
							//Column 21
							var PastorStreet = ((((ExcelRangeBase)worksheet.Cells[row, 21]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 21]).Value.ToString() : null);
							//Column 22
							var PastorCity = ((((ExcelRangeBase)worksheet.Cells[row, 22]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 22]).Value.ToString() : null);
							//Column 23
							var PastorStateCode = ((((ExcelRangeBase)worksheet.Cells[row, 23]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 23]).Value.ToString() : null);
							//Column 24
							var PastorPostal = ((((ExcelRangeBase)worksheet.Cells[row, 24]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 24]).Value.ToString() : null);
							//Column 25
							var PastorCountryAlpha2 = ((((ExcelRangeBase)worksheet.Cells[row, 25]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 25]).Value.ToString() : null);
							//Column 26
							var ChurchHQAccountNumber = ((((ExcelRangeBase)worksheet.Cells[row, 26]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 26]).Value.ToString() : null);
							//Column 27
							var UserHQID = ((((ExcelRangeBase)worksheet.Cells[row, 27]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 27]).Value.ToString() : null);
							//Column 28
							var sensitiveNationFlag = ((((ExcelRangeBase)worksheet.Cells[row, 28]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 28]).Value.ToString() : null);
							//Column 29
							var r1Flag = ((((ExcelRangeBase)worksheet.Cells[row, 29]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 29]).Value.ToString() : null);
							var r1FlagBool = false;
							Boolean.TryParse(r1Flag, out r1FlagBool);
							//Column 30
							var PastorUserSalutation = ((((ExcelRangeBase)worksheet.Cells[row, 30]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 30]).Value.ToString() : null);

							//Column 31
							var roleIDString = ((((ExcelRangeBase)worksheet.Cells[row, 31]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 31]).Value.ToString() : null);
							int roleID = 0;
							Int32.TryParse(roleIDString, out roleID);

							if(roleID == 0)
                            {
								Errors.AppendLine("Row Number:" + row + " Role ID is a required field");
								numberFailed++;
								continue;
							}
							//Making sure the ID given exist in the database. If not we will stop reading in this row and move on to the next
							if (!roleEntities.Select((TblRoleNta m) => m.Id.ToString()).Contains(((ExcelRangeBase)worksheet.Cells[row, 31]).Value.ToString()))
							{
								Errors.AppendLine("Row Number:" + row + " Role not right in " + row + " th row of excel sheet. Kindly check Role ID");
								numberFailed++;
								continue;
							}
							//Column 31
							var churchWebsite = ((((ExcelRangeBase)worksheet.Cells[row, 31]).Value != null) ? ((ExcelRangeBase)worksheet.Cells[row, 31]).Value.ToString() : null);


							//For PastorFullAddress. if it does not exist but he street state city and zip do then we need to create
							//the address with that.
							if(PastorFullAddress == null)
                            {
								//If we have all the information needed to create the address
								if(PastorStreet != null && PastorCity != null && PastorStateCode != null && PastorPostal != null)
                                {
									PastorFullAddress = PastorStreet + "," + PastorCity +","+ PastorStateCode + " " + PastorPostal;

									//This will get the longitude and lat from the address. If we cannot find it then
									//The assumption is that it is a bad address. 
									var locationService = new GoogleLocationService("AIzaSyAoL5Cb3GKL803gYag0jud6d3iPHFZmbuI");
										MapPoint point = null;
										try
										{

											point = locationService.GetLatLongFromAddress(PastorFullAddress);
										}
										catch (Exception ex)
										{
											Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Pastor Generated Address(" + PastorFullAddress + ") : Please verify Street, City State and Postal Code");
											numberFailed++;
											Console.WriteLine(ex.Message);
											continue;
										}

									if(point != null)
                                    {
										Pastorlatitude = point.Latitude.ToString();
										Pastorlongitude = point.Longitude.ToString();
									}
                                    else
                                    {
										Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Pastor Generated Address(" + PastorFullAddress + ") : Please verify Street, City State and Postal Code");
										numberFailed++;										
										continue;
									}

									
								}
								//This means we do not have enough information to have an address for the pastor and we are going to ignore this row
								//else
                               // {
								//	Errors.AppendLine("Row Number:" + row + "Pastor Full Address was not given.Data City, State, Zip and Street were not given. Unable to create address for Pastor");
								//	numberFailed++;
								//}
                            }
							//Since the Full address does exist we are going to vaidate it using Google Validation. 
							//This will get the longitude and lat from the address. If we cannot find it then
							//The assumption is that it is a bad address. 
							else if (PastorFullAddress != null)
							{
								var locationService = new GoogleLocationService("AIzaSyAoL5Cb3GKL803gYag0jud6d3iPHFZmbuI");
								MapPoint point = null;
								try
								{

									point = locationService.GetLatLongFromAddress(PastorFullAddress);
								}
								catch (Exception ex)
								{
									Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Pastor Full Address: Please verify address syntax and validity");
									numberFailed++;
									Console.WriteLine(ex.Message);
									continue;
								}
								if (point != null)
                                {
									Pastorlatitude = point.Latitude.ToString();
									Pastorlongitude = point.Longitude.ToString();
								}
								else
                                {
									Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Pastor Generated Address(" + PastorFullAddress + ") : Please verify Street, City State and Postal Code");
									numberFailed++;
									continue;
								}


							}


							//For ChurchAddress. if it does not exist but he street state city and zip do then we need to create
							//the address with that.
							if (churchFullAddress == null)
							{
								//If we have all the information needed to create the address
								if (churchStreet != null && churchCity != null && churchState != null && churchPostalCode != null)
								{
									churchFullAddress = churchStreet + "," + churchCity + "," + churchState + " " + churchPostalCode;

									//This will get the longitude and lat from the address. If we cannot find it then
									//The assumption is that it is a bad address. 
									var locationService = new GoogleLocationService("AIzaSyAoL5Cb3GKL803gYag0jud6d3iPHFZmbuI");
									MapPoint point = null;
									try
									{

										point = locationService.GetLatLongFromAddress(churchFullAddress);
									}
									catch (Exception ex)
									{
										Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Church Generated Address(" + churchFullAddress + ") : Please verify Street, City State and Postal Code");
										numberFailed++;
										Console.WriteLine(ex.Message);
										continue;
									}
									if(point != null)
                                    {
										Churchlatitude = point.Latitude.ToString();
										Churchlongitude = point.Longitude.ToString();
									}
									else
                                    {
										Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Church Generated Address(" + churchFullAddress + ") : Please verify Street, City State and Postal Code");
										numberFailed++;
										continue;
									}


								}
								//This means we do not have enough information to have an address for the pastor and we are going to ignore this row
								else
								{
									Errors.AppendLine("Row Number:" + row + "Church Full Address was not given.Data City, State, Zip and Street were not given. Unable to create address for Church");
									numberFailed++;
								}
							}
							//Since the Full address does exist we are going to vaidate it using Google Validation. 
							//This will get the longitude and lat from the address. If we cannot find it then
							//The assumption is that it is a bad address. 
							else if (churchFullAddress != null)
							{
								var locationService = new GoogleLocationService("AIzaSyAoL5Cb3GKL803gYag0jud6d3iPHFZmbuI");
								MapPoint point = null;
								try
								{

									point = locationService.GetLatLongFromAddress(churchFullAddress);
								}
								catch (Exception ex)
								{
									Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Church Full Address: Please verify address syntax and validity");
									numberFailed++;
									Console.WriteLine(ex.Message);
									continue;
								}
								if(point != null)
                                {
									Churchlatitude = point.Latitude.ToString();
									Churchlongitude = point.Longitude.ToString();
								}
								else
                                {
									Errors.AppendLine("Row Number:" + row + " Google was not able to get Lat and Long from Church Full Address: Please verify address syntax and validity");
									numberFailed++;
									continue;
								}


							}

							//Retrieving the ID for the state based on the alias name
							var stateId = ((PastorStateCode != null) ? new int?(source.Where((TblStateNta m) => m.Alias != null && m.Alias.ToString().ToUpper() == PastorStateCode.ToString().ToUpper()).FirstOrDefault().Id) : null);

							//Retrieving the ID for the country based on the relationship from the state given
							var countryId = ((PastorStateCode != null) ? new int?(source.Where((TblStateNta m) => m.Alias != null && m.Alias.ToString().ToUpper() == PastorStateCode.ToString().ToUpper()).FirstOrDefault().CountryId) : null);


							TblUserNta formModel = new TblUserNta
							{

								FirstName = PastorfirstName == null? "":PastorfirstName,

								LastName = PastorlastName == null ? "" : PastorlastName,

								UserName = email == null ? "" : email,

								Email = email == null ? "" : email,

								Address = PastorFullAddress == null ? "" : PastorFullAddress,

								Zipcode = PastorPostal == null ? "" : PastorPostal,

								SectionId = sectionId == 0 ? 0 : sectionId,

								StateId = stateId == null ? 0 : stateId,

								RoleId = roleID == 0 ? 0 : roleID,

								City = PastorCity == null ? "" : PastorCity,
								AddressState = PastorStateCode == null ? "" : PastorStateCode,
								Street = PastorStreet == null ? "" : PastorStreet,
								Zip = PastorPostal == null ? "" : PastorPostal,
								R1 = r1FlagBool == false ? false : r1FlagBool,
								sensitiveNation = sensitiveNationFlag == null ? false : Convert.ToBoolean(sensitiveNationFlag),
								UserSalutation = PastorUserSalutation == null ? "" : PastorUserSalutation,

								HQID = UserHQID == null ? "" : UserHQID,
								Gender = PastorGender == null ? "" : PastorGender,

								WorkPhoneNo = workPhone == null ? "" : workPhone,

								TelePhoneNo = mobilePhone == null ? "" : mobilePhone,

								Phone = phone == null ? "" : phone,

								//We are looking up the country based on the state ID that is why we are looking at the state value here and extracting the Country ID from that table. 
								CountryId = countryId == 0 ? 0 : countryId,

								//This was another lookup that happened previous. We know the district because we know the section
								DistrictId = districtID == 0 ? 0 : districtID,

								Lat = Pastorlatitude == null ? "" : Pastorlatitude,

								Long = Pastorlongitude == null ? "" : Pastorlongitude,
								

								IsActive = true,
								InsertDatetime = DateTime.Now,
								InsertUser = loggedinUser.Id.ToString(),
								
							};


						

							try
							{
								//Adding User into Database Here
								_context.TblUserNta.AddRange(formModel);
								await _context.SaveChangesAsync();
							}
							catch(Exception ex)
                            {
								Errors.AppendLine("Adding User Failed: " + ex.InnerException.Message);
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
							
							try
                            {
								//Adding Password for User into Database Here
								_context.TblUserPasswordNta.AddRange(tmpInsert);
								await _context.SaveChangesAsync();
							}
							catch (Exception ex)
                            {
								Errors.AppendLine("Adding Password For User Failed: " + ex.InnerException.Message);
								numberFailed++;
								continue;
                            }

							TblChurchNta ChurchModel = new TblChurchNta
							{
								
								ChurchName = churchName == null ? "" : churchName,
								City = churchCity == null ? "" : churchCity,
								State = churchState == null ? "" : churchState,
								Zip = churchPostalCode == null ? "" : churchPostalCode,
								Street = churchStreet == null ? "" : churchStreet,
								Address = churchFullAddress == null ? "" : churchFullAddress,
								WebSite = churchWebsite == null ? "" : churchWebsite,
								AccountNo = ChurchHQAccountNumber == null ? "" : ChurchHQAccountNumber,
								Lat = Churchlatitude == null ? "" : Churchlatitude,
								Lon = Churchlongitude == null ? "" : Churchlongitude,
								InsertDatetime = DateTime.Now ,
								InsertUser = loggedinUser.Id.ToString(),
								DistrictId = districtID == 0 ? 0 : districtID,
								SectionId = sectionId == 0 ? 0 : sectionId,
							};

							try
							{
								//Adding Churchinto Database Here
								_context.TblChurchNta.AddRange(ChurchModel);
								await _context.SaveChangesAsync();
							}
							catch (Exception ex)
							{
								Errors.AppendLine("Inserting Church Failed: " + ex.InnerException.Message);
								numberFailed++;
								continue;

							}

							TblUserChurchNta pastorChurchConnetion = new TblUserChurchNta
							{
								UserId = formModel.Id,
								ChurchId = ChurchModel.Id,
								InsertDatetime = DateTime.Now,
								InsertUser = loggedinUser.Id.ToString(),
							};
							try
							{
								//Adding Church Pastor Connection into Database Here
								_context.TblUserChurchNta.AddRange(pastorChurchConnetion);
								await _context.SaveChangesAsync();
							}
							catch (Exception ex)
							{
								Errors.AppendLine("Pastor Church Connection Failed: " + ex.InnerException.Message);
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

	private static string GetNumbersOnly(string input)
	{
		return new string(input.Where(c => char.IsDigit(c)).ToArray());
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

