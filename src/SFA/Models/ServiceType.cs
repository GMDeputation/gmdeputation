using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class ServiceTypeQuery : Query
    {
        public string Filter { get; set; }
    }
}
