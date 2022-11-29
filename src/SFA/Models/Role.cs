using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataAccessCode { get; set; }
        public bool IsActive { get; set; }
        public List<AccessMenu> AccessMenus { get; set; } 
    }
    public class RoleMenu
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public string Path { get; set; }
        public bool IsSelected { get; set; }
        public bool HasReadAccess { get; set; }
        public bool HasWriteAccess { get; set; }
        public bool HasFullAccess { get; set; }
        public List<RoleMenu> Menus { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? InsertDatetime { get; set; }
        public DateTime? UpdateDatetime { get; set; }
    }

    public class AccessMenu
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

    public class RoleQuery : Query
    {
        public string Name { get; set; }
    }
}
