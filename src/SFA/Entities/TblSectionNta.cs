using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_Section_NTA", Schema = "Global")]
    public partial class TblSectionNta
    {
        public TblSectionNta()
        {
            TblChurchNta = new HashSet<TblChurchNta>();
            TblUserNta = new HashSet<TblUserNta>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int DistrictId { get; set; }
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
        [InverseProperty("TblSectionNta")]
        public TblDistrictNta District { get; set; }
        [InverseProperty("Section")]
        public ICollection<TblChurchNta> TblChurchNta { get; set; }
        [InverseProperty("Section")]
        public ICollection<TblUserNta> TblUserNta { get; set; }
    }
}
