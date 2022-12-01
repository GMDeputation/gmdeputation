using Microsoft.EntityFrameworkCore;
using SFA.Entities;
using SFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface IChurchServiceTimeService
    {
        Task<List<ChurchServiceTime>> GetAll();
        Task<ChurchServiceTime> GetById(int id);
        Task<QueryResult<ChurchServiceTime>> Search(ChurchServiceTimeQuery query, string accessCode, int userId);
        Task<string> Save(ChurchServiceTime churchServiceTime, User loggedinUser);
        Task<List<ChurchServiceTime>> GetTimeByChurch(int churchId, string day);
    }
    public class ChurchServiceTimeService : IChurchServiceTimeService
    {
        private readonly SFADBContext _context = null;

        public ChurchServiceTimeService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<ChurchServiceTime>> GetAll()
        {
            var churchServiceTimeEntities = await _context.TblChurchServiceTimeNta.Include(m => m.Church).Include(m => m.ServiceType).OrderBy(m => m.Church.ChurchName).ToListAsync();
            return churchServiceTimeEntities.Select(m => new ChurchServiceTime
            {
                Id = m.Id,
                ChurchId = m.ChurchId,
                ChurchName = m.Church.ChurchName,
                WeekDay = m.WeekDay,
                ServiceTime = m.ServiceTime,
                ServiceTypeId = m.ServiceTypeId,
                ServiceTypeName = m.ServiceType.Name,
                Preferencelevel = m.Preferencelevel,
                Notes = m.Notes
            }).ToList();
        }

        public async Task<List<ChurchServiceTime>> GetTimeByChurch(int churchId, string day)
        {
            var churchServiceTimeEntities = await _context.TblChurchServiceTimeNta.Include(m => m.Church).Include(m => m.ServiceType).Where(m => m.ChurchId == churchId && m.WeekDay == day).ToListAsync();
            return churchServiceTimeEntities.Select(m => new ChurchServiceTime
            {
                Id = m.Id,
                WeekDay = m.WeekDay,
                ServiceTime = m.ServiceTime,
                TimeString = m.ServiceTime.ToString().Substring(0, 5) +" (" + m.ServiceType.Name + ")",
            }).ToList();
        }

        public async Task<ChurchServiceTime> GetById(int id)
        {
            var churchServiceTimeEntity = await _context.TblChurchServiceTimeNta.Include(m => m.Church).Include(m => m.ServiceType).FirstOrDefaultAsync(m => m.Id == id);
            return churchServiceTimeEntity == null ? null : new ChurchServiceTime
            {
                Id = churchServiceTimeEntity.Id,
                ChurchId = churchServiceTimeEntity.ChurchId,
                ChurchName = churchServiceTimeEntity.Church.ChurchName,
                WeekDay = churchServiceTimeEntity.WeekDay,
                ServiceTime = churchServiceTimeEntity.ServiceTime,
                ServiceTypeId = churchServiceTimeEntity.ServiceTypeId,
                ServiceTypeName = churchServiceTimeEntity.ServiceType.Name,
                Preferencelevel = churchServiceTimeEntity.Preferencelevel,
                Notes = churchServiceTimeEntity.Notes
            };
        }

        public async Task<QueryResult<ChurchServiceTime>> Search(ChurchServiceTimeQuery query, string accessCode, int userId)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var userEntity = await _context.TblUserNta.Include(m => m.TblUserChurchNta).FirstOrDefaultAsync(m => m.Id == userId);

                var churchServiceTimeQuery = _context.TblChurchServiceTimeNta.Include(m => m.Church).Include(m => m.ServiceType).AsNoTracking().AsQueryable();

                switch (accessCode)
                {
                    case "S":
                        churchServiceTimeQuery = userEntity.DistrictId != null ? churchServiceTimeQuery.Where(n => n.Church.DistrictId == userEntity.DistrictId).AsQueryable() : churchServiceTimeQuery.AsQueryable();
                        break;
                    case "D":
                        churchServiceTimeQuery = userEntity.DistrictId != null ? churchServiceTimeQuery.Where(n => n.Church.DistrictId == userEntity.DistrictId).AsQueryable() : churchServiceTimeQuery.AsQueryable();
                        break;
                    case "P":
                        churchServiceTimeQuery = churchServiceTimeQuery.Where(n => userEntity.TblUserChurchNta.Select(a => a.ChurchId).Contains(n.Church.Id)).AsQueryable();
                        break;
                    case "M":
                        churchServiceTimeQuery = churchServiceTimeQuery.Where(n => userEntity.TblUserChurchNta.Select(a => a.ChurchId).Contains(n.Church.Id)).AsQueryable();
                        break;
                    default:
                        churchServiceTimeQuery = churchServiceTimeQuery.AsQueryable();
                        break;
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    churchServiceTimeQuery = churchServiceTimeQuery.Where(m => m.Preferencelevel.ToString().Contains(query.Filter));
                }
                if(query.ChurchId != null && query.ChurchId >0)
                {
                    churchServiceTimeQuery = churchServiceTimeQuery.Where(m => m.ChurchId == query.ChurchId);
                }
                if (query.ServiceTypeId != null && query.ServiceTypeId.Count > 0)
                {
                    churchServiceTimeQuery = churchServiceTimeQuery.Where(m => query.ServiceTypeId.Contains(m.ServiceTypeId));
                }
                if (query.WeekDay != null && query.WeekDay.Count > 0)
                {
                    churchServiceTimeQuery = churchServiceTimeQuery.Where(m => query.WeekDay.Contains(m.WeekDay));
                }
                if (query.StartTime != null)
                {
                    churchServiceTimeQuery = churchServiceTimeQuery.Where(m => m.ServiceTime >= query.StartTime);
                }
                if (query.EndTime != null)
                {
                    churchServiceTimeQuery = churchServiceTimeQuery.Where(m => m.ServiceTime <= query.EndTime);
                }

                var count = await churchServiceTimeQuery.CountAsync();

                switch (query.Order)
                {
                    case "day":
                        churchServiceTimeQuery = churchServiceTimeQuery.OrderBy(m => m.WeekDay);
                        break;
                    case "-day":
                        churchServiceTimeQuery = churchServiceTimeQuery.OrderByDescending(m => m.WeekDay);
                        break;
                    case "serviceTime":
                        churchServiceTimeQuery = churchServiceTimeQuery.OrderBy(m => m.ServiceTime);
                        break;
                    case "-serviceTime":
                        churchServiceTimeQuery = churchServiceTimeQuery.OrderByDescending(m => m.ServiceTime);
                        break;
                    case "serviceType":
                        churchServiceTimeQuery = churchServiceTimeQuery.OrderBy(m => m.ServiceType.Name);
                        break;
                    case "-serviceType":
                        churchServiceTimeQuery = churchServiceTimeQuery.OrderByDescending(m => m.ServiceType.Name);
                        break;
                    case "level":
                        churchServiceTimeQuery = churchServiceTimeQuery.OrderBy(m => m.Preferencelevel);
                        break;
                    case "-level":
                        churchServiceTimeQuery = churchServiceTimeQuery.OrderByDescending(m => m.Preferencelevel);
                        break;
                    default:
                        churchServiceTimeQuery = query.Order.StartsWith("-") ? churchServiceTimeQuery.OrderByDescending(m => m.Church.ChurchName) : churchServiceTimeQuery.OrderBy(m => m.Church.ChurchName);
                        break;
                }
                var churchServiceTimeEntities = await churchServiceTimeQuery.Skip(skip).Take(query.Limit).ToListAsync();               
                var churchServiceTimes = churchServiceTimeEntities.Select(m => new ChurchServiceTime
                {
                    Id = m.Id,
                    ChurchId = m.ChurchId,
                    ChurchName = m.Church.ChurchName,
                    WeekDay = m.WeekDay,
                    ServiceTime = m.ServiceTime,
                    ServiceTypeId = m.ServiceTypeId,
                    ServiceTypeName = m.ServiceType.Name,
                    Preferencelevel = m.Preferencelevel,
                    Notes = m.Notes,
                    TimeString = m.ServiceTime.ToString().Substring(0,5).ToString()
                }).ToList();

                return new QueryResult<ChurchServiceTime> { Result = churchServiceTimes, Count = count };
            }
            catch
            {
                return null;
            }
        }
        public async Task<string> Save(ChurchServiceTime churchServiceTime, User loggedinUser)
        {        
                if (churchServiceTime.Id  == 0)
            {
                var churchServiceTimeEntities = new TblChurchServiceTimeNta
                {
                    ChurchId = churchServiceTime.ChurchId,
                    WeekDay = churchServiceTime.WeekDay,
                    ServiceTime = churchServiceTime.ServiceTime,
                    ServiceTypeId = churchServiceTime.ServiceTypeId,
                    Preferencelevel = churchServiceTime.Preferencelevel,
                    Notes = churchServiceTime.Notes,
                    InsertUser = loggedinUser.Id.ToString(),
                    InsertDatetime = DateTime.Now
                };
                _context.TblChurchServiceTimeNta.Add(churchServiceTimeEntities);
            }
            else
            {
                var churchServiceTimeEntity = await _context.TblChurchServiceTimeNta.FirstOrDefaultAsync(m => m.Id == churchServiceTime.Id);

                churchServiceTimeEntity.ChurchId = churchServiceTime.ChurchId;
                churchServiceTimeEntity.WeekDay = churchServiceTime.WeekDay;
                churchServiceTimeEntity.ServiceTime = churchServiceTime.ServiceTime;
                churchServiceTimeEntity.ServiceTypeId = churchServiceTime.ServiceTypeId;
                churchServiceTimeEntity.Preferencelevel = churchServiceTime.Preferencelevel;
                churchServiceTimeEntity.Notes = churchServiceTime.Notes;
                churchServiceTimeEntity.UpdateUser = loggedinUser.Id.ToString();
                churchServiceTimeEntity.UpdateDatetime = DateTime.Now;
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
