using DocumentFormat.OpenXml.Spreadsheet;
using GoogleMaps.LocationServices;
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

        Task<bool> UpdatePassword(string email, string password);
        Task<int> Save(User user, User loggedinUser);
        Task<User> Validate(string email, string password);
        Task<List<RoleMenu>> GetMenuByUser(int userId);
        //Task<List<ApplicationAccess>> GetAccessList(int id);
        //Task<List<UserApplication>> GetMenuByUser(int userId);
        Task<bool> SaveLogIn(int id);
        Task<bool> SaveLogOut(int id);
        Task<bool> DisableNewUser(int id);
        Task<bool> VerifyMail(int otp, int userId);
        Task<bool> SaveOTP(int otp, int userId);

        Task<string> UpdateDistrictAndSection(List<User> users);
        Task<List<User>> GetAllMissionariesUser();

        Task<List<User>> GetAllPastorsByDistrict(int districtID);

        Task<User> SendSecurityCode(string email);

        Task<Boolean> ValidateSecurityCode(string email, string securityCode);
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

        public async Task<bool> DisableNewUser(int id)
        {
            TblUserNta tblUser = await _context.TblUserNta.Where((TblUserNta m) => m.Id == id).FirstOrDefaultAsync();
            if (tblUser != null)
            {
                tblUser.IsNewUser = false;
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

        public async Task<bool> UpdatePassword(string email, string password)
        {
            var userEntity = await _context.TblUserNta.FirstOrDefaultAsync(m => m.UserName == email);

            if (userEntity == null)
            {
                return false;
            }

            var passwordEntity = new TblUserPasswordNta
            {
                UpdateDatetime = DateTime.Now,
                Password = password,
                UserId = userEntity.Id
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
                City = m.City,
                WorkPhoneNo = m.WorkPhoneNo,
                TelePhoneNo = m.TelePhoneNo,
                Status = m.Status,
                TravelingVia = m.TravelingVia,
                NumberTraveling = m.NumberTraveling,
                R1 = m.R1,
                sensitiveNation = m.sensitiveNation,
                IsNewUser = m.IsNewUser,
                IsWebUser = m.IsWebUser
                
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


        public static string GenerateRandomPassword()
        {
            int numsLength = 12;

            const string nums = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string tempPass = string.Empty;
            Random rnd = new Random();

            for (int i = 1; i <= numsLength; i++)
            {
                int index = rnd.Next(nums.Length);
                tempPass += nums[index];
            }

            return tempPass;
        }

        public async Task<List<User>> GetAllPastorsByDistrict(int districtID)
        {
            var userEntities = await _context.TblUserNta.Include(m => m.Role).Where(m => m.Role.DataAccessCode == "P" && m.DistrictId == districtID).ToListAsync();
            return userEntities.Select(m => new User
            {
                Id = m.Id,
                Name = m.FirstName + " " + m.LastName,
                IsSuperAdmin = m.IsSuperAdmin,
                IsActive = m.IsActive
            }).ToList();
        }
        public async Task <User> SendSecurityCode(string email)
        {
            var userEntity = await _context.TblUserNta.Include(m => m.Role).Include(m => m.TblUserChurchNta).ThenInclude(m => m.Church).Include(m => m.TblUserAttributeNta)
                 .ThenInclude(m => m.Attribute).Include(m => m.Section).Include(m => m.District).Include(m => m.Country).FirstOrDefaultAsync(m => m.UserName == email);

            var attributeEntities = await _context.TblAttributeNta.ToListAsync();

            if (userEntity == null)
            {
                return null;
            }

            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var securityCode = "";
            Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                int index = random.Next(validChars.Length);
                securityCode = securityCode + validChars[index];
            }

            Utilites tmp = new Utilites();
            if (tmp.SendSecurityCode(email, securityCode))
            {
                userEntity.SecurityCode = securityCode;
                userEntity.SecurityCodeInsertDatetime = DateTime.Now;

                //This will save the users code into the database 
                await _context.SaveChangesAsync();
            }
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
                IsWebUser = userEntity.IsWebUser,
                IsNewUser = userEntity.IsNewUser,
                City = userEntity.City,
                WorkPhoneNo = userEntity.WorkPhoneNo,
                TelePhoneNo = userEntity.TelePhoneNo,
                Status = userEntity.Status,
                NumberTraveling = userEntity.NumberTraveling,
                TravelingVia = userEntity.TravelingVia,
                R1 = userEntity.R1,
                sensitiveNation = userEntity.sensitiveNation,
                UserSalutation = userEntity.UserSalutation,
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
        public async Task<Boolean> ValidateSecurityCode(string email,string securityCode)
        {

            var userEntity = await _context.TblUserNta.Include(m => m.Role).Include(m => m.TblUserChurchNta).ThenInclude(m => m.Church).Include(m => m.TblUserAttributeNta)
                             .ThenInclude(m => m.Attribute).Include(m => m.Section).Include(m => m.District).Include(m => m.Country).FirstOrDefaultAsync(m => m.UserName == email);


            if (userEntity.SecurityCode == securityCode && !IsOlderThan30Minutes(userEntity.SecurityCodeInsertDatetime??DateTime.Now))
            {
                return true;
            }
            else
                return false;

        }
        static bool IsOlderThan30Minutes(DateTime dateTime)
        {
            TimeSpan difference = DateTime.Now - dateTime;

            return difference.TotalMinutes > 30;
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
                    IsWebUser = userEntity.IsWebUser,
                    IsNewUser = userEntity.IsNewUser,
                    City = userEntity.City,
                    WorkPhoneNo = userEntity.WorkPhoneNo,
                    TelePhoneNo = userEntity.TelePhoneNo,
                    Status = userEntity.Status,
                    NumberTraveling = userEntity.NumberTraveling,
                    TravelingVia = userEntity.TravelingVia,
                    R1 = userEntity.R1,
                    sensitiveNation = userEntity.sensitiveNation,
                    UserSalutation = userEntity.UserSalutation,
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
                    IsWebUser = m.IsWebUser,
                    IsNewUser = m.IsNewUser,
                    ActiveStatus = m.IsActive ? "Active" : "In-Active",
                    UserStatus = m.IsSuperAdmin ? "Yes" : "No",
                    City = m.City,
                    WorkPhoneNo = m.WorkPhoneNo,
                    TelePhoneNo = m.TelePhoneNo,
                    Status = m.Status,
                    NumberTraveling = m.NumberTraveling,
                    TravelingVia = m.TravelingVia,
                    R1 = m.R1,
                    sensitiveNation = m.sensitiveNation
                    
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
                double latitude = 0.0;
                double longitude = 0.0;

                var existingEntity = await _context.TblUserNta.Where(m => m.Email.ToLower() == user.Email.ToLower()).FirstOrDefaultAsync();
                if (existingEntity != null && user.Id == 0)
                {
                    return -1;
                }

                if (user.Address != null)
                {
                    var addressToGeoCode = user.Address;

                    var locationService = new GoogleLocationService("AIzaSyAoL5Cb3GKL803gYag0jud6d3iPHFZmbuI");

                    try
                    {
                        var point = locationService.GetLatLongFromAddress(addressToGeoCode);

                        latitude = point.Latitude;
                        longitude = point.Longitude;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return 3;
                    }
                   
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
                    userEntity.Lat = latitude == 0.0?null:latitude.ToString();
                    userEntity.Long = longitude == 0.0 ? null : longitude.ToString();
                    userEntity.Phone = user.Phone;
                    userEntity.WorkPhoneNo = user.WorkPhoneNo;
                    userEntity.TelePhoneNo = user.TelePhoneNo;
                    userEntity.Zipcode = user.Zipcode;
                    userEntity.SectionId = user.SectionId;
                    userEntity.DistrictId = user.DistrictId;
                    userEntity.CountryId = user.CountryId;
                    userEntity.UserName = user.Email;
                    userEntity.Email = user.Email;
                    userEntity.IsSuperAdmin = user.IsSuperAdmin;
                    userEntity.IsActive = user.IsActive;
                    userEntity.IsWebUser = user.IsWebUser;
                    userEntity.IsNewUser = true;
                    userEntity.RoleId = user.RoleId;
                    userEntity.NumberTraveling = user.NumberTraveling;
                    userEntity.TravelingVia = user.TravelingVia;
                    userEntity.UserSalutation = user.UserSalutation;
                    userEntity.R1 = user.R1;
                    
                    var attributes = user.Attributes.Select(m => new TblUserAttributeNta
                    {
                        AttributeId = m.AttributeId,
                        AttributeValue = m.AttributeValue,
                        UserId = user.Id,
                        Notes = m.Notes,
                    }).ToList();

                    if(attributes.Count != 0)
                    {
                        if (attributes[0].AttributeId != 0)
                            userEntity.TblUserAttributeNta = attributes;

                    }

                    var churches = user.Churches.Select(m => new TblUserChurchNta
                    {
                        ChurchId = m.ChurchId,
                        InsertDatetime = DateTime.Now,
                        RelationType = m.RelationType,
                        UserId = user.Id,
                    }).ToList();

                    if(churches.Count != 0)
                    {
                        if (churches[0].ChurchId != 0)
                            userEntity.TblUserChurchNta = churches;
                    }

                    //This allows a user to input their own first password or leave it blank for a password to be generated                     
                   if(user.Password == null || user.Password == String.Empty )
                    {
                        user.Password = GenerateRandomPassword();
                    }
                    var model = new TblUserPasswordNta
                    {                       
                        InsertDatetime = DateTime.Now,                  
                        Password = user.Password,
                        UserId = user.Id,
                        InsertUser = loggedinUser.Id.ToString(),
                    };

                    userEntity.TblUserPasswordNta.Add(model);

                    _context.TblUserNta.Add(userEntity);

                    //Send Email to New User if a web user
                    if(user.IsWebUser)
                    {
                        Utilites email = new Utilites();
                        email.SendEmailForNewUser(user.FirstName, user.LastName, user.Email, user.Password, "Support@gmdeputation.com");
                    }



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
                    userEntity.Lat = latitude.ToString();
                    userEntity.Long = longitude.ToString();
                    userEntity.Phone = user.Phone;
                    userEntity.WorkPhoneNo = user.WorkPhoneNo;
                    userEntity.TelePhoneNo = user.TelePhoneNo;
                    userEntity.Zipcode = user.Zipcode;
                    userEntity.SectionId = user.SectionId;
                    userEntity.DistrictId = user.DistrictId;
                    userEntity.CountryId = user.CountryId;
                    userEntity.UserName = user.UserName;
                    userEntity.Email = user.Email;
                    userEntity.IsSuperAdmin = user.IsSuperAdmin;
                    userEntity.IsActive = user.IsActive;
                    userEntity.IsWebUser = user.IsWebUser;
                    userEntity.RoleId = user.RoleId;
                    userEntity.UpdateDatetime = DateTime.Now;
                    userEntity.UpdateUser = loggedinUser.Id.ToString();
                    userEntity.NumberTraveling = user.NumberTraveling;
                    userEntity.TravelingVia = user.TravelingVia;
                    userEntity.UserSalutation = user.UserSalutation;
                    userEntity.R1 = user.R1;
                    userEntity.sensitiveNation = user.sensitiveNation;

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

            try
            {
                foreach (var user in _context.TblUserNta.ToList())
                {
                    Console.WriteLine(user.FirstName);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
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
                IsNewUser = userEntity.IsNewUser,
                IsWebUser = userEntity.IsWebUser,
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
