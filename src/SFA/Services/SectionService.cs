using SFA.Models;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface ISectionService
    {
        Task<List<Section>> GetAll();
        Task<Section> GetById(int id);
        Task<List<Section>> GetSectionByDistrictId(int id);
        Task<QueryResult<Section>> Search(SectionQuery query);
        Task<string> Add(Section section, User loggedInUser);
        Task<string> Edit(Section section, User loggedInUser);
        Task<bool> Delete(int id, int userId);
    }
    public class SectionService : ISectionService
    {
        private readonly SFADBContext _context = null;

        public SectionService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<Section>> GetAll()
        {
            var sectionEntities = await _context.TblSectionNta.Include(m => m.District).OrderBy(m => m.Name).ToListAsync();
            return sectionEntities.Select(m => new Section
            {
                Id = m.Id,
                Name = m.Name,
                DistrictName = m.District.Name,
                DistrictId = m.DistrictId,
                //StateName = m.District.State.Name,
                //StateId = m.District.StateId,
                //CountryId = m.District.State.CountryId,
                //CountryName = m.District.State.Country.Name
            }).ToList();
        }
        public async Task<Section> GetById(int id)
        {
            var sectionEntities = await _context.TblSectionNta.Include(m => m.District).FirstOrDefaultAsync(m => m.Id == id);
            return sectionEntities == null ? null : new Section
            {
                Id = sectionEntities.Id,
                Name = sectionEntities.Name,
                DistrictId = sectionEntities.DistrictId,
                //StateId = sectionEntities.District.StateId,
                //CountryId = sectionEntities.District.State.CountryId
            };
        }
        public async Task<List<Section>> GetSectionByDistrictId(int id)
        {
            var sectionEntities = await _context.TblSectionNta.Where(a => a.DistrictId == id).ToListAsync();
            return sectionEntities.Select(a => new Section
            {
                Id = a.Id,
                Name = a.Name
            }).OrderBy(a => a.Name).ToList();
        }
        public async Task<QueryResult<Section>> Search(SectionQuery query)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var sectionQuery = _context.TblSectionNta.Include(m=>m.District).AsNoTracking().AsQueryable();

                //if (query.CountryId != int.Empty)
                //{
                //    sectionQuery = sectionQuery.Where(m => m.District.State.CountryId == query.CountryId);
                //}
                //if (query.StateId != int.Empty)
                //{
                //    sectionQuery = sectionQuery.Where(m => m.District.StateId == query.StateId);
                //}
                if (query.DistrictId > 0)
                {
                    sectionQuery = sectionQuery.Where(m => m.DistrictId == query.DistrictId);
                }
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    sectionQuery = sectionQuery.Where(m => m.Name.Contains(query.Filter));
                }
                var count = await sectionQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    default:
                        sectionQuery = query.Order.StartsWith("-") ? sectionQuery.OrderByDescending(m => m.Name) : sectionQuery.OrderBy(m => m.Name);
                        break;
                }
                var sectionEntities = await sectionQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var sections = sectionEntities.Select(m => new Section
                {
                    Id = m.Id,
                    Name = m.Name,
                    //CountryId = m.District.State.CountryId,
                    //CountryName = m.District.State.Country.Name,
                    //StateName = m.District.State.Name,
                    //StateId = m.District.StateId,
                    DistrictId = m.DistrictId,
                    DistrictName = m.District.Name
                }).ToList();

                return new QueryResult<Section> { Result = sections, Count = count };
            }
            catch
            {
                return null;
            }
        }
        public async Task<string> Add(Section section, User loggedInUser)
        {
            var entity = await _context.TblSectionNta.FirstOrDefaultAsync(m => (m.Name == section.Name && m.DistrictId == section.DistrictId));
            if (entity != null)
            {
                return "Section name is already exists.Kindly change section name";
            }
            var sectionEntities = new TblSectionNta
            {                
                Name = section.Name,
                DistrictId = section.DistrictId,            
                InsertUser = loggedInUser.Id.ToString(),
                InsertDatetime = DateTime.Now
            };

            try
            {
                _context.TblSectionNta.Add(sectionEntities);
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> Edit(Section section, User loggedInUser)
        {
            var entity = await _context.TblSectionNta.FirstOrDefaultAsync(m => (m.Name == section.Name && m.DistrictId == section.DistrictId) && m.Id != section.Id);
            if (entity != null)
            {
                return "Section name is already exists.Kindly change section name";
            }
            var sectionEntities = await _context.TblSectionNta.FirstOrDefaultAsync(m => m.Id == section.Id);
            sectionEntities.Name = section.Name;
            //sectionEntities.Code = section.Code;
            sectionEntities.DistrictId = section.DistrictId;
            sectionEntities.UpdateUser = loggedInUser.Id.ToString();
            sectionEntities.UpdateDatetime = DateTime.Now;

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
        public async Task<bool> Delete(int id, int userId)
        {
            var sectionEntities = await _context.TblSectionNta.FirstOrDefaultAsync(m => m.Id == id);

            //If archive is -1 this means there was an issue. The return should be 1 which is the number of rows that were affected
            //This is calling a stored procedure in the database that will archive deleted records. 
            var archive = _context.Database.ExecuteSqlCommand("exec Global.ArchiveRecords @p0, @p1, @p2;", parameters: new[] { id.ToString(), "Global.Tbl_Section_NTA", userId.ToString() });

            if (sectionEntities == null || archive == -1)
            {
                return false;
            }
            try
            {
                _context.TblSectionNta.Remove(sectionEntities);
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
