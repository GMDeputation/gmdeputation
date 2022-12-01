using System;
using System.Threading.Tasks;
using SFA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.Models;
using SFA.Extensions;
using Microsoft.AspNetCore.Authorization;
using SFA.Filters;
using System.Collections.Generic;

namespace SFA.Controllers
{
    [Route("appointments")]
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService = null;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Route("")]
        [CheckAccess]
        public IActionResult Index()
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            ViewBag.AccessCode = loggedinUser.DataAccessCode;
            return View();
        }

        [Route("add/{macroScheduleDetailId}")]
        [CheckAccess]
        public IActionResult Add(int macroScheduleDetailId)
        {
            return View();
        }

        [Route("detail/{id}")]
        [CheckAccess]
        public IActionResult Detail(int? id)
        {
            ViewBag.Id = id;
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
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            var appointment = await _appointmentService.GetAll(loggedinUser.DataAccessCode, loggedinUser.Id);
            return new JsonResult(appointment);
        }
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var appointment = await _appointmentService.GetById(id, loggedinUser.DataAccessCode);
            return new JsonResult(appointment);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody]AppointmentQuery query)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            TimeZoneInfo timeZone = TimeZoneInfo.Local;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZone.Id);

            if (query.FromDate != null)
            {
                query.FromDate = TimeZoneInfo.ConvertTimeFromUtc(query.FromDate.Value, tzi);
            }
            if (query.ToDate != null)
            {
                query.ToDate = TimeZoneInfo.ConvertTimeFromUtc(query.ToDate.Value, tzi);
            }            

            var appointment = await _appointmentService.Search(query, loggedinUser.DataAccessCode, loggedinUser.Id);
            return new JsonResult(appointment);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody]Appointment appointment)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            TimeZoneInfo timeZone = TimeZoneInfo.Local;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZone.Id);
            appointment.EventDate = TimeZoneInfo.ConvertTimeFromUtc(appointment.EventDate, tzi);

            var result = await _appointmentService.Save(appointment, loggedinUser);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("submitAppointment")]
        public async Task<IActionResult> SubmitAppointment([FromBody]Appointment appointment)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            appointment.SubmittedBy = loggedinUser.Id;

            var result = await _appointmentService.SubmitAppointment(appointment);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("acceptAppointmentByPastor")]
        public async Task<IActionResult> ApproveAppointmentByPator([FromBody]Appointment appointment)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            appointment.AcceptByPastorBy = loggedinUser.Id;

            var result = await _appointmentService.ApproveAppointmentByPator(appointment);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("forwardAppointmentForMissionary")]
        public async Task<IActionResult> ForwardAppointmentForMissionary([FromBody]Appointment appointment)
        {
            var result = await _appointmentService.ForwardAppointmentForMissionary(appointment);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("acceptAppointmentByMissionary")]
        public async Task<IActionResult> ApproveAppointmentByMissionary([FromBody]Appointment appointment)
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            appointment.AcceptMissionaryBy = loggedinUser.Id;

            var result = await _appointmentService.ApproveAppointmentByMissionary(appointment);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("approvedPastorApointmentsIds")]
        public async Task<IActionResult> ApprovedPastorApointmentsIds([FromBody]List<int> selectApointment) 
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var result = await _appointmentService.ApprovedPastorApointmentsIds(selectApointment, loggedinUser.Id);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("approvedMissionaryApointmentsIds")]
        public async Task<IActionResult> ApprovedMissionaryApointmentsIds([FromBody]List<int> selectApointment) 
        {
            var loggedinUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");

            var result = await _appointmentService.ApprovedMissionaryApointmentsIds(selectApointment, loggedinUser.Id); 
            return new JsonResult(result);
        }
    }
}