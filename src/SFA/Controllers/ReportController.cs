

// SFA.Controllers.ReportController
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SFA.Extensions;
using SFA.Filters;
using SFA.Models;
using SFA.Services;

namespace SFA.Controllers
{
	[Route("reports")]
	[Authorize]
	public class ReportController : Controller
	{
		private readonly IReportService _reportService;

		private readonly IHostingEnvironment _environment;

		public ReportController(IReportService reportService, IHostingEnvironment environment)
		{
			_reportService = reportService;
			_environment = environment;
		}

		[Route("userActivityReport")]
		[CheckAccess]
		public IActionResult UserActivityReport()
		{
			return View();
		}

		[Route("churchServiceCountReport")]
		[CheckAccess]
		public IActionResult ChurchServiceCountReport()
		{
			return View();
		}

		[Route("pastorContactReport")]
		[CheckAccess]
		public IActionResult PastorContactReport()
		{
			return View();
		}

		[Route("macroscheduleWithApoinmentReport")]
		[CheckAccess]
		public IActionResult MacroscheduleWithApoinmentReport()
		{
			return View();
		}

		[Route("churchWiseApoinmentReport")]
		[CheckAccess]
		public IActionResult ChurchWiseApoinmentReport()
		{
			return View();
		}

		[Route("offeringOnlyReport")]
		[CheckAccess]
		public IActionResult OfferingOnlyReport()
		{
			return View();
		}

		[Route("missionaryMacroScheduleReport")]
		[CheckAccess]
		public IActionResult MissionaryMacroScheduleReport()
		{
			return View();
		}

		[Route("pastorAppoinmentReport")]
		[CheckAccess]
		public IActionResult PastorAppoinmentReport()
		{
			return View();
		}

		[Route("accomodationBookingReport")]
		[CheckAccess]
		public IActionResult AccomodationBookingReport()
		{
			return View();
		}

		[HttpPost]
		[Route("getUserActivityData")]
		public async Task<IActionResult> GetUserActivityData([FromBody] ReportParams reportParams)
		{
			TimeZoneInfo local = TimeZoneInfo.Local;
			TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(local.Id);
			reportParams.FromDate = TimeZoneInfo.ConvertTimeFromUtc(reportParams.FromDate.Value, destinationTimeZone);
			reportParams.ToDate = TimeZoneInfo.ConvertTimeFromUtc(reportParams.ToDate.Value, destinationTimeZone);
			return new JsonResult(await _reportService.GetUserActivityReport(reportParams));
		}

		[HttpPost]
		[Route("getChurchServiceCountReportData")]
		public async Task<IActionResult> GetChurchServiceCountReportData([FromBody] ChurchServiceReportParam reportParams)
		{
			return new JsonResult(await _reportService.ChurchServiceCountReport(reportParams));
		}

		[HttpPost]
		[Route("getPastorContactData")]
		public async Task<IActionResult> GetPastorContactData([FromBody] PastorReportParam reportParams)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			return new JsonResult(await _reportService.GetPastorContactData(reportParams, user.DataAccessCode, user.Id));
		}

		[HttpPost]
		[Route("getMacroscheduleWiseAppoinmentData")]
		public async Task<IActionResult> GetMacroscheduleWiseAppoinmentData([FromBody] MacroScheduleReportParams reportParams)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			return new JsonResult(await _reportService.GetMacroscheduleWiseAppoinmentData(reportParams, user.DataAccessCode, user.Id));
		}

		[HttpPost]
		[Route("getChurchWiseAppoinmentData")]
		public async Task<IActionResult> GetChurchWiseAppoinmentData([FromBody] ChurchServiceReportParam reportParams)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			return new JsonResult(await _reportService.GetChurchWiseAppoinmentData(reportParams, user.DataAccessCode, user.Id));
		}

		[HttpPost]
		[Route("getOfferingOnlyReportData")]
		public async Task<IActionResult> GetOfferingOnlyReportData([FromBody] ChurchServiceReportParam reportParams)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			return new JsonResult(await _reportService.GetOfferingOnlyReportData(reportParams, user.DataAccessCode, user.Id));
		}

		[HttpPost]
		[Route("getMissionaryScheduleData")]
		public async Task<IActionResult> GetMissionaryScheduleData([FromBody] MacroScheduleReportParams reportParams)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			return new JsonResult(await _reportService.GetMissionaryScheduleData(reportParams, user.DataAccessCode, user.Id));
		}

		[HttpPost]
		[Route("getPastorAppoinmentData")]
		public async Task<IActionResult> GetPastorAppoinmentData([FromBody] MacroScheduleReportParams reportParams)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			return new JsonResult(await _reportService.GetPastorAppoinmentData(reportParams, user.DataAccessCode, user.Id));
		}

		[HttpPost]
		[Route("getAccomodationBookingReportData")]
		public async Task<IActionResult> GetAccomodationBookingReportData([FromBody] ReportParams reportParams)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			TimeZoneInfo local = TimeZoneInfo.Local;
			TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(local.Id);
			if (reportParams.FromDate.HasValue)
			{
				reportParams.FromDate = TimeZoneInfo.ConvertTimeFromUtc(reportParams.FromDate.Value, destinationTimeZone);
			}
			if (reportParams.ToDate.HasValue)
			{
				reportParams.ToDate = TimeZoneInfo.ConvertTimeFromUtc(reportParams.ToDate.Value, destinationTimeZone);
			}
			return new JsonResult(await _reportService.GetAccomodationBookingReportData(reportParams, user.DataAccessCode, user.Id));
		}

		[HttpPost]
		[Route("userActivityReport")]
		public async Task<IActionResult> GenerateUserActivityReport([FromBody] List<UserReport> userActivityList)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			string path = "User Activity Report.xlsx";
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
				ExcelWorksheet val2 = val.Workbook.Worksheets.Add("User Activity Report");
				val2.Cells[1, 1, 1, 8].Merge = true;
				val2.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[1, 1, 1, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[2, 1, 2, 8].Merge = (true);
				val2.Cells[2, 1, 2, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[2, 1, 2, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[1, 1].Style.Font.Size = (15f);
				val2.Cells[1, 1].Style.Font.Bold = (true);
				val2.Cells[1, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.Bold = (true);
				val2.Cells[2, 1].Style.Font.Size = (12f);
				val2.Cells[1, 1].Value = ((object)"UPCI GLOBAL MISSIONS DEPUTATION MANAGEMENT SYSTEM.");
				val2.Cells[2, 1].Value = ((object)"User Activity Report");
				val2.Row(3).Style.Font
					.Bold = (true);
				val2.Row(4).Style.Font
					.Bold = (true);
				int num = 3;
				val2.Cells[num, 3].Value = ((object)"Report Generation Date & Time :");
				val2.Cells[num, 4].Value = ((object)DateTime.Now.ToString("dd-MM-yyyy"));
				val2.Cells[num, 5].Value = ((object)"Report Generated By :");
				val2.Cells[num, 6].Value = ((object)user.Name);
				num = 4;
				int num2 = 0;
				val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 1].Value = ((object)"SL NO");
				val2.Cells[num, 2].Value = ((object)"USER NAME");
				val2.Cells[num, 3].Value = ((object)"ROLE");
				val2.Cells[num, 4].Value = ((object)"EMAIL");
				val2.Cells[num, 5].Value = ((object)"PAGE");
				val2.Cells[num, 6].Value = ((object)"DESCRIPTION");
				val2.Cells[num, 7].Value = ((object)"ACTION");
				val2.Cells[num, 8].Value = ((object)"ACTION TIME");
				if (userActivityList != null)
				{
					if (userActivityList.Count > 0)
					{
						foreach (UserReport userActivity in userActivityList)
						{
							num++;
							num2++;
							val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 1].Value = ((object)num2);
							val2.Cells[num, 2].Value = ((object)userActivity.Name);
							val2.Cells[num, 3].Value = ((object)userActivity.Role);
							val2.Cells[num, 4].Value = ((object)userActivity.Email);
							val2.Cells[num, 5].Value = ((object)userActivity.Page);
							val2.Cells[num, 6].Value = ((object)userActivity.Description);
							val2.Cells[num, 7].Value = ((object)userActivity.Action);
							val2.Cells[num, 8].Value = ((object)((!userActivity.ActionTime.HasValue) ? "-" : userActivity.ActionTime.Value.ToString("dd-MM-yyyy HH:mm:ss")));
						}
					}
					else
					{
						val2.Cells[5, 4].Value = ((object)"No Data Found");
					}
				}
				else
				{
					val2.Cells[5, 4].Value = ((object)"No Data Found");
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

		[HttpPost]
		[Route("generateChurchServiceCountReport")]
		public async Task<IActionResult> GenerateChurchServiceCountReport([FromBody] List<ChurchServiceReport> churchServiceReports)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			string path = "Church Service Count Report.xlsx";
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
				ExcelWorksheet val2 = val.Workbook.Worksheets.Add(" Church Service Count Report");
				val2.Cells[1, 1, 1, 4].Merge = (true);
				val2.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[1, 1, 1, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[2, 1, 2, 4].Merge = (true);
				val2.Cells[2, 1, 2, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[2, 1, 2, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[1, 1].Style.Font.Size = (15f);
				val2.Cells[1, 1].Style.Font.Bold = (true);
				val2.Cells[1, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.Bold = (true);
				val2.Cells[2, 1].Style.Font.Size = (12f);
				val2.Cells[1, 1].Value = ((object)"UPCI GLOBAL MISSIONS DEPUTATION MANAGEMENT SYSTEM.");
				val2.Cells[2, 1].Value = ((object)"Church Service Count Report");
				val2.Row(3).Style.Font
					.Bold = (true);
				val2.Row(4).Style.Font
					.Bold = (true);
				int num = 3;
				val2.Cells[num, 1].Value = ((object)"Report Generation Date & Time :");
				val2.Cells[num, 2].Value = ((object)DateTime.Now.ToString("dd-MM-yyyy"));
				val2.Cells[num, 3].Value = ((object)"Report Generated By :");
				val2.Cells[num, 4].Value = ((object)user.Name);
				num = 4;
				int num2 = 0;
				val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 1].Value = ((object)"Sl No");
				val2.Cells[num, 2].Value = ((object)"Church Name");
				val2.Cells[num, 3].Value = ((object)"Total_CountofServiceDateTime");
				val2.Cells[num, 4].Value = ((object)"Total_CountofServiceType");
				if (churchServiceReports != null)
				{
					if (churchServiceReports.Count > 0)
					{
						foreach (ChurchServiceReport churchServiceReport in churchServiceReports)
						{
							num++;
							num2++;
							val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 1].Value = ((object)num2);
							val2.Cells[num, 2].Value = ((object)churchServiceReport.ChurchName);
							val2.Cells[num, 3].Value = ((object)churchServiceReport.ServiceTimeCount);
							val2.Cells[num, 4].Value = ((object)churchServiceReport.ServiceTypeCount);
						}
					}
					else
					{
						val2.Cells[5, 2].Value = ((object)"No Data Found");
					}
				}
				else
				{
					val2.Cells[5, 2].Value = ((object)"No Data Found");
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

		[HttpPost]
		[Route("generatePastorContactReport")]
		public async Task<IActionResult> GeneratePastorContactReport([FromBody] List<PastorContactReport> pastorContactList)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			string path = "Pastor Contact List Report.xlsx";
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
				ExcelWorksheet val2 = val.Workbook.Worksheets.Add("Pastor Contact List Report");
				val2.Cells[1, 1, 1, 9].Merge = (true);
				val2.Cells[1, 1, 1, 9].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[1, 1, 1, 9].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[2, 1, 2, 9].Merge = (true);
				val2.Cells[2, 1, 2, 9].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[2, 1, 2, 9].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[1, 1].Style.Font.Size = (15f);
				val2.Cells[1, 1].Style.Font.Bold = (true);
				val2.Cells[1, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.Bold = (true);
				val2.Cells[2, 1].Style.Font.Size = (12f);
				val2.Cells[1, 1].Value = ((object)"UPCI GLOBAL MISSIONS DEPUTATION MANAGEMENT SYSTEM.");
				val2.Cells[2, 1].Value = ((object)"Pastor Contact List Report");
				val2.Row(3).Style.Font
					.Bold = (true);
				val2.Row(4).Style.Font
					.Bold = (true);
				int num = 3;
				val2.Cells[num, 3].Value = ((object)"Report Generation Date & Time :");
				val2.Cells[num, 4].Value = ((object)DateTime.Now.ToString("dd-MM-yyyy"));
				val2.Cells[num, 6].Value = ((object)"Report Generated By :");
				val2.Cells[num, 7].Value = ((object)user.Name);
				num = 4;
				int num2 = 0;
				val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 9, num, 9].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 9, num, 9].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 1].Value = ((object)"SL NO");
				val2.Cells[num, 2].Value = ((object)"NAME");
				val2.Cells[num, 3].Value = ((object)"EMAIL");
				val2.Cells[num, 4].Value = ((object)"PHONE NO");
				val2.Cells[num, 5].Value = ((object)"ADDRESS");
				val2.Cells[num, 6].Value = ((object)"CITY");
				val2.Cells[num, 7].Value = ((object)"STATE");
				val2.Cells[num, 8].Value = ((object)"POSTAL CODE");
				val2.Cells[num, 9].Value = ((object)"CHURCH WEBSITE");
				if (pastorContactList != null)
				{
					if (pastorContactList.Count > 0)
					{
						foreach (PastorContactReport pastorContact in pastorContactList)
						{
							num++;
							num2++;
							val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 9, num, 9].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 9, num, 9].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 1].Value = ((object)num2);
							val2.Cells[num, 2].Value = ((object)pastorContact.Name);
							val2.Cells[num, 3].Value = ((object)pastorContact.Email);
							val2.Cells[num, 4].Value = ((object)pastorContact.Phone);
							val2.Cells[num, 5].Value = ((object)pastorContact.Address);
							val2.Cells[num, 6].Value = ((object)pastorContact.City);
							val2.Cells[num, 7].Value = ((object)pastorContact.State);
							val2.Cells[num, 8].Value = ((object)pastorContact.Zipcode);
							val2.Cells[num, 9].Value = ((object)pastorContact.ChurchWebsite);
						}
					}
					else
					{
						val2.Cells[5, 5].Value = ((object)"No Data Found");
					}
				}
				else
				{
					val2.Cells[5, 5].Value = ((object)"No Data Found");
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

		[HttpPost]
		[Route("generateMacroscheduleWiseAppoinmentReport")]
		public async Task<IActionResult> GenerateMacroscheduleWiseAppoinmentReport([FromBody] List<MacroscheduleWiseAppoinmentReport> macroschedules)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			string path = "Macroschedule Wise Appoinment Report.xlsx";
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
				ExcelWorksheet val2 = val.Workbook.Worksheets.Add("Macroschedule Wise Appoinment Report");
				val2.Cells[1, 1, 1, 7].Merge = (true);
				val2.Cells[1, 1, 1, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[1, 1, 1, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[2, 1, 2, 7].Merge = (true);
				val2.Cells[2, 1, 2, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[2, 1, 2, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[1, 1].Style.Font.Size = (15f);
				val2.Cells[1, 1].Style.Font.Bold = (true);
				val2.Cells[1, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.Bold = (true);
				val2.Cells[2, 1].Style.Font.Size = (12f);
				val2.Cells[1, 1].Value = ((object)"UPCI GLOBAL MISSIONS DEPUTATION MANAGEMENT SYSTEM.");
				val2.Cells[2, 1].Value = ((object)"Macroschedule Wise Appoinment Report");
				val2.Row(3).Style.Font
					.Bold = (true);
				val2.Row(4).Style.Font
					.Bold = (true);
				int num = 3;
				val2.Cells[num, 2].Value = ((object)"Report Generation Date & Time :");
				val2.Cells[num, 3].Value = ((object)DateTime.Now.ToString("dd-MM-yyyy"));
				val2.Cells[num, 5].Value = ((object)"Report Generated By :");
				val2.Cells[num, 6].Value = ((object)user.Name);
				num = 4;
				int num2 = 0;
				val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 1].Value = ((object)"SL NO");
				val2.Cells[num, 2].Value = ((object)"MacroSchedule Description");
				val2.Cells[num, 3].Value = ((object)"Church Name");
				val2.Cells[num, 4].Value = ((object)"Appoinment Date");
				val2.Cells[num, 5].Value = ((object)"Appoinment Time");
				val2.Cells[num, 6].Value = ((object)"Service Type");
				val2.Cells[num, 7].Value = ((object)"Pastor Name");
				if (macroschedules != null)
				{
					if (macroschedules.Count > 0)
					{
						foreach (MacroscheduleWiseAppoinmentReport macroschedule in macroschedules)
						{
							num++;
							num2++;
							val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							if (macroschedule.AppoinmentDetails.Count > 1)
							{
								val2.Cells[num, 1, num + (macroschedule.AppoinmentDetails.Count - 1), 1].Merge = (true);
								val2.Cells[num, 2, num + (macroschedule.AppoinmentDetails.Count - 1), 2].Merge = (true);
							}
							val2.Cells[num, 1].Value = ((object)num2);
							val2.Cells[num, 2].Value = ((object)macroschedule.Description);
							foreach (AppoinmentDetails appoinmentDetail in macroschedule.AppoinmentDetails)
							{
								val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 3].Value = ((object)appoinmentDetail.ChurchName);
								val2.Cells[num, 4].Value = ((object)appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"));
								val2.Cells[num, 5].Value = ((object)appoinmentDetail.Time);
								val2.Cells[num, 6].Value = ((object)appoinmentDetail.ServiceType);
								val2.Cells[num, 7].Value = ((object)appoinmentDetail.PastorName);
								if (macroschedule.AppoinmentDetails.Count > 1)
								{
									num++;
								}
							}
							if (macroschedule.AppoinmentDetails.Count > 1)
							{
								num--;
							}
						}
					}
					else
					{
						val2.Cells[5, 4].Value = ((object)"No Data Found");
					}
				}
				else
				{
					val2.Cells[5, 4].Value = ((object)"No Data Found");
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

		[HttpPost]
		[Route("generateChurchWiseAppoinmentReport")]
		public async Task<IActionResult> GenerateChurchWiseAppoinmentReport([FromBody] List<ChurchWiseAppoinmentReport> churches)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			string path = "Church Wise Appoinment Report.xlsx";
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
				ExcelWorksheet val2 = val.Workbook.Worksheets.Add("Church Wise Appoinment Report");
				val2.Cells[1, 1, 1, 8].Merge = (true);
				val2.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[1, 1, 1, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[2, 1, 2, 8].Merge = (true);
				val2.Cells[2, 1, 2, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[2, 1, 2, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[1, 1].Style.Font.Size = (15f);
				val2.Cells[1, 1].Style.Font.Bold = (true);
				val2.Cells[1, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.Bold = (true);
				val2.Cells[2, 1].Style.Font.Size = (12f);
				val2.Cells[1, 1].Value = ((object)"UPCI GLOBAL MISSIONS DEPUTATION MANAGEMENT SYSTEM.");
				val2.Cells[2, 1].Value = ((object)"Church Wise Appoinment Report");
				val2.Row(3).Style.Font
					.Bold = (true);
				val2.Row(4).Style.Font
					.Bold = (true);
				int num = 3;
				val2.Cells[num, 3].Value = ((object)"Report Generation Date & Time :");
				val2.Cells[num, 4].Value = ((object)DateTime.Now.ToString("dd-MM-yyyy"));
				val2.Cells[num, 5].Value = ((object)"Report Generated By :");
				val2.Cells[num, 6].Value = ((object)user.Name);
				num = 4;
				int num2 = 0;
				val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 1].Value = ((object)"SL NO");
				val2.Cells[num, 2].Value = ((object)"Church Name");
				val2.Cells[num, 3].Value = ((object)"Total ServiceDateTime");
				val2.Cells[num, 4].Value = ((object)"Appoinment Date");
				val2.Cells[num, 5].Value = ((object)"Appoinment Time");
				val2.Cells[num, 6].Value = ((object)"Service Type");
				val2.Cells[num, 7].Value = ((object)"Missionary LastName");
				val2.Cells[num, 8].Value = ((object)"Missionary FirstName");
				if (churches != null)
				{
					if (churches.Count > 0)
					{
						foreach (ChurchWiseAppoinmentReport church in churches)
						{
							num++;
							num2++;
							val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							if (church.AppoinmentDetails.Count > 1)
							{
								val2.Cells[num, 1, num + (church.AppoinmentDetails.Count - 1), 1].Merge = (true);
								val2.Cells[num, 2, num + (church.AppoinmentDetails.Count - 1), 2].Merge = (true);
								val2.Cells[num, 3, num + (church.AppoinmentDetails.Count - 1), 3].Merge = (true);
							}
							val2.Cells[num, 1].Value = ((object)num2);
							val2.Cells[num, 2].Value = ((object)church.ChurchName);
							val2.Cells[num, 3].Value = ((object)church.TotalServiceTime);
							foreach (AppoinmentDetails appoinmentDetail in church.AppoinmentDetails)
							{
								val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 4].Value = ((object)appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"));
								val2.Cells[num, 5].Value = ((object)appoinmentDetail.Time);
								val2.Cells[num, 6].Value = ((object)appoinmentDetail.ServiceType);
								val2.Cells[num, 7].Value = ((object)appoinmentDetail.LastName);
								val2.Cells[num, 8].Value = ((object)appoinmentDetail.FirstName);
								if (church.AppoinmentDetails.Count > 1)
								{
									num++;
								}
							}
							if (church.AppoinmentDetails.Count > 1)
							{
								num--;
							}
						}
					}
					else
					{
						val2.Cells[5, 4].Value = ((object)"No Data Found");
					}
				}
				else
				{
					val2.Cells[5, 4].Value = ((object)"No Data Found");
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

		[HttpPost]
		[Route("generateOfferingOnlyReport")]
		public async Task<IActionResult> GenerateOfferingOnlyReport([FromBody] List<ChurchWiseAppoinmentReport> churches)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			string path = "Offering Only Report.xlsx";
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
				ExcelWorksheet val2 = val.Workbook.Worksheets.Add("Offering Only Report");
				val2.Cells[1, 1, 1, 8].Merge = (true);
				val2.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[1, 1, 1, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[2, 1, 2, 8].Merge = (true);
				val2.Cells[2, 1, 2, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[2, 1, 2, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[1, 1].Style.Font.Size = (15f);
				val2.Cells[1, 1].Style.Font.Bold = (true);
				val2.Cells[1, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.Bold = (true);
				val2.Cells[2, 1].Style.Font.Size = (12f);
				val2.Cells[1, 1].Value = ((object)"UPCI GLOBAL MISSIONS DEPUTATION MANAGEMENT SYSTEM.");
				val2.Cells[2, 1].Value = ((object)"Offering Only Report");
				val2.Row(3).Style.Font
					.Bold = (true);
				val2.Row(4).Style.Font
					.Bold = (true);
				int num = 3;
				val2.Cells[num, 3].Value = ((object)"Report Generation Date & Time :");
				val2.Cells[num, 4].Value = ((object)DateTime.Now.ToString("dd-MM-yyyy"));
				val2.Cells[num, 5].Value = ((object)"Report Generated By :");
				val2.Cells[num, 6].Value = ((object)user.Name);
				num = 4;
				int num2 = 0;
				val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 1].Value = ((object)"SL NO");
				val2.Cells[num, 2].Value = ((object)"Church Name");
				val2.Cells[num, 3].Value = ((object)"Service Date");
				val2.Cells[num, 4].Value = ((object)"Service Type");
				val2.Cells[num, 5].Value = ((object)"Offering");
				val2.Cells[num, 6].Value = ((object)"Offering Amount");
				val2.Cells[num, 7].Value = ((object)"Missionary LastName");
				val2.Cells[num, 8].Value = ((object)"Missionary FirstName");
				if (churches != null)
				{
					if (churches.Count > 0)
					{
						foreach (ChurchWiseAppoinmentReport church in churches)
						{
							num++;
							num2++;
							val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							if (church.AppoinmentDetails.Count > 1)
							{
								val2.Cells[num, 1, num + (church.AppoinmentDetails.Count - 1), 1].Merge = (true);
								val2.Cells[num, 2, num + (church.AppoinmentDetails.Count - 1), 2].Merge = (true);
							}
							val2.Cells[num, 1].Value = ((object)num2);
							val2.Cells[num, 2].Value = ((object)church.ChurchName);
							foreach (AppoinmentDetails appoinmentDetail in church.AppoinmentDetails)
							{
								val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 3].Value = ((object)appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"));
								val2.Cells[num, 4].Value = ((object)appoinmentDetail.ServiceType);
								val2.Cells[num, 5].Value = ((object)appoinmentDetail.Offer);
								val2.Cells[num, 6].Value = ((object)appoinmentDetail.OfferingAmount);
								val2.Cells[num, 7].Value = ((object)appoinmentDetail.LastName);
								val2.Cells[num, 8].Value = ((object)appoinmentDetail.FirstName);
								if (church.AppoinmentDetails.Count > 1)
								{
									num++;
								}
							}
							if (church.AppoinmentDetails.Count > 1)
							{
								num--;
							}
						}
					}
					else
					{
						val2.Cells[5, 4].Value = ((object)"No Data Found");
					}
				}
				else
				{
					val2.Cells[5, 4].Value = ((object)"No Data Found");
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

		[HttpPost]
		[Route("generateMissionaryScheduleReport")]
		public async Task<IActionResult> GenerateMissionaryScheduleReport([FromBody] List<MissionaryWiseSchedule> missionaries)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			string path = "Macroschedule Wise Appoinment Report.xlsx";
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
				ExcelWorksheet val2 = val.Workbook.Worksheets.Add("Macroschedule Wise Appoinment Report");
				val2.Cells[1, 1, 1, 8].Merge = (true);
				val2.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[1, 1, 1, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[2, 1, 2, 8].Merge = (true);
				val2.Cells[2, 1, 2, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[2, 1, 2, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[1, 1].Style.Font.Size = (15f);
				val2.Cells[1, 1].Style.Font.Bold = (true);
				val2.Cells[1, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.Bold = (true);
				val2.Cells[2, 1].Style.Font.Size = (12f);
				val2.Cells[1, 1].Value = ((object)"UPCI GLOBAL MISSIONS DEPUTATION MANAGEMENT SYSTEM.");
				val2.Cells[2, 1].Value = ((object)"Macroschedule Wise Appoinment Report");
				val2.Row(3).Style.Font
					.Bold = (true);
				val2.Row(4).Style.Font
					.Bold = (true);
				int num = 3;
				val2.Cells[num, 3].Value = ((object)"Report Generation Date & Time :");
				val2.Cells[num, 4].Value = ((object)DateTime.Now.ToString("dd-MM-yyyy"));
				val2.Cells[num, 5].Value = ((object)"Report Generated By :");
				val2.Cells[num, 6].Value = ((object)user.Name);
				num = 4;
				int num2 = 0;
				val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 1].Value = ((object)"SL NO");
				val2.Cells[num, 2].Value = ((object)"Missionary Name");
				val2.Cells[num, 3].Value = ((object)"MacroSchedule Description");
				val2.Cells[num, 4].Value = ((object)"Church Name");
				val2.Cells[num, 5].Value = ((object)"Appoinment Date");
				val2.Cells[num, 6].Value = ((object)"Appoinment Time");
				val2.Cells[num, 7].Value = ((object)"Service Type");
				val2.Cells[num, 8].Value = ((object)"Pastor Name");
				if (missionaries != null)
				{
					if (missionaries.Count > 0)
					{
						foreach (MissionaryWiseSchedule missionary in missionaries)
						{
							num++;
							num2++;
							val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							if (missionary.MacroSchedules.Select((MacroscheduleWiseAppoinmentReport n) => n.AppoinmentDetails.Count()).Count() > 1)
							{
								val2.Cells[num, 1, num + (missionary.MacroSchedules.SelectMany((MacroscheduleWiseAppoinmentReport n) => n.AppoinmentDetails).Count() - 1), 1].Merge = (true);
								val2.Cells[num, 2, num + (missionary.MacroSchedules.SelectMany((MacroscheduleWiseAppoinmentReport n) => n.AppoinmentDetails).Count() - 1), 2].Merge = (true);
							}
							val2.Cells[num, 1].Value = ((object)num2);
							val2.Cells[num, 2].Value = ((object)missionary.MissionaryName);
							foreach (MacroscheduleWiseAppoinmentReport macroSchedule in missionary.MacroSchedules)
							{
								val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								if (macroSchedule.AppoinmentDetails.Count > 1)
								{
									val2.Cells[num, 3, num + (macroSchedule.AppoinmentDetails.Count - 1), 3].Merge = (true);
								}
								val2.Cells[num, 3].Value = ((object)macroSchedule.Description);
								foreach (AppoinmentDetails appoinmentDetail in macroSchedule.AppoinmentDetails)
								{
									val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
									val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
									val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
									val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
									val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
									val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
									val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
									val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
									val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
									val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
									val2.Cells[num, 4].Value = ((object)appoinmentDetail.ChurchName);
									val2.Cells[num, 5].Value = ((object)appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"));
									val2.Cells[num, 6].Value = ((object)appoinmentDetail.Time);
									val2.Cells[num, 7].Value = ((object)appoinmentDetail.ServiceType);
									val2.Cells[num, 8].Value = ((object)appoinmentDetail.PastorName);
									if (macroSchedule.AppoinmentDetails.Count > 1)
									{
										num++;
									}
								}
								if (macroSchedule.AppoinmentDetails.Count > 1)
								{
									num--;
								}
								num++;
							}
							if (missionary.MacroSchedules.Select((MacroscheduleWiseAppoinmentReport n) => n.AppoinmentDetails.Count()).Count() > 1)
							{
								num--;
							}
						}
					}
					else
					{
						val2.Cells[5, 4].Value = ((object)"No Data Found");
					}
				}
				else
				{
					val2.Cells[5, 4].Value = ((object)"No Data Found");
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

		[HttpPost]
		[Route("generatePastorAppoinmentReport")]
		public async Task<IActionResult> GeneratePastorAppoinmentReport([FromBody] List<PastorAppoinmentReport> pastors)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			string path = "Pastor Appoinment Report.xlsx";
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
				ExcelWorksheet val2 = val.Workbook.Worksheets.Add("Pastor Appoinment Report");
				val2.Cells[1, 1, 1, 8].Merge = (true);
				val2.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[1, 1, 1, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[2, 1, 2, 8].Merge = (true);
				val2.Cells[2, 1, 2, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[2, 1, 2, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[1, 1].Style.Font.Size = (15f);
				val2.Cells[1, 1].Style.Font.Bold = (true);
				val2.Cells[1, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.Bold = (true);
				val2.Cells[2, 1].Style.Font.Size = (12f);
				val2.Cells[1, 1].Value = ((object)"UPCI GLOBAL MISSIONS DEPUTATION MANAGEMENT SYSTEM.");
				val2.Cells[2, 1].Value = ((object)"Pastor Appoinment Report");
				val2.Row(3).Style.Font
					.Bold = (true);
				val2.Row(4).Style.Font
					.Bold = (true);
				int num = 3;
				val2.Cells[num, 3].Value = ((object)"Report Generation Date & Time :");
				val2.Cells[num, 4].Value = ((object)DateTime.Now.ToString("dd-MM-yyyy"));
				val2.Cells[num, 5].Value = ((object)"Report Generated By :");
				val2.Cells[num, 6].Value = ((object)user.Name);
				num = 4;
				int num2 = 0;
				val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 1].Value = ((object)"SL NO");
				val2.Cells[num, 2].Value = ((object)"Pastor Name");
				val2.Cells[num, 3].Value = ((object)"Appoinment Date");
				val2.Cells[num, 4].Value = ((object)"Appoinment Time");
				val2.Cells[num, 5].Value = ((object)"Service Type");
				val2.Cells[num, 6].Value = ((object)"Missionary LastName");
				val2.Cells[num, 7].Value = ((object)"Missionary FirstName");
				val2.Cells[num, 8].Value = ((object)"Missionary Country");
				if (pastors != null)
				{
					if (pastors.Count > 0)
					{
						foreach (PastorAppoinmentReport pastor in pastors)
						{
							num++;
							num2++;
							val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							if (pastor.AppoinmentDetails.Count > 1)
							{
								val2.Cells[num, 1, num + (pastor.AppoinmentDetails.Count - 1), 1].Merge = (true);
								val2.Cells[num, 2, num + (pastor.AppoinmentDetails.Count - 1), 2].Merge = (true);
							}
							val2.Cells[num, 1].Value = ((object)num2);
							val2.Cells[num, 2].Value = ((object)pastor.PastorName);
							foreach (AppoinmentDetails appoinmentDetail in pastor.AppoinmentDetails)
							{
								val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
								val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
								val2.Cells[num, 3].Value = ((object)appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"));
								val2.Cells[num, 4].Value = ((object)appoinmentDetail.Time);
								val2.Cells[num, 5].Value = ((object)appoinmentDetail.ServiceType);
								val2.Cells[num, 6].Value = ((object)appoinmentDetail.LastName);
								val2.Cells[num, 7].Value = ((object)appoinmentDetail.FirstName);
								val2.Cells[num, 8].Value = ((object)appoinmentDetail.MissionaryCountry);
								if (pastor.AppoinmentDetails.Count > 1)
								{
									num++;
								}
							}
							if (pastor.AppoinmentDetails.Count > 1)
							{
								num--;
							}
						}
					}
					else
					{
						val2.Cells[5, 4].Value = ((object)"No Data Found");
					}
				}
				else
				{
					val2.Cells[5, 4].Value = ((object)"No Data Found");
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

		[HttpPost]
		[Route("generateAccomodationBookingReport")]
		public async Task<IActionResult> GenerateAccomodationBookingReport([FromBody] List<AccomodationBooking> accomodationBookings)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			string path = "Evangangelist Quarter Report.xlsx";
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
				ExcelWorksheet val2 = val.Workbook.Worksheets.Add("Evangangelist Quarter Report");
				val2.Cells[1, 1, 1, 10].Merge = (true);
				val2.Cells[1, 1, 1, 10].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[1, 1, 1, 10].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[2, 1, 2, 10].Merge = (true);
				val2.Cells[2, 1, 2, 10].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[2, 1, 2, 10].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[1, 1].Style.Font.Size = (15f);
				val2.Cells[1, 1].Style.Font.Bold = (true);
				val2.Cells[1, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.UnderLine = (true);
				val2.Cells[2, 1].Style.Font.Bold = (true);
				val2.Cells[2, 1].Style.Font.Size = (12f);
				val2.Cells[1, 1].Value = ((object)"UPCI GLOBAL MISSIONS DEPUTATION MANAGEMENT SYSTEM.");
				val2.Cells[2, 1].Value = ((object)"Evangangelist Quarter Report");
				val2.Row(3).Style.Font
					.Bold = (true);
				val2.Row(4).Style.Font
					.Bold = (true);
				int num = 3;
				val2.Cells[num, 3].Value = ((object)"Report Generation Date & Time :");
				val2.Cells[num, 4].Value = ((object)DateTime.Now.ToString("dd-MM-yyyy"));
				val2.Cells[num, 6].Value = ((object)"Report Generated By :");
				val2.Cells[num, 7].Value = ((object)user.Name);
				num = 4;
				int num2 = 0;
				val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 9, num, 9].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 9, num, 9].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 10, num, 10].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 10, num, 10].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 11, num, 11].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
				val2.Cells[num, 11, num, 11].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
				val2.Cells[num, 1].Value = ((object)"Sl No");
				val2.Cells[num, 2].Value = ((object)"District Name");
				val2.Cells[num, 3].Value = ((object)"Church Name");
				val2.Cells[num, 4].Value = ((object)"Accommodation Name");
				val2.Cells[num, 5].Value = ((object)"Missionary Last Name");
				val2.Cells[num, 6].Value = ((object)"Missionary First Name");
				val2.Cells[num, 7].Value = ((object)"Adult No");
				val2.Cells[num, 8].Value = ((object)"Child No");
				val2.Cells[num, 9].Value = ((object)"CheckIn Date");
				val2.Cells[num, 10].Value = ((object)"CheckOut Date");
				val2.Cells[num, 11].Value = ((object)"Accommodation Reservation Feedback");
				if (accomodationBookings != null)
				{
					if (accomodationBookings.Count > 0)
					{
						foreach (AccomodationBooking accomodationBooking in accomodationBookings)
						{
							num++;
							num2++;
							val2.Cells[num, 1, num, 1].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 1, num, 1].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 2, num, 2].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 2, num, 2].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 3, num, 3].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 3, num, 3].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 4, num, 4].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 4, num, 4].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 5, num, 5].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 5, num, 5].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 6, num, 6].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 6, num, 6].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 7, num, 7].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 7, num, 7].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 8, num, 8].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 8, num, 8].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 9, num, 9].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 9, num, 9].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 10, num, 10].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 10, num, 10].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 11, num, 11].Style.HorizontalAlignment = ((ExcelHorizontalAlignment)2);
							val2.Cells[num, 11, num, 11].Style.VerticalAlignment = ((ExcelVerticalAlignment)1);
							val2.Cells[num, 1].Value = ((object)num2);
							val2.Cells[num, 2].Value = ((object)accomodationBooking.DistrictName);
							val2.Cells[num, 3].Value = ((object)accomodationBooking.ChurchName);
							val2.Cells[num, 4].Value = ((object)accomodationBooking.AccomodationDesc);
							val2.Cells[num, 5].Value = ((object)accomodationBooking.LastName);
							val2.Cells[num, 6].Value = ((object)accomodationBooking.FirstName);
							val2.Cells[num, 7].Value = ((object)accomodationBooking.AdultNo);
							val2.Cells[num, 8].Value = ((object)accomodationBooking.ChildNo);
							val2.Cells[num, 9].Value = ((object)accomodationBooking.CheckinDate.ToString("dd-MM-yyyy"));
							val2.Cells[num, 10].Value = ((object)accomodationBooking.CheckoutDate.ToString("dd-MM-yyyy"));
							val2.Cells[num, 11].Value = ((object)accomodationBooking.FeedBack);
						}
					}
					else
					{
						val2.Cells[5, 5].Value = ((object)"No Data Found");
					}
				}
				else
				{
					val2.Cells[5, 5].Value = ((object)"No Data Found");
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
}