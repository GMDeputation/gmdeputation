using Newtonsoft.Json;
using System;

namespace SFA.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int CodeVal { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
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

    public class StateQuery : Query
    {
        public string Filter { get; set; }
        public int CountryId { get; set; }
    }
}
