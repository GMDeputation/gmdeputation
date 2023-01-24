using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.test
{
    [Table("Tbl_MacroScheduleDetails_NTA", Schema = "Global")]
    public partial class TblMacroScheduleDetailsNta
    {
        public TblMacroScheduleDetailsNta()
        {
            TblAppointmentNta = new HashSet<TblAppointmentNta>();
        }

        [Key]
        public int Id { get; set; }
        public int MacroScheduleId { get; set; }
        public int DistrictId { get; set; }
        public int? UserId { get; set; }
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
        [Column("reason")]
        [StringLength(200)]
        public string Reason { get; set; }

        [ForeignKey(nameof(ApprovedRejectBy))]
        [InverseProperty(nameof(TblUserNta.TblMacroScheduleDetailsNtaApprovedRejectByNavigation))]
        public virtual TblUserNta ApprovedRejectByNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TblUserNta.TblMacroScheduleDetailsNtaUser))]
        public virtual TblUserNta User { get; set; }
        [InverseProperty("MacroScheduleDetail")]
        public virtual ICollection<TblAppointmentNta> TblAppointmentNta { get; set; }
    }
}
