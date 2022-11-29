using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Target { get; set; }
        public int MenuGroupId { get; set; }
        public string StartingPath { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? InsertDatetime { get; set; }
        public DateTime? UpdateDatetime { get; set; }
        public string MenuGroupName { get; set; }
        public bool HasReadAccess { get; set; }
        public bool HasWriteAccess { get; set; }
        public bool HasFullAccess { get; set; }

        public string Category { get; set; }
    }
    public class MenuQuery : Query
    {
        public string Name { get; set; }
        public string Group { get; set; }
    }

    

    public class MenuAccess
    {
        public Menu Menu { get; set; }
        public bool HasReadAccess { get; set; }
        public bool HasWriteAccess { get; set; }
        public bool HasFullAccess { get; set; }
        public List<MenuAccess> Menus { get; set; }
    }
}
