using SFA.Models;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface IServiceTypeService
    {
        Task<List<ServiceType>> GetAll();
        Task<ServiceType> GetById(int id);
        Task<QueryResult<ServiceType>> Search(ServiceTypeQuery query);
        Task<string> Save(ServiceType serviceType);
        Task<bool> Delete(int id, int userId);
    }
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly SFADBContext _context = null;

        public ServiceTypeService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceType>> GetAll()
        {
            var serviceTypeEntities = await _context.TblServiceTypeNta.OrderBy(m => m.Name).ToListAsync();
            return serviceTypeEntities.Select(m => new ServiceType
            {
                Id = m.Id,
                Name = m.Name
            }).ToList();
        }
        public async Task<ServiceType> GetById(int id)
        {
            var serviceTypeEntity = await _context.TblServiceTypeNta.FirstOrDefaultAsync(m => m.Id == id);
            return serviceTypeEntity == null ? null : new ServiceType
            {
                Id = serviceTypeEntity.Id,
                Name = serviceTypeEntity.Name,
            };
        }

        public async Task<QueryResult<ServiceType>> Search(ServiceTypeQuery query)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var serviceTypeQuery = _context.TblServiceTypeNta.AsNoTracking().AsQueryable();
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    serviceTypeQuery = serviceTypeQuery.Where(m => m.Name.Contains(query.Filter));
                }
                var count = await serviceTypeQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    default:
                        serviceTypeQuery = query.Order.StartsWith("-") ? serviceTypeQuery.OrderByDescending(m => m.Name) : serviceTypeQuery.OrderBy(m => m.Name);
                        break;
                }
                var serviceTypeEntities = await serviceTypeQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var serviceTypes = serviceTypeEntities.Select(m => new ServiceType
                {
                    Id = m.Id,
                    Name = m.Name,
                }).ToList();

                return new QueryResult<ServiceType> { Result = serviceTypes, Count = count };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> Save(ServiceType serviceType)
        {
            if (serviceType.Id == 0)
            {
                var serviceTypeEntity = new TblServiceTypeNta
                {      
                    Name = serviceType.Name,  
                    InsertUser = serviceType.CreatedBy.ToString(),
                    InsertDatetime = DateTime.Now
                };
                _context.TblServiceTypeNta.Add(serviceTypeEntity);
            }
            else
            {
                var serviceTypeEntity = await _context.TblServiceTypeNta.FirstOrDefaultAsync(m => m.Id == serviceType.Id);
                serviceTypeEntity.Name = serviceType.Name;
                serviceTypeEntity.UpdateUser = serviceType.ModifiedBy.ToString();
                serviceTypeEntity.UpdateDatetime = DateTime.Now;
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

        public async Task<bool> Delete(int id, int userId)
        {
            var serviceTypeEntity = await _context.TblServiceTypeNta.FirstOrDefaultAsync(m => m.Id == id);

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
