using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class MenuGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayPosition { get; set; }
        public int? Sequence { get; set; }
        public string Category { get; set; }
        public string Target { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? InsertDatetime { get; set; }
        public DateTime? UpdateDatetime { get; set; }
    }
}
