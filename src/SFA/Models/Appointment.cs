using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan? EventTime { get; set; }
        public int ChurchId { get; set; }
        public string ChurchName { get; set; }
        public int? MacroScheduleDetailId { get; set; }
        public string Description { get; set; }
        public decimal? PimAmount { get; set; }
        public string Offering { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsSubmit { get; set; }
        public int? SubmittedBy { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public bool IsAcceptByPastor { get; set; }
        public string AcceptByPastorRemarks { get; set; }
        public int? AcceptByPastorBy { get; set; }
        public DateTime? AcceptByPastorOn { get; set; }
        public bool IsForwardForMissionary { get; set; }
        public bool IsAcceptMissionary { get; set; }
        public string AcceptMissionaryRemarks { get; set; }
        public int? AcceptMissionaryBy { get; set; }
        public DateTime? AcceptMissionaryOn { get; set; }

        public int ServiceTypeId { get; set; }
        public bool OfferingOnly { get; set; }
        public string Status { get; set; }
        public string TimeString { get; set; }
        public string AccessCode { get; set; } 
        public string DistrictName { get; set; } 
        public string MacroScheduleDesc { get; set; } 
        public string MissionaryUser { get; set; }

        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? InsertDatetime { get; set; }
        public DateTime? UpdateDatetime { get; set; }
    }

    public class AppointmentQuery : Query
    {
        public string Filter { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; } 
    }
}
