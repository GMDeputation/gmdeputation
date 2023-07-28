using Microsoft.EntityFrameworkCore;
using SFA.Entities;
using SFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface IMacroScheduleService
    {
        Task<List<MacroScheduleDetails>> GetAll(string dataAccessCode,int userId);
        Task<MacroSchedule> GetById(int id);
        Task<QueryResult<MacroScheduleDetails>> Search(MacroScheduleQuery query, int userId);
        Task<string> Save(MacroSchedule macroSchedule, User loggedinUser);
        Task<string> Edit(MacroScheduleDetails macroScheduleDetails, User loggedinUser);
        Task<MacroScheduleDetails> GetMacroScheduleDetailsById(int id, string accesscode);
        Task<string> Approved(MacroScheduleDetails macroScheduleDetails, int userId);
        Task<string> Rejected(MacroScheduleDetails macroScheduleDetails, int userId);
        Task<string> Cancel(MacroScheduleDetails macroScheduleDetails, int userId);
        Task<string> ApprovedMacroSchedulesIds(List<int>  selectSchedule, int userId);
        Task<string> RejectMacroSchedulesIds(List<int>  selectSchedule, int userId);

        Task<List<MacroScheduleDetails>> GetAllapproved(MacroScheduleQuery query, int userId, bool superAdmn);
    }
    public class MacroScheduleService : IMacroScheduleService
    {
        private readonly SFADBContext _context = null;

        public MacroScheduleService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<string> Cancel(MacroScheduleDetails macroScheduleDetails, int loggedinUser)
        {
           
            var macroScheduleDetailsEntity = await _context.TblMacroScheduleDetailsNta.FirstOrDefaultAsync(m => m.Id == macroScheduleDetails.Id);



            var macroScheduleDetailsAppointmentsEntity = await _context.TblAppointmentNta.Where(m => m.MacroScheduleDetailId == macroScheduleDetails.Id).ToListAsync();
           
            macroScheduleDetailsEntity.IsCanceled = true;
            macroScheduleDetailsEntity.Cancellation_DateTime = DateTime.Now;
            macroScheduleDetailsEntity.Cancellation_Notes = macroScheduleDetails.Cancellation_Notes;
            macroScheduleDetailsEntity.IsCanceledBy = loggedinUser;

            //Send email that macro Scheudle was cancelled
            Utilites email = new Utilites();
           
          
            var macroScheudleDetailEntitymain = await _context.TblMacroScheduleDetailsNta.Where(m => m.Id == macroScheduleDetails.Id).FirstAsync();
            var missionaryEntitymain = await _context.TblUserNta.Include(m => m.Country).Where(m => m.Id == macroScheudleDetailEntitymain.UserId).FirstAsync();
            var dgmdmain = await _context.TblUserNta.Where(m => m.DistrictId == macroScheduleDetails.DistrictId && m.RoleId == 7).FirstAsync();


            email.SendEmailForCancelledMacroSchedule(macroScheduleDetails.DistrictName, missionaryEntitymain.FirstName, missionaryEntitymain.LastName, dgmdmain.UserSalutation, dgmdmain.LastName, macroScheduleDetails.StartDate.ToString(), macroScheduleDetails.EndDate.ToString(), "Cancelled", macroScheduleDetails.Cancellation_Notes, dgmdmain.Email, dgmdmain.Phone);

            //This is going through all the appointments that are tied to the macro schedule and
            //Cancelling them as well.
            foreach (var item in macroScheduleDetailsAppointmentsEntity)
            {
                item.IsCanceled = true;
                item.Cancellation_DateTime = DateTime.Now;
                item.Cancellation_Notes = macroScheduleDetails.Cancellation_Notes;
                item.IsCanceledBy = loggedinUser;

                //Get Data to Send Email
                var churchEntity = await _context.TblChurchNta.Where(m => m.Id == item.ChurchId).FirstAsync();
                var districtEntity = await _context.TblDistrictNta.Where(m => m.Id == churchEntity.DistrictId).FirstAsync();
                var macroScheudleDetailEntity = await _context.TblMacroScheduleDetailsNta.Where(m => m.Id == item.MacroScheduleDetailId).FirstAsync();
                var missionaryEntity = await _context.TblUserNta.Include(m => m.Country).Where(m => m.Id == macroScheudleDetailEntity.UserId).FirstAsync();
                //Gets the link to see who the pastor is in a church
                var linkToPastorAndChurchEntity = await _context.TblUserChurchNta.Where(m => m.ChurchId == item.ChurchId).FirstAsync();
                var pastorEntity = await _context.TblUserNta.Include(m => m.TblUserChurchNta).Where(m => m.Id == linkToPastorAndChurchEntity.UserId).FirstAsync();
                var dgmd = await _context.TblUserNta.Where(m => m.DistrictId == districtEntity.Id && m.RoleId == 7).FirstAsync();
                var serviceTimeEntity = await _context.TblChurchServiceTimeNta.Where(m => m.Id == item.ServiceTypeId).FirstAsync();
                var serviceTypeEntity = await _context.TblServiceTypeNta.Where(m => m.Id == serviceTimeEntity.ServiceTypeId).FirstAsync();

                //Send email that appointment was cancelled
                Utilites tmp = new Utilites();
                tmp.SendEmailForCancelledAppointments(districtEntity.Name, missionaryEntity.UserSalutation, missionaryEntity.FirstName, missionaryEntity.LastName, pastorEntity.UserSalutation, pastorEntity.FirstName, pastorEntity.LastName,
                  pastorEntity.Email, dgmd.FirstName, dgmd.LastName, dgmd.Phone, dgmd.Email, missionaryEntity.Email, item.EventDate.DayOfWeek.ToString(), churchEntity.ChurchName, dgmd.UserSalutation, item.EventDate.ToString(), missionaryEntity.Country.Name, dgmd.TelePhoneNo);
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

        public async Task<List<MacroScheduleDetails>> GetAll(string dataAccessCode,int userId)
        {           
            var userEntity = await _context.TblUserNta.Include(m => m.Role).FirstOrDefaultAsync(m => m.Id == userId);

            var macroScheduleEntities = await _context.TblMacroScheduleDetailsNta.Include(m => m.MacroSchedule).ToListAsync();

            switch (dataAccessCode)
            {
                case "S":
                    macroScheduleEntities = macroScheduleEntities.Where(n => n.DistrictId == userEntity.DistrictId).ToList();
                    break;
                case "D":
                    macroScheduleEntities = macroScheduleEntities.Where(n => n.DistrictId == userEntity.DistrictId).ToList();
                    break;
                default:
                    macroScheduleEntities = macroScheduleEntities.ToList();
                    break;
            }
            return macroScheduleEntities.Select(m => new MacroScheduleDetails
            {
                Id = m.Id,
                MacroScheduleId = m.MacroScheduleId,
                MacroScheduleDesc = m.MacroSchedule.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                IsApproved = m.IsApproved,
                IsRejected = m.IsRejected,
                Notes = m.Notes,
                Status = m.IsCanceled ? "Canceled" : m.IsApproved ? "Approved" : m.IsRejected ? "Rejected" : "Initiate",
                AccessCode = dataAccessCode
            }).ToList();
        }

        public async Task<MacroSchedule> GetById(int id)
        {
            var macroScheduleEntity = await _context.TblMacroScheduleNta.Include(m => m.TblMacroScheduleDetailsNta).ThenInclude(m => m.District)
                                        .Include(m => m.TblMacroScheduleDetailsNta).ThenInclude(m => m.User).FirstOrDefaultAsync(m => m.Id == id);
            return macroScheduleEntity == null ? null : new MacroSchedule
            {
                Id = macroScheduleEntity.Id,
                Description = macroScheduleEntity.Description,
                EntryDate = macroScheduleEntity.EntryDate,
                IsActive = macroScheduleEntity.IsActive,

                MacroScheduleDetails = macroScheduleEntity.TblMacroScheduleDetailsNta.Select(n => new MacroScheduleDetails
                {
                    Id = n.Id,
                    MacroScheduleId = n.MacroScheduleId,
                    DistrictId = n.DistrictId,
                    DistrictName = n.District.Name,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    IsApproved = n.IsApproved,
                    IsRejected = n.IsRejected,
                    Notes = n.Notes,
                    ApprovedRejectRemarks = n.ApprovedRejectRemarks,
                    UserId = (int)n.UserId,
                    UserName = n.User?.FirstName + " " + n.User?.LastName,
                }).ToList()
            };
        }

        public async Task<MacroScheduleDetails> GetMacroScheduleDetailsById(int id, string accesscode)
        {
            var macroScheduleDetailsEntity = await _context.TblMacroScheduleDetailsNta.Include(m => m.User).Include(m => m.District).FirstOrDefaultAsync(m => m.Id == id);
            return macroScheduleDetailsEntity == null ? null : new MacroScheduleDetails
            {
                Id = macroScheduleDetailsEntity.Id,
                MacroScheduleId = macroScheduleDetailsEntity.MacroScheduleId,
                DistrictId = macroScheduleDetailsEntity.DistrictId,
                DistrictName = macroScheduleDetailsEntity.District.Name,
                StartDate = macroScheduleDetailsEntity.StartDate,
                EndDate = macroScheduleDetailsEntity.EndDate,
                IsApproved = macroScheduleDetailsEntity.IsApproved,
                IsRejected = macroScheduleDetailsEntity.IsRejected,
                ApprovedRejectRemarks = macroScheduleDetailsEntity.ApprovedRejectRemarks,
                AccessCode = accesscode,
                Notes = macroScheduleDetailsEntity.Notes,
                UserId = (int)macroScheduleDetailsEntity.UserId,
                UserName = macroScheduleDetailsEntity.User?.FirstName + " " + macroScheduleDetailsEntity.User?.LastName
            };
        }

        public async Task<List<MacroScheduleDetails>> GetAllapproved(MacroScheduleQuery query, int userId, bool superAdmn)
        {
            var userEntity = await _context.TblUserNta.FirstOrDefaultAsync(m => m.Id == userId);
            if(userEntity.DistrictId == null)
            {
                return null;
            }
            var macroScheduleEntities = superAdmn ? await _context.TblMacroScheduleDetailsNta.Include(m => m.MacroSchedule).Include(m => m.District).Include(m => m.User).Where(m => m.IsApproved).ToListAsync()
                                        : await _context.TblMacroScheduleDetailsNta.Include(m => m.MacroSchedule).Include(m => m.District).Include(m => m.User).Where(m => m.IsApproved && m.DistrictId == userEntity.DistrictId).ToListAsync();

            if (!string.IsNullOrEmpty(query.Filter))
            {
                macroScheduleEntities = macroScheduleEntities.Where(m => m.MacroSchedule.Description.ToLower().Contains(query.Filter.ToLower()) || m.District.Name.ToLower().Contains(query.Filter.ToLower()) || m.User.FirstName.ToLower().Contains(query.Filter.ToLower())).ToList();
            }
            if (query.FromDate != null)
            {
                macroScheduleEntities = macroScheduleEntities.Where(m => m.StartDate.Date >= query.FromDate).ToList();
            }
            if (query.ToDate != null)
            {
                macroScheduleEntities = macroScheduleEntities.Where(m => m.EndDate.Date <= query.ToDate).ToList();
            }

            return macroScheduleEntities.Select(n => new MacroScheduleDetails
            {
                Id = n.Id,
                MacroScheduleId = n.MacroScheduleId,
                MacroScheduleDesc = n.MacroSchedule.Description,
                DistrictId = n.DistrictId,
                DistrictName = n.District.Name,
                UserId = (int)n.UserId,
                UserName = n.User?.FirstName + " " + n.User?.LastName,
                StartDate = n.StartDate,
                EndDate = n.EndDate,
                IsApproved = n.IsApproved,
                IsRejected = n.IsRejected,
                Notes = n.Notes,
                Status = n.IsCanceled ? "Canceled" : n.IsApproved ? "Approved" : n.IsRejected ? "Rejected" : "Initiate",

                IsDateOver = n.EndDate.Date < DateTime.Now.Date ? true : false
            }).ToList();
        }

        public async Task<QueryResult<MacroScheduleDetails>> Search(MacroScheduleQuery query, int userId)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var userEntity = await _context.TblUserNta.Include(m => m.Role).FirstOrDefaultAsync(m => m.Id == userId);

                var macroScheduleQuery = _context.TblMacroScheduleDetailsNta.Include(m => m.MacroSchedule).Include(m => m.District).Include(m => m.User)
                                        .Include(m => m.ApprovedRejectByNavigation).ThenInclude(m => m.Role).AsNoTracking().AsQueryable();

                switch (userEntity.Role.DataAccessCode)
                {
                    case "S":
                        macroScheduleQuery = macroScheduleQuery.Where(n => n.DistrictId == userEntity.DistrictId).AsQueryable();
                        break;
                    case "D":
                        macroScheduleQuery = macroScheduleQuery.Where(n => n.DistrictId == userEntity.DistrictId).AsQueryable();
                        break;
                    default:
                        macroScheduleQuery = macroScheduleQuery.AsQueryable();
                        break;
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    macroScheduleQuery = macroScheduleQuery.Where(m => m.MacroSchedule.Description.Contains(query.Filter) || m.District.Name.Contains(query.Filter) || m.User.FirstName.Contains(query.Filter));
                }
                if (query.FromDate != null)
                {
                    macroScheduleQuery = macroScheduleQuery.Where(m => m.StartDate.Date >= query.FromDate);
                }
                if (query.ToDate != null)
                {
                    macroScheduleQuery = macroScheduleQuery.Where(m => m.EndDate.Date <= query.ToDate);
                }
                if (query.FromEntryDate != null)
                {
                    macroScheduleQuery = macroScheduleQuery.Where(m => m.MacroSchedule.EntryDate.Date >= query.FromEntryDate);
                }
                if (query.ToEntryDate != null)
                {
                    macroScheduleQuery = macroScheduleQuery.Where(m => m.MacroSchedule.EntryDate.Date <= query.ToEntryDate);
                }

                var count = await macroScheduleQuery.CountAsync();
                switch (query.Order)
                {
                    case "-entryDate":
                        macroScheduleQuery = macroScheduleQuery.OrderByDescending(m => m.MacroSchedule.EntryDate);
                        break;
                    case "entryDate":
                        macroScheduleQuery = macroScheduleQuery.OrderBy(m => m.MacroSchedule.EntryDate);
                        break;
                    case "-startDate":
                        macroScheduleQuery =  macroScheduleQuery.OrderByDescending(m => m.StartDate);
                        break;
                    case "startDate":
                        macroScheduleQuery = macroScheduleQuery.OrderBy(m => m.StartDate);
                        break;
                    case "-endDate":
                        macroScheduleQuery = macroScheduleQuery.OrderByDescending(m => m.EndDate);
                        break;
                    case "endDate":
                        macroScheduleQuery = macroScheduleQuery.OrderBy(m => m.EndDate);
                        break;
                    default:
                        macroScheduleQuery = query.Order.StartsWith("-") ? macroScheduleQuery.OrderByDescending(m => m.District.Name) : macroScheduleQuery.OrderBy(m => m.District.Name);
                        break;
                }

                var macroScheduleEntities = await macroScheduleQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var schedules = macroScheduleEntities.Select(n => new MacroScheduleDetails
                {
                    Id = n.Id,
                    MacroScheduleId = n.MacroScheduleId,
                    MacroScheduleDesc = n.MacroSchedule.Description,
                    DistrictId = n.DistrictId,
                    DistrictName = n.District.Name,
                    UserId = n.UserId,
                    UserName = n.User?.FirstName + " " + n.User?.LastName,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    IsApproved = n.IsApproved,
                    IsRejected = n.IsRejected,
                    Notes = n.Notes,
                    Status = n.IsCanceled ? "Canceled" : n.IsApproved ? "Approved" : n.IsRejected ? "Rejected" : "Initiate",
                    AccessCode = userEntity?.Role?.DataAccessCode,
                    EntryDate = n.MacroSchedule.EntryDate,
                    ApprovedRejectUser = n.ApprovedRejectBy != null ? n.ApprovedRejectByNavigation?.FirstName + " " + n.ApprovedRejectByNavigation?.MiddleName + " " + n.ApprovedRejectByNavigation?.LastName + " (" + n.ApprovedRejectByNavigation?.Role.Name + ")" : null
                }).ToList();

                return new QueryResult<MacroScheduleDetails> { Result = schedules, Count = count };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> Save(MacroSchedule macroSchedule, User loggedinUser)
        {
            if (macroSchedule.Id == 0)
            {
        
                var macroScheduleEntity = new TblMacroScheduleNta
                { 
                    IsActive = macroSchedule.IsActive,
                    Description = macroSchedule.Description,
                    EntryDate = macroSchedule.EntryDate,
                    InsertUser = loggedinUser.Id.ToString(),
                    InsertDatetime = DateTime.Now
                };

                var macroScheduleDetailsEntities = new List<TblMacroScheduleDetailsNta>();

                foreach (var item in macroSchedule.MacroScheduleDetails)
                {
                    var macroScheduleDetailsEntity = new TblMacroScheduleDetailsNta
                    {                         
                        DistrictId = item.DistrictId,
                        UserId = item.UserId,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        IsApproved = false,
                        IsRejected = false,
                        InsertDatetime = DateTime.Now,
                        InsertUser = loggedinUser.Id.ToString(),
                        Notes = item.Notes
                        
                        
                    };

                    macroScheduleDetailsEntities.Add(macroScheduleDetailsEntity);
                    //This is grabbing the DGMD from the database for the district. 7 Is the dgmd ID for the role. 
                    var dgmd = await _context.TblUserNta.Where(m => m.DistrictId == item.DistrictId && m.RoleId == 7).FirstAsync();
                    var missionary = await _context.TblUserNta.Where(m => m.Id == item.UserId ).FirstAsync();
                    var districtEntity = await _context.TblDistrictNta.Where(m => m.Id == item.DistrictId).FirstAsync();
                    macroScheduleDetailsEntities.Add(macroScheduleDetailsEntity);
                    Utilites tmp = new Utilites();


                    //This will get passed into email function and based on type choose the correct template that live in ZohoMail.
                    //The keys will be in the class the function lives in
                    string typeOfEmail = "";
                    //This is the standard email that we need to send
                    if (missionary.R1 != true && missionary.sensitiveNation != true)
                    {
                        typeOfEmail = "standard";
                    }
                    else if (missionary.R1 && missionary.sensitiveNation)
                    {
                        typeOfEmail = "combined";
                    }
                    else if (missionary.R1)
                    {
                        typeOfEmail = "R1";
                    }
                    else if (missionary.sensitiveNation)
                    {
                        typeOfEmail = "sensitiveNation";
                    }
                        tmp.SendEmailDGMDIncomingSchedule(districtEntity.Name, missionary.FirstName, missionary.LastName, missionary.UserSalutation, missionary.Email, dgmd.Email, dgmd.Phone, item.StartDate.ToString("MM/dd/yyyy"), item.EndDate.ToString("MM/dd/yyyy"), typeOfEmail);

                    }

                macroScheduleEntity.TblMacroScheduleDetailsNta = macroScheduleDetailsEntities;
                _context.TblMacroScheduleNta.Add(macroScheduleEntity);
            }
            else
            {
                var macroScheduleEntity = await _context.TblMacroScheduleNta.Include(m => m.TblMacroScheduleDetailsNta).ThenInclude(m => m.District).FirstOrDefaultAsync(m => m.Id == macroSchedule.Id);

                if (macroScheduleEntity.TblMacroScheduleDetailsNta.Count > 0)
                {
                    _context.TblMacroScheduleDetailsNta.RemoveRange(macroScheduleEntity.TblMacroScheduleDetailsNta);
                }


                macroScheduleEntity.IsActive = macroSchedule.IsActive;
                macroScheduleEntity.Description = macroSchedule.Description;
                macroScheduleEntity.EntryDate = macroSchedule.EntryDate;
                macroScheduleEntity.UpdateUser = loggedinUser.Id.ToString();
                macroScheduleEntity.UpdateDatetime = DateTime.Now;

                var macroScheduleDetailsEntities = new List<TblMacroScheduleDetailsNta>();

                foreach (var item in macroSchedule.MacroScheduleDetails)
                {
                    var macroScheduleDetailsEntity = new TblMacroScheduleDetailsNta
                    {          
                        MacroScheduleId = macroScheduleEntity.Id,
                        DistrictId = item.DistrictId,
                        UserId = item.UserId,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Notes = item.Notes
                    };

                    //This is grabbing the DGMD from the database for the district. 7 Is the dgmd ID for the role. 
                    var dgmd = await _context.TblUserNta.Where(m => m.DistrictId == item.DistrictId && m.RoleId == 7).FirstAsync();
                    var missionary = await _context.TblUserNta.Where(m => m.Id == item.UserId).FirstAsync();
                    macroScheduleDetailsEntities.Add(macroScheduleDetailsEntity);
                    var districtEntity = await _context.TblDistrictNta.Where(m => m.Id == item.DistrictId).FirstAsync();
                    Utilites tmp = new Utilites();

                    //This will get passed into email function and based on type choose the correct template that live in ZohoMail.
                    //The keys will be in the class the function lives in
                    string typeOfEmail = "";

            
                    if (missionary.R1 != true && missionary.sensitiveNation != true)
                    {
                        typeOfEmail = "standard";
                    }
                    else if (missionary.R1 && missionary.sensitiveNation)
                    {
                        typeOfEmail = "combined";
                    }
                    else if (missionary.R1)
                    {
                        typeOfEmail = "R1";
                    }
                    else if (missionary.sensitiveNation)
                    {
                        typeOfEmail = "sensitiveNation";
                    }
                    tmp.SendEmailDGMDIncomingSchedule(districtEntity.Name, missionary.FirstName, missionary.LastName, missionary.UserSalutation, missionary.Email, dgmd.Email, dgmd.Phone, item.StartDate.ToString("MM/dd/yyyy"), item.EndDate.ToString("MM/dd/yyyy"), typeOfEmail);




                }

                _context.TblMacroScheduleDetailsNta.AddRange(macroScheduleDetailsEntities);
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

        public async Task<string> Edit(MacroScheduleDetails macroScheduleDetails, User loggedinUser)
        {
            var macroScheduleDetailsEntity = await _context.TblMacroScheduleDetailsNta.FirstOrDefaultAsync(m => m.Id == macroScheduleDetails.Id);

            macroScheduleDetailsEntity.MacroScheduleId = macroScheduleDetails.MacroScheduleId;
            macroScheduleDetailsEntity.DistrictId = macroScheduleDetails.DistrictId;
            macroScheduleDetailsEntity.UserId = macroScheduleDetails.UserId;
            macroScheduleDetailsEntity.StartDate = macroScheduleDetails.StartDate;
            macroScheduleDetailsEntity.EndDate = macroScheduleDetails.EndDate;
            macroScheduleDetailsEntity.Notes = macroScheduleDetails.Notes;
            macroScheduleDetailsEntity.ApprovedRejectRemarks = macroScheduleDetails.ApprovedRejectRemarks;
            macroScheduleDetailsEntity.UpdateDatetime = DateTime.Now;
            macroScheduleDetailsEntity.UpdateUser = loggedinUser.Id.ToString();

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

        public async Task<string> Approved(MacroScheduleDetails macroScheduleDetails, int userId)
        {
            var macroScheduleDetailsEntity = await _context.TblMacroScheduleDetailsNta.FirstOrDefaultAsync(m => m.Id == macroScheduleDetails.Id);

            macroScheduleDetailsEntity.IsApproved = true;
            macroScheduleDetailsEntity.ApprovedRejectRemarks = macroScheduleDetails.ApprovedRejectRemarks;
            macroScheduleDetailsEntity.Notes = macroScheduleDetails.Notes;
            macroScheduleDetailsEntity.ApprovedRejectBy = userId;
            macroScheduleDetailsEntity.ApprovedRejectOn = DateTime.Now;
            macroScheduleDetailsEntity.UpdateUser = userId.ToString();
            macroScheduleDetailsEntity.UpdateDatetime = DateTime.Now;


            try
            {
                //{{district}}
                //{{MMissionaryFirstName }}
                //{{MissionaryLastName}}
                //{{SpecialDGMDText}} ??
                //{{DGMDFirstName}}
                //{{DGMDLastName}}
                //{{DGMDPrimaryPhone}}
                //{{DGMDEmail}}
                //{{DistrictWebsite}}
               await _context.SaveChangesAsync();
                Utilites tmp = new Utilites();
                var dgmd = await _context.TblUserNta.Where(m => m.DistrictId == macroScheduleDetailsEntity.DistrictId && m.RoleId == 7).FirstAsync();
                var missionary = await _context.TblUserNta.Where(m => m.Id == macroScheduleDetailsEntity.UserId).FirstAsync();
                var districtEntity = await _context.TblDistrictNta.Where(m => m.Id == macroScheduleDetailsEntity.DistrictId).FirstAsync();
                tmp.SendEmailToMissionaryConfirmedMacro(districtEntity.Name, missionary.FirstName,missionary.LastName, missionary.Email, macroScheduleDetails.ApprovedRejectRemarks, dgmd.FirstName,dgmd.LastName,dgmd.Phone,dgmd.Email);
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Rejected(MacroScheduleDetails macroScheduleDetails, int userId)
        {
            var macroScheduleDetailsEntity = await _context.TblMacroScheduleDetailsNta.FirstOrDefaultAsync(m => m.Id == macroScheduleDetails.Id);

            macroScheduleDetailsEntity.IsRejected = true;
            macroScheduleDetailsEntity.ApprovedRejectRemarks = macroScheduleDetails.ApprovedRejectRemarks;
            macroScheduleDetailsEntity.ApprovedRejectBy = userId;
            macroScheduleDetailsEntity.ApprovedRejectOn = DateTime.Now;
            macroScheduleDetailsEntity.UpdateDatetime = DateTime.Now;
            macroScheduleDetailsEntity.UpdateUser = userId.ToString();

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

        public async Task<string> ApprovedMacroSchedulesIds(List<int> selectSchedule, int userId)
        {
            var macroScheduleDetailsEntities = await _context.TblMacroScheduleDetailsNta.Where(m => selectSchedule.Contains(m.Id)).ToListAsync();
          
            foreach(var item in macroScheduleDetailsEntities)
            {
                item.IsApproved = true;
                item.ApprovedRejectBy = userId;
                item.ApprovedRejectOn = DateTime.Now;
                item.UpdateDatetime = DateTime.Now;
                item.UpdateUser = userId.ToString();
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

        public async Task<string> RejectMacroSchedulesIds(List<int> selectSchedule, int userId)
        {
            var macroScheduleDetailsEntities = await _context.TblMacroScheduleDetailsNta.Where(m => selectSchedule.Contains(m.Id)).ToListAsync();

            foreach (var item in macroScheduleDetailsEntities)
            {
                item.IsRejected = true;
                item.ApprovedRejectBy = userId;
                item.ApprovedRejectOn = DateTime.Now;
                item.UpdateUser = userId.ToString();
                item.UpdateDatetime = DateTime.Now;
            }

            try
            {
                _context.TblMacroScheduleDetailsNta.UpdateRange(macroScheduleDetailsEntities);
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
