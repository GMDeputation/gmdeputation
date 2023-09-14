using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_User_NTA", Schema = "Auth")]
    public partial class TblUserNta
    {
        public TblUserNta()
        {


            TblAccomodationBookingNtaApprovedByNavigation = new HashSet<TblAccomodationBookingNta>();
            TblAccomodationBookingNtaRequestedUser = new HashSet<TblAccomodationBookingNta>();
            TblMacroScheduleDetailsNtaApprovedRejectByNavigation = new HashSet<TblMacroScheduleDetailsNta>();
            TblMacroScheduleDetailsNtaUser = new HashSet<TblMacroScheduleDetailsNta>();
            TblUserAttributeNta = new HashSet<TblUserAttributeNta>();
            TblUserLogNta = new HashSet<TblUserLogNta>();
            TblUserPasswordNta = new HashSet<TblUserPasswordNta>();
            TblUserChurchNta = new HashSet<TblUserChurchNta>();


            TblAccomodationBookingNtaApprovedByNavigation = new HashSet<TblAccomodationBookingNta>();
            TblAccomodationBookingNtaRequestedUser = new HashSet<TblAccomodationBookingNta>();
            TblAppointmentNtaAcceptByPastorByNavigation = new HashSet<TblAppointmentNta>();
            TblAppointmentNtaAcceptMissionaryByNavigation = new HashSet<TblAppointmentNta>();
            TblAppointmentNtaCreatedByNavigation = new HashSet<TblAppointmentNta>();
            TblAppointmentNtaSubmittedByNavigation = new HashSet<TblAppointmentNta>();
            TblMacroScheduleDetailsNtaApprovedRejectByNavigation = new HashSet<TblMacroScheduleDetailsNta>();
            TblMacroScheduleDetailsNtaUser = new HashSet<TblMacroScheduleDetailsNta>();
            TblUserAttributeNta = new HashSet<TblUserAttributeNta>();
            TblUserLogNta = new HashSet<TblUserLogNta>();
            TblUserPasswordNta = new HashSet<TblUserPasswordNta>();

        }

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
        public bool IsWebUser { get; set; }
        public bool IsNewUser { get; set; }
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

        public string UserSalutation { get; set; }

        public bool R1 { get; set; }

        public bool sensitiveNation { get; set; }


        [ForeignKey("CountryId")]
        [InverseProperty("TblUserNta")]
        public TblCountryNta Country { get; set; }
        [ForeignKey("DistrictId")]
        [InverseProperty("TblUserNta")]
        public TblDistrictNta District { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("TblUserNta")]
        public TblRoleNta Role { get; set; }
        [ForeignKey("SectionId")]
        [InverseProperty("TblUserNta")]
        public TblSectionNta Section { get; set; }
        [ForeignKey("StateId")]
        [InverseProperty("TblUserNta")]
        public TblStateNta State { get; set; }

        public string AddressState { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string HQID { get; set; }

        [InverseProperty("ApprovedByNavigation")]
        public ICollection<TblAccomodationBookingNta> TblAccomodationBookingNtaApprovedByNavigation { get; set; }
        [InverseProperty("RequestedUser")]
        public ICollection<TblAccomodationBookingNta> TblAccomodationBookingNtaRequestedUser { get; set; }
        [InverseProperty("ApprovedRejectByNavigation")]
        public ICollection<TblMacroScheduleDetailsNta> TblMacroScheduleDetailsNtaApprovedRejectByNavigation { get; set; }
        [InverseProperty("User")]
        public ICollection<TblMacroScheduleDetailsNta> TblMacroScheduleDetailsNtaUser { get; set; }
        [InverseProperty("User")]
        public ICollection<TblUserAttributeNta> TblUserAttributeNta { get; set; }
        [InverseProperty("User")]
        public ICollection<TblUserLogNta> TblUserLogNta { get; set; }
        [InverseProperty("User")]
        public ICollection<TblUserPasswordNta> TblUserPasswordNta { get; set; }

        public ICollection<TblUserChurchNta> TblUserChurchNta { get; set; }

        public ICollection<TblAppointmentNta> TblAppointmentNtaAcceptByPastorByNavigation { get; set; }

        public ICollection<TblAppointmentNta> TblAppointmentNtaAcceptMissionaryByNavigation { get; set; }

        public ICollection<TblAppointmentNta> TblAppointmentNtaCreatedByNavigation { get; set; }

        public ICollection<TblAppointmentNta> TblAppointmentNtaSubmittedByNavigation { get; set; }

    }
}
