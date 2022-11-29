using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_District_NTA", Schema = "Global")]
    public partial class TblDistrictNta
    {
        public TblDistrictNta()
        {
            TblAccomodationBookingNta = new HashSet<TblAccomodationBookingNta>();
            TblChurchNta = new HashSet<TblChurchNta>();
            TblSectionNta = new HashSet<TblSectionNta>();
            TblStateDistrictNta = new HashSet<TblStateDistrictNta>();
            TblUserNta = new HashSet<TblUserNta>();
        }

        public int Id { get; set; }
        [StringLength(4000)]
        public string Code { get; set; }
        public int CodeVal { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Alias { get; set; }
        [StringLength(50)]
        public string Website { get; set; }

        [StringLength(100)]
        [Column("INSERT_USER")]
        public string InsertUser { get; set; }
        [StringLength(100)]
        [Column("UPDATE_USER")]
        
        public string UpdateUser { get; set; }
        [StringLength(100)]
        [Column("INSERT_DATETIME", TypeName = "datetime")]
        public DateTime? InsertDatetime { get; set; }
        [Column("UPDATE_DATETIME", TypeName = "datetime")]
        public DateTime? UpdateDatetime { get; set; }

        [InverseProperty("District")]
        public ICollection<TblAccomodationBookingNta> TblAccomodationBookingNta { get; set; }
        [InverseProperty("District")]
        public ICollection<TblChurchNta> TblChurchNta { get; set; }
        [InverseProperty("District")]
        public ICollection<TblSectionNta> TblSectionNta { get; set; }
        [InverseProperty("District")]
        public ICollection<TblStateDistrictNta> TblStateDistrictNta { get; set; }
        [InverseProperty("District")]
        public ICollection<TblUserNta> TblUserNta { get; set; }
    }
}
