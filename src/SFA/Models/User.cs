using SFA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public int? SectionId { get; set; }
        public int? DistrictId { get; set; } 
        public string DistrictName { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; } 
        public string SectionName { get; set; }
        public bool IsEmailVerify { get; set; }
        public string ImageFile { get; set; }
        public string ImageSequence { get; set; }
        public DateTime? InsertDatetime { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsActive { get; set; }
        public string DataAccessCode { get; set; }
        public string City { get; set; }
        public string WorkPhoneNo { get; set; }
        public string TelePhoneNo { get; set; }
        public string Status { get; set; }
        public int? NumberTraveling { get; set; }
        public string TravelingVia { get; set; }

        public bool IsWebUser { get; set; }
        public bool IsNewUser { get; set; }

        public string UserSalutation { get; set; }
        public bool R1 { get; set; }
        public bool sensitiveNation { get; set; }

        //public List<MenuAccess> Menus { get; set; }
        public List<GroupPermission> Groups { get; set; }
        public List<MenuPermission> Permissions { get; set; }
        public List<UserPassword> Passwords { get; set; }
        public List<UserAttribute> Attributes { get; set; }
        public List<UserChurch> Churches { get; set; }

        public string ActiveStatus { get; set; }
        public string UserStatus { get; set; }

        public string State { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }

        public string HQID { get; set; }
    }

    public class UserQuery : Query
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserPassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserAttribute
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AttributeId { get; set; }
        public int AttributeTypeId { get; set; }
        public decimal? AttributeValue { get; set; }
        public string Notes { get; set; }
        public List<AttributeModel> Butes { get; set; }
    }

    public class UserChurch
    {
        public int UserId { get; set; }
        public int ChurchId { get; set; }
        public string ChurchName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string RelationType { get; set; }
    }

    public class GroupPermission
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int GroupPosition { get; set; }
        public int? Sequence { get; set; }
        public string GroupNameBeng { get; set; }
        public string DefaultCategory { get; set; }
        public string Category { get; set; }
        public string CategoryBeng { get; set; }
        public string Target { get; set; }
        public string Icon { get; set; }
    }

    public class MenuPermission
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string NameBeng { get; set; }
        public string MenuIcon { get; set; }
        public string MenuTarget { get; set; }
        public int MenuPosition { get; set; }
        public int MenuGroupId { get; set; }
        public bool HasReadAccess { get; set; }
        public bool HasWriteAccess { get; set; }
        public bool HasFullAccess { get; set; }
    }
}
