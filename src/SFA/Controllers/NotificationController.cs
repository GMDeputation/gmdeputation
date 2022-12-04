

// SFA.Controllers.NotificationController
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFA.Extensions;
using SFA.Models;
using SFA.Services;


namespace SFA.Controllers
{
	[Route("notification")]
	[Authorize]
	public class NotificationController : Controller
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[Route("getUnOpenedNotification")]
		public async Task<IActionResult> GetUnOpenByUserId()
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			if (user == null)
			{
				return new JsonResult(null);
			}
			return new JsonResult(await _notificationService.GetUnOpenByUserId(user.Id));
		}

		[Route("openByUserId/{id}")]
		public async Task<IActionResult> OpenByUserId(int id)
		{
			return new JsonResult(await _notificationService.OpenByUserId(id));
		}

		[HttpPost]
		[Route("add")]
		public async Task<IActionResult> Add([FromBody] Notification notification)
		{
			User user = base.HttpContext.Session.Get<User>("SESSIONSFAUSER");
			notification.InsertUser = user.Id.ToString();
			return new JsonResult(await _notificationService.Add(notification));
		}

		[HttpPost]
		[Route("edit/{id}")]
		public async Task<IActionResult> Edit(int id, [FromBody] Notification notification)
		{
			if (id != notification.Id)
			{
				return BadRequest("Notification Id not matched");
			}
			return new JsonResult(await _notificationService.Edit(notification));
		}
	}

}