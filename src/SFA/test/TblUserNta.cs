using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.test
{
    [Table("Tbl_User_NTA", Schema = "Auth")]
    public partial class TblUserNta
    {
        public TblUserNta()
        {
            TblAppointmentNtaAcceptByPastorByNavigation = new HashSet<TblAppointmentNta>();
            TblAppointmentNtaAcceptMissionaryByNavigation = new HashSet<TblAppointmentNta>();
            TblAppointmentNtaCreatedByNavigation = new HashSet<TblAppointmentNta>();
            TblAppointmentNtaSubmittedByNavigation = new HashSet<TblAppointmentNta>();
            TblMacroScheduleDetailsNtaApprovedRejectByNavigation = new HashSet<TblMacroScheduleDetailsNta>();
            TblMacroScheduleDetailsNtaUser = new HashSet<TblMacroScheduleDetailsNta>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(75)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string Gender { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string Zipcode { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Long { get; set; }
        public int? SectionId { get; set; }
        public int? DistrictId { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public int RoleId { get; set; }
        public bool IsEmailVerify { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VerifiedOn { get; set; }
        public int? Otp { get; set; }
        [StringLength(100)]
        public string ImageFile { get; set; }
        [StringLength(50)]
        public string ImageSequence { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsActive { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string WorkPhoneNo { get; set; }
        [StringLength(50)]
        public string TelePhoneNo { get; set; }
        [StringLength(100)]
        public string Status { get; set; }
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
        public int? NumberTraveling { get; set; }
        [StringLength(100)]
        public string TravelingVia { get; set; }

        [InverseProperty(nameof(TblAppointmentNta.AcceptByPastorByNavigation))]
        public virtual ICollection<TblAppointmentNta> TblAppointmentNtaAcceptByPastorByNavigation { get; set; }
        [InverseProperty(nameof(TblAppointmentNta.AcceptMissionaryByNavigation))]
        public virtual ICollection<TblAppointmentNta> TblAppointmentNtaAcceptMissionaryByNavigation { get; set; }
        [InverseProperty(nameof(TblAppointmentNta.CreatedByNavigation))]
        public virtual ICollection<TblAppointmentNta> TblAppointmentNtaCreatedByNavigation { get; set; }
        [InverseProperty(nameof(TblAppointmentNta.SubmittedByNavigation))]
        public virtual ICollection<TblAppointmentNta> TblAppointmentNtaSubmittedByNavigation { get; set; }
        [InverseProperty(nameof(TblMacroScheduleDetailsNta.ApprovedRejectByNavigation))]
        public virtual ICollection<TblMacroScheduleDetailsNta> TblMacroScheduleDetailsNtaApprovedRejectByNavigation { get; set; }
        [InverseProperty(nameof(TblMacroScheduleDetailsNta.User))]
        public virtual ICollection<TblMacroScheduleDetailsNta> TblMacroScheduleDetailsNtaUser { get; set; }
    }
}
