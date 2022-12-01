

// SFA.Services.AccomodationBookService
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.Entities;
using SFA.Models;
using SFA.Services;

namespace SFA.Services
{
	public interface IAccomodationBookService
	{
		Task<List<AccomodationBooking>> GetAll(string accessCode, int userId);

		Task<AccomodationBooking> GetById(int id, string accessCode);

		Task<QueryResult<AccomodationBooking>> Search(AccomodationBookingQuery query, string accessCode, int userId);

		Task<int> Save(AccomodationBooking accomodationBooking);

		Task<string> Submit(AccomodationBooking accomodationBooking, int userId);

		Task<string> SubmitFeedBack(AccomodationBooking accomodationBooking, int userId);

		Task<string> Approved(AccomodationBooking accomodationBooking, int userId);
	}

	public class AccomodationBookService : IAccomodationBookService
	{
		private readonly SFADBContext _context;

		private readonly INotificationService _notificationService;

		public AccomodationBookService(SFADBContext context, INotificationService notificationService)
		{
			_context = context;
			_notificationService = notificationService;
		}

		public async Task<List<AccomodationBooking>> GetAll(string accessCode, int userId)
		{
			TblUserNta userEntity = await _context.TblUserNta.Include((TblUserNta m) => m.TblUserChurchNta).FirstOrDefaultAsync((TblUserNta m) => m.Id == userId);
			List<TblAccomodationBookingNta> source = await _context.TblAccomodationBookingNta.Include((TblAccomodationBookingNta m) => m.District).Include((TblAccomodationBookingNta m) => m.Church).Include((TblAccomodationBookingNta m) => m.RequestedUser)
				.Include((TblAccomodationBookingNta m) => m.Accomodation)
				.ToListAsync();
			return (accessCode switch
			{
				"S" => userEntity.DistrictId.HasValue ? source.Where(delegate (TblAccomodationBookingNta n)
				{
					int districtId3 = n.DistrictId;
					int? districtId4 = userEntity.DistrictId;
					return districtId3 == districtId4 && n.IsSubmit;
				}).ToList() : source.ToList(),
				"D" => userEntity.DistrictId.HasValue ? source.Where(delegate (TblAccomodationBookingNta n)
				{
					int districtId = n.DistrictId;
					int? districtId2 = userEntity.DistrictId;
					return districtId == districtId2 && n.IsSubmit;
				}).ToList() : source.ToList(),
				"P" => source.Where((TblAccomodationBookingNta n) => userEntity.TblUserChurchNta.Select((TblUserChurchNta a) => a.ChurchId).Contains(n.ChurchId) && n.IsSubmit).ToList(),
				"M" => source.Where((TblAccomodationBookingNta n) => n.RequestedUserId == userId).ToList(),
				_ => source.ToList(),
			}).Select((TblAccomodationBookingNta m) => new AccomodationBooking
			{
				Id = m.Id,
				RequestedUserId = m.RequestedUserId,
				UserName = m.RequestedUser?.FirstName + " " + m.RequestedUser?.MiddleName + " " + m.RequestedUser?.LastName,
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
				ArrivalTime = m.ArrivalTime,
				DepartureTime = m.DepartureTime,
				Reason = m.Reason,
				FeedBack = m.FeedBack,
				IsSubmit = m.IsSubmit,
				IsApproved = m.IsApproved
			}).ToList();
		}

		public async Task<AccomodationBooking> GetById(int id, string accessCode)
		{
			TblAccomodationBookingNta tblAccomodationBooking = await _context.TblAccomodationBookingNta.Include((TblAccomodationBookingNta m) => m.District).Include((TblAccomodationBookingNta m) => m.Church).Include((TblAccomodationBookingNta m) => m.RequestedUser)
				.Include((TblAccomodationBookingNta m) => m.Accomodation)
				.FirstOrDefaultAsync((TblAccomodationBookingNta m) => m.Id == id);
			object result;
			if (tblAccomodationBooking != null)
			{
				AccomodationBooking accomodationBooking = new AccomodationBooking();
				accomodationBooking.Id = tblAccomodationBooking.Id;
				accomodationBooking.RequestedUserId = tblAccomodationBooking.RequestedUserId;
				accomodationBooking.UserName = tblAccomodationBooking.RequestedUser?.FirstName + " " + tblAccomodationBooking.RequestedUser?.MiddleName + " " + tblAccomodationBooking.RequestedUser?.LastName;
				accomodationBooking.DistrictId = tblAccomodationBooking.DistrictId;
				accomodationBooking.DistrictName = tblAccomodationBooking.District?.Name;
				accomodationBooking.ChurchId = tblAccomodationBooking.ChurchId;
				accomodationBooking.ChurchName = tblAccomodationBooking.Church?.ChurchName;
				accomodationBooking.AccomodationId = tblAccomodationBooking.AccomodationId;
				accomodationBooking.AccomodationDesc = tblAccomodationBooking.Accomodation.AccomType;
				accomodationBooking.AdultNo = tblAccomodationBooking.AdultNo;
				accomodationBooking.ChildNo = tblAccomodationBooking.ChildNo;
				accomodationBooking.CheckinDate = tblAccomodationBooking.CheckinDate;
				accomodationBooking.CheckoutDate = tblAccomodationBooking.CheckoutDate;
				accomodationBooking.ArrivalTime = tblAccomodationBooking.ArrivalTime;
				accomodationBooking.DepartureTime = tblAccomodationBooking.DepartureTime;
				accomodationBooking.Reason = tblAccomodationBooking.Reason;
				accomodationBooking.Remarks = tblAccomodationBooking.Remarks;
				accomodationBooking.FeedBack = tblAccomodationBooking.FeedBack;
				accomodationBooking.IsSubmit = tblAccomodationBooking.IsSubmit;
				accomodationBooking.IsFeedBack = tblAccomodationBooking.IsFeedBack;
				accomodationBooking.IsApproved = tblAccomodationBooking.IsApproved;
				accomodationBooking.AccessCode = accessCode;
				result = accomodationBooking;
			}
			else
			{
				result = null;
			}
			return (AccomodationBooking)result;
		}

		public async Task<QueryResult<AccomodationBooking>> Search(AccomodationBookingQuery query, string accessCode, int userId)
		{
			try
			{
				int skip = (query.Page - 1) * query.Limit;
				TblUserNta userEntity = await _context.TblUserNta.Include((TblUserNta m) => m.TblUserChurchNta).FirstOrDefaultAsync((TblUserNta m) => m.Id == userId);
				IQueryable<TblAccomodationBookingNta> accomodationBookingQuery = _context.TblAccomodationBookingNta.Include((TblAccomodationBookingNta m) => m.District).Include((TblAccomodationBookingNta m) => m.Church).Include((TblAccomodationBookingNta m) => m.RequestedUser)
					.Include((TblAccomodationBookingNta m) => m.Accomodation)
					.AsNoTracking()
					.AsQueryable();
				accomodationBookingQuery = accessCode switch
				{
					"S" => userEntity.DistrictId.HasValue ? accomodationBookingQuery.Where((TblAccomodationBookingNta n) => n.DistrictId == userEntity.DistrictId && n.IsSubmit).AsQueryable() : accomodationBookingQuery.AsQueryable(),
					"D" => userEntity.DistrictId.HasValue ? accomodationBookingQuery.Where((TblAccomodationBookingNta n) => n.DistrictId == userEntity.DistrictId && n.IsSubmit).AsQueryable() : accomodationBookingQuery.AsQueryable(),
					"P" => accomodationBookingQuery.Where((TblAccomodationBookingNta n) => userEntity.TblUserChurchNta.Select((TblUserChurchNta a) => a.ChurchId).Contains(n.ChurchId) && n.IsSubmit).AsQueryable(),
					"M" => accomodationBookingQuery.Where((TblAccomodationBookingNta n) => n.RequestedUserId == userId).AsQueryable(),
					_ => accomodationBookingQuery.AsQueryable(),
				};
				if (!string.IsNullOrEmpty(query.SearchText))
				{
					accomodationBookingQuery = accomodationBookingQuery.Where((TblAccomodationBookingNta m) => m.District.Name.Contains(query.SearchText) || m.Church.ChurchName.Contains(query.SearchText) || m.Accomodation.AccomType.Contains(query.SearchText));
				}
				if (query.FromDate.HasValue)
				{
					accomodationBookingQuery = accomodationBookingQuery.Where((TblAccomodationBookingNta m) => m.CheckinDate.Date >= query.FromDate.Value.Date || m.CheckoutDate.Date >= query.FromDate.Value.Date);
				}
				if (query.ToDate.HasValue)
				{
					accomodationBookingQuery = accomodationBookingQuery.Where((TblAccomodationBookingNta m) => m.CheckinDate.Date <= query.ToDate.Value.Date || m.CheckoutDate.Date <= query.ToDate.Value.Date);
				}
				int count = await accomodationBookingQuery.CountAsync();
				List<AccomodationBooking> result = (await (query.Order.ToLower() switch
				{
					"-userName" => accomodationBookingQuery.OrderByDescending((TblAccomodationBookingNta m) => m.RequestedUser.FirstName),
					"userName" => accomodationBookingQuery.OrderBy((TblAccomodationBookingNta m) => m.RequestedUser.FirstName),
					"-distName" => accomodationBookingQuery.OrderByDescending((TblAccomodationBookingNta m) => m.District.Name),
					"distName" => accomodationBookingQuery.OrderBy((TblAccomodationBookingNta m) => m.District.Name),
					"-churchName" => accomodationBookingQuery.OrderByDescending((TblAccomodationBookingNta m) => m.Church.ChurchName),
					"churchName" => accomodationBookingQuery.OrderBy((TblAccomodationBookingNta m) => m.Church.ChurchName),
					"-type" => accomodationBookingQuery.OrderByDescending((TblAccomodationBookingNta m) => m.Accomodation.AccomType),
					"type" => accomodationBookingQuery.OrderBy((TblAccomodationBookingNta m) => m.Accomodation.AccomType),
					"-checkInDate" => accomodationBookingQuery.OrderByDescending((TblAccomodationBookingNta m) => m.CheckinDate),
					"checkInDate" => accomodationBookingQuery.OrderBy((TblAccomodationBookingNta m) => m.CheckinDate),
					"-checkOutDate" => accomodationBookingQuery.OrderByDescending((TblAccomodationBookingNta m) => m.CheckoutDate),
					"checkOutDate" => accomodationBookingQuery.OrderBy((TblAccomodationBookingNta m) => m.CheckoutDate),
					_ => query.Order.StartsWith("-") ? accomodationBookingQuery.OrderByDescending((TblAccomodationBookingNta m) => m.CheckinDate) : accomodationBookingQuery.OrderBy((TblAccomodationBookingNta m) => m.CheckinDate),
				}).Skip(skip).Take(query.Limit).ToListAsync()).Select((TblAccomodationBookingNta m) => new AccomodationBooking
				{
					Id = m.Id,
					RequestedUserId = m.RequestedUserId,
					UserName = m.RequestedUser?.FirstName + " " + m.RequestedUser?.MiddleName + " " + m.RequestedUser?.LastName,
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
					ArrivalTime = m.ArrivalTime,
					DepartureTime = m.DepartureTime,
					Reason = m.Reason,
					FeedBack = m.FeedBack,
					IsSubmit = m.IsSubmit,
					IsApproved = m.IsApproved,
					IsFeedBack = ((m.CheckoutDate.Date < DateTime.Now.Date && !m.IsFeedBack && m.IsApproved) ? true : false),
					AccessCode = accessCode
				}).ToList();
				return new QueryResult<AccomodationBooking>
				{
					Result = result,
					Count = count
				};
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<int> Save(AccomodationBooking accomodationBooking)
		{
			if (accomodationBooking.Id == 0)
			{
				if (await _context.TblAccomodationBookingNta.Where((TblAccomodationBookingNta m) => (m.ChurchId == accomodationBooking.ChurchId && m.AccomodationId == accomodationBooking.AccomodationId && m.CheckinDate.Date >= accomodationBooking.CheckinDate.Date && m.CheckinDate.Date <= accomodationBooking.CheckoutDate.Date) || (m.CheckoutDate.Date >= accomodationBooking.CheckinDate.Date && m.CheckoutDate.Date <= accomodationBooking.CheckoutDate.Date)).FirstOrDefaultAsync() != null)
				{
					return -1;
				}
				TblAccomodationBookingNta entity = new TblAccomodationBookingNta
				{
					RequestedUserId = accomodationBooking.RequestedUserId,
					DistrictId = accomodationBooking.DistrictId,
					ChurchId = accomodationBooking.ChurchId,
					AccomodationId = accomodationBooking.AccomodationId,
					AdultNo = accomodationBooking.AdultNo,
					ChildNo = accomodationBooking.ChildNo,
					CheckinDate = accomodationBooking.CheckinDate,
					CheckoutDate = accomodationBooking.CheckoutDate,
					ArrivalTime = accomodationBooking.ArrivalTime,
					DepartureTime = accomodationBooking.DepartureTime,
					Reason = accomodationBooking.Reason,
					IsSubmit = false,
					IsFeedBack = false,
					IsApproved = false,
					InsertUser = accomodationBooking.CreatedBy.ToString(),
					InsertDatetime = DateTime.Now
				};
				_context.TblAccomodationBookingNta.Add(entity);
			}
			else
			{
				TblAccomodationBookingNta tblAccomodationBooking = await _context.TblAccomodationBookingNta.FirstOrDefaultAsync((TblAccomodationBookingNta m) => m.Id == accomodationBooking.Id);
				tblAccomodationBooking.RequestedUserId = accomodationBooking.RequestedUserId;
				tblAccomodationBooking.DistrictId = accomodationBooking.DistrictId;
				tblAccomodationBooking.ChurchId = accomodationBooking.ChurchId;
				tblAccomodationBooking.AccomodationId = accomodationBooking.AccomodationId;
				tblAccomodationBooking.AdultNo = accomodationBooking.AdultNo;
				tblAccomodationBooking.ChildNo = accomodationBooking.ChildNo;
				tblAccomodationBooking.CheckinDate = accomodationBooking.CheckinDate;
				tblAccomodationBooking.CheckoutDate = accomodationBooking.CheckoutDate;
				tblAccomodationBooking.ArrivalTime = accomodationBooking.ArrivalTime;
				tblAccomodationBooking.DepartureTime = accomodationBooking.DepartureTime;
				tblAccomodationBooking.Reason = accomodationBooking.Reason;
				tblAccomodationBooking.UpdateUser = accomodationBooking.ModifiedBy.ToString();
				tblAccomodationBooking.UpdateDatetime = DateTime.Now;
			}
			try
			{
				await _context.SaveChangesAsync();
				return 1;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<string> Submit(AccomodationBooking accomodationBooking, int userId)
		{
			TblAccomodationBookingNta accomodationBookingEntity = await _context.TblAccomodationBookingNta.FirstOrDefaultAsync((TblAccomodationBookingNta m) => m.Id == accomodationBooking.Id);
			TblUserNta userEntity = await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role)
										where m.Id == userId
										select m).FirstOrDefaultAsync();
			List<TblUserNta> list = await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role).Include((TblUserNta m) => m.District).ThenInclude((TblDistrictNta m) => m.TblChurchNta)
										where m.Role.DataAccessCode == "P" && m.TblUserChurchNta.Where((TblUserChurchNta n) => n.ChurchId == accomodationBookingEntity.ChurchId).Count() > 0
										select m).ToListAsync();
			List<Notification> list2 = new List<Notification>();
			accomodationBookingEntity.IsSubmit = true;
			foreach (TblUserNta item2 in list)
			{
				Notification notification = new Notification();
				notification.Description = "Accomodation Booked By " + userEntity.FirstName + " " + userEntity.LastName + " (" + userEntity.Role.Name + " ), please approved it.";
				notification.EventUrl = "/accomodation-booking";
				notification.InsertUser = userId.ToString();
				notification.EventUser = item2.Id;
				Notification item = notification;
				list2.Add(item);
			}
			try
			{
				await _notificationService.Add(list2);
				await _context.SaveChangesAsync();
				return "";
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<string> SubmitFeedBack(AccomodationBooking accomodationBooking, int userId)
		{
			TblAccomodationBookingNta accomodationBookingEntity = await _context.TblAccomodationBookingNta.FirstOrDefaultAsync((TblAccomodationBookingNta m) => m.Id == accomodationBooking.Id);
			TblUserNta userEntity = await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role)
										where m.Id == userId
										select m).FirstOrDefaultAsync();
			List<TblUserNta> list = await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role).Include((TblUserNta m) => m.District).ThenInclude((TblDistrictNta m) => m.TblChurchNta)
										where m.Role.DataAccessCode == "P" && m.TblUserChurchNta.Where((TblUserChurchNta n) => n.ChurchId == accomodationBookingEntity.ChurchId).Count() > 0
										select m).ToListAsync();
			List<Notification> list2 = new List<Notification>();
			accomodationBookingEntity.IsFeedBack = true;
			accomodationBookingEntity.FeedBack = accomodationBooking.FeedBack;
			foreach (TblUserNta item2 in list)
			{
				Notification notification = new Notification();
				notification.Description = "Accomodation Booking FeedBack given By " + userEntity.FirstName + " " + userEntity.LastName + " (" + userEntity.Role.Name + " ). ";
				notification.EventUrl = "/accomodation-booking";
				notification.InsertUser = userId.ToString();
				notification.EventUser = item2.Id;
				Notification item = notification;
				list2.Add(item);
			}
			try
			{
				await _notificationService.Add(list2);
				await _context.SaveChangesAsync();
				return "";
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<string> Approved(AccomodationBooking accomodationBooking, int userId)
		{
			TblUserNta userEntity = await (from m in _context.TblUserNta.Include((TblUserNta m) => m.Role)
										where m.Id == userId
										select m).FirstOrDefaultAsync();
			TblAccomodationBookingNta tblAccomodationBooking = await _context.TblAccomodationBookingNta.FirstOrDefaultAsync((TblAccomodationBookingNta m) => m.Id == accomodationBooking.Id);
			tblAccomodationBooking.IsApproved = true;
			tblAccomodationBooking.Remarks = accomodationBooking.Remarks;
			tblAccomodationBooking.ApprovedBy = userId;
			tblAccomodationBooking.ApprovedOn = DateTime.Now;
			Notification notification = new Notification();
			notification.Description = "Accomodation Booking Submit By " + userEntity.FirstName + " " + userEntity.LastName + " (" + userEntity.Role.Name + " ). ";
			notification.EventUrl = "/accomodation-booking";
			notification.InsertUser = userId.ToString();
			notification.EventUser = accomodationBooking.RequestedUserId;
			Notification notification2 = notification;
			try
			{
				await _notificationService.Add(notification2);
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