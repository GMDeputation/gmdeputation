using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_Country_NTA", Schema = "Global")]
    public partial class TblCountryNta
    {
        public TblCountryNta()
        {
            TblStateNta = new HashSet<TblStateNta>();
            TblUserNta = new HashSet<TblUserNta>();
        }

        public int Id { get; set; }
        [StringLength(4000)]
        public string Code { get; set; }
        [StringLength(50)]
        public string CountryCode { get; set; }
        public int CodeVal { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string FrenchName { get; set; }
        [StringLength(10)]
        public string Alpha2Code { get; set; }
        [StringLength(10)]
        public string Alpha3Code { get; set; }
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

        [InverseProperty("Country")]
        public ICollection<TblStateNta> TblStateNta { get; set; }
        [InverseProperty("Country")]
        public ICollection<TblUserNta> TblUserNta { get; set; }


    }
}
