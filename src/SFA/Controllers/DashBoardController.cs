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

[Route("dashBoard")]
[Authorize]
public class DashBoardController : Controller
{
	private readonly IDashBoardService _dashBoardService;

	private readonly IWebHostEnvironment _environment;

	private readonly SFADBContext _context;

	public DashBoardController(IDashBoardService userService, IWebHostEnvironment environment, SFADBContext context)
	{
		_dashBoardService = userService;
		_environment = environment;
		_context = context;
	}

	
	public IActionResult Index()
	{
		return View();
	}

	[Route("getCount")]
	public async Task<IActionResult> GetCount()
	{
		var result = await _dashBoardService.GetMacroScheduleWithFiveServicesOrLess();

		return new JsonResult(result);
	}

	[Route("getServiceOneYearCount")]
	public async Task<IActionResult> GetServiceOneYearCount()
	{
		var result = await _dashBoardService.GetServiceCountOneYear();

		return new JsonResult(result);
	}

	[Route("getMacroScheduleThirtyDayCount")]
	public async Task<IActionResult> GetMacroScheduleThirtyDayCount()
	{
		var result = await _dashBoardService.GetMacroScheduleDetailThirtyDayCount();

		return new JsonResult(result);
	}

    [Route("getMissionarySummary")]
    public async Task<IActionResult> GetMissionarySummary()
    {
        var result = await _dashBoardService.GetMissionarySummary();

        return new JsonResult(result);
    }

    [Route("getChurchEQ")]
    public async Task<IActionResult> GetChurchEQ()
    {
        var result = await _dashBoardService.GetChurchEQ();

        return new JsonResult(result);
    }

    [Route("getChurchPastorKPI")]
    public async Task<IActionResult> GetChurchPastorKPI()
    {
        var result = await _dashBoardService.GetChurchPastorKPI();

        return new JsonResult(result);
    }
}

