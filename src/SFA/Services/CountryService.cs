using SFA.Models;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetAll();
        Task<Country> GetById(int id);
        Task<QueryResult<Country>> Search(CountryQuery query);
        Task<string> Add(Country country, User LoggedinUser);
        Task<string> Edit(Country country, User LoggedinUser);
        Task<bool> Delete(int id,int userId);
        //Task<QueryResult<Country>> Search(CountryQuery query);
    }

    public class CountryService : ICountryService
    {
        private readonly SFADBContext _context = null;

        public CountryService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAll()
        {
            var countryEntities = await _context.TblCountryNta.OrderBy(m => m.Name).ToListAsync();
            return countryEntities.Select(m => new Country
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name
            }).ToList();
        }
        public async Task<Country> GetById(int id)
        {
            var countryEntity = await _context.TblCountryNta.FirstOrDefaultAsync(m => m.Id == id);
            return countryEntity == null ? null : new Country
            {
                Id = countryEntity.Id,
                Code = countryEntity.Code,
                Name = countryEntity.Name,
                Alpha2Code = countryEntity.Alpha2Code,
                Alpha3Code = countryEntity.Alpha3Code,
                FrenchName = countryEntity.FrenchName,
                CountryCode = countryEntity.CountryCode
            };
        }

        public async Task<QueryResult<Country>> Search(CountryQuery query)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var countryQuery = _context.TblCountryNta.AsNoTracking().AsQueryable();
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    countryQuery = countryQuery.Where(m => m.Name.Contains(query.Filter) || m.Alpha2Code.Contains(query.Filter) || m.Alpha3Code.Contains(query.Filter));
                }
                var count = await countryQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    default:
                        countryQuery = query.Order.StartsWith("-") ? countryQuery.OrderByDescending(m => m.Name) : countryQuery.OrderBy(m => m.Name);
                        break;
                }
                var stateEntities = await countryQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var countries = stateEntities.Select(m => new Country
                {
                    Id = m.Id,
                    Name = m.Name,
                    Alpha2Code = m.Alpha2Code,
                    Alpha3Code = m.Alpha3Code,
                    FrenchName = m.FrenchName,
                    Code = m.Code,
                    CountryCode = m.CountryCode
                }).ToList();

                return new QueryResult<Country> { Result = countries, Count = count };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> Add(Country country, User LoggedinUser)
        {
            var entity = await _context.TblCountryNta.FirstOrDefaultAsync(m => (m.Name == country.Name));
            var maxEntity = await _context.TblCountryNta.OrderByDescending(m => m.CodeVal).FirstOrDefaultAsync();
            var maxNumber = 1;
            if(maxEntity != null)
            {
                maxNumber = maxEntity.CodeVal + 1;
            }
            if(entity!=null)
            {
                
                return "Country name is already exists.Kindly change country name";
            }
            var countryEntity = new TblCountryNta
            {
                CodeVal = maxNumber,
                Name = country.Name,
                Alpha2Code = country.Alpha2Code,
                Alpha3Code = country.Alpha3Code,
                FrenchName = country.FrenchName,         
                InsertUser = LoggedinUser.Id.ToString(),
                InsertDatetime = DateTime.Now,
                CountryCode = country.CountryCode
            };

            try
            {
                _context.TblCountryNta.Add(countryEntity);
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> Edit(Country country, User LoggedinUser)
        {
            var entity = await _context.TblCountryNta.FirstOrDefaultAsync(m => (m.Name == country.Name) && m.Id != country.Id);
            if (entity != null)
            {
                
                return "Country name is already exists.Kindly change country name";
                
            }
            var countryEntity = await _context.TblCountryNta.FirstOrDefaultAsync(m => m.Id == country.Id);
            countryEntity.Name = country.Name;
            countryEntity.Alpha2Code = country.Alpha2Code;
            countryEntity.Alpha3Code = country.Alpha3Code;
            countryEntity.FrenchName = country.FrenchName;
            countryEntity.UpdateUser = LoggedinUser.Id.ToString();
            countryEntity.UpdateDatetime = DateTime.Now;
            countryEntity.CountryCode = country.CountryCode;

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
            var countryEntity = await _context.TblCountryNta.FirstOrDefaultAsync(m => m.Id == id);
           
            //If archive is -1 this means there was an issue. The return should be 1 which is the number of rows that were affected
            //This is calling a stored procedure in the database that will archive deleted records. 
            var archive = _context.Database.ExecuteSqlCommand("exec Global.ArchiveRecords @p0, @p1, @p2;", parameters: new[] { id.ToString(), "Global.Tbl_Country_NTA", userId.ToString() });

            if (countryEntity == null || archive == -1)
            {
                return false;
            }
            try
            {
                _context.TblCountryNta.Remove(countryEntity);
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