using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SFA.Entities;
using SFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetAll();
        Task<QueryResult<Role>> Search(RoleQuery query);
        Task<Role> GetById(int id);
        Task<bool> Save(Role role,User loggedinUser);
    }
    public class RoleService : IRoleService
    {
        private readonly SFADBContext _context = null;

        public RoleService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAll()
        {
            var roleEntities = await _context.TblRoleNta.Where(m => m.IsActive == true).OrderBy(m => m.Name).ToListAsync();
            return roleEntities.Select(m => new Role
            {
                Id = m.Id,
                Name = m.Name,
                IsActive = m.IsActive,
                DataAccessCode = m.DataAccessCode
            }).ToList();
        }

        public async Task<QueryResult<Role>> Search(RoleQuery query)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var roleQuery = _context.TblRoleNta.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(query.Name))
                {
                    roleQuery = roleQuery.Where(m => m.Name.Contains(query.Name));
                }
                var count = await roleQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    default:
                        roleQuery = query.Order.StartsWith("-") ? roleQuery.OrderByDescending(m => m.Name) : roleQuery.OrderBy(m => m.Name);
                        break;
                }
                var roleEntities = await roleQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var roles = roleEntities.Select(m => new Role
                {
                    Id = m.Id,
                    Name = m.Name,
                    IsActive = m.IsActive,
                    DataAccessCode = m.DataAccessCode
                }).ToList();

                return new QueryResult<Role> { Result = roles, Count = count };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Role> GetById(int id)
        {
            var roleEntity = await _context.TblRoleNta.FirstOrDefaultAsync(m => m.Id == id);
            return roleEntity == null ? null : new Role
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name,
                IsActive = roleEntity.IsActive,
                DataAccessCode = roleEntity.DataAccessCode
            };
        }
        public async Task<bool> Save(Role role, User loggedinUser)
        {
            if (role.Id  == 0)
            {
                var roleEntity = new TblRoleNta
                {
                    Name = role.Name,
                    DataAccessCode = role.DataAccessCode,
                    IsActive = role.IsActive,
                    InsertDatetime = DateTime.Now,
                    InsertUser = loggedinUser.Id.ToString(),


                };
                _context.TblRoleNta.Add(roleEntity);
            }
            else
            {
                var roleEntity = await _context.TblRoleNta.FirstOrDefaultAsync(m => m.Id == role.Id);
                roleEntity.Name = role.Name;
                roleEntity.IsActive = role.IsActive;
                roleEntity.DataAccessCode = role.DataAccessCode;
                roleEntity.UpdateUser = loggedinUser.Id.ToString();
                roleEntity.UpdateDatetime = DateTime.Now;
            }
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
