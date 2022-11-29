using Newtonsoft.Json;
using System;

namespace SFA.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int CodeVal { get; set; }
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
    public class SectionQuery : Query
    {
        public string Filter { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
    }
}
