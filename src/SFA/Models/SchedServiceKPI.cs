using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class SchedServiceKPI
    {
        public string service_yr_mo { get; set; }
        public string service_yr { get; set; }
        public string service_mo_name { get; set; }
        public int tot_services { get; set; }
        public int prev_services { get; set; }
    }
   
}
