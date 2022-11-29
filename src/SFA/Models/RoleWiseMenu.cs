using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class RoleWiseMenu
    {
        public int RoleId { get; set; }

        public List<Menu> Menus { get; set; }
    }
}
