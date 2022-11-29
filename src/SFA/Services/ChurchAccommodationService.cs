using SFA.Models;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SFA.Services
{
    public interface IChurchAccommodationService
    {
        Task<List<ChurchAccommodation>> GetAll();
        Task<ChurchAccommodation> GetById(int id);
        Task<QueryResult<ChurchAccommodation>> Search(ChurchAccommodationQuery query, string accessCode, int userId);
        Task<string> Save(ChurchAccommodation accommodation);
    }
    public class ChurchAccommodationService : IChurchAccommodationService
    {
        private readonly SFADBContext _context = null;

        public ChurchAccommodationService(SFADBContext context)
        {
            _context = context;
        }


        public async Task<List<ChurchAccommodation>> GetAll()
        {
            var accommodationeEntities = await _context.TblChurchAccommodationNta.Include(m => m.Church).OrderBy(m => m.AccomType).ToListAsync();

            return accommodationeEntities.Select(m => new ChurchAccommodation
            {
                Id = m.Id,
                ChurchId = m.ChurchId,
                AccomType = m.AccomType,
                AccomNotes = m.AccomNotes,
                ChurchName = m.Church?.ChurchName
            }).ToList();
        }

        public async Task<ChurchAccommodation> GetById(int id)
        {
            var accommodationEntity = await _context.TblChurchAccommodationNta.Include(m => m.Church).FirstOrDefaultAsync(m => m.Id == id);

            return accommodationEntity == null ? null : new ChurchAccommodation
            {
                Id = accommodationEntity.Id,
                ChurchId = accommodationEntity.ChurchId,
                AccomType = accommodationEntity.AccomType,
                AccomNotes = accommodationEntity.AccomNotes,
                ChurchName = accommodationEntity.Church?.ChurchName
            };
        }


        public async Task<QueryResult<ChurchAccommodation>> Search(ChurchAccommodationQuery query, string accessCode, int userId)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var userEntity = await _context.TblUserNta.Include(m => m.TblUserChurchNta).FirstOrDefaultAsync(m => m.Id == userId);

                var accommodationQuery = _context.TblChurchAccommodationNta.Include(m => m.Church).AsNoTracking().AsQueryable();

                var listofacc = _context.TblChurchAccommodationNta.ToList();

                foreach(var acc in listofacc)
                {
                    Console.WriteLine(acc.Id);
                }

                switch (accessCode)
                {
                    case "S":
                        accommodationQuery = userEntity.DistrictId != null ? accommodationQuery.Where(n => n.Church.DistrictId == userEntity.DistrictId).AsQueryable() : accommodationQuery.AsQueryable();
                        break;
                    case "D":
                        accommodationQuery = userEntity.DistrictId != null ? accommodationQuery.Where(n => n.Church.DistrictId == userEntity.DistrictId).AsQueryable() : accommodationQuery.AsQueryable();
                        break;
                    case "P":
                        accommodationQuery = accommodationQuery.Where(n => userEntity.TblUserChurchNta.Select(a => a.ChurchId).Contains(n.Church.Id)).AsQueryable();
                        break;
                    case "M":
                        accommodationQuery = accommodationQuery.Where(n => userEntity.TblUserChurchNta.Select(a => a.ChurchId).Contains(n.Church.Id)).AsQueryable();
                        break;
                    default:
                        accommodationQuery = accommodationQuery.AsQueryable();
                        break;
                }

                if (!string.IsNullOrEmpty(query.Name))
                {
                    accommodationQuery = accommodationQuery.Where(m => m.AccomType.Contains(query.Name) || m.Church.ChurchName.Contains(query.Name));
                }
                var count = await accommodationQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    case "type":
                        accommodationQuery = accommodationQuery.OrderBy(m => m.AccomType);
                        break;
                    case "-type":
                        accommodationQuery = accommodationQuery.OrderByDescending(m => m.AccomType);
                        break;
                    default:
                        accommodationQuery = query.Order.StartsWith("-") ? accommodationQuery.OrderByDescending(m => m.Church.ChurchName) : accommodationQuery.OrderBy(m => m.Church.ChurchName);
                        break;
                }
                var accommodationEntities = await accommodationQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var accommodations = accommodationEntities.Select(m => new ChurchAccommodation
                {
                    Id = m.Id,
                    ChurchId = m.ChurchId,
                    AccomType = m.AccomType,
                    AccomNotes = m.AccomNotes,
                    ChurchName = m.Church?.ChurchName
                }).ToList();

                return new QueryResult<ChurchAccommodation> { Result = accommodations, Count = count };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> Save(ChurchAccommodation accommodation)
        {
            if (accommodation.Id == 0)
            {
                var accommodationEntity = new TblChurchAccommodationNta
                {                   
                    ChurchId = accommodation.ChurchId,
                    AccomType = accommodation.AccomType,
                    AccomNotes = accommodation.AccomNotes
                };
                _context.TblChurchAccommodationNta.Add(accommodationEntity);
            }
            else
            {
                var accommodationEntity = await _context.TblChurchAccommodationNta.Include(m => m.Church).FirstOrDefaultAsync(m => m.Id == accommodation.Id);

                accommodationEntity.ChurchId = accommodation.ChurchId;
                accommodationEntity.AccomType = accommodation.AccomType;
                accommodationEntity.AccomNotes = accommodation.AccomNotes;
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
