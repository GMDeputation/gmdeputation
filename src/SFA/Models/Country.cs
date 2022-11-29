using Newtonsoft.Json;
using System;

namespace SFA.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int CodeVal { get; set; }
        public string Name { get; set; }
        public string FrenchName { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? InsertDatetime { get; set; }
        public DateTime? UpdateDatetime { get; set; }
    }

    public class CountryQuery : Query
    {
        public string Filter { get; set; }
    }
}