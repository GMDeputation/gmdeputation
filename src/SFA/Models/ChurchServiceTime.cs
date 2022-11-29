using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class ChurchServiceTime
    {
        public int Id { get; set; }
        public int ChurchId { get; set; }
        public string ChurchName { get; set; }
        public string WeekDay { get; set; }
        public TimeSpan ServiceTime { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public int Preferencelevel { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string TimeString { get; set; }
    }

    public class ChurchServiceTimeQuery : Query
    {
        public string Filter { get; set; }
        public List<int> ServiceTypeId { get; set; }
        public int? ChurchId { get; set; }
        public List<string> WeekDay { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; } 
    }
}
