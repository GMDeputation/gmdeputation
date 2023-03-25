using SFA.Models;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Org.BouncyCastle.Utilities.Collections;

namespace SFA.Services
{
    public interface IDistrictService
    {
        Task<List<District>> GetAll();
        Task<District> GetById(int id);
        Task<List<District>> GetDistricByStateId(int id);
        Task<QueryResult<District>> Search(DistrictQuery query);
        Task<string> Add(District district, User LoggedinUser);
        Task<string> Edit(District district, User LoggedinUser);
        Task<bool> Delete(int id,int userId);
    }
    public class DistrictService : IDistrictService
    {
        private readonly SFADBContext _context = null;

        public DistrictService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<District>> GetAll()
        {
            var districtEntities = await _context.TblDistrictNta.Include(m => m.TblStateDistrictNta).ThenInclude(m => m.State).OrderBy(m => m.Name).ToListAsync();
            return districtEntities.Select(m => new District
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name,
                Alias = m.Alias,
                Website = m.Website,
                StateName = m.TblStateDistrictNta.Count() > 0 ? m.TblStateDistrictNta.Where(m => m.StateId == m.State.Id).FirstOrDefault()?.State.Alias + " - " + m.TblStateDistrictNta.Where(m => m.StateId == m.State.Id).FirstOrDefault()?.State.Name : null
            }).ToList();
        }
        public async Task<District> GetById(int id)
        {
            var districtEntities = await _context.TblDistrictNta.FirstOrDefaultAsync(m => m.Id == id);
            var stateEntities = await _context.TblStateDistrictNta.Include(m => m.State).Where(m => m.DistrictId == id).OrderBy(m=>m.State.Name).ToListAsync();
            return districtEntities == null ? null : new District
            {
                Id = districtEntities.Id,
                Code = districtEntities.Code,
                Name = districtEntities.Name,
                Alias = districtEntities.Alias,
                Website = districtEntities.Website,
                States = stateEntities.Select(m=> new State
                {
                    Id = m.State.Id,
                    Name = m.State.Name + " (" + m.State.Alias + ")"
                }).ToList()
            };
        }
        public async Task<List<District>> GetDistricByStateId(int id)
        {
            var districtEntities = await _context.TblStateDistrictNta.Include(m=>m.District).Where(a => a.StateId == id).ToListAsync();
            return districtEntities.Select(a => new District
            {
                Id = a.District.Id,
                Name = a.District.Name,
                Alias = a.District.Alias,
                Website = a.District.Website
            }).OrderBy(a => a.Name).ToList();
        }

        public async Task<QueryResult<District>> Search(DistrictQuery query)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var districtQuery = _context.TblDistrictNta.Include(m => m.TblStateDistrictNta).ThenInclude(m => m.State).Include(m => m.TblUserNta).ThenInclude(m => m.Role).AsNoTracking().AsQueryable();
                var count = await districtQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    case "code":
                        districtQuery = districtQuery.OrderBy(m => m.Code);
                        break;
                    case "-code":
                        districtQuery = districtQuery.OrderByDescending(m => m.Code);
                        break;
                    case "alias":
                        districtQuery = districtQuery.OrderBy(m => m.Alias);
                        break;
                    case "-alias":
                        districtQuery = districtQuery.OrderByDescending(m => m.Alias);
                        break;
                    default:
                        districtQuery = query.Order.StartsWith("-") ? districtQuery.OrderByDescending(m => m.Name) : districtQuery.OrderBy(m => m.Name);
                        break;
                }
                var districtEntities = await districtQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var districts = districtEntities.Select(m => new District
                {
                    Id = m.Id,
                    Name = m.Name,
                    Code = m.Code,
                    Alias = m.Alias,
                    Website = m.Website,
                    StateName = m.TblStateDistrictNta.Count() > 0 ? m.TblStateDistrictNta.Where(m => m.StateId == m.State.Id).FirstOrDefault()?.State.Alias + " - " + m.TblStateDistrictNta.Where(m => m.StateId == m.State.Id).FirstOrDefault()?.State.Name : null,
                    DGMDUserName = m.TblUserNta.Count() > 0 ? m.TblUserNta.Where(n => n.Role.DataAccessCode == "D").FirstOrDefault()?.FirstName + "  " + m.TblUserNta.Where(n => n.Role.DataAccessCode == "D").FirstOrDefault()?.LastName : null,
                    SGMDUserName = m.TblUserNta.Count() > 0 ? m.TblUserNta.Where(n => n.Role.DataAccessCode == "S").FirstOrDefault()?.FirstName + "  " + m.TblUserNta.Where(n => n.Role.DataAccessCode == "S").FirstOrDefault()?.LastName : null
                }).ToList();

                return new QueryResult<District> { Result = districts, Count = count };
            }
            catch
            {
                return null;
            }
        }
        public async Task<string> Add(District district, User LoggedinUser)
        {
            var entity = await _context.TblDistrictNta.FirstOrDefaultAsync(m => (m.Name == district.Name));
            var maxEntity = await _context.TblDistrictNta.OrderByDescending(m => m.CodeVal).FirstOrDefaultAsync();
            var maxNumber = 1;
            if (maxEntity != null)
            {
                maxNumber = maxEntity.CodeVal + 1;
            }
            if (entity != null)
            {
                return "The District Name entered already exists. Please either change the district name and try again or use the existing name.";
            }
            var districtEntities = new TblDistrictNta
            {               
                CodeVal = maxNumber,
                Alias = district.Alias,
                Name = district.Name,
                Website = district.Website,
                InsertUser = LoggedinUser.Id.ToString(),
                InsertDatetime = DateTime.Now
            };
            
            _context.TblDistrictNta.Add(districtEntities);
            //Needed to save first so you get the ID for the relationship for the distract state table. 
            //Otherwise the ID would be 0 and we would get a FK relationship error
            await _context.SaveChangesAsync();

            if (district.States != null)
            {
                var districtStateEntities = district.States.Select(m => new TblStateDistrictNta
                {
                    DistrictId = districtEntities.Id,
                    StateId = m.Id,
                    InsertUser = LoggedinUser.Id.ToString(),
                    InsertDatetime = DateTime.Now
                }).ToList();

                _context.TblStateDistrictNta.AddRange(districtStateEntities);
                
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

        public async Task<string> Edit(District district, User LoggedinUser)
        {
            var entity = await _context.TblDistrictNta.FirstOrDefaultAsync(m => (m.Name == district.Name) && m.Id != district.Id);
            if (entity != null)
            {
                return "The District name entered already exists. Please use a different district name and try again.";
            }
            var districtEntities = await _context.TblDistrictNta.Include(m=>m.TblStateDistrictNta).FirstOrDefaultAsync(m => m.Id == district.Id);
            districtEntities.Name = district.Name;
            districtEntities.Alias = district.Alias;
            districtEntities.Website = district.Website;
            districtEntities.UpdateUser = LoggedinUser.Id.ToString();
            districtEntities.UpdateDatetime = DateTime.Now;

            try
            {
                _context.TblStateDistrictNta.RemoveRange(districtEntities.TblStateDistrictNta);

                await _context.SaveChangesAsync();
                var districtStateEntities = district.States.Select(m => new TblStateDistrictNta
                {
                    DistrictId = district.Id,
                    StateId = m.Id,
                    InsertUser = LoggedinUser.Id.ToString(),
                    InsertDatetime = DateTime.Now,
                    UpdateUser = LoggedinUser.Id.ToString(),
                    UpdateDatetime = DateTime.Now
                }).ToList();

                await _context.TblStateDistrictNta.AddRangeAsync(districtStateEntities);

                await _context.SaveChangesAsync();

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<bool> Delete(int id, int userId)
        {
            var districtEntities = await _context.TblDistrictNta.FirstOrDefaultAsync(m => m.Id == id);

            //If archive is -1 this means there was an issue. The return should be 1 which is the number of rows that were affected
            //This is calling a stored procedure in the database that will archive deleted records.
            var archive = _context.Database.ExecuteSqlCommand("exec Global.ArchiveRecords @p0, @p1, @p2;", parameters: new[] { id.ToString(), "Global.Tbl_District_NTA", userId.ToString() });

            if (districtEntities == null || archive == -1)
            {
                return false;
            }
            try
            {
                _context.TblDistrictNta.Remove(districtEntities);
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
