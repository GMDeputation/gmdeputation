

// SFA.Services.ReportService
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.Entities;
using SFA.Models;
using SFA.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace SFA.Services
{
	public interface IReportService
	{
		Task<List<UserReport>> GetUserActivityReport(ReportParams reportParams);

		Task<List<ChurchServiceReport>> ChurchServiceCountReport(ChurchServiceReportParam reportParams);

		Task<List<PastorContactReport>> GetPastorContactData(PastorReportParam reportParams, string accessCode, int userId);

		Task<List<MacroscheduleWiseAppoinmentReport>> GetMacroscheduleWiseAppoinmentData(MacroScheduleReportParams reportParams, string accessCode, int userId);

		Task<List<ChurchWiseAppoinmentReport>> GetChurchWiseAppoinmentData(ChurchServiceReportParam reportParams, string accessCode, int userId);

		Task<List<ChurchWiseAppoinmentReport>> GetOfferingOnlyReportData(ChurchServiceReportParam reportParams, string accessCode, int userId);

		Task<List<MissionaryWiseSchedule>> GetMissionaryScheduleData(MacroScheduleReportParams reportParams, string accessCode, int userId);

		Task<List<PastorAppoinmentReport>> GetPastorAppoinmentData(MacroScheduleReportParams reportParams, string accessCode, int userId);

		Task<List<AccomodationBooking>> GetAccomodationBookingReportData(ReportParams reportParams, string accessCode, int userId);
	}
	public class ReportService : IReportService
	{

		


		private readonly SFADBContext _context;

		public ReportService(SFADBContext context)
		{
			_context = context;
		}
	
		
		public async Task<List<UserReport>> GetUserActivityReport(ReportParams reportParams)
		{
			List<UserReport> userDataList = new List<UserReport>();
			using DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "USP_UserActivity";
			cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.Date)
			{
				Value = reportParams.FromDate.Value
			});
			cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.Date)
			{
				Value = reportParams.ToDate
			});
			cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int)
			{
				Value = ((!reportParams.UserId.HasValue) ? 0 : reportParams.UserId)
			});
			if (cmd.Connection.State != ConnectionState.Open)
			{
				cmd.Connection.Open();
			}
			try
			{
				using (DbDataReader dbDataReader = await cmd.ExecuteReaderAsync())
				{
					while (dbDataReader.Read())
					{
						userDataList.Add(new UserReport
						{
							Name = ((!dbDataReader.IsDBNull(dbDataReader.GetOrdinal("Name"))) ? dbDataReader.GetString(dbDataReader.GetOrdinal("Name")) : string.Empty),
							Role = ((!dbDataReader.IsDBNull(dbDataReader.GetOrdinal("RoleName"))) ? dbDataReader.GetString(dbDataReader.GetOrdinal("RoleName")) : string.Empty),
							Email = ((!dbDataReader.IsDBNull(dbDataReader.GetOrdinal("Email"))) ? dbDataReader.GetString(dbDataReader.GetOrdinal("Email")) : string.Empty),
							Page = ((!dbDataReader.IsDBNull(dbDataReader.GetOrdinal("Page"))) ? dbDataReader.GetString(dbDataReader.GetOrdinal("Page")) : string.Empty),
							Description = ((!dbDataReader.IsDBNull(dbDataReader.GetOrdinal("Description"))) ? dbDataReader.GetString(dbDataReader.GetOrdinal("Description")) : string.Empty),
							Action = ((!dbDataReader.IsDBNull(dbDataReader.GetOrdinal("Action"))) ? dbDataReader.GetString(dbDataReader.GetOrdinal("Action")) : string.Empty),
							ActionTime = ((!dbDataReader.IsDBNull(dbDataReader.GetOrdinal("ActionTime"))) ? new DateTime?(dbDataReader.GetDateTime(dbDataReader.GetOrdinal("ActionTime"))) : null)
						});
					}
				}
				if (!string.IsNullOrEmpty(reportParams.Action))
				{
					userDataList = userDataList.Where((UserReport m) => m.Action == reportParams.Action).ToList();
				}
				if (!string.IsNullOrEmpty(reportParams.RoleName))
				{
					userDataList = userDataList.Where((UserReport m) => m.Role == reportParams.RoleName).ToList();
				}
				return userDataList;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<ChurchServiceReport>> ChurchServiceCountReport(ChurchServiceReportParam reportParams)
		{
			List<TblChurchNta> source = await (from m in _context.TblChurchNta.Include((TblChurchNta m) => m.TblChurchServiceTimeNta)
											orderby m.ChurchName
											select m).ToListAsync();
			if (reportParams.DistrictId.HasValue && reportParams.DistrictId != 0)
			{
				source = source.Where(delegate (TblChurchNta m)
				{
					int districtId = m.DistrictId;
					int? districtId2 = reportParams.DistrictId;
					return districtId == districtId2;
				}).ToList();
			}
			if (reportParams.SectionId.HasValue && reportParams.SectionId != 0)
			{
				source = source.Where(delegate (TblChurchNta m)
				{
					int sectionId = m.SectionId;
					int? sectionId2 = reportParams.SectionId;
					return sectionId == sectionId2;
				}).ToList();
			}
			if (reportParams.ChurchId.HasValue && reportParams.ChurchId != 0)
			{
				source = source.Where(delegate (TblChurchNta m)
				{
					int id = m.Id;
					int? churchId = reportParams.ChurchId;
					return id == churchId;
				}).ToList();
			}
			return source.Select((TblChurchNta m) => new ChurchServiceReport
			{
				ChurchName = m.ChurchName,
				ServiceTimeCount = ((m.TblChurchServiceTimeNta.Count > 0) ? m.TblChurchServiceTimeNta.Select((TblChurchServiceTimeNta n) => n.ServiceTime).Count() : 0),
				ServiceTypeCount = ((m.TblChurchServiceTimeNta.Count > 0) ? m.TblChurchServiceTimeNta.Select((TblChurchServiceTimeNta n) => n.ServiceTypeId).Distinct().Count() : 0)
			}).ToList();
		}

		public async Task<List<PastorContactReport>> GetPastorContactData(PastorReportParam reportParams, string accessCode, int userId)
		{
			try
			{
				TblUserNta userEnttity = await _context.TblUserNta.FirstOrDefaultAsync((TblUserNta m) => m.Id == userId);
				List<TblUserNta> list = ((!(accessCode == "A") && !(accessCode == "H")) ? (await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role).Include((TblUserNta m) => m.TblUserChurchNta).ThenInclude((TblUserChurchNta m) => m.Church)
						.Include((TblUserNta m) => m.District)
						.ThenInclude((TblDistrictNta m) => m.TblStateDistrictNta)
						.ThenInclude((TblStateDistrictNta m) => m.State)
																							   where m.Role.DataAccessCode == "P" && m.DistrictId == userEnttity.DistrictId
																							   select m).ToListAsync()) : (await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role).Include((TblUserNta m) => m.TblUserChurchNta).ThenInclude((TblUserChurchNta m) => m.Church)
																								   .Include((TblUserNta m) => m.District)
																								   .ThenInclude((TblDistrictNta m) => m.TblStateDistrictNta)
																								   .ThenInclude((TblStateDistrictNta m) => m.State)
																																  where m.Role.DataAccessCode == "P"
																																  select m).ToListAsync()));
				List<TblUserNta> source = list;
				if (!string.IsNullOrEmpty(reportParams.LastName))
				{
					source = source.Where((TblUserNta m) => m.LastName.Contains(reportParams.LastName)).ToList();
				}
				if (!string.IsNullOrEmpty(reportParams.Email))
				{
					source = source.Where((TblUserNta m) => m.Email != null && m.Email.Contains(reportParams.Email)).ToList();
				}
				if (!string.IsNullOrEmpty(reportParams.City))
				{
					source = source.Where((TblUserNta m) => m.City != null && m.City.Contains(reportParams.City)).ToList();
				}
				if (!string.IsNullOrEmpty(reportParams.State))
				{
					source = source.Where(delegate (TblUserNta m)
					{
						TblDistrictNta district = m.District;
						if (district != null && district.TblStateDistrictNta.Count() > 0)
						{
							TblDistrictNta district2 = m.District;
							if (district2 == null)
							{
								return false;
							}
							return district2.TblStateDistrictNta?.Where((TblStateDistrictNta n) => n.State.Name.Contains(reportParams.State)).Count() > 0;
						}
						return false;
					}).ToList();
				}
				if (!string.IsNullOrEmpty(reportParams.Zipcode))
				{
					source = source.Where((TblUserNta m) => m.Zipcode != null && m.Zipcode.Contains(reportParams.Zipcode)).ToList();
				}
				return (from m in source
						select new PastorContactReport
						{
							Name = m.FirstName + " " + m.MiddleName + " " + m.LastName,
							Email = m.Email,
							Address = m.Address,
							City = m.City,
							State = m.District?.TblStateDistrictNta?.FirstOrDefault()?.State?.Name,
							Phone = m.Phone,							
							Zipcode = m.Zipcode,
							ChurchWebsite = m.TblUserChurchNta?.FirstOrDefault()?.Church?.WebSite
						} into m
						orderby m.Name
						select m).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<MacroscheduleWiseAppoinmentReport>> GetMacroscheduleWiseAppoinmentData(MacroScheduleReportParams reportParams, string accessCode, int userId)
		{
			try
			{
				TblUserNta userEntity = await _context.TblUserNta.Include((TblUserNta m) => m.Role).FirstOrDefaultAsync((TblUserNta m) => m.Id == userId);
				List<TblMacroScheduleDetailsNta> list = ((!(accessCode == "A") && !(accessCode == "H")) ? (await (from m in _context.TblMacroScheduleDetailsNta.Include((TblMacroScheduleDetailsNta m) => m.MacroSchedule).Include((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta).ThenInclude((TblAppointmentNta m) => m.AcceptByPastorByNavigation)
						.Include((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta)
						.ThenInclude((TblAppointmentNta m) => m.Church)
						.ThenInclude((TblChurchNta m) => m.TblChurchServiceTimeNta)
						.ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType)
																											   where m.DistrictId == userEntity.DistrictId && m.TblAppointmentNta.Count() > 0
																											   select m).ToListAsync()) : (await (from m in _context.TblMacroScheduleDetailsNta.Include((TblMacroScheduleDetailsNta m) => m.MacroSchedule).Include((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta).ThenInclude((TblAppointmentNta m) => m.AcceptByPastorByNavigation)
																												   .Include((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta)
																												   .ThenInclude((TblAppointmentNta m) => m.Church)
																												   .ThenInclude((TblChurchNta m) => m.TblChurchServiceTimeNta)
																												   .ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType)
																																				  where m.TblAppointmentNta.Count() > 0
																																				  select m).ToListAsync()));
				List<TblMacroScheduleDetailsNta> source = list;
				if (reportParams.StartFromDate.HasValue)
				{
					source = source.Where((TblMacroScheduleDetailsNta m) => m.StartDate.Date >= reportParams.StartFromDate.Value.Date).ToList();
				}
				if (reportParams.StartToDate.HasValue)
				{
					source = source.Where((TblMacroScheduleDetailsNta m) => m.StartDate.Date <= reportParams.StartToDate.Value.Date).ToList();
				}
				if (reportParams.EventFromDate.HasValue)
				{
					source = source.Where((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta.Count() > 0 && m.TblAppointmentNta.Where((TblAppointmentNta n) => n.EventDate.Date >= reportParams.EventFromDate.Value.Date).Count() > 0).ToList();
				}
				if (reportParams.EventToDate.HasValue)
				{
					source = source.Where((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta.Count() > 0 && m.TblAppointmentNta.Where((TblAppointmentNta n) => n.EventDate.Date <= reportParams.EventToDate.Value.Date).Count() > 0).ToList();
				}
				return source.Select((TblMacroScheduleDetailsNta m) => new MacroscheduleWiseAppoinmentReport
				{
					Description = m.MacroSchedule?.Description,
					AppoinmentDetails = m.TblAppointmentNta?.Select((TblAppointmentNta n) => new AppoinmentDetails
					{
						ChurchName = n.Church?.ChurchName,
						AppoinmentDate = n.EventDate,
						Time = n.EventTime.ToString().Substring(0, 5),
						ServiceType = n.Church?.TblChurchServiceTimeNta?.Where(delegate (TblChurchServiceTimeNta a)
						{
							TimeSpan serviceTime = a.ServiceTime;
							TimeSpan? eventTime = n.EventTime;
							return serviceTime == eventTime;
						}).FirstOrDefault()?.ServiceType?.Name,
						PastorName = n.AcceptByPastorByNavigation?.FirstName + " " + n.AcceptByPastorByNavigation?.MiddleName + " " + n.AcceptByPastorByNavigation?.LastName
					}).ToList()
				}).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<ChurchWiseAppoinmentReport>> GetChurchWiseAppoinmentData(ChurchServiceReportParam reportParams, string accessCode, int userId)
		{
			TblUserNta userEntity = await _context.TblUserNta.Include((TblUserNta m) => m.Role).FirstOrDefaultAsync((TblUserNta m) => m.Id == userId);
			List<TblChurchNta> list = ((!(accessCode == "A") && !(accessCode == "H")) ? (await (from m in _context.TblChurchNta.Include((TblChurchNta m) => m.TblChurchServiceTimeNta).ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType).Include((TblChurchNta m) => m.TblAppointmentNta)
					.ThenInclude((TblAppointmentNta m) => m.MacroScheduleDetail)
					.ThenInclude((TblMacroScheduleDetailsNta m) => m.User)
																							 orderby m.ChurchName
																							 where m.TblAppointmentNta.Count() > 0 && m.DistrictId == userEntity.DistrictId
																							 select m).ToListAsync()) : (await (from m in _context.TblChurchNta.Include((TblChurchNta m) => m.TblChurchServiceTimeNta).ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType).Include((TblChurchNta m) => m.TblAppointmentNta)
																								 .ThenInclude((TblAppointmentNta m) => m.MacroScheduleDetail)
																								 .ThenInclude((TblMacroScheduleDetailsNta m) => m.User)
																																orderby m.ChurchName
																																where m.TblAppointmentNta.Count() > 0
																																select m).ToListAsync()));
			List<TblChurchNta> source = list;
			if (reportParams.DistrictId.HasValue && reportParams.DistrictId != 0)
			{
				source = source.Where(delegate (TblChurchNta m)
				{
					int districtId = m.DistrictId;
					int? districtId2 = reportParams.DistrictId;
					return districtId == districtId2;
				}).ToList();
			}
			if (reportParams.SectionId.HasValue && reportParams.SectionId != 0)
			{
				source = source.Where(delegate (TblChurchNta m)
				{
					int sectionId = m.SectionId;
					int? sectionId2 = reportParams.SectionId;
					return sectionId == sectionId2;
				}).ToList();
			}
			if (reportParams.ChurchId.HasValue && reportParams.ChurchId != 0)
			{
				source = source.Where(delegate (TblChurchNta m)
				{
					int id = m.Id;
					int? churchId = reportParams.ChurchId;
					return id == churchId;
				}).ToList();
			}
			return source.Select((TblChurchNta m) => new ChurchWiseAppoinmentReport
			{
				ChurchName = m.ChurchName,
				TotalServiceTime = ((m.TblChurchServiceTimeNta.Count > 0) ? m.TblChurchServiceTimeNta.Select((TblChurchServiceTimeNta n) => n.ServiceTime).Count() : 0),
				AppoinmentDetails = m.TblAppointmentNta?.Select((TblAppointmentNta n) => new AppoinmentDetails
				{
					AppoinmentDate = n.EventDate,
					Time = n.EventTime.ToString().Substring(0, 5),
					ServiceType = n.Church?.TblChurchServiceTimeNta?.Where(delegate (TblChurchServiceTimeNta a)
					{
						TimeSpan serviceTime = a.ServiceTime;
						TimeSpan? eventTime = n.EventTime;
						return serviceTime == eventTime;
					}).FirstOrDefault()?.ServiceType?.Name,
					LastName = n.MacroScheduleDetail?.User?.LastName,
					FirstName = n.MacroScheduleDetail?.User?.FirstName
				}).ToList()
			}).ToList();
		}

		public async Task<List<ChurchWiseAppoinmentReport>> GetOfferingOnlyReportData(ChurchServiceReportParam reportParams, string accessCode, int userId)
		{
			TblUserNta userEntity = await _context.TblUserNta.Include((TblUserNta m) => m.Role).FirstOrDefaultAsync((TblUserNta m) => m.Id == userId);
			List<TblChurchNta> list = ((!(accessCode == "A") && !(accessCode == "H")) ? (await (from m in _context.TblChurchNta.Include((TblChurchNta m) => m.TblChurchServiceTimeNta).ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType).Include((TblChurchNta m) => m.TblAppointmentNta)
					.ThenInclude((TblAppointmentNta m) => m.MacroScheduleDetail)
					.ThenInclude((TblMacroScheduleDetailsNta m) => m.User)
																							 orderby m.ChurchName
																							 where m.TblAppointmentNta.Count() > 0 && m.DistrictId == userEntity.DistrictId
																							 select m).ToListAsync()) : (await (from m in _context.TblChurchNta.Include((TblChurchNta m) => m.TblChurchServiceTimeNta).ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType).Include((TblChurchNta m) => m.TblAppointmentNta)
																								 .ThenInclude((TblAppointmentNta m) => m.MacroScheduleDetail)
																								 .ThenInclude((TblMacroScheduleDetailsNta m) => m.User)
																																orderby m.ChurchName
																																where m.TblAppointmentNta.Count() > 0
																																select m).ToListAsync()));
			List<TblChurchNta> churchEntities = list;
			if (reportParams.DistrictId.HasValue && reportParams.DistrictId != 0)
			{
				churchEntities = churchEntities.Where(delegate (TblChurchNta m)
				{
					int districtId = m.DistrictId;
					int? districtId2 = reportParams.DistrictId;
					return districtId == districtId2;
				}).ToList();
			}
			if (reportParams.SectionId.HasValue && reportParams.SectionId != 0)
			{
				churchEntities = churchEntities.Where(delegate (TblChurchNta m)
				{
					int sectionId = m.SectionId;
					int? sectionId2 = reportParams.SectionId;
					return sectionId == sectionId2;
				}).ToList();
			}
			if (reportParams.ChurchId.HasValue && reportParams.ChurchId != 0)
			{
				churchEntities = churchEntities.Where(delegate (TblChurchNta m)
				{
					int id = m.Id;
					int? churchId = reportParams.ChurchId;
					return id == churchId;
				}).ToList();
			}
			if (reportParams.ServiceTypeId.HasValue && reportParams.ServiceTypeId != 0)
			{
				churchEntities = churchEntities.Where((TblChurchNta m) => m.TblChurchServiceTimeNta.Where(delegate (TblChurchServiceTimeNta n)
				{
					int serviceTypeId3 = n.ServiceTypeId;
					int? serviceTypeId4 = reportParams.ServiceTypeId;
					return serviceTypeId3 == serviceTypeId4;
				}).ToList() != null && m.TblChurchServiceTimeNta.Where(delegate (TblChurchServiceTimeNta n)
				{
					int serviceTypeId = n.ServiceTypeId;
					int? serviceTypeId2 = reportParams.ServiceTypeId;
					return serviceTypeId == serviceTypeId2;
				}).ToList().Count() > 0).ToList();
			}
			TblServiceTypeNta serviceTypeEntity = await _context.TblServiceTypeNta.FirstOrDefaultAsync((TblServiceTypeNta m) => m.Id == reportParams.ServiceTypeId);
			return churchEntities.Select((TblChurchNta m) => new ChurchWiseAppoinmentReport
			{
				ChurchName = m.ChurchName,
				AppoinmentDetails = (from n in m.TblAppointmentNta?.Select((TblAppointmentNta n) => new AppoinmentDetails
				{
					AppoinmentDate = n.EventDate,
					Time = n.EventTime.ToString().Substring(0, 5),
					ServiceType = n.Church?.TblChurchServiceTimeNta?.Where(delegate (TblChurchServiceTimeNta a)
					{
						TimeSpan serviceTime = a.ServiceTime;
						TimeSpan? eventTime = n.EventTime;
						return serviceTime == eventTime;
					}).FirstOrDefault()?.ServiceType?.Name,
					LastName = n.MacroScheduleDetail?.User?.LastName,
					FirstName = n.MacroScheduleDetail?.User?.FirstName,
					OfferingAmount = n.PimAmount,
					Offer = n.Offering
				})
									 where n.ServiceType == serviceTypeEntity.Name
									 select n).ToList()
			}).ToList();
		}

		public async Task<List<MissionaryWiseSchedule>> GetMissionaryScheduleData(MacroScheduleReportParams reportParams, string accessCode, int userId)
		{
			try
			{
				List<TblUserNta> list = ((!(accessCode == "A") && !(accessCode == "H")) ? (await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role).Include((TblUserNta m) => m.TblMacroScheduleDetailsNtaUser).ThenInclude((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta)
						.ThenInclude((TblAppointmentNta m) => m.Church)
						.ThenInclude((TblChurchNta m) => m.TblChurchServiceTimeNta)
						.ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType)
						.Include((TblUserNta m) => m.TblMacroScheduleDetailsNtaUser)
						.ThenInclude((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta)
						.ThenInclude((TblAppointmentNta m) => m.AcceptByPastorByNavigation)
						.Include((TblUserNta m) => m.TblMacroScheduleDetailsNtaUser)
						.ThenInclude((TblMacroScheduleDetailsNta m) => m.MacroSchedule)
																							   where m.Role.DataAccessCode == "M" && m.Id == userId
																							   select m).ToListAsync()) : (await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role).Include((TblUserNta m) => m.TblMacroScheduleDetailsNtaUser).ThenInclude((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta)
																								   .ThenInclude((TblAppointmentNta m) => m.Church)
																								   .ThenInclude((TblChurchNta m) => m.TblChurchServiceTimeNta)
																								   .ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType)
																								   .Include((TblUserNta m) => m.TblMacroScheduleDetailsNtaUser)
																								   .ThenInclude((TblMacroScheduleDetailsNta m) => m.TblAppointmentNta)
																								   .ThenInclude((TblAppointmentNta m) => m.AcceptByPastorByNavigation)
																								   .Include((TblUserNta m) => m.TblMacroScheduleDetailsNtaUser)
																								   .ThenInclude((TblMacroScheduleDetailsNta m) => m.MacroSchedule)
																																  where m.Role.DataAccessCode == "M"
																																  select m).ToListAsync()));
				List<TblUserNta> source = list;
				if (reportParams.EventFromDate.HasValue)
				{
					source = source.Where(delegate (TblUserNta m)
					{
						ICollection<TblMacroScheduleDetailsNta> tblMacroScheduleDetailsUser3 = m.TblMacroScheduleDetailsNtaUser;
						if (tblMacroScheduleDetailsUser3 != null && tblMacroScheduleDetailsUser3.FirstOrDefault()?.TblAppointmentNta?.Count() > 0)
						{
							ICollection<TblMacroScheduleDetailsNta> tblMacroScheduleDetailsUser4 = m.TblMacroScheduleDetailsNtaUser;
							if (tblMacroScheduleDetailsUser4 == null)
							{
								return false;
							}
							return tblMacroScheduleDetailsUser4.FirstOrDefault()?.TblAppointmentNta?.Where((TblAppointmentNta n) => n.EventDate.Date >= reportParams.EventFromDate.Value.Date).Count() > 0;
						}
						return false;
					}).ToList();
				}
				if (reportParams.EventToDate.HasValue)
				{
					source = source.Where(delegate (TblUserNta m)
					{
						ICollection<TblMacroScheduleDetailsNta> tblMacroScheduleDetailsUser = m.TblMacroScheduleDetailsNtaUser;
						if (tblMacroScheduleDetailsUser != null && tblMacroScheduleDetailsUser.FirstOrDefault()?.TblAppointmentNta?.Count() > 0)
						{
							ICollection<TblMacroScheduleDetailsNta> tblMacroScheduleDetailsUser2 = m.TblMacroScheduleDetailsNtaUser;
							if (tblMacroScheduleDetailsUser2 == null)
							{
								return false;
							}
							return tblMacroScheduleDetailsUser2.FirstOrDefault()?.TblAppointmentNta?.Where((TblAppointmentNta n) => n.EventDate.Date <= reportParams.EventToDate.Value.Date).Count() > 0;
						}
						return false;
					}).ToList();
				}
				DateTime today = DateTime.Today;
				DateTime thisWeekStart = today.AddDays(0 - today.DayOfWeek);
				DateTime thisWeekEnd = thisWeekStart.AddDays(7.0).AddSeconds(-1.0);
				DateTime lastWeekStart = thisWeekStart.AddDays(-7.0);
				DateTime lastWeekEnd = thisWeekStart.AddSeconds(-1.0);
				DateTime nextWeekStart = thisWeekEnd.AddDays(1.0);
				DateTime nextWeekEnd = nextWeekStart.AddDays(6.0);
				if (reportParams.Week == "Current Week")
				{
					source = source.Where((TblUserNta m) => m.TblMacroScheduleDetailsNtaUser.Where((TblMacroScheduleDetailsNta n) => (n.StartDate.Date >= thisWeekStart.Date && n.StartDate.Date <= thisWeekEnd.Date) || (n.EndDate.Date >= thisWeekStart.Date && n.EndDate.Date <= thisWeekEnd.Date)).Count() > 0).ToList();
				}
				if (reportParams.Week == "Previous Week")
				{
					source = source.Where((TblUserNta m) => m.TblMacroScheduleDetailsNtaUser.Where((TblMacroScheduleDetailsNta n) => (n.StartDate.Date >= lastWeekStart.Date && n.StartDate.Date <= lastWeekEnd.Date) || (n.EndDate.Date >= lastWeekStart.Date && n.EndDate.Date <= lastWeekStart.Date)).Count() > 0).ToList();
				}
				if (reportParams.Week == "Next Week")
				{
					source = source.Where((TblUserNta m) => m.TblMacroScheduleDetailsNtaUser.Where((TblMacroScheduleDetailsNta n) => (n.StartDate.Date >= nextWeekStart.Date && n.StartDate.Date <= nextWeekEnd.Date) || (n.EndDate.Date >= nextWeekStart.Date && n.EndDate.Date <= nextWeekEnd.Date)).Count() > 0).ToList();
				}
				return source.Select((TblUserNta m) => new MissionaryWiseSchedule
				{
					MissionaryName = m.FirstName + " " + m.MiddleName + " " + m.LastName,
					MacroSchedules = (from n in m.TblMacroScheduleDetailsNtaUser?.Where((TblMacroScheduleDetailsNta n) => n.TblAppointmentNta.Count() > 0)
									  select new MacroscheduleWiseAppoinmentReport
									  {
										  Description = n.MacroSchedule?.Description,
										  AppoinmentDetails = n.TblAppointmentNta?.Select((TblAppointmentNta a) => new AppoinmentDetails
										  {
											  ChurchName = a.Church?.ChurchName,
											  AppoinmentDate = a.EventDate,
											  Time = a.EventTime.ToString().Substring(0, 5),
											  ServiceType = a.Church?.TblChurchServiceTimeNta?.Where(delegate (TblChurchServiceTimeNta q)
											  {
												  TimeSpan serviceTime = q.ServiceTime;
												  TimeSpan? eventTime = a.EventTime;
												  return serviceTime == eventTime;
											  }).FirstOrDefault()?.ServiceType?.Name,
											  PastorName = a.AcceptByPastorByNavigation?.FirstName + " " + a.AcceptByPastorByNavigation?.MiddleName + " " + a.AcceptByPastorByNavigation?.LastName
										  }).ToList()
									  }).ToList()
				}).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<PastorAppoinmentReport>> GetPastorAppoinmentData(MacroScheduleReportParams reportParams, string accessCode, int userId)
		{
			List<TblUserNta> list = ((!(accessCode == "A") && !(accessCode == "H")) ? (await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role).Include((TblUserNta m) => m.TblUserChurchNta).ThenInclude((TblUserChurchNta m) => m.Church)
					.ThenInclude((TblChurchNta m) => m.TblAppointmentNta)
					.ThenInclude((TblAppointmentNta m) => m.MacroScheduleDetail)
					.ThenInclude((TblMacroScheduleDetailsNta m) => m.User)
					.ThenInclude((TblUserNta m) => m.Country)
					.Include((TblUserNta m) => m.TblUserChurchNta)
					.ThenInclude((TblUserChurchNta m) => m.Church)
					.ThenInclude((TblChurchNta m) => m.TblChurchServiceTimeNta)
					.ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType)
																						   where m.Role.DataAccessCode == "P" && m.Id == userId && m.TblUserChurchNta.Where((TblUserChurchNta n) => n.Church.TblAppointmentNta.Count() > 0).Count() > 0
																						   select m).ToListAsync()) : (await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role).Include((TblUserNta m) => m.TblUserChurchNta).ThenInclude((TblUserChurchNta m) => m.Church)
																							   .ThenInclude((TblChurchNta m) => m.TblAppointmentNta)
																							   .ThenInclude((TblAppointmentNta m) => m.MacroScheduleDetail)
																							   .ThenInclude((TblMacroScheduleDetailsNta m) => m.User)
																							   .ThenInclude((TblUserNta m) => m.Country)
																							   .Include((TblUserNta m) => m.TblUserChurchNta)
																							   .ThenInclude((TblUserChurchNta m) => m.Church)
																							   .ThenInclude((TblChurchNta m) => m.TblChurchServiceTimeNta)
																							   .ThenInclude((TblChurchServiceTimeNta m) => m.ServiceType)
																															  where m.Role.DataAccessCode == "P" && m.TblUserChurchNta.Where((TblUserChurchNta n) => n.Church.TblAppointmentNta.Count() > 0).Count() > 0
																															  select m).ToListAsync()));
			List<TblUserNta> source = list;
			if (reportParams.EventFromDate.HasValue)
			{
				source = source.Where(delegate (TblUserNta m)
				{
					ICollection<TblUserChurchNta> tblUserChurch3 = m.TblUserChurchNta;
					if (tblUserChurch3 != null && tblUserChurch3.FirstOrDefault().Church?.TblAppointmentNta?.Count() > 0)
					{
						ICollection<TblUserChurchNta> tblUserChurch4 = m.TblUserChurchNta;
						if (tblUserChurch4 == null)
						{
							return false;
						}
						return tblUserChurch4.FirstOrDefault().Church?.TblAppointmentNta?.Where((TblAppointmentNta n) => n.EventDate.Date >= reportParams.EventFromDate.Value.Date).Count() > 0;
					}
					return false;
				}).ToList();
			}
			if (reportParams.EventToDate.HasValue)
			{
				source = source.Where(delegate (TblUserNta m)
				{
					ICollection<TblUserChurchNta> tblUserChurch = m.TblUserChurchNta;
					if (tblUserChurch != null && tblUserChurch.FirstOrDefault().Church?.TblAppointmentNta?.Count() > 0)
					{
						ICollection<TblUserChurchNta> tblUserChurch2 = m.TblUserChurchNta;
						if (tblUserChurch2 == null)
						{
							return false;
						}
						return tblUserChurch2.FirstOrDefault().Church?.TblAppointmentNta?.Where((TblAppointmentNta n) => n.EventDate.Date <= reportParams.EventToDate.Value.Date).Count() > 0;
					}
					return false;
				}).ToList();
			}
			return source.Select((TblUserNta m) => new PastorAppoinmentReport
			{
				PastorName = m.FirstName + " " + m.MiddleName + " " + m.LastName,
				AppoinmentDetails = m.TblUserChurchNta?.FirstOrDefault().Church?.TblAppointmentNta?.Select((TblAppointmentNta n) => new AppoinmentDetails
				{
					AppoinmentDate = n.EventDate,
					Time = n.EventTime.ToString().Substring(0, 5),
					ServiceType = n.Church?.TblChurchServiceTimeNta?.Where(delegate (TblChurchServiceTimeNta a)
					{
						TimeSpan serviceTime = a.ServiceTime;
						TimeSpan? eventTime = n.EventTime;
						return serviceTime == eventTime;
					}).FirstOrDefault()?.ServiceType?.Name,
					LastName = n.MacroScheduleDetail?.User?.LastName,
					FirstName = n.MacroScheduleDetail?.User?.FirstName,
					MissionaryCountry = n.MacroScheduleDetail?.User?.Country?.Name
				}).ToList()
			}).ToList();
		}

		public async Task<List<AccomodationBooking>> GetAccomodationBookingReportData(ReportParams reportParams, string accessCode, int userId)
		{
			TblUserNta userEntity = await _context.TblUserNta.Include((TblUserNta m) => m.TblUserChurchNta).FirstOrDefaultAsync((TblUserNta m) => m.Id == userId);
			List<TblAccomodationBookingNta> list = ((!(accessCode == "A") && !(accessCode == "H")) ? (await (from m in _context.TblAccomodationBookingNta.Include((TblAccomodationBookingNta m) => m.District).Include((TblAccomodationBookingNta m) => m.Church).Include((TblAccomodationBookingNta m) => m.RequestedUser)
			.Include((TblAccomodationBookingNta m) => m.Accomodation)
			where m.DistrictId == userEntity.DistrictId
			select m).ToListAsync()) : (await _context.TblAccomodationBookingNta.Include((TblAccomodationBookingNta m) => m.District).Include((TblAccomodationBookingNta m) => m.Church).Include((TblAccomodationBookingNta m) => m.RequestedUser).Include((TblAccomodationBookingNta m) => m.Accomodation).ToListAsync()));
			List<TblAccomodationBookingNta> source = list;
			if (reportParams.FromDate.HasValue)
			{
				source = source.Where((TblAccomodationBookingNta m) => m.CheckinDate.Date >= reportParams.FromDate.Value.Date).ToList();
			}
			if (reportParams.ToDate.HasValue)
			{
				source = source.Where((TblAccomodationBookingNta m) => m.CheckinDate.Date <= reportParams.ToDate.Value.Date).ToList();
			}
			if (reportParams.ChurchId.HasValue && reportParams.ChurchId != 0)
			{
				source = source.Where(delegate (TblAccomodationBookingNta m)
				{
					int churchId = m.ChurchId;
					int? churchId2 = reportParams.ChurchId;
					return churchId == churchId2;
				}).ToList();
			}
			return source.Select((TblAccomodationBookingNta m) => new AccomodationBooking
			{
				Id = m.Id,
				RequestedUserId = m.RequestedUserId,
				UserName = m.RequestedUser?.FirstName + " " + m.RequestedUser?.MiddleName + " " + m.RequestedUser?.LastName,
				FirstName = m.RequestedUser?.FirstName,
				LastName = m.RequestedUser?.LastName,
				DistrictId = m.DistrictId,
				DistrictName = m.District?.Name,
				ChurchId = m.ChurchId,
				ChurchName = m.Church?.ChurchName,
				AccomodationId = m.AccomodationId,
				AccomodationDesc = m.Accomodation.AccomType,
				AdultNo = m.AdultNo,
				ChildNo = m.ChildNo,
				CheckinDate = m.CheckinDate,
				CheckoutDate = m.CheckoutDate,
				Reason = m.Reason,
				FeedBack = m.FeedBack
			}).ToList();
		}
	}
}