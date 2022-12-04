using Microsoft.EntityFrameworkCore;
using SFA.Entities;
using SFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<List<User>> GetByRoleId(int roleId);
        Task<QueryResult<User>> Search(UserQuery query);
        Task<bool> ChangePassword(int userId, User user);
        Task<int> Save(User user, User loggedinUser);
        Task<User> Validate(string email, string password);
        Task<List<RoleMenu>> GetMenuByUser(int userId);
        //Task<List<ApplicationAccess>> GetAccessList(int id);
        //Task<List<UserApplication>> GetMenuByUser(int userId);
        Task<bool> SaveLogIn(int id);
        Task<bool> SaveLogOut(int id);
        Task<bool> VerifyMail(int otp, int userId);
        Task<bool> SaveOTP(int otp, int userId);

        Task<string> UpdateDistrictAndSection(List<User> users);
        Task<List<User>> GetAllMissionariesUser();
    }

    public class UserService : IUserService
    {
        private readonly SFADBContext _context = null;
        private readonly IMenuService _menuService = null;
        private readonly IMenuGroupService _menuGroupService = null;

        public UserService(SFADBContext context, IMenuService menuService, IMenuGroupService menuGroupService)
        {
            _context = context;
            _menuService = menuService;
            _menuGroupService = menuGroupService;
        }
        public async Task<bool> SaveLogIn(int id)
        {
            TblUserLogNta tblUserLog = await _context.TblUserLogNta.Where((TblUserLogNta m) => m.UserId == id).FirstOrDefaultAsync();
            if (tblUserLog != null)
            {
                tblUserLog.LoginTime = DateTime.Now;
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveLogOut(int id)
        {
            TblUserLogNta tblUserLog = await _context.TblUserLogNta.Where((TblUserLogNta m) => m.UserId == id && m.LogoutTime == null).FirstOrDefaultAsync();
            if (tblUserLog != null)
            {
                tblUserLog.LogoutTime = DateTime.Now;
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> UpdateDistrictAndSection(List<User> users)
        {
            foreach (User item in users)
            {
                TblUserNta tblUser = await _context.TblUserNta.FirstOrDefaultAsync((TblUserNta m) => m.Id == item.Id);
                tblUser.DistrictId = item.DistrictId;
                tblUser.SectionId = item.SectionId;
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


        public async Task<bool> ChangePassword(int userId, User user)
        {
            var userEntity = await _context.TblUserNta.FirstOrDefaultAsync(m => m.Id == userId);

            if (userEntity == null)
            {
                return false;
            }

            var passwordEntity = new TblUserPasswordNta
            {       
                InsertDatetime = DateTime.Now,
                Password = user.NewPassword,
                UserId = userId
            };
            try
            {
                _context.TblUserPasswordNta.Add(passwordEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> SaveOTP(int otp, int userId)
        {
            (await _context.TblUserNta.FirstOrDefaultAsync((TblUserNta m) => m.Id == userId)).Otp = otp;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> VerifyMail(int otp, int userId)
        {
            TblUserNta tblUser = await _context.TblUserNta.FirstOrDefaultAsync((TblUserNta m) => m.Id == userId);
            if (tblUser.Otp != otp)
            {
                return false;
            }
            tblUser.IsEmailVerify = true;
            tblUser.VerifiedOn = DateTime.Now;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<User>> GetAll()
        {
            var userEntities = await _context.TblUserNta.Include(m => m.Role).Include(m => m.Section).Include(m => m.District).Include(m => m.Country).OrderBy(m => m.Email).ToListAsync();
            return userEntities.Select(m => new User
            {
                Id = m.Id,
                MiddleName = m.MiddleName,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Address = m.Address,
                InsertDatetime = (DateTime)m.InsertDatetime,
                Gender = m.Gender,
                ImageFile = m.ImageFile,
                ImageSequence = m.ImageSequence,
                IsEmailVerify = m.IsEmailVerify,
                Lat = m.Lat,
                Long = m.Long,
                Name = m.FirstName + " " + m.LastName,
                Phone = m.Phone,
                Zipcode = m.Zipcode,
                SectionId = (int)m.SectionId,
                SectionName = m.Section.Name,
                DistrictId = (int)m.DistrictId,
                DistrictName = m.District?.Name,
                CountryId = (int)m.CountryId,
                CountryName = m.Country?.Name,
                UserName = m.UserName,
                Email = m.Email,
                RoleName = m.Role.Name,
                RoleId = m.RoleId,
                IsSuperAdmin = m.IsSuperAdmin,
                IsActive = m.IsActive,
                City = m.City,
                WorkPhoneNo = m.WorkPhoneNo,
                TelePhoneNo = m.TelePhoneNo,
                Status = m.Status,
            }).ToList();
        }

        public async Task<List<User>> GetByRoleId(int roleId)
        {
            var userEntities = await _context.TblUserNta.Include(m => m.Role).Where(m => m.RoleId == roleId).ToListAsync();
            return userEntities.Select(m => new User
            {
                Id = m.Id,
                Name = m.FirstName + " " + m.LastName,
                Email = m.Email,
                RoleName = m.Role.Name,
                RoleId = m.RoleId,
                IsSuperAdmin = m.IsSuperAdmin,
                IsActive = m.IsActive
            }).ToList();
        }

        public async Task<List<User>> GetAllMissionariesUser()
        {
            var userEntities = await _context.TblUserNta.Include(m => m.Role).Where(m => m.Role.DataAccessCode =="M").ToListAsync();
            return userEntities.Select(m => new User
            {
                Id = m.Id,
                Name = m.FirstName + " " + m.LastName,
                IsSuperAdmin = m.IsSuperAdmin,
                IsActive = m.IsActive
            }).ToList();
        }

        public async Task<User> GetById(int id)
        {

            try
            {
                var userEntity = await _context.TblUserNta.Include(m => m.Role).Include(m => m.TblUserChurchNta).ThenInclude(m => m.Church).Include(m => m.TblUserAttributeNta)
                                .ThenInclude(m => m.Attribute).Include(m => m.Section).Include(m => m.District).Include(m => m.Country).FirstOrDefaultAsync(m => m.Id == id);
                var attributeEntities = await _context.TblAttributeNta.ToListAsync();


                return new User
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    MiddleName = userEntity.MiddleName,
                    LastName = userEntity.LastName,
                    Address = userEntity.Address,
                    InsertDatetime = (DateTime)userEntity.InsertDatetime,
                    Gender = userEntity.Gender,
                    ImageFile = userEntity.ImageFile,
                    ImageSequence = userEntity.ImageSequence,
                    IsEmailVerify = userEntity.IsEmailVerify,
                    Lat = userEntity.Lat,
                    Long = userEntity.Long,
                    Name = userEntity.FirstName + " " + userEntity.LastName,
                    Phone = userEntity.Phone,
                    Zipcode = userEntity.Zipcode,
                    SectionId = userEntity.SectionId,
                    SectionName = userEntity.Section?.Name,
                    DistrictId = userEntity.DistrictId,
                    DistrictName = userEntity.District?.Name,
                    CountryId = userEntity.CountryId,
                    CountryName = userEntity.Country?.Name,
                    UserName = userEntity.UserName,
                    Email = userEntity.Email,
                    RoleName = userEntity.Role.Name,
                    RoleId = userEntity.RoleId,
                    IsSuperAdmin = userEntity.IsSuperAdmin,
                    IsActive = userEntity.IsActive,
                    City = userEntity.City,
                    WorkPhoneNo = userEntity.WorkPhoneNo,
                    TelePhoneNo = userEntity.TelePhoneNo,
                    Status = userEntity.Status,
                    Attributes = userEntity.TblUserAttributeNta.Select(m => new UserAttribute
                    {            
                        AttributeId = m.AttributeId,
                        AttributeTypeId = m.Attribute.AttributeTypeId,
                        AttributeValue = m.AttributeValue,
                        UserId = userEntity.Id,
                        Notes = m.Notes,
                        Butes = attributeEntities.Where(n => n.AttributeTypeId == m.Attribute.AttributeTypeId).Select(n => new AttributeModel
                        {
                            Id = n.Id,
                            AttributeName = n.AttributeName
                        }).ToList()
                    }).ToList(),
                    Churches = userEntity.TblUserChurchNta.Select(m => new UserChurch
                    {
                        ChurchId = m.ChurchId,
                        ChurchName = m.Church.ChurchName,
                        CreatedOn = DateTime.Now,
                        RelationType = m.RelationType,
                        UserId = userEntity.Id,
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


        }

        public async Task<QueryResult<User>> Search(UserQuery query)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var userQuery = _context.TblUserNta.Include(m => m.Role).Include(m => m.Section).Include(m => m.District).Include(m => m.Country).AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(query.Name))
                {
                    userQuery = userQuery.Where(m => m.FirstName.Contains(query.Name) || m.LastName.Contains(query.Name) || m.Role.Name.Contains(query.Name) || m.Email.Contains(query.Name) 
                                || m.Phone.Contains(query.Name) || m.District.Name.Contains(query.Name) || m.Section.Name.Contains(query.Name));
                }
                var count = await userQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    case "role":
                        userQuery = userQuery.OrderBy(m => m.Role.Name);
                        break;
                    case "-role":
                        userQuery = userQuery.OrderByDescending(m => m.Role.Name);
                        break;
                    case "email":
                        userQuery = userQuery.OrderBy(m => m.Email);
                        break;
                    case "-email":
                        userQuery = userQuery.OrderByDescending(m => m.Email);
                        break;
                    case "phone":
                        userQuery = userQuery.OrderBy(m => m.Phone);
                        break;
                    case "-phone":
                        userQuery = userQuery.OrderByDescending(m => m.Phone);
                        break;
                    case "district":
                        userQuery = userQuery.OrderBy(m => m.District.Name);
                        break;
                    case "-district":
                        userQuery = userQuery.OrderByDescending(m => m.District.Name);
                        break;
                    case "section":
                        userQuery = userQuery.OrderBy(m => m.Section.Name);
                        break;
                    case "-section":
                        userQuery = userQuery.OrderByDescending(m => m.Section.Name);
                        break;
                    default:
                        userQuery = query.Order.StartsWith("-") ? userQuery.OrderByDescending(m => m.FirstName) : userQuery.OrderBy(m => m.FirstName);
                        break;
                }
                var userEntities = await userQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var users = userEntities.Select(m => new User
                {
                    Id = m.Id,
                    MiddleName = m.MiddleName,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Address = m.Address,
                    InsertDatetime = m.InsertDatetime,
                    Gender = m.Gender,
                    ImageFile = m.ImageFile,
                    ImageSequence = m.ImageSequence,
                    IsEmailVerify = m.IsEmailVerify,
                    Lat = m.Lat,
                    Long = m.Long,
                    Name = m.FirstName + " " + m.LastName,
                    Phone = m.Phone,
                    Zipcode = m.Zipcode,
                    SectionId = m.SectionId,
                    SectionName = m.Section?.Name,
                    DistrictId = m.DistrictId,
                    DistrictName = m.District?.Name,
                    CountryId = m.CountryId,
                    CountryName = m.Country?.Name,
                    UserName = m.UserName,
                    Email = m.Email,
                    RoleName = m.Role.Name,
                    RoleId = m.RoleId,
                    IsSuperAdmin = m.IsSuperAdmin,
                    IsActive = m.IsActive,
                    ActiveStatus = m.IsActive ? "Active" : "In-Active",
                    UserStatus = m.IsSuperAdmin ? "Yes" : "No",
                    City = m.City,
                    WorkPhoneNo = m.WorkPhoneNo,
                    TelePhoneNo = m.TelePhoneNo,
                    Status = m.Status,
                }).ToList();

                return new QueryResult<User> { Result = users, Count = count };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int> Save(User user,User loggedinUser)
        {
            try
            {
                var existingEntity = await _context.TblUserNta.Where(m => m.Email.ToLower() == user.Email.ToLower()).FirstOrDefaultAsync();
                if (existingEntity != null && user.Id == 0)
                {
                    return -1;
                }

                var userEntity = new TblUserNta();        
                if (user.Id == 0)
                {      
                    userEntity.FirstName = user.FirstName;
                    userEntity.LastName = user.LastName;
                    userEntity.MiddleName = user.MiddleName;
                    userEntity.ImageFile = user.ImageFile;
                    userEntity.ImageSequence = user.ImageSequence;
                    userEntity.Address = user.Address;
                    userEntity.InsertDatetime = DateTime.Now;
                    userEntity.InsertUser = loggedinUser.Id.ToString();
                    userEntity.Gender = user.Gender;
                    userEntity.IsEmailVerify = false;
                    userEntity.Lat = user.Lat;
                    userEntity.Long = user.Long;
                    userEntity.Phone = user.Phone;
                    userEntity.Zipcode = user.Zipcode;
                    userEntity.SectionId = user.SectionId;
                    userEntity.DistrictId = user.DistrictId;
                    userEntity.CountryId = user.CountryId;
                    userEntity.UserName = user.UserName;
                    userEntity.Email = user.Email;
                    userEntity.IsSuperAdmin = user.IsSuperAdmin;
                    userEntity.IsActive = user.IsActive;
                    userEntity.RoleId = user.RoleId;

                    var attributes = user.Attributes.Select(m => new TblUserAttributeNta
                    {                        
                        AttributeId = m.AttributeId,
                        AttributeValue = m.AttributeValue,
                        UserId = user.Id,
                        Notes = m.Notes,
                    }).ToList();

                    userEntity.TblUserAttributeNta = attributes;

                    var churches = user.Churches.Select(m => new TblUserChurchNta
                    {
                        ChurchId = m.ChurchId,
                        InsertDatetime = DateTime.Now,
                        RelationType = m.RelationType,
                        UserId = user.Id,
                    }).ToList();

                    userEntity.TblUserChurchNta = churches;

                    var model = new TblUserPasswordNta
                    {                       
                        InsertDatetime = DateTime.Now,                  
                        Password = user.Password,
                        UserId = user.Id,
                    };

                    userEntity.TblUserPasswordNta.Add(model);

                    _context.TblUserNta.Add(userEntity);
                }
                //This is the update code
                else
                {
                    existingEntity = await _context.TblUserNta.Where(m => m.Email.ToLower() == user.Email.ToLower() && m.Id != user.Id).FirstOrDefaultAsync();
                    if (existingEntity != null)
                    {
                        return -1;
                    }

                    userEntity = await _context.TblUserNta.Include(m => m.TblUserPasswordNta).Include(m => m.TblUserChurchNta).Include(m => m.TblUserAttributeNta).FirstOrDefaultAsync(m => m.Id == user.Id);

                    userEntity.FirstName = user.FirstName;
                    userEntity.LastName = user.LastName;
                    userEntity.MiddleName = user.MiddleName;
                    userEntity.ImageFile = user.ImageFile;
                    userEntity.ImageSequence = user.ImageSequence;
                    userEntity.Address = user.Address;
                    userEntity.Gender = user.Gender;
                    userEntity.Lat = user.Lat;
                    userEntity.Long = user.Long;
                    userEntity.Phone = user.Phone;
                    userEntity.Zipcode = user.Zipcode;
                    userEntity.SectionId = user.SectionId;
                    userEntity.DistrictId = user.DistrictId;
                    userEntity.CountryId = user.CountryId;
                    userEntity.UserName = user.UserName;
                    userEntity.Email = user.Email;
                    userEntity.IsSuperAdmin = user.IsSuperAdmin;
                    userEntity.IsActive = user.IsActive;
                    userEntity.RoleId = user.RoleId;
                    userEntity.UpdateDatetime = DateTime.Now;
                    userEntity.UpdateUser = loggedinUser.Id.ToString();

                    _context.TblUserAttributeNta.RemoveRange(userEntity.TblUserAttributeNta);
                    _context.TblUserChurchNta.RemoveRange(userEntity.TblUserChurchNta);

                    await _context.SaveChangesAsync();

                    var attributes = user.Attributes.Select(m => new TblUserAttributeNta
                    {          
                        AttributeId = m.AttributeId,
                        AttributeValue = m.AttributeValue,
                        UserId = user.Id,
                        Notes = m.Notes,
                    }).ToList();

                    userEntity.TblUserAttributeNta = attributes;

                    var churches = user.Churches.Select(m => new TblUserChurchNta
                    {
                        ChurchId = m.ChurchId,
                        InsertDatetime = DateTime.Now,
                        RelationType = m.RelationType,
                        UserId = user.Id,
                    }).ToList();

                    userEntity.TblUserChurchNta = churches;

                    //var model = new TblUserPassword
                    //{
                    //    Id = int.Newint(),
                    //    CreatedOn = DateTime.Now,
                    //    IsActive = true,
                    //    Password = user.Password,
                    //    UserId = userId,
                    //};

                    //userEntity.TblUserPassword.Add(model);
                }

                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<User> Validate(string email, string password)
        {
            foreach(var user in _context.TblUserNta.ToList())
            {
                Console.WriteLine(user.FirstName);
            }
           // var testEmail =  _context.TblUserNta.Where(m => m.Email == email).FirstOrDefault();
            var userEntity = await _context
                .TblUserNta.Include(m => m.Role).Include(m => m.TblUserPasswordNta)
                .FirstOrDefaultAsync(m => m.Email.Equals(email)
                 && m.TblUserPasswordNta.OrderByDescending(n => n.InsertDatetime).FirstOrDefault().Password.Equals(password)
                 && m.IsActive);
                Console.WriteLine("Test");
            if (userEntity == null)
            {
                return null;
            }
            var menuGroups = await _menuGroupService.GetAll();

            var userRole = await _menuGroupService.GetRoleMenuAccess(userEntity.Id);

            if (userEntity == null)
            {
                return null;
            }

            return new User
            {
                Name = userEntity.FirstName + " " + userEntity.LastName,
                Id = userEntity.Id,
                Email = userEntity.Email,
                IsSuperAdmin = userEntity.IsSuperAdmin,
                RoleId = userEntity.RoleId,
                RoleName = userEntity.Role.Name,
                DataAccessCode = userEntity.Role.DataAccessCode,
                Groups = menuGroups.Select(m => new GroupPermission
                {
                    GroupId = m.Id,
                    GroupName = m.Name,
                    Category = m.Category,
                    GroupPosition = m.DisplayPosition,
                    Icon = m.Icon,
                    Sequence = m.Sequence,
                    Target = m.Target,
                    DefaultCategory = m.Category
                }).ToList(),
                Permissions = userRole.AccessMenus.Select(m => new MenuPermission
                {
                    MenuId = m.MenuId,
                    MenuName = m.MenuName,
                    NameBeng = m.NameBeng,
                    MenuIcon = m.MenuIcon,
                    MenuTarget = m.MenuTarget,
                    MenuPosition = m.MenuPosition,
                    MenuGroupId = m.MenuGroupId,
                    HasReadAccess = m.HasReadAccess,
                    HasWriteAccess = m.HasWriteAccess,
                    HasFullAccess = m.HasFullAccess
                }).ToList()
            };
        }

        public async Task<List<RoleMenu>> GetMenuByUser(int userId)
        {
            var user = await _context.TblUserNta.FirstOrDefaultAsync(m => m.Id == userId);
            var apps = (user.IsSuperAdmin) ? await _menuService.GetAll() : await _menuService.GetByRole(user.RoleId);
            var appGroups = apps.OrderBy(m => m.MenuGroupName).Select(m => m.MenuGroupName).Distinct();
            var allApps = new List<RoleMenu>();
            foreach (var appGroup in appGroups)
            {
                var groupApps = apps.Where(m => m.MenuGroupName.Equals(appGroup)).OrderBy(m => m.Position);
                allApps.Add(new RoleMenu
                {
                    AppName = appGroup,
                    Menus = groupApps.Select(m => new RoleMenu
                    {
                        AppName = m.Name,
                        Path = m.StartingPath
                    }).ToList()
                });
            }

            return allApps;
        }
    }
}
