using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_AccomodationBooking_NTA", Schema = "Global")]
    public partial class TblAccomodationBookingNta
    {
        public int Id { get; set; }
        public int RequestedUserId { get; set; }
        public int DistrictId { get; set; }
        public int ChurchId { get; set; }
        public int AccomodationId { get; set; }
        public int? AdultNo { get; set; }
        public int? ChildNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CheckinDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CheckoutDate { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        [StringLength(100)]
        public string Reason { get; set; }
        public bool IsFeedBack { get; set; }
        [StringLength(200)]
        public string FeedBack { get; set; }
        public bool IsSubmit { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApprovedOn { get; set; }
        [StringLength(200)]
        public string Remarks { get; set; }
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

        [ForeignKey("AccomodationId")]
        [InverseProperty("TblAccomodationBookingNta")]
        public TblChurchAccommodationNta Accomodation { get; set; }
        [ForeignKey("ApprovedBy")]
        [InverseProperty("TblAccomodationBookingNtaApprovedByNavigation")]
        public TblUserNta ApprovedByNavigation { get; set; }
        [ForeignKey("ChurchId")]
        [InverseProperty("TblAccomodationBookingNta")]
        public TblChurchNta Church { get; set; }
        [ForeignKey("DistrictId")]
        [InverseProperty("TblAccomodationBookingNta")]
        public TblDistrictNta District { get; set; }
        [ForeignKey("RequestedUserId")]
        [InverseProperty("TblAccomodationBookingNtaRequestedUser")]
        public TblUserNta RequestedUser { get; set; }
    }
}
