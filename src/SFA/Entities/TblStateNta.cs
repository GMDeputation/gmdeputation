using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_State_NTA", Schema = "Global")]
    public partial class TblStateNta
    {
        public TblStateNta()
        {
            TblStateDistrictNta = new HashSet<TblStateDistrictNta>();
            TblUserNta = new HashSet<TblUserNta>();
        }

        public int Id { get; set; }
        [StringLength(4000)]
        public string Code { get; set; }
        public int CodeVal { get; set; }
        [StringLength(50)]
        public string Alias { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int CountryId { get; set; }
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

        [ForeignKey("CountryId")]
        [InverseProperty("TblStateNta")]
        public TblCountryNta Country { get; set; }
        [InverseProperty("State")]
        public ICollection<TblStateDistrictNta> TblStateDistrictNta { get; set; }
        [InverseProperty("State")]
        public ICollection<TblUserNta> TblUserNta { get; set; }
    }
}
