

// SFA.Controllers.AccomodationBookController
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFA.Extensions;
using SFA.Filters;
using SFA.Models;
using SFA.Services;

[Route("accomodation-booking")]
[Authorize]
public class AccomodationBookController : Controller
{
	private readonly IAccomodationBookService _accomodationBookService;

	public AccomodationBookController(IAccomodationBookService accomodationBookService)
	{
		_accomodationBookService = accomodationBookService;
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

	[Route("view/{id?}")]
	[CheckAccess]
	public IActionResult View(int? id)
	{
		base.ViewBag.Id = id;
		return View();
	}

	[Route("feedBack/{id?}")]
	[CheckAccess]
	public IActionResult FeedBack(int? id)
	{
		base.ViewBag.Id = id;
		return View();
	}

	[Route("calender")]
	[CheckAccess]
	public IActionResult CalenderView()
	{
		return View();
	}

	[Route("all")]
	public async Task<IActionResult> GetAll()
	{
		User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		return new JsonResult(await _accomodationBookService.GetAll(user.DataAccessCode, user.Id));
	}

	[HttpPost]
	[Route("search")]
	public async Task<IActionResult> Search([FromBody] AccomodationBookingQuery query)
	{
		User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		return new JsonResult(await _accomodationBookService.Search(query, user.DataAccessCode, user.Id));
	}

	[Route("get/{id}")]
	public async Task<IActionResult> Get(int id)
	{
		User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		return new JsonResult(await _accomodationBookService.GetById(id, user.DataAccessCode));
	}

	[HttpPost]
	[Route("save")]
	public async Task<IActionResult> Save([FromBody] AccomodationBooking accomodationBook)
	{
		User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		accomodationBook.CreatedBy = user.Id;
		accomodationBook.ModifiedBy = user.Id;
		accomodationBook.RequestedUserId = user.Id;
		TimeZoneInfo local = TimeZoneInfo.Local;
		TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(local.Id);
		accomodationBook.CheckinDate = TimeZoneInfo.ConvertTimeFromUtc(accomodationBook.CheckinDate, destinationTimeZone);
		accomodationBook.CheckoutDate = TimeZoneInfo.ConvertTimeFromUtc(accomodationBook.CheckoutDate, destinationTimeZone);
		return new JsonResult(await _accomodationBookService.Save(accomodationBook));
	}

	[HttpPost]
	[Route("submit")]
	public async Task<IActionResult> Submmit([FromBody] AccomodationBooking accomodationBook)
	{
		User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		return new JsonResult(await _accomodationBookService.Submit(accomodationBook, user.Id));
	}

	[HttpPost]
	[Route("submitFeedBack")]
	public async Task<IActionResult> SubmitFeedBack([FromBody] AccomodationBooking accomodationBook)
	{
		User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		return new JsonResult(await _accomodationBookService.SubmitFeedBack(accomodationBook, user.Id));
	}

	[HttpPost]
	[Route("approved")]
	public async Task<IActionResult> Approved([FromBody] AccomodationBooking accomodationBook)
	{
		User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
		return new JsonResult(await _accomodationBookService.Approved(accomodationBook, user.Id));
	}
}
