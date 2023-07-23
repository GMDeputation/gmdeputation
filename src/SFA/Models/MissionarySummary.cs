using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class MissionarySummary
    {
        public int cnt_months { get; set; }
        public string rpt_month_name { get; set; }
        public string rpt_year { get; set; }
        public string rpt_month { get; set; }

        public int missionary_cy { get; set; }
        public int missionary_py { get; set; }

        public int schedule_cy { get; set; }
        public int schedule_py { get; set; }
    }
   
}
