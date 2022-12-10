using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_Church_NTA", Schema = "Global")]
    public partial class TblChurchNta
    {
        public TblChurchNta()
        {
            TblAccomodationBookingNta = new HashSet<TblAccomodationBookingNta>();
            TblChurchAccommodationNta = new HashSet<TblChurchAccommodationNta>();
            TblChurchAttributeNta = new HashSet<TblChurchAttributeNta>();
            TblChurchServiceTimeNta = new HashSet<TblChurchServiceTimeNta>();
            TblAppointmentNta = new HashSet<TblAppointmentNta>();

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string ChurchIdNo { get; set; }
        [Required]
        [StringLength(150)]
        public string ChurchName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(300)]
        public string Directory { get; set; }
        [StringLength(200)]
        public string MailAddress { get; set; }
        [StringLength(50)]
        public string ChurchType { get; set; }
        [StringLength(50)]
        public string AccountNo { get; set; }
        [Column("lat")]
        [StringLength(50)]
        public string Lat { get; set; }
        [Column("lon")]
        [StringLength(50)]
        public string Lon { get; set; }
        public int DistrictId { get; set; }
        public int SectionId { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Phone2 { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string WebSite { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public bool IsDelete { get; set; }
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

        [ForeignKey("DistrictId")]
        [InverseProperty("TblChurchNta")]
        public TblDistrictNta District { get; set; }
        [ForeignKey("SectionId")]
        [InverseProperty("TblChurchNta")]
        public TblSectionNta Section { get; set; }
        [InverseProperty("Church")]
        public ICollection<TblAccomodationBookingNta> TblAccomodationBookingNta { get; set; }
        [InverseProperty("Church")]
        public ICollection<TblChurchAccommodationNta> TblChurchAccommodationNta { get; set; }
        [InverseProperty("Church")]
        public ICollection<TblChurchAttributeNta> TblChurchAttributeNta { get; set; }
        [InverseProperty("Church")]
        public ICollection<TblChurchServiceTimeNta> TblChurchServiceTimeNta { get; set; }

        public ICollection<TblUserChurchNta> TblUserChurch { get; set; }
        public ICollection<TblAppointmentNta> TblAppointmentNta { get; set; }


    
}
}
