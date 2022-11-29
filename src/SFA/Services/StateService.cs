using SFA.Models;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface IStateService
    {
        Task<List<State>> GetAll();
        Task<State> GetById(int id);
        Task<List<State>> GetStateByCountryId(int id);
        Task<QueryResult<State>> Search(StateQuery query);
        Task<string> Add(State state, User loggedInUser);
        Task<string> Edit(State state, User loggedInUser);
        Task<bool> Delete(int id,int userId);
    }
    public class StateService : IStateService
    {
        private readonly SFADBContext _context = null;

        public StateService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<State>> GetAll()
        {
            var stateEntities = await _context.TblStateNta.Include(m => m.Country).OrderBy(m => m.Name).ToListAsync();
            return stateEntities.Select(m => new State
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name + "( " + m.Alias + " )",
                Alias = m.Alias,
                CountryName = m.Country.Name
            }).ToList();
        }
        public async Task<State> GetById(int id)
        {
            var stateEntities = await _context.TblStateNta.FirstOrDefaultAsync(m => m.Id == id);
            return stateEntities == null ? null : new State
            {
                Id = stateEntities.Id,
                Code = stateEntities.Code,
                Name = stateEntities.Name,
                Alias = stateEntities.Alias,
                CountryId = stateEntities.CountryId
            };
        }
        public async Task<List<State>> GetStateByCountryId(int id)
        {
            var stateEntities = await _context.TblStateNta.Where(a => a.CountryId == id).ToListAsync();
            return stateEntities.Select(a => new State
            {
                Id = a.Id,
                Name = a.Name,
                Code = a.Code,
                Alias = a.Alias,
            }).OrderBy(a => a.Name).ToList();
        }
        public async Task<QueryResult<State>> Search(StateQuery query)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var stateQuery = _context.TblStateNta.Include(m => m.Country).AsNoTracking().AsQueryable();

                if (query.CountryId > 0)
                {
                    stateQuery = stateQuery.Where(m => m.CountryId == query.CountryId);
                }
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    stateQuery = stateQuery.Where(m => m.Name.Contains(query.Filter));
                }
                var count = await stateQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    default:
                        stateQuery = query.Order.StartsWith("-") ? stateQuery.OrderByDescending(m => m.Name) : stateQuery.OrderBy(m => m.Name);
                        break;
                }
                var stateEntities = await stateQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var states = stateEntities.Select(m => new State
                {
                    Id = m.Id,
                    Name = m.Name,
                    Alias = m.Alias,
                    Code = m.Code,
                    CountryId = m.CountryId,
                    CountryName = m.Country.Name
                }).ToList();

                return new QueryResult<State> { Result = states, Count = count };
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<string> Add(State state, User loggedInUser)
        {
            var entity = await _context.TblStateNta.FirstOrDefaultAsync(m => (m.Name == state.Name));
            var maxEntity = await _context.TblStateNta.OrderByDescending(m => m.CodeVal).FirstOrDefaultAsync();
            var maxNumber = 1;
            if (maxEntity != null)
            {
                maxNumber = maxEntity.CodeVal + 1;
            }
            if (entity != null)
            {
                
                    return "State name is already exists.Kindly change state name";
                
            }
            var stateEntities = new TblStateNta
            {              
                CodeVal = maxNumber,
                Name = state.Name,
                Alias = state.Alias,               
                CountryId = state.CountryId,
                InsertUser = loggedInUser.Id.ToString(),
                InsertDatetime = DateTime.Now
            };

            try
            {
                _context.TblStateNta.Add(stateEntities);
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> Edit(State state, User loggedInUser)
        {
            var entity = await _context.TblStateNta.FirstOrDefaultAsync(m => (m.Name == state.Name) && m.Id != state.Id);
            if (entity != null)
            {
                return "State name is already exists.Kindly change state name";   
            }
            var stateEntities = await _context.TblStateNta.FirstOrDefaultAsync(m => m.Id == state.Id);
            stateEntities.Name = state.Name;
            stateEntities.Alias = state.Alias;
            //stateEntities.Code = state.Code;
            stateEntities.CountryId = state.CountryId;
            stateEntities.UpdateUser = loggedInUser.Id.ToString();
            stateEntities.UpdateDatetime = DateTime.Now;

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
            var stateEntities = await _context.TblStateNta.FirstOrDefaultAsync(m => m.Id == id);

            //If archive is -1 this means there was an issue. The return should be 1 which is the number of rows that were affected
            //This is calling a stored procedure in the database that will archive deleted records. 
            var archive = _context.Database.ExecuteSqlCommand("exec Global.ArchiveRecords @p0, @p1, @p2;", parameters: new[] { id.ToString(), "Global.Tbl_State_NTA", userId.ToString() });


            if (stateEntities == null || archive == -1)
            {
                return false;
            }
            try
            {
                _context.TblStateNta.Remove(stateEntities);
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
