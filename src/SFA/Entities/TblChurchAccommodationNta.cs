using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_ChurchAccommodation_NTA", Schema = "Global")]
    public partial class TblChurchAccommodationNta
    {
        public TblChurchAccommodationNta()
        {
            TblAccomodationBookingNta = new HashSet<TblAccomodationBookingNta>();
        }

        public int Id { get; set; }
        public int ChurchId { get; set; }
        [StringLength(50)]
        public string AccomType { get; set; }
        [StringLength(250)]
        public string AccomNotes { get; set; }
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

        [ForeignKey("ChurchId")]
        [InverseProperty("TblChurchAccommodationNta")]
        public TblChurchNta Church { get; set; }
        [InverseProperty("Accomodation")]

        public string EqaccomAddress { get; set; }
        public ICollection<TblAccomodationBookingNta> TblAccomodationBookingNta { get; set; }
    }
}
