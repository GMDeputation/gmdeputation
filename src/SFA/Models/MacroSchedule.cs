using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class MacroSchedule
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime EntryDate { get; set; }
        public int InsertUser { get; set; }
        public DateTime? InsertDatetime { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDatetime { get; set; }

        public List<MacroScheduleDetails> MacroScheduleDetails { get; set; }
    }

    public class MacroScheduleDetails
    {
        public int Id { get; set; }
        public int MacroScheduleId { get; set; }
        public string MacroScheduleDesc { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public string Notes { get; set; }
        public string ApprovedRejectRemarks { get; set; }
        public int ApprovedRejectBy { get; set; }
        public DateTime ApprovedRejectOn { get; set; }

        public string Status { get; set; }
        public string AccessCode { get; set; } 
        public bool IsDateOver { get; set; }

        public DateTime EntryDate { get; set; }
        public string ApprovedRejectUser { get; set; }
        public bool IsCanceled { get; set; }
        public int? IsCanceledBy { get; set; }
        public string Cancellation_Notes { get; set; }
        public DateTime? Cancellation_DateTime { get; set; }
    }

    public class MacroScheduleQuery : Query
    {
        public string Filter { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromEntryDate { get; set; }
        public DateTime? ToEntryDate { get; set; } 
    }
}
