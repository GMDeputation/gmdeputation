using Microsoft.EntityFrameworkCore;
using SFA.Entities;
using SFA.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAll(string accessCode, int userId);
        Task<Appointment> GetById(int id, string accessCode);
        Task<QueryResult<Appointment>> Search(AppointmentQuery query, string accessCode, int userId);
        Task<string> Save(Appointment appointment, User loggedinUser);
        Task<string> Cancel(Appointment appointment, User loggedinUser);
        Task<string> SubmitAppointment(Appointment appointment);
        Task<string> ApproveAppointmentByPator(Appointment appointment);
        Task<string> ForwardAppointmentForMissionary(Appointment appointment);
        Task<string> ApproveAppointmentByMissionary(Appointment appointment);
        Task<string> ApprovedPastorApointmentsIds(List<int> selectApointment,int userId);
        Task<string> ApprovedMissionaryApointmentsIds(List<int> selectApointment,int userId);

        Task<string> Add(List<Appointment> appointments, string accessCode, int userId);
    }
    public class AppointmentService : IAppointmentService
    {
        private readonly SFADBContext _context = null;

        public AppointmentService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAll(string accessCode, int userId)
        {
            var appointmentEntities = await _context.TblAppointmentNta.Include(m => m.Church).ThenInclude(m => m.District).ThenInclude(m => m.TblUserNta).Include(m => m.MacroScheduleDetail)
                                      .ThenInclude(m => m.User).OrderBy(m => m.Church.ChurchName).ToListAsync();

            switch (accessCode)
            {
                case "D":
                    appointmentEntities = appointmentEntities.Where(m => m.Church.District.TblUserNta.Where(n => n.Id == userId).Count() > 0).ToList();
                    break;
                case "S":
                    appointmentEntities = appointmentEntities.Where(m => m.Church.District.TblUserNta.Where(n => n.Id == userId).Count() > 0).ToList();
                    break;
                case "P":
                    appointmentEntities = appointmentEntities.Where(m => m.Church.District.TblUserNta.Where(n => n.Id == userId).Count() > 0 && m.IsSubmit).ToList();
                    break;
                case "M":
                    appointmentEntities = appointmentEntities.Where(m => m.Church.District.TblUserNta.Where(n => n.Id == userId).Count() > 0 && m.IsSubmit && m.IsAcceptByPastor && m.IsForwardForMissionary).ToList();
                    break;
                default:
                    appointmentEntities = appointmentEntities.ToList();
                    break;
            }

            return appointmentEntities.Select(m => new Appointment
            {
                Id = m.Id,
                ChurchId = m.ChurchId,
                ChurchName = m.Church.ChurchName,
                MacroScheduleDetailId = m.MacroScheduleDetailId,
                EventDate = m.EventDate.Add(m.EventTime.Value),
                EventTime = m.EventTime,
                Description = m.Description,
                PimAmount = m.PimAmount,
                Offering = m.Offering,
                Notes = m.Notes,
                IsSubmit = m.IsSubmit,
                IsAcceptByPastor = m.IsAcceptByPastor,
                IsAcceptMissionary = m.IsAcceptMissionary,
                IsForwardForMissionary = m.IsForwardForMissionary,
                MissionaryUser = m.MacroScheduleDetail?.User?.FirstName + " " + m.MacroScheduleDetail?.User?.MiddleName + " " + m.MacroScheduleDetail?.User?.LastName,
                IsCanceled = m.IsCanceled,
                Cancellation_DateTime = m.Cancellation_DateTime,
                Cancellation_Notes = m.Cancellation_Notes,
                Status = m.IsCanceled ? "Canceled" : m.IsAcceptMissionary ? "Accepted by Missionary" : m.IsAcceptByPastor ? "Accepted by Pastor" : m.IsSubmit ? "Submitted" : "Initiated"
            }).ToList();
        }

        public async Task<Appointment> GetById(int id, string accessCode)
        {
            var appointmentEntity = await _context.TblAppointmentNta.Include(m => m.Church).FirstOrDefaultAsync(m => m.Id == id);
            return appointmentEntity == null ? null : new Appointment
            {
                Id = appointmentEntity.Id,
                ChurchId = appointmentEntity.ChurchId,
                ChurchName = appointmentEntity.Church.ChurchName,
                MacroScheduleDetailId = appointmentEntity.MacroScheduleDetailId,
                EventDate = appointmentEntity.EventDate,
                EventTime = appointmentEntity.EventTime,
                Description = appointmentEntity.Description,
                PimAmount = appointmentEntity.PimAmount,
                Offering = appointmentEntity.Offering,
                Notes = appointmentEntity.Notes,
                IsSubmit = appointmentEntity.IsSubmit,
                IsAcceptByPastor = appointmentEntity.IsAcceptByPastor,
                IsForwardForMissionary = appointmentEntity.IsForwardForMissionary,
                IsAcceptMissionary = appointmentEntity.IsAcceptMissionary,
                AcceptByPastorRemarks = appointmentEntity.AcceptByPastorRemarks,
                AcceptMissionaryRemarks = appointmentEntity.AcceptMissionaryRemarks,
                AccessCode = accessCode,
                IsCanceled = appointmentEntity.IsCanceled,
                Cancellation_DateTime = appointmentEntity.Cancellation_DateTime,
                Cancellation_Notes = appointmentEntity.Cancellation_Notes,
                Status = appointmentEntity.IsCanceled ? "Canceled" : appointmentEntity.IsAcceptMissionary ? "Accepted by Missionary" : appointmentEntity.IsAcceptByPastor ? "Accepted by Pastor" : appointmentEntity.IsSubmit ? "Submitted" : "Initiated"
            };
        }

        public async Task<QueryResult<Appointment>> Search(AppointmentQuery query, string accessCode,int userId)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var appointmentQuery = _context.TblAppointmentNta.Include(m => m.Church).ThenInclude(m => m.District).ThenInclude(m => m.TblUserNta)
                                       .Include(m => m.AcceptByPastorByNavigation).Include(m => m.MacroScheduleDetail).ThenInclude(m => m.MacroSchedule)
                                       .Include(m => m.MacroScheduleDetail).ThenInclude(m => m.District).Include(m => m.AcceptMissionaryByNavigation).Include(m => m.MacroScheduleDetail)
                                      .ThenInclude(m => m.User).AsNoTracking().AsQueryable();

                //We need to make the correct searches based on access codes. 
                switch(accessCode)
                {
                    case "D":
                        appointmentQuery = appointmentQuery.Where(m => m.Church.District.TblUserNta.Where(n => n.Id == userId).Count() > 0).AsQueryable();
                        break;
                    case "S":
                        appointmentQuery = appointmentQuery.Where(m => m.Church.District.TblUserNta.Where(n => n.Id == userId).Count() > 0).AsQueryable();
                        break;
                    case "P":
                        appointmentQuery = appointmentQuery.Where(m => m.Church.District.TblUserNta.Where(n => n.Id == userId).Count() > 0 && m.IsSubmit).AsQueryable();
                        break;
                    case "M":
                        appointmentQuery = appointmentQuery.Where(m => m.MacroScheduleDetail.UserId == userId && m.IsForwardForMissionary).AsQueryable();
                        break;
                    default:
                        appointmentQuery = appointmentQuery.AsQueryable(); 
                        break;
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    appointmentQuery = appointmentQuery.Where(m => m.Church.ChurchName.Contains(query.Filter));
                }
                if (query.FromDate != null)
                {
                    appointmentQuery = appointmentQuery.Where(m => m.EventDate.Date >= query.FromDate.Value.Date);
                }
                if (query.ToDate != null)
                {
                    appointmentQuery = appointmentQuery.Where(m => m.EventDate.Date <= query.ToDate.Value.Date);
                }
                var count = await appointmentQuery.CountAsync();

                switch (query.Order)
                {
                    case "-distName":
                        appointmentQuery = appointmentQuery.OrderByDescending(m => m.MacroScheduleDetail.District.Name);
                        break;
                    case "distName":
                        appointmentQuery = appointmentQuery.OrderBy(m => m.MacroScheduleDetail.District.Name);
                        break;
                    case "-macroDesc":
                        appointmentQuery = appointmentQuery.OrderByDescending(m => m.MacroScheduleDetail.MacroSchedule.Description);
                        break;
                    case "macroDesc":
                        appointmentQuery = appointmentQuery.OrderBy(m => m.MacroScheduleDetail.MacroSchedule.Description);
                        break;
                    case "-eventDate":
                        appointmentQuery = appointmentQuery.OrderByDescending(m => m.EventDate);
                        break;
                    case "eventDate":
                        appointmentQuery = appointmentQuery.OrderBy(m => m.EventDate);
                        break;
                    case "-eventTime":
                        appointmentQuery = appointmentQuery.OrderByDescending(m => m.EventTime);
                        break;
                    case "eventTime":
                        appointmentQuery = appointmentQuery.OrderBy(m => m.EventTime);
                        break;
                    default:
                        appointmentQuery = query.Order.StartsWith("-") ? appointmentQuery.OrderByDescending(m => m.Church.ChurchName) : appointmentQuery.OrderBy(m => m.Church.ChurchName);
                        break;
                }
                var appointmentEntities = await appointmentQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var appointments = appointmentEntities.Select(m => new Appointment
                {
                    Id = m.Id,
                    ChurchId = m.ChurchId,
                    ChurchName = m.Church.ChurchName,
                    MacroScheduleDetailId = m.MacroScheduleDetailId,
                    EventDate = m.EventDate,
                    EventTime = m.EventTime,
                    Description = m.Description,
                    PimAmount = m.PimAmount,
                    Offering = m.Offering,
                    Notes = m.Notes,
                    IsSubmit = m.IsSubmit,
                    IsAcceptByPastor = m.IsAcceptByPastor,
                    IsAcceptMissionary = m.IsAcceptMissionary,
                    DistrictName = m.MacroScheduleDetail?.District?.Name,
                    MacroScheduleDesc = m.MacroScheduleDetail?.MacroSchedule.Description,
                    TimeString = m.EventTime.ToString().Substring(0, 5),
                    MissionaryUser = m.MacroScheduleDetail?.User?.FirstName + " " + m.MacroScheduleDetail?.User?.MiddleName + " " + m.MacroScheduleDetail?.User?.LastName,
                    AccessCode = accessCode,
                    IsCanceled = m.IsCanceled,
                    Cancellation_DateTime = m.Cancellation_DateTime,
                    Cancellation_Notes = m.Cancellation_Notes,
                    Status = m.IsCanceled ? "Canceled" : m.IsAcceptMissionary ? "Accepted by Missionary" + " (" + m.AcceptMissionaryByNavigation?.FirstName + " " + m.AcceptMissionaryByNavigation?.MiddleName + " " + m.AcceptMissionaryByNavigation?.LastName + ")"
                             : m.IsAcceptByPastor ? "Accepted by Pastor" + " (" + m.AcceptByPastorByNavigation?.FirstName +" "+m.AcceptByPastorByNavigation?.MiddleName +" "+ m.AcceptByPastorByNavigation?.LastName + ")" : m.IsSubmit ? "Submitted" : "Initiated"
                }).ToList();

                return new QueryResult<Appointment> { Result = appointments, Count = count };
            }
            catch
            {
                return null;
            }
        }
        //This is called when we add an appointment initially
        public async Task<string> Add(List<Appointment> appointments, string accessCode, int userId)
        {
            List<TblAppointmentNta> list = new List<TblAppointmentNta>();
            foreach (Appointment appointment in appointments)
            {
                TblAppointmentNta item = new TblAppointmentNta
                {          
                    ChurchId = appointment.ChurchId,
                    MacroScheduleDetailId = appointment.MacroScheduleDetailId,
                    EventDate = appointment.EventDate,
                    EventTime = appointment.EventTime,
                    Description = appointment.Description,
                    PimAmount = appointment.PimAmount,
                    Offering = appointment.Offering,
                    Notes = appointment.Notes,
                    //IsSubmit = true,
                    //SubmittedBy = userId,
                    //SubmittedOn = DateTime.Now,
                    IsAcceptByDgmd = false,
                    IsCreatedByPastor = ((accessCode == "P") ? true : false),
                    IsAcceptByPastor = false,
                    IsRejectByPastor = false,
                    IsForwardForMissionary = false,
                    IsAcceptMissionary = false,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now,
                    InsertDatetime = DateTime.Now,
                    InsertUser = userId.ToString(),
                    OfferingOnly = appointment.OfferingOnly
                   // ServiceTypeId = appointment.ServiceTypeId
                    
                };
                list.Add(item);
            }
            try
            {
                _context.TblAppointmentNta.AddRange(list);
                await _context.SaveChangesAsync();
                //This will go throgh all the appointments and send out emails
                foreach (TblAppointmentNta appointment in list)
                {
                    //These are the variables that the email function needs
                    //{ { district} }
                    //{ { MissionaryFirstName} }
                    //{ { MissionaryLastName} }
                    //{ { UserSalutation} }
                    //{ { PastorFirstName} }
                    //{ { PastorLastName} }
                    //{ { ChurchName} }
                    //{ { MissionaryCountry} }
                    //{ { NumberTraveling} }
                    //{ { TravelingVia} }
                    //{ { DayOfWeek} }
                    //{ { EventDate} }
                    //{ { EventTime} }
                    //{ { TypeOfEvent} }
                    //{ { MissionaryMobile} }
                    //{ { MissionaryEmail} }
                    //{ { DGMDFirstName} }
                    //{ { DGMDLastName} }
                    //{ { DGMDMobile} }
                    //{ { DGMDEmail} }
                    //{ { DistrictWebsite} }
                    //{ { DGMDPrimaryPhone} }
                    try
                    {
                        var churchEntity = await _context.TblChurchNta.Where(m => m.Id == appointment.ChurchId).FirstAsync();
                        var districtEntity = await _context.TblDistrictNta.Where(m => m.Id == churchEntity.DistrictId).FirstAsync();
                        var macroScheudleDetailEntity = await _context.TblMacroScheduleDetailsNta.Where(m => m.Id == appointment.MacroScheduleDetailId).FirstAsync();
                        var missionaryEntity = await _context.TblUserNta.Where(m => m.Id == macroScheudleDetailEntity.UserId).FirstAsync();
                        //Gets the link to see who the pastor is in a church: Could be mutiple? 
                        var linkToPastorAndChurchEntity = await _context.TblUserChurchNta.Where(m => m.ChurchId == appointment.ChurchId).FirstAsync();
                        var pastorEntity = await _context.TblUserNta.Where(m => m.Id == linkToPastorAndChurchEntity.UserId).FirstAsync();
                        var dgmd = await _context.TblUserNta.Where(m => m.DistrictId == districtEntity.Id && m.RoleId == 7).FirstAsync();
                        var serviceTimeEntity = await _context.TblChurchServiceTimeNta.Where(m => m.Id == appointment.ServiceTypeId).FirstAsync();
                        var serviceTypeEntity = await _context.TblServiceTypeNta.Where(m => m.Id == serviceTimeEntity.ServiceTypeId).FirstAsync();
                        Utilites tmp = new Utilites();
                        //Hard coded USA for country for now. 
                        if(appointment.OfferingOnly)
                        {
                            tmp.SendEmailToPastorNewServiceSchedule(districtEntity.Name, missionaryEntity.FirstName, missionaryEntity.LastName, missionaryEntity.UserSalutation,
                            pastorEntity.FirstName, pastorEntity.LastName, churchEntity.ChurchName, "USA", missionaryEntity.NumberTraveling.ToString(),
                            missionaryEntity.TravelingVia, appointment.EventDate.DayOfWeek.ToString(), appointment.EventDate.ToString("MM/dd/yyyy"),
                            appointment.EventTime.ToString(), serviceTypeEntity.Name, missionaryEntity.TelePhoneNo, missionaryEntity.Email, dgmd.FirstName,
                            dgmd.LastName, dgmd.TelePhoneNo, dgmd.Email, dgmd.Phone, pastorEntity.Email, appointment.OfferingOnly,dgmd.FirstName + " " + dgmd.LastName,dgmd.Address);
                        }
                        else
                        {
                            tmp.SendEmailToPastorNewServiceSchedule(districtEntity.Name, missionaryEntity.FirstName, missionaryEntity.LastName, missionaryEntity.UserSalutation,
                            pastorEntity.FirstName, pastorEntity.LastName, churchEntity.ChurchName, "USA", missionaryEntity.NumberTraveling.ToString(),
                            missionaryEntity.TravelingVia, appointment.EventDate.DayOfWeek.ToString(), appointment.EventDate.ToString("MM/dd/yyyy"),
                            appointment.EventTime.ToString(), serviceTypeEntity.Name, missionaryEntity.TelePhoneNo, missionaryEntity.Email, dgmd.FirstName,
                            dgmd.LastName, dgmd.TelePhoneNo, dgmd.Email, dgmd.Phone, pastorEntity.Email, appointment.OfferingOnly,"","");
                        }

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("One or more emails failed to send for appointment additions: " + ex.Message);
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> Cancel(Appointment appointment, User loggedinUser)
        {
            var appointmentEntity = await _context.TblAppointmentNta.Include(m => m.Church).FirstOrDefaultAsync(m => m.Id == appointment.Id);

            appointmentEntity.IsCanceled = true;
            appointmentEntity.Cancellation_DateTime = DateTime.Now;
            appointmentEntity.Cancellation_Notes = appointment.Cancellation_Notes;
            appointmentEntity.IsCanceledBy = loggedinUser.Id;

            try
            {
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        public async Task<string> Save(Appointment appointment, User loggedinUser)
        {
            //This is for inserts
            if (appointment.Id == 0)
            {
                var eventDate = new DateTime();
                try
                {
                    eventDate = DateTime.ParseExact(appointment.EventDate.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                var appointmentEntities = new TblAppointmentNta
                {           
                    ChurchId = appointment.ChurchId,
                    MacroScheduleDetailId = appointment.MacroScheduleDetailId,
                    EventDate = eventDate,
                    EventTime = appointment.EventTime,
                    Description = appointment.Description,
                    PimAmount = appointment.PimAmount,
                    Offering = appointment.Offering,
                    Notes = appointment.Notes,
                    IsSubmit = false,
                    IsAcceptByPastor = false,
                    IsForwardForMissionary = false,
                    IsAcceptMissionary = false,
                    InsertUser = loggedinUser.Id.ToString(),
                    InsertDatetime = DateTime.Now,
                    OfferingOnly = true
                };
                _context.TblAppointmentNta.Add(appointmentEntities);

            }
            else
            {
                var appointmentEntity = await _context.TblAppointmentNta.FirstOrDefaultAsync(m => m.Id == appointment.Id);

                appointmentEntity.ChurchId = appointment.ChurchId;
                appointmentEntity.EventDate = appointment.EventDate;
                appointmentEntity.EventTime = appointment.EventTime;
                appointmentEntity.Description = appointment.Description;
                appointmentEntity.PimAmount = appointment.PimAmount;
                appointmentEntity.Offering = appointment.Offering;
                appointmentEntity.Notes = appointment.Notes;
                appointmentEntity.AcceptByPastorRemarks = appointment.AcceptByPastorRemarks;
                appointmentEntity.AcceptMissionaryRemarks = appointment.AcceptMissionaryRemarks;
                appointmentEntity.UpdateDatetime = DateTime.Now;
                appointmentEntity.UpdateUser = loggedinUser.Id.ToString();
            }
            try
            {
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SubmitAppointment(Appointment appointment)
        {
            var appointmentEntity = await _context.TblAppointmentNta.FirstOrDefaultAsync(m => m.Id == appointment.Id);

            appointmentEntity.IsSubmit = true;
            appointmentEntity.SubmittedBy = appointment.SubmittedBy;
            appointmentEntity.SubmittedOn = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Called when the pastor accepts the service in appointments/detail page. 
        public async Task<string> ApproveAppointmentByPator(Appointment appointment) 
        {
            var appointmentEntity = await _context.TblAppointmentNta.FirstOrDefaultAsync(m => m.Id == appointment.Id);

            appointmentEntity.IsAcceptByPastor = true;
            appointmentEntity.Description = appointment.Description;
            appointmentEntity.PimAmount = appointment.PimAmount;
            appointmentEntity.Offering = appointment.Offering;
            appointmentEntity.Notes = appointment.Notes;
            appointmentEntity.AcceptByPastorRemarks = appointment.AcceptByPastorRemarks;
            appointmentEntity.AcceptByPastorBy = appointment.AcceptByPastorBy;
            appointmentEntity.AcceptByPastorOn = DateTime.Now;
            appointmentEntity.IsForwardForMissionary = true;

            try
            {
                await _context.SaveChangesAsync();

                //Get Data to Send Email
                var churchEntity = await _context.TblChurchNta.Where(m => m.Id == appointment.ChurchId).FirstAsync();
                var districtEntity = await _context.TblDistrictNta.Where(m => m.Id == churchEntity.DistrictId).FirstAsync();
                var macroScheudleDetailEntity = await _context.TblMacroScheduleDetailsNta.Where(m => m.Id == appointment.MacroScheduleDetailId).FirstAsync();
                var missionaryEntity = await _context.TblUserNta.Where(m => m.Id == macroScheudleDetailEntity.UserId).FirstAsync();
                //Gets the link to see who the pastor is in a church: Could be mutiple? 
                var linkToPastorAndChurchEntity = await _context.TblUserChurchNta.Where(m => m.ChurchId == appointment.ChurchId).FirstAsync();
                var pastorEntity = await _context.TblUserNta.Where(m => m.Id == linkToPastorAndChurchEntity.UserId).FirstAsync();
                var dgmd = await _context.TblUserNta.Where(m => m.DistrictId == districtEntity.Id && m.RoleId == 7).FirstAsync();
                var serviceTimeEntity = await _context.TblChurchServiceTimeNta.Where(m => m.Id == appointmentEntity.ServiceTypeId).FirstAsync();
                var serviceTypeEntity = await _context.TblServiceTypeNta.Where(m => m.Id == serviceTimeEntity.ServiceTypeId).FirstAsync();
                Utilites tmp = new Utilites();
                tmp.SendEmailToMissionaryConfirmedService(districtEntity.Name, missionaryEntity.FirstName, missionaryEntity.LastName, pastorEntity.FirstName, pastorEntity.LastName,
                    "Church City", serviceTypeEntity.Name, appointment.EventDate.DayOfWeek.ToString(), appointment.EventTime.ToString(),pastorEntity.TelePhoneNo,pastorEntity.Email,
                    churchEntity.ChurchName,churchEntity.Address,churchEntity.Phone,"Special Pastor Text", missionaryEntity.UserSalutation, dgmd.FirstName, dgmd.LastName,
                    dgmd.Phone, dgmd.Email,missionaryEntity.Email);


                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ForwardAppointmentForMissionary(Appointment appointment)
        {
            var appointmentEntity = await _context.TblAppointmentNta.FirstOrDefaultAsync(m => m.Id == appointment.Id);

            appointmentEntity.IsForwardForMissionary = true;

            try
            {
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ApproveAppointmentByMissionary(Appointment appointment)
        {
            var appointmentEntity = await _context.TblAppointmentNta.FirstOrDefaultAsync(m => m.Id == appointment.Id);

            appointmentEntity.IsAcceptMissionary = true;
            appointmentEntity.Description = appointment.Description;
            appointmentEntity.PimAmount = appointment.PimAmount;
            appointmentEntity.Offering = appointment.Offering;
            appointmentEntity.Notes = appointment.Notes;
            appointmentEntity.AcceptMissionaryRemarks = appointment.AcceptMissionaryRemarks;
            appointmentEntity.AcceptMissionaryBy = appointment.AcceptMissionaryBy;
            appointmentEntity.AcceptMissionaryOn = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ApprovedPastorApointmentsIds(List<int> selectApointment, int userId)
        {
            var appointmentEntities = await _context.TblAppointmentNta.Where(m => selectApointment.Contains(m.Id)).ToListAsync();

            foreach (var item in appointmentEntities)
            {
                item.IsAcceptByPastor = true;
                item.AcceptByPastorBy = userId;
                item.AcceptByPastorOn = DateTime.Now;
            }

            try
            {
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ApprovedMissionaryApointmentsIds(List<int> selectApointment, int userId)
        {
            var appointmentEntities = await _context.TblAppointmentNta.Where(m => selectApointment.Contains(m.Id)).ToListAsync();

            foreach (var item in appointmentEntities)
            {
                item.IsAcceptMissionary = true;
                item.AcceptMissionaryBy = userId;
                item.AcceptMissionaryOn = DateTime.Now;
            }

            try
            {
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
