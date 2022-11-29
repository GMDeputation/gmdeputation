using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_StateDistrict_NTA", Schema = "Global")]
    public partial class TblStateDistrictNta
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
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
        [InverseProperty("TblStateDistrictNta")]
        public TblDistrictNta District { get; set; }
        [ForeignKey("StateId")]
        [InverseProperty("TblStateDistrictNta")]
        public TblStateNta State { get; set; }
    }
}
