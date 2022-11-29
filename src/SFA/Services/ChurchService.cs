using SFA.Models;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface IChurchService
    {
        Task<List<Church>> GetAll();
        Task<Church> GetById(int id);
        Task<List<Church>> GetChurchBySectionId(int id);
        Task<List<Church>> GetChurchByDistrict(int id);
        Task<QueryResult<Church>> Search(ChurchQuery query, string accessCode, int userId);
        Task<string> Add(Church church, User loggedinUser);
        Task<string> Edit(Church church,User loggedinUser);
        Task<bool> Delete(int id,int userId);
    }
    public class ChurchService : IChurchService
    {
        private readonly SFADBContext _context = null;

        public ChurchService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<Church>> GetAll()
        {
            var churchEntities = await _context.TblChurchNta.Include(m => m.Section).Include(m => m.District).Where(m=>!m.IsDelete).OrderBy(m => m.ChurchName).ToListAsync();
            return churchEntities.Select(m => new Church
            {
                Id = m.Id,
                ChurchName = m.ChurchName,
                Address = m.Address,
                Email = m.Email,
                IsDelete = m.IsDelete,
                Lat = m.Lat,
                Lon = m.Lon,
                Phone = m.Phone,
                SectionId = m.SectionId,
                SectionName = m.Section.Name,
                DistrictId = m.DistrictId,
                DistrictName = m.District.Name,
                Status = m.Status,
                WebSite = m.WebSite
            }).ToList();
        }
        public async Task<Church> GetById(int id)
        {
            var churchEntity = await _context.TblChurchNta.Include(m =>m.Section).Include(m => m.District).FirstOrDefaultAsync(m => m.Id == id && !m.IsDelete);
            return churchEntity == null ? null : new Church
            {
                Id = churchEntity.Id,
                ChurchName = churchEntity.ChurchName,
                Address = churchEntity.Address,
                Email = churchEntity.Email,
                AccountNo = churchEntity.AccountNo,
                ChurchType = churchEntity.ChurchType,
                Directory = churchEntity.Directory,
                MailAddress = churchEntity.MailAddress,
                Phone2 = churchEntity.Phone2,
                IsDelete = churchEntity.IsDelete,
                Lat = churchEntity.Lat,
                Lon = churchEntity.Lon,
                Phone = churchEntity.Phone,
                SectionId = churchEntity.SectionId,
                SectionName = churchEntity.Section.Name,
                DistrictId = churchEntity.DistrictId,
                DistrictName = churchEntity.District.Name,
                Status = churchEntity.Status,
                WebSite = churchEntity.WebSite
            };
        }
        public async Task<List<Church>> GetChurchBySectionId(int id)
        {
            var churchEntities = await _context.TblChurchNta.Where(a => a.SectionId == id && !a.IsDelete).ToListAsync();
            return churchEntities.Select(a => new Church
            {
                Id = a.Id,
                ChurchName = a.ChurchName,
                Lat = a.Lat,
                Lon = a.Lon,
                Address = a.Address,
                Email = a.Email,
                WebSite = a.WebSite
            }).OrderBy(a => a.ChurchName).ToList();
        }

        public async Task<List<Church>> GetChurchByDistrict(int id)
        {
            var churchEntities = await _context.TblChurchNta.Include(a => a.Section).Where(a => a.Section.DistrictId == id && !a.IsDelete).ToListAsync();
            return churchEntities.Select(a => new Church
            {
                Id = a.Id,
                ChurchName = a.ChurchName,
                Lat = a.Lat,
                Lon = a.Lon,
                Address = a.Address,
                Email = a.Email,
                WebSite = a.WebSite
            }).OrderBy(a => a.ChurchName).ToList();
        }

        public async Task<QueryResult<Church>> Search(ChurchQuery query, string accessCode, int userId)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var userEntity = await _context.TblUserNta.Include(m=> m.TblUserChurchNta).FirstOrDefaultAsync(m => m.Id == userId);

                var churchQuery = _context.TblChurchNta.Include(m => m.Section).Include(m => m.District).Include(m=>m.TblUserChurch).ThenInclude(m=>m.User)
                                  .ThenInclude(m=>m.Role).Where(m=> !m.IsDelete).AsNoTracking().AsQueryable();

                switch (accessCode)
                {
                    case "S":
                        churchQuery = userEntity.DistrictId != null ? churchQuery.Where(n => n.DistrictId == userEntity.DistrictId).AsQueryable() : churchQuery.AsQueryable();
                        break;
                    case "D":
                        churchQuery = userEntity.DistrictId != null ? churchQuery.Where(n => n.DistrictId == userEntity.DistrictId).AsQueryable() : churchQuery.AsQueryable();
                        break;
                    case "P":
                        churchQuery = churchQuery.Where(n => userEntity.TblUserChurchNta.Select(a => a.ChurchId).Contains(n.Id)).AsQueryable();
                        break;
                    case "M":
                        churchQuery = churchQuery.Where(n => userEntity.TblUserChurchNta.Select(a => a.ChurchId).Contains(n.Id)).AsQueryable();
                        break;
                    default:
                        churchQuery = churchQuery.AsQueryable();
                        break;
                }

                if (query.SectionId != 0)
                {
                    churchQuery = churchQuery.Where(m => m.Section.Id == query.SectionId);
                }
                if (query.DistrictId != 0)
                {
                    churchQuery = churchQuery.Where(m => m.Section.District.Id == query.DistrictId);
                }
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    churchQuery = churchQuery.Where(m => m.ChurchName.Contains(query.Filter));
                }
                if (!string.IsNullOrEmpty(query.Email))
                {
                    churchQuery = churchQuery.Where(m => m.Email.Contains(query.Email));
                }
                var count = await churchQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    default:
                        churchQuery = query.Order.StartsWith("-") ? churchQuery.OrderByDescending(m => m.ChurchName) : churchQuery.OrderBy(m => m.ChurchName);
                        break;
                }
                var churchEntities = await churchQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var churchs = churchEntities.Select(m => new Church
                {
                    Id = m.Id,
                    ChurchName = m.ChurchName,
                    Address = m.Address,
                    Phone2 = m.Phone2,
                    AccountNo = m.AccountNo,
                    MailAddress = m.MailAddress,
                    ChurchType = m.ChurchType,
                    Directory = m.Directory,
                    Email = m.Email,
                    IsDelete = m.IsDelete,
                    Lat = m.Lat,
                    Lon = m.Lon,
                    Phone = m.Phone,
                    SectionId = m.SectionId,
                    SectionName = m.Section.Name,
                    DistrictId = m.DistrictId,
                    DistrictName = m.District.Name,
                    Status = m.Status,
                    WebSite = m.WebSite,
                    Pastor = m.TblUserChurch.Count()>0?(m.TblUserChurch.Where(n=>n.User.Role.DataAccessCode == "P").FirstOrDefault()?.User.FirstName + " "+ m.TblUserChurch.Where(n => n.User.Role.DataAccessCode == "P").FirstOrDefault()?.User.LastName):""
                }).ToList();

                return new QueryResult<Church> { Result = churchs, Count = count };
            }
            catch
            {
                return null;
            }
        }
        public async Task<string> Add(Church church, User loggedinUser)
        {
            var churchEntities = new TblChurchNta
            {
                ChurchName = church.ChurchName,
                Address = church.Address,
                Email = church.Email,
                Lat = church.Lat,
                Lon = church.Lon,
                Directory = church.Directory,
                ChurchType = church.ChurchType,
                AccountNo = church.AccountNo,
                MailAddress = church.MailAddress,
                Phone2 = church.Phone2,
                Phone = church.Phone,
                SectionId = church.SectionId,
                DistrictId = church.DistrictId,
                Status = church.Status,
                WebSite = church.WebSite,
                IsDelete = false,
                InsertUser = loggedinUser.Id.ToString(),
                InsertDatetime = DateTime.Now
            };

            try
            {
                _context.TblChurchNta.Add(churchEntities);
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> Edit(Church church, User loggedinUser)
        {
            var churchEntities = await _context.TblChurchNta.FirstOrDefaultAsync(m => m.Id == church.Id);
            churchEntities.ChurchName = church.ChurchName;
            churchEntities.Address = church.Address;
            churchEntities.Email = church.Email;
            churchEntities.Phone2 = church.Phone2;
            churchEntities.MailAddress = church.MailAddress;
            churchEntities.Directory = church.Directory;
            churchEntities.AccountNo = church.AccountNo;
            churchEntities.ChurchType = church.ChurchType;
            churchEntities.Lat = church.Lat;
            churchEntities.Lon = church.Lon;
            churchEntities.Phone = church.Phone;
            churchEntities.SectionId = church.SectionId;
            churchEntities.DistrictId = church.DistrictId;
            churchEntities.Status = church.Status;
            churchEntities.WebSite = church.WebSite;
            churchEntities.UpdateUser = loggedinUser.Id.ToString();
            churchEntities.UpdateDatetime = DateTime.Now;

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
        public async Task<bool> Delete(int id,int userId)
        {
            var churchEntities = await _context.TblChurchNta.FirstOrDefaultAsync(m => m.Id == id);
            churchEntities.IsDelete = true;           

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
