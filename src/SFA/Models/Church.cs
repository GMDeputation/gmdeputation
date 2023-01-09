using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.Models
{
    public class Church
    {
        public int Id { get; set; }

        public string ChurchIdNo { get; set; }
        public string ChurchName { get; set; }
        public string Address { get; set; }
        public string Directory { get; set; }
        public string MailAddress { get; set; }
        public string ChurchType { get; set; }
        public string AccountNo { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } 
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? InsertDatetime { get; set; }
        public DateTime? UpdateDatetime { get; set; }
        public string Pastor { get; set; }
        public string ServiceTypewiseTime { get; set; }

        public decimal? TotalPoint { get; set; }

        public List<ChurchServiceTime> ChurchServiceTimes { get; set; }

        public List<ChurchAttribute> Attributes { get; set; }
    }
    public class ChurchQuery : Query
    {
        public string Filter { get; set; }
        public int DistrictId { get; set; }
        public int SectionId { get; set; }
        public string Email { get; set; }
    }
}