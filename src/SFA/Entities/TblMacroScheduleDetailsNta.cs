using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_MacroScheduleDetails_NTA", Schema = "Global")]
    public partial class TblMacroScheduleDetailsNta
    {

        public TblMacroScheduleDetailsNta()
        {
            TblAppointmentNta = new HashSet<TblAppointmentNta>();
        }

        public int Id { get; set; }
        public int MacroScheduleId { get; set; }
        public int DistrictId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        [StringLength(100)]
        public string Notes { get; set; }
        [StringLength(100)]
        public string ApprovedRejectRemarks { get; set; }
        public int? ApprovedRejectBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApprovedRejectOn { get; set; }
        [Column("INSERT_USER")]
        [StringLength(100)]
        public string InsertUser { get; set; }
        [Column("UPDATE_USER")]
        [StringLength(100)]
        public string UpdateUser { get; set; }
        [Column("INSERT_DATETIME", TypeName = "datetime")]
        public DateTime? InsertDatetime { get; set; }
        [Column("UPDATE_DATETIME", TypeName = "datetime")]
        public DateTime? UpdateDatetime { get; set; }

        [ForeignKey("ApprovedRejectBy")]
        [InverseProperty("TblMacroScheduleDetailsNtaApprovedRejectByNavigation")]
        public TblUserNta ApprovedRejectByNavigation { get; set; }
        [ForeignKey("MacroScheduleId")]
        [InverseProperty("TblMacroScheduleDetailsNta")]
        public TblMacroScheduleNta MacroSchedule { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("TblMacroScheduleDetailsNtaUser")]
        public TblUserNta User { get; set; }
        public string Reason { get; set; }
        public TblDistrictNta District { get; set; }
        public ICollection<TblAppointmentNta> TblAppointmentNta { get; set; }
    }
}
