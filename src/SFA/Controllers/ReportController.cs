

// SFA.Controllers.ReportController
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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

		private readonly IWebHostEnvironment _environment;

		public ReportController(IReportService reportService, IWebHostEnvironment environment)
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
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("userActivityReportWord")]
		public async Task<IActionResult> userActivityReportWord([FromBody] List<UserReport> products)
		{
			string fileName = "";
			string filePath = "";
			if (products.Count > 0)
			{
				StringBuilder sbDocumentBody = new StringBuilder();

				sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
				if (products.Count > 0)
				{
					sbDocumentBody.Append("<tr><td>");
					sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

					// Add Column Headers dynamically from datatable  
					sbDocumentBody.Append("<tr>");

					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "User Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Role" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Email" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Page" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Description" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Action" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Action Time" + "</td>");


					sbDocumentBody.Append("</tr>");

					// Add Data Rows dynamically from datatable  
					foreach (UserReport data in products)
					{
						sbDocumentBody.Append("<tr>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Name + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Role + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Email + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Page + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Description + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Action + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.ActionTime.ToString() + "</td>");
						sbDocumentBody.Append("</tr>");
					}
					sbDocumentBody.Append("</table>");
					sbDocumentBody.Append("</td></tr></table>");
				}
				byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sbDocumentBody.ToString());
				fileName = "User Activity Report.doc";
				filePath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Response.Clear();
				//Response.Headers.Add("Content-Type", "application/msword");
				//Response.Headers.Add("Content-disposition", "attachment; filename=ProductDetails.doc");				
				System.IO.File.WriteAllBytes(filePath, byteArray); // Same contents you will get in byte[] and that will be save here 
				//await Response.Body.WriteAsync(byteArray);
				//await Response.Body.FlushAsync();
			}
			fileName = "User Activity Report.doc";
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filePath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("userActivityReportPdf")]
		public async Task<IActionResult> userActivityReportPdf([FromBody] List<UserReport> products)
		{
			string fileName = "";
			string filepath = "";
			if (products.Count > 0)
			{
				int pdfRowIndex = 1;
				fileName = "User Activity Report.pdf";
				filepath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
				FileStream fs = new FileStream(filepath, FileMode.Create);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);
				
				document.Open();

				Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
				Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

				float[] columnDefinitionSize = { 2F, 2F, 5F, 2F, 2F, 5F, 5F };
				PdfPTable table;
				PdfPCell cell;

				table = new PdfPTable(columnDefinitionSize)
				{
					WidthPercentage = 100
					
				};

				cell = new PdfPCell
				{
					BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
				};

				table.AddCell(new Phrase("User Name", font1));
				table.AddCell(new Phrase("Role", font1));
				table.AddCell(new Phrase("Email", font1));
				table.AddCell(new Phrase("Page", font1));
				table.AddCell(new Phrase("Description", font1));
				table.AddCell(new Phrase("Action", font1));
				table.AddCell(new Phrase("Action Time", font1));
				table.HeaderRows = 1;

				foreach (UserReport data in products)
				{
					table.AddCell(new Phrase(data.Name, font2));
					table.AddCell(new Phrase(data.Role, font2));
					table.AddCell(new Phrase(data.Email, font2));
					table.AddCell(new Phrase(data.Page, font2));
					table.AddCell(new Phrase(data.Description, font2));
					table.AddCell(new Phrase(data.Action, font2));
					table.AddCell(new Phrase(data.ActionTime.ToString(), font2));

					pdfRowIndex++;
				}

				document.Add(table);
				document.Close();
				document.CloseDocument();
				document.Dispose();
				writer.Close();
				writer.Dispose();
				fs.Close();
				fs.Dispose();

				FileStream sourceFile = new FileStream(filepath, FileMode.Open);
				float fileSize = 0;
				fileSize = sourceFile.Length;
				byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
				sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));
				
				Response.Clear();
				//Response.Headers.Clear();
				//Response.ContentType = "application/pdf";
				//Response.Headers.Add("Content-Length", getContent.Length.ToString());
				//Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName + ";");
    //             await Response.Body.WriteAsync( getContent);
				//Response.Body.Flush();
				sourceFile.Close();
				
				
				

			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filepath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}

		[HttpPost]
		[Route("generateChurchServiceCountReportWord")]
		public async Task<IActionResult> generateChurchServiceCountReportWord([FromBody] List<ChurchServiceReport> products)
		{
			string fileName = "Church Service Count Report.doc";
			string filePath = "";
			if (products.Count > 0)
			{
				StringBuilder sbDocumentBody = new StringBuilder();

				sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
				if (products.Count > 0)
				{
					sbDocumentBody.Append("<tr><td>");
					sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

					// Add Column Headers dynamically from datatable  
					sbDocumentBody.Append("<tr>");

					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Church Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Total Count Of Service" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Total Count Of Service Type" + "</td>");



					sbDocumentBody.Append("</tr>");

					// Add Data Rows dynamically from datatable  
					foreach (ChurchServiceReport data in products)
					{
						sbDocumentBody.Append("<tr>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.ChurchName + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.ServiceTimeCount.ToString() + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.ServiceTypeCount.ToString()+ "</td>");
						sbDocumentBody.Append("</tr>");
					}
					sbDocumentBody.Append("</table>");
					sbDocumentBody.Append("</td></tr></table>");
				}
				byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sbDocumentBody.ToString());
				filePath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Response.Clear();
				//Response.Headers.Add("Content-Type", "application/msword");
				//Response.Headers.Add("Content-disposition", "attachment; filename=ProductDetails.doc");				
				System.IO.File.WriteAllBytes(filePath, byteArray); // Same contents you will get in byte[] and that will be save here 
																   //await Response.Body.WriteAsync(byteArray);
																   //await Response.Body.FlushAsync();
			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filePath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}

		[HttpPost]
		[Route("generateChurchServiceCountReportPdf")]
		public async Task<IActionResult> generateChurchServiceCountReportPdf([FromBody] List<ChurchServiceReport> products)
		{
			string fileName = "";
			string filepath = "";
			if (products.Count > 0)
			{
				int pdfRowIndex = 1;
				fileName = "Church Service Count Report.pdf";
				filepath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
				FileStream fs = new FileStream(filepath, FileMode.Create);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);

				document.Open();

				Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
				Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

				float[] columnDefinitionSize = { 5F, 5F, 5F };
				PdfPTable table;
				PdfPCell cell;

				table = new PdfPTable(columnDefinitionSize)
				{
					WidthPercentage = 100

				};

				cell = new PdfPCell
				{
					BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
				};

				table.AddCell(new Phrase("Church Name", font1));
				table.AddCell(new Phrase("Total Count Of Service", font1));
				table.AddCell(new Phrase("Total Count Of Service Type", font1));

				table.HeaderRows = 1;

				foreach (ChurchServiceReport data in products)
				{
					table.AddCell(new Phrase(data.ChurchName, font2));
					table.AddCell(new Phrase(data.ServiceTimeCount.ToString(), font2));
					table.AddCell(new Phrase(data.ServiceTypeCount.ToString(), font2));


					pdfRowIndex++;
				}

				document.Add(table);
				document.Close();
				document.CloseDocument();
				document.Dispose();
				writer.Close();
				writer.Dispose();
				fs.Close();
				fs.Dispose();

				FileStream sourceFile = new FileStream(filepath, FileMode.Open);
				float fileSize = 0;
				fileSize = sourceFile.Length;
				byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
				sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));

				Response.Clear();
				//Response.Headers.Clear();
				//Response.ContentType = "application/pdf";
				//Response.Headers.Add("Content-Length", getContent.Length.ToString());
				//Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName + ";");
				//             await Response.Body.WriteAsync( getContent);
				//Response.Body.Flush();
				sourceFile.Close();




			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filepath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
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
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("generatePastorContactReportWord")]
		public async Task<IActionResult> GeneratePastorContactReportWord([FromBody] List<PastorContactReport> report)
		{
			string fileName = "Pastor Contact List Report.doc";
			string filePath = "";
			if (report.Count > 0)
			{
				StringBuilder sbDocumentBody = new StringBuilder();

				sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
				if (report.Count > 0)
				{
					sbDocumentBody.Append("<tr><td>");
					sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

					// Add Column Headers dynamically from datatable  
					sbDocumentBody.Append("<tr>");

					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Email" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Phone Number" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Address" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "City" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "State" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Postal Code" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Website" + "</td>");

					sbDocumentBody.Append("</tr>");

					// Add Data Rows dynamically from datatable  
					foreach (PastorContactReport data in report)
					{
						sbDocumentBody.Append("<tr>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Name + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Email + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Phone + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Address + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.City + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.State + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Zipcode + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.ChurchWebsite + "</td>");
						sbDocumentBody.Append("</tr>");
					}
					sbDocumentBody.Append("</table>");
					sbDocumentBody.Append("</td></tr></table>");
				}
				byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sbDocumentBody.ToString());
				filePath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Response.Clear();
				//Response.Headers.Add("Content-Type", "application/msword");
				//Response.Headers.Add("Content-disposition", "attachment; filename=ProductDetails.doc");				
				System.IO.File.WriteAllBytes(filePath, byteArray); // Same contents you will get in byte[] and that will be save here 
																   //await Response.Body.WriteAsync(byteArray);
																   //await Response.Body.FlushAsync();
			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filePath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}

		[HttpPost]
		[Route("generatePastorContactReportPdf")]
		public async Task<IActionResult> GeneratePastorContactReportPdf([FromBody] List<PastorContactReport> report)
		{
			string fileName = "";
			string filepath = "";
			if (report.Count > 0)
			{
				int pdfRowIndex = 1;
				fileName = "Pastor Contact List Report.pdf";
				filepath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
				FileStream fs = new FileStream(filepath, FileMode.Create);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);

				document.Open();

				Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
				Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

				float[] columnDefinitionSize = { 5F, 5F, 5F, 5F, 5F, 5F, 5F, 5F };
				PdfPTable table;
				PdfPCell cell;

				table = new PdfPTable(columnDefinitionSize)
				{
					WidthPercentage = 100

				};

				cell = new PdfPCell
				{
					BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
				};

				table.AddCell(new Phrase("Name", font1));
				table.AddCell(new Phrase("Email", font1));
				table.AddCell(new Phrase("Phone Number", font1));
				table.AddCell(new Phrase("Address", font1));
				table.AddCell(new Phrase("City", font1));
				table.AddCell(new Phrase("State", font1));
				table.AddCell(new Phrase("Postal Code", font1));
				table.AddCell(new Phrase("Website", font1));

				table.HeaderRows = 1;

				foreach (PastorContactReport data in report)
				{
					table.AddCell(new Phrase(data.Name, font2));
					table.AddCell(new Phrase(data.Email, font2));
					table.AddCell(new Phrase(data.Phone, font2));
					table.AddCell(new Phrase(data.Address, font2));
					table.AddCell(new Phrase(data.City, font2));
					table.AddCell(new Phrase(data.State, font2));
					table.AddCell(new Phrase(data.Zipcode, font2));
					table.AddCell(new Phrase(data.ChurchWebsite, font2));


					pdfRowIndex++;
				}

				document.Add(table);
				document.Close();
				document.CloseDocument();
				document.Dispose();
				writer.Close();
				writer.Dispose();
				fs.Close();
				fs.Dispose();

				FileStream sourceFile = new FileStream(filepath, FileMode.Open);
				float fileSize = 0;
				fileSize = sourceFile.Length;
				byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
				sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));

				Response.Clear();
				//Response.Headers.Clear();
				//Response.ContentType = "application/pdf";
				//Response.Headers.Add("Content-Length", getContent.Length.ToString());
				//Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName + ";");
				//             await Response.Body.WriteAsync( getContent);
				//Response.Body.Flush();
				sourceFile.Close();




			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filepath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
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
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("generateMacroscheduleWiseAppoinmentReportWord")]
		public async Task<IActionResult> GenerateMacroscheduleWiseAppoinmentReportWord([FromBody] List<MacroscheduleWiseAppoinmentReport> products)
		{
			string fileName = "Macroschedule Wise Appoinment Report.doc";
			string filePath = "";
			if (products.Count > 0)
			{
				StringBuilder sbDocumentBody = new StringBuilder();

				sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
				if (products.Count > 0)
				{
					sbDocumentBody.Append("<tr><td>");
					sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

					// Add Column Headers dynamically from datatable  
					sbDocumentBody.Append("<tr>");

					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "MacroSchedule Description" + " </td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Church Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Appoinment Date" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Appoinment Time" + " </td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Service Type" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Pastor Name" + "</td>");


					sbDocumentBody.Append("</tr>");

					// Add Data Rows dynamically from datatable  
					foreach (MacroscheduleWiseAppoinmentReport data in products)
					{


						sbDocumentBody.Append("<tr>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.Description + "</td>");

						foreach (AppoinmentDetails appoinmentDetail in data.AppoinmentDetails)
                        {
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy") + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.Time + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.ServiceType + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.PastorName + "</td>");
							sbDocumentBody.Append("</tr>");
						}

					}
					sbDocumentBody.Append("</table>");
					sbDocumentBody.Append("</td></tr></table>");
				}
				byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sbDocumentBody.ToString());
				filePath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Response.Clear();
				//Response.Headers.Add("Content-Type", "application/msword");
				//Response.Headers.Add("Content-disposition", "attachment; filename=ProductDetails.doc");				
				System.IO.File.WriteAllBytes(filePath, byteArray); // Same contents you will get in byte[] and that will be save here 
																   //await Response.Body.WriteAsync(byteArray);
																   //await Response.Body.FlushAsync();
			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filePath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("generateMacroscheduleWiseAppoinmentReportPdf")]
		public async Task<IActionResult> GenerateMacroscheduleWiseAppoinmentReportPdf([FromBody] List<MacroscheduleWiseAppoinmentReport> report)
		{
			string fileName = "";
			string filepath = "";
			if (report.Count > 0)
			{
				int pdfRowIndex = 1;
				fileName = "Macroschedule Wise Appoinment Report.pdf";
				filepath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
				FileStream fs = new FileStream(filepath, FileMode.Create);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);

				document.Open();

				Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
				Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

				float[] columnDefinitionSize = { 5F, 5F, 5F, 5F, 5F, 5F };
				PdfPTable table;
				PdfPCell cell;

				table = new PdfPTable(columnDefinitionSize)
				{
					WidthPercentage = 100

				};

				cell = new PdfPCell
				{
					BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
				};

				table.AddCell(new Phrase("MacroSchedule Description", font1));
				table.AddCell(new Phrase("Church Name", font1));
				table.AddCell(new Phrase("Appoinment Date", font1));
				table.AddCell(new Phrase("Appoinment Time", font1));
				table.AddCell(new Phrase("Service Type", font1));
				table.AddCell(new Phrase("Pastor Name", font1));


				table.HeaderRows = 1;

				foreach (MacroscheduleWiseAppoinmentReport data in report)
				{
					table.AddCell(new Phrase(data.Description, font2));

					foreach (AppoinmentDetails appoinmentDetail in data.AppoinmentDetails)
					{
						table.AddCell(new Phrase(appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"), font2));
						table.AddCell(new Phrase(appoinmentDetail.Time, font2));
						table.AddCell(new Phrase(appoinmentDetail.ServiceType, font2));
						table.AddCell(new Phrase(appoinmentDetail.PastorName, font2));
					}


					pdfRowIndex++;
				}

				document.Add(table);
				document.Close();
				document.CloseDocument();
				document.Dispose();
				writer.Close();
				writer.Dispose();
				fs.Close();
				fs.Dispose();

				FileStream sourceFile = new FileStream(filepath, FileMode.Open);
				float fileSize = 0;
				fileSize = sourceFile.Length;
				byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
				sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));

				Response.Clear();
				//Response.Headers.Clear();
				//Response.ContentType = "application/pdf";
				//Response.Headers.Add("Content-Length", getContent.Length.ToString());
				//Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName + ";");
				//             await Response.Body.WriteAsync( getContent);
				//Response.Body.Flush();
				sourceFile.Close();




			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filepath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
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
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("generateChurchWiseAppoinmentReportWord")]
		public async Task<IActionResult> GenerateChurchWiseAppoinmentReportWord([FromBody] List<ChurchWiseAppoinmentReport> products)
		{
			string fileName = "Church Wise Appoinment Report.doc";
			string filePath = "";
			if (products.Count > 0)
			{
				StringBuilder sbDocumentBody = new StringBuilder();

				sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
				if (products.Count > 0)
				{
					sbDocumentBody.Append("<tr><td>");
					sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

					// Add Column Headers dynamically from datatable  
					sbDocumentBody.Append("<tr>");

					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Church Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Total ServiceDateTime" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Appoinment Date" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Appoinment Time" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Service Type" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary LastName" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary FirstName" + "</td>");

					sbDocumentBody.Append("</tr>");

					// Add Data Rows dynamically from datatable  
					foreach (ChurchWiseAppoinmentReport data in products)
					{
						sbDocumentBody.Append("<tr>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.ChurchName + "</td>");
						foreach (AppoinmentDetails appoinmentDetail in data.AppoinmentDetails)
                        {
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy").ToString() + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.Time + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.ServiceType + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.LastName + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.FirstName + "</td>");
						}

						sbDocumentBody.Append("</tr>");
					}
					sbDocumentBody.Append("</table>");
					sbDocumentBody.Append("</td></tr></table>");
				}
				byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sbDocumentBody.ToString());
				filePath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Response.Clear();
				//Response.Headers.Add("Content-Type", "application/msword");
				//Response.Headers.Add("Content-disposition", "attachment; filename=ProductDetails.doc");				
				System.IO.File.WriteAllBytes(filePath, byteArray); // Same contents you will get in byte[] and that will be save here 
																   //await Response.Body.WriteAsync(byteArray);
																   //await Response.Body.FlushAsync();
			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filePath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}

		[HttpPost]
		[Route("generateChurchWiseAppoinmentReportPdf")]
		public async Task<IActionResult> GenerateChurchWiseAppoinmentReportPdf([FromBody] List<ChurchWiseAppoinmentReport> report)
		{
			string fileName = "";
			string filepath = "";
			if (report.Count > 0)
			{
				int pdfRowIndex = 1;
				fileName = "Church Wise Appoinment Report.pdf";
				filepath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
				FileStream fs = new FileStream(filepath, FileMode.Create);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);

				document.Open();

				Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
				Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

				float[] columnDefinitionSize = { 5F, 5F, 5F, 5F, 5F, 5F, 5F };
				PdfPTable table;
				PdfPCell cell;

				table = new PdfPTable(columnDefinitionSize)
				{
					WidthPercentage = 100

				};

				cell = new PdfPCell
				{
					BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
				};

				table.AddCell(new Phrase("Church Name", font1));
				table.AddCell(new Phrase("Total ServiceDateTime", font1));
				table.AddCell(new Phrase("Appoinment Date", font1));
				table.AddCell(new Phrase("Appoinment Time", font1));
				table.AddCell(new Phrase("Service Type", font1));
				table.AddCell(new Phrase("Missionary LastName", font1));
				table.AddCell(new Phrase("Missionary FirstName", font1));

				table.HeaderRows = 1;

				foreach (ChurchWiseAppoinmentReport data in report)
				{
					table.AddCell(new Phrase(data.ChurchName, font2));
					table.AddCell(new Phrase(data.TotalServiceTime.ToString(), font2));
					foreach (AppoinmentDetails appoinmentDetail in data.AppoinmentDetails)
                    {
						table.AddCell(new Phrase(appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"), font2));
						table.AddCell(new Phrase(appoinmentDetail.Time, font2));
						table.AddCell(new Phrase(appoinmentDetail.ServiceType, font2));
						table.AddCell(new Phrase(appoinmentDetail.LastName, font2));
						table.AddCell(new Phrase(appoinmentDetail.FirstName, font2));
					}

					pdfRowIndex++;
				}

				document.Add(table);
				document.Close();
				document.CloseDocument();
				document.Dispose();
				writer.Close();
				writer.Dispose();
				fs.Close();
				fs.Dispose();

				FileStream sourceFile = new FileStream(filepath, FileMode.Open);
				float fileSize = 0;
				fileSize = sourceFile.Length;
				byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
				sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));

				Response.Clear();
				//Response.Headers.Clear();
				//Response.ContentType = "application/pdf";
				//Response.Headers.Add("Content-Length", getContent.Length.ToString());
				//Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName + ";");
				//             await Response.Body.WriteAsync( getContent);
				//Response.Body.Flush();
				sourceFile.Close();




			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filepath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
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
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("generateOfferingOnlyReportWord")]
		public async Task<IActionResult> GenerateOfferingOnlyReportWord([FromBody] List<ChurchWiseAppoinmentReport> products)
		{
			string fileName = "Offering Only Report.doc";
			string filePath = "";
			if (products.Count > 0)
			{
				StringBuilder sbDocumentBody = new StringBuilder();

				sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
				if (products.Count > 0)
				{
					sbDocumentBody.Append("<tr><td>");
					sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

					// Add Column Headers dynamically from datatable  
					sbDocumentBody.Append("<tr>");

					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Church Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Service Date" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Service Type" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Offering" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Offering Amount" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary Last Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary First Name" + "</td>");


					sbDocumentBody.Append("</tr>");

					// Add Data Rows dynamically from datatable  
					foreach (ChurchWiseAppoinmentReport data in products)
					{
						sbDocumentBody.Append("<tr>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.ChurchName + "</td>");
						foreach (AppoinmentDetails appoinmentDetail in data.AppoinmentDetails)
                        {
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy") + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.ServiceType + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.Offer + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.OfferingAmount.ToString() + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.LastName + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.FirstName + "</td>");
						}

						sbDocumentBody.Append("</tr>");
					}
					sbDocumentBody.Append("</table>");
					sbDocumentBody.Append("</td></tr></table>");
				}
				byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sbDocumentBody.ToString());
				filePath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Response.Clear();
				//Response.Headers.Add("Content-Type", "application/msword");
				//Response.Headers.Add("Content-disposition", "attachment; filename=ProductDetails.doc");				
				System.IO.File.WriteAllBytes(filePath, byteArray); // Same contents you will get in byte[] and that will be save here 
																   //await Response.Body.WriteAsync(byteArray);
																   //await Response.Body.FlushAsync();
			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filePath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}

		[HttpPost]
		[Route("generateOfferingOnlyReportPdf")]
		public async Task<IActionResult> GenerateOfferingOnlyReportPdf([FromBody] List<ChurchWiseAppoinmentReport> report)
		{
			string fileName = "";
			string filepath = "";
			if (report.Count > 0)
			{
				int pdfRowIndex = 1;
				fileName = "Offering Only Report.pdf";
				filepath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
				FileStream fs = new FileStream(filepath, FileMode.Create);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);

				document.Open();

				Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
				Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

				float[] columnDefinitionSize = { 5F, 5F, 5F, 5F, 5F, 5F, 5F };
				PdfPTable table;
				PdfPCell cell;

				table = new PdfPTable(columnDefinitionSize)
				{
					WidthPercentage = 100

				};

				cell = new PdfPCell
				{
					BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
				};

				table.AddCell(new Phrase("Church Name", font1));
				table.AddCell(new Phrase("Service Date", font1));
				table.AddCell(new Phrase("Service Type", font1));
				table.AddCell(new Phrase("Offering", font1));
				table.AddCell(new Phrase("Offering Amount", font1));
				table.AddCell(new Phrase("Missionary Last Name", font1));
				table.AddCell(new Phrase("Missionary First Name", font1));

				table.HeaderRows = 1;

				foreach (ChurchWiseAppoinmentReport data in report)
				{
					table.AddCell(new Phrase(data.ChurchName, font2));
					foreach (AppoinmentDetails appoinmentDetail in data.AppoinmentDetails)
                    {
						table.AddCell(new Phrase(appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"), font2));
						table.AddCell(new Phrase(appoinmentDetail.ServiceType, font2));
						table.AddCell(new Phrase(appoinmentDetail.Offer, font2));
						table.AddCell(new Phrase(appoinmentDetail.OfferingAmount.ToString(), font2));
						table.AddCell(new Phrase(appoinmentDetail.LastName, font2));
						table.AddCell(new Phrase(appoinmentDetail.FirstName, font2));
					}



					pdfRowIndex++;
				}

				document.Add(table);
				document.Close();
				document.CloseDocument();
				document.Dispose();
				writer.Close();
				writer.Dispose();
				fs.Close();
				fs.Dispose();

				FileStream sourceFile = new FileStream(filepath, FileMode.Open);
				float fileSize = 0;
				fileSize = sourceFile.Length;
				byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
				sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));

				Response.Clear();
				//Response.Headers.Clear();
				//Response.ContentType = "application/pdf";
				//Response.Headers.Add("Content-Length", getContent.Length.ToString());
				//Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName + ";");
				//             await Response.Body.WriteAsync( getContent);
				//Response.Body.Flush();
				sourceFile.Close();




			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filepath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
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
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("generateMissionaryScheduleReportWord")]
		public async Task<IActionResult> GenerateMissionaryScheduleReportWord([FromBody] List<MissionaryWiseSchedule> products)
		{
			string fileName = "Macroschedule Wise Appoinment Report.doc";
			string filePath = "";
			if (products.Count > 0)
			{
				StringBuilder sbDocumentBody = new StringBuilder();

				sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
				if (products.Count > 0)
				{
					sbDocumentBody.Append("<tr><td>");
					sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

					// Add Column Headers dynamically from datatable  
					sbDocumentBody.Append("<tr>");

					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "MacroSchedule Description" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Church Name" + "</td>");


					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Appoinment Date" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Appoinment Time" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Service Type" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Pastor Name" + "</td>");


					sbDocumentBody.Append("</tr>");

					// Add Data Rows dynamically from datatable  
					foreach (MissionaryWiseSchedule data in products)
					{
						sbDocumentBody.Append("<tr>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.MissionaryName + "</td>");
						foreach (MacroscheduleWiseAppoinmentReport macroSchedule in data.MacroSchedules)
                        {
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + macroSchedule.Description + "</td>");
							foreach (AppoinmentDetails appoinmentDetail in macroSchedule.AppoinmentDetails)
                            {
								sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.ChurchName + "</td>");
								sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy") + "</td>");
								sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.Time + "</td>");
								sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.ServiceType + "</td>");
								sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.PastorName + "</td>");
							}
						}
						sbDocumentBody.Append("</tr>");
					}
					sbDocumentBody.Append("</table>");
					sbDocumentBody.Append("</td></tr></table>");
				}
				byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sbDocumentBody.ToString());
				filePath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Response.Clear();
				//Response.Headers.Add("Content-Type", "application/msword");
				//Response.Headers.Add("Content-disposition", "attachment; filename=ProductDetails.doc");				
				System.IO.File.WriteAllBytes(filePath, byteArray); // Same contents you will get in byte[] and that will be save here 
																   //await Response.Body.WriteAsync(byteArray);
																   //await Response.Body.FlushAsync();
			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filePath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}

		[HttpPost]
		[Route("generateMissionaryScheduleReportPdf")]
		public async Task<IActionResult> GenerateMissionaryScheduleReportPdf([FromBody] List<MissionaryWiseSchedule> report)
		{
			string fileName = "";
			string filepath = "";
			if (report.Count > 0)
			{
				int pdfRowIndex = 1;
				fileName = "Macroschedule Wise Appoinment Report.pdf";
				filepath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
				FileStream fs = new FileStream(filepath, FileMode.Create);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);

				document.Open();

				Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
				Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

				float[] columnDefinitionSize = { 5F, 5F, 5F, 5F, 5F, 5F, 5F };
				PdfPTable table;
				PdfPCell cell;

				table = new PdfPTable(columnDefinitionSize)
				{
					WidthPercentage = 100

				};

				cell = new PdfPCell
				{
					BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
				};

				table.AddCell(new Phrase("Missionary Name", font1));
				table.AddCell(new Phrase("MacroSchedule Description", font1));
				table.AddCell(new Phrase("Church Name", font1));
				table.AddCell(new Phrase("Appointment Date", font1));
				table.AddCell(new Phrase("Appointment Time", font1));
				table.AddCell(new Phrase("Service Type", font1));
				table.AddCell(new Phrase("Pastor Name", font1));

				table.HeaderRows = 1;

				foreach (MissionaryWiseSchedule data in report)
				{
					table.AddCell(new Phrase(data.MissionaryName, font2));

					foreach (MacroscheduleWiseAppoinmentReport macroSchedule in data.MacroSchedules)
                    {
						table.AddCell(new Phrase(macroSchedule.Description, font2));
						foreach (AppoinmentDetails appoinmentDetail in macroSchedule.AppoinmentDetails)
                        {
							table.AddCell(new Phrase(appoinmentDetail.ChurchName, font2));
							table.AddCell(new Phrase(appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"), font2));
							table.AddCell(new Phrase(appoinmentDetail.Time, font2));
							table.AddCell(new Phrase(appoinmentDetail.ServiceType, font2));
							table.AddCell(new Phrase(appoinmentDetail.PastorName, font2));	
						}
					}					

					pdfRowIndex++;
				}

				document.Add(table);
				document.Close();
				document.CloseDocument();
				document.Dispose();
				writer.Close();
				writer.Dispose();
				fs.Close();
				fs.Dispose();

				FileStream sourceFile = new FileStream(filepath, FileMode.Open);
				float fileSize = 0;
				fileSize = sourceFile.Length;
				byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
				sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));

				Response.Clear();
				//Response.Headers.Clear();
				//Response.ContentType = "application/pdf";
				//Response.Headers.Add("Content-Length", getContent.Length.ToString());
				//Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName + ";");
				//             await Response.Body.WriteAsync( getContent);
				//Response.Body.Flush();
				sourceFile.Close();




			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filepath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
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
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("generatePastorAppoinmentReportWord")]
		public async Task<IActionResult> GeneratePastorAppoinmentReportWord([FromBody] List<PastorAppoinmentReport> products)
		{
			string fileName = "Pastor Appoinment Report.doc";
			string filePath = "";
			if (products.Count > 0)
			{
				StringBuilder sbDocumentBody = new StringBuilder();

				sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
				if (products.Count > 0)
				{
					sbDocumentBody.Append("<tr><td>");
					sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

					// Add Column Headers dynamically from datatable  
					sbDocumentBody.Append("<tr>");

					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Pastor Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Appoinment Date" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Appoinment Time" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Service Type" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary LastName" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary FirstName" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary Country" + "</td>");



					sbDocumentBody.Append("</tr>");

					// Add Data Rows dynamically from datatable  
					foreach (PastorAppoinmentReport data in products)
					{
						sbDocumentBody.Append("<tr>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.PastorName + "</td>");
						foreach (AppoinmentDetails appoinmentDetail in data.AppoinmentDetails)
                        {
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy") + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.Time + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.ServiceType + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.LastName + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.FirstName + "</td>");
							sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + appoinmentDetail.MissionaryCountry + "</td>");
						}
						sbDocumentBody.Append("</tr>");
					}
					sbDocumentBody.Append("</table>");
					sbDocumentBody.Append("</td></tr></table>");
				}
				byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sbDocumentBody.ToString());
				filePath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Response.Clear();
				//Response.Headers.Add("Content-Type", "application/msword");
				//Response.Headers.Add("Content-disposition", "attachment; filename=ProductDetails.doc");				
				System.IO.File.WriteAllBytes(filePath, byteArray); // Same contents you will get in byte[] and that will be save here 
																   //await Response.Body.WriteAsync(byteArray);
																   //await Response.Body.FlushAsync();
			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filePath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}

		[HttpPost]
		[Route("generatePastorAppoinmentReportPdf")]
		public async Task<IActionResult> GeneratePastorAppoinmentReportPdf([FromBody] List<PastorAppoinmentReport> report)
		{
			string fileName = "";
			string filepath = "";
			if (report.Count > 0)
			{
				int pdfRowIndex = 1;
				fileName = "Pastor Appoinment Report.pdf";
				filepath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
				FileStream fs = new FileStream(filepath, FileMode.Create);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);

				document.Open();

				Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
				Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

				float[] columnDefinitionSize = { 5F, 5F, 5F, 5F, 5F, 5F, 5F };
				PdfPTable table;
				PdfPCell cell;

				table = new PdfPTable(columnDefinitionSize)
				{
					WidthPercentage = 100

				};

				cell = new PdfPCell
				{
					BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
				};

				table.AddCell(new Phrase("Pastor Name", font1));
				table.AddCell(new Phrase("Appoinment Date", font1));
				table.AddCell(new Phrase("Appoinment Time", font1));
				table.AddCell(new Phrase("Service Type", font1));
				table.AddCell(new Phrase("Missionary LastName", font1));
				table.AddCell(new Phrase("Missionary FirstName", font1));
				table.AddCell(new Phrase("Missionary Country", font1));

				table.HeaderRows = 1;

				foreach (PastorAppoinmentReport data in report)
				{
					table.AddCell(new Phrase(data.PastorName, font2));
					foreach (AppoinmentDetails appoinmentDetail in data.AppoinmentDetails)
                    {
						table.AddCell(new Phrase(appoinmentDetail.AppoinmentDate.Value.ToString("dd-MM-yyyy"), font2));
						table.AddCell(new Phrase(appoinmentDetail.Time, font2));
						table.AddCell(new Phrase(appoinmentDetail.ServiceType, font2));
						table.AddCell(new Phrase(appoinmentDetail.LastName, font2));
						table.AddCell(new Phrase(appoinmentDetail.FirstName, font2));
						table.AddCell(new Phrase(appoinmentDetail.MissionaryCountry, font2));
					}



					pdfRowIndex++;
				}

				document.Add(table);
				document.Close();
				document.CloseDocument();
				document.Dispose();
				writer.Close();
				writer.Dispose();
				fs.Close();
				fs.Dispose();

				FileStream sourceFile = new FileStream(filepath, FileMode.Open);
				float fileSize = 0;
				fileSize = sourceFile.Length;
				byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
				sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));

				Response.Clear();
				//Response.Headers.Clear();
				//Response.ContentType = "application/pdf";
				//Response.Headers.Add("Content-Length", getContent.Length.ToString());
				//Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName + ";");
				//             await Response.Body.WriteAsync( getContent);
				//Response.Body.Flush();
				sourceFile.Close();




			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filepath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
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
			await Task.Delay(100);
			return result;
		}
		[HttpPost]
		[Route("generateAccomodationBookingReportWord")]
		public async Task<IActionResult> GenerateAccomodationBookingReportWord([FromBody] List<AccomodationBooking> products)
		{
			string fileName = "Evangangelist Quarter Report.doc";
			string filePath = "";
			if (products.Count > 0)
			{
				StringBuilder sbDocumentBody = new StringBuilder();

				sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
				if (products.Count > 0)
				{
					sbDocumentBody.Append("<tr><td>");
					sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

					// Add Column Headers dynamically from datatable  
					sbDocumentBody.Append("<tr>");

					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "District Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Church Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Accommodation Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary Last Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Missionary First Name" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Adult No" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Child No" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "CheckIn Date" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "CheckOut Date" + "</td>");
					sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + "Accommodation Reservation Feedback" + "</td>");


					sbDocumentBody.Append("</tr>");

					// Add Data Rows dynamically from datatable  
					foreach (AccomodationBooking data in products)
					{
						sbDocumentBody.Append("<tr>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.DistrictName + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.ChurchName + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.AccomodationDesc + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.LastName + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.FirstName + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.AdultNo + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.ChildNo + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.CheckinDate.ToString("dd-MM-yyyy") + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.CheckoutDate.ToString("dd-MM-yyyy") + "</td>");
						sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + data.FeedBack + "</td>");
						sbDocumentBody.Append("</tr>");
					}
					sbDocumentBody.Append("</table>");
					sbDocumentBody.Append("</td></tr></table>");
				}
				byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sbDocumentBody.ToString());
				filePath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Response.Clear();
				//Response.Headers.Add("Content-Type", "application/msword");
				//Response.Headers.Add("Content-disposition", "attachment; filename=ProductDetails.doc");				
				System.IO.File.WriteAllBytes(filePath, byteArray); // Same contents you will get in byte[] and that will be save here 
																   //await Response.Body.WriteAsync(byteArray);
																   //await Response.Body.FlushAsync();
			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filePath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
			return result;
		}


		[HttpPost]
		[Route("generateAccomodationBookingReportPdf")]
		public async Task<IActionResult> GenerateAccomodationBookingReportPdf([FromBody] List<AccomodationBooking> report)
		{
			string fileName = "";
			string filepath = "";
			if (report.Count > 0)
			{
				int pdfRowIndex = 1;
				fileName = "Evangangelist Quarter Report.pdf";
				filepath = Environment.CurrentDirectory + ("\\wwwroot\\resources\\reports\\") + "" + fileName;
				Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
				FileStream fs = new FileStream(filepath, FileMode.Create);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);

				document.Open();

				Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
				Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

				float[] columnDefinitionSize = { 5F, 5F, 5F, 5F, 5F, 5F, 5F, 5F, 5F,5F };
				PdfPTable table;
				PdfPCell cell;

				table = new PdfPTable(columnDefinitionSize)
				{
					WidthPercentage = 100

				};

				cell = new PdfPCell
				{
					BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
				};

				table.AddCell(new Phrase("District Name", font1));
				table.AddCell(new Phrase("Church Name", font1));
				table.AddCell(new Phrase("Accommodation Name", font1));
				table.AddCell(new Phrase("Missionary Last Name", font1));
				table.AddCell(new Phrase("Missionary First Name", font1));
				table.AddCell(new Phrase("Adult No", font1));
				table.AddCell(new Phrase("Child No", font1));
				table.AddCell(new Phrase("CheckIn Date", font1));
				table.AddCell(new Phrase("CheckOut Date", font1));
				table.AddCell(new Phrase("Accommodation Reservation Feedback", font1));

				table.HeaderRows = 1;

				foreach (AccomodationBooking data in report)
				{
					table.AddCell(new Phrase(data.DistrictName, font2));
					table.AddCell(new Phrase(data.ChurchName, font2));
					table.AddCell(new Phrase(data.AccomodationDesc, font2));
					table.AddCell(new Phrase(data.LastName, font2));
					table.AddCell(new Phrase(data.FirstName, font2));
					table.AddCell(new Phrase(data.AdultNo.ToString(), font2));
					table.AddCell(new Phrase(data.ChildNo.ToString(), font2));
					table.AddCell(new Phrase(data.CheckinDate.ToString("dd-MM-yyyy"), font2));
					table.AddCell(new Phrase(data.CheckoutDate.ToString("dd-MM-yyyy"), font2));
					table.AddCell(new Phrase(data.FeedBack, font2));


					pdfRowIndex++;
				}

				document.Add(table);
				document.Close();
				document.CloseDocument();
				document.Dispose();
				writer.Close();
				writer.Dispose();
				fs.Close();
				fs.Dispose();

				FileStream sourceFile = new FileStream(filepath, FileMode.Open);
				float fileSize = 0;
				fileSize = sourceFile.Length;
				byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
				sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));

				Response.Clear();
				//Response.Headers.Clear();
				//Response.ContentType = "application/pdf";
				//Response.Headers.Add("Content-Length", getContent.Length.ToString());
				//Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName + ";");
				//             await Response.Body.WriteAsync( getContent);
				//Response.Body.Flush();
				sourceFile.Close();




			}
			string contentType = "";
			new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);
			PhysicalFileResult result = PhysicalFile(Path.Combine(filepath), contentType);
			base.Response.Headers.Add("x-filename", fileName);
			base.Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			}.ToString();
			await Task.Delay(100);
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
			await Task.Delay(100);
			return result;
		}
	}
}