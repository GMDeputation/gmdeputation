using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_ChurchServiceTime_NTA", Schema = "Global")]
    public partial class TblChurchServiceTimeNta
    {
        public int Id { get; set; }
        public int ChurchId { get; set; }
        [Required]
        [StringLength(30)]
        public string WeekDay { get; set; }
        public TimeSpan ServiceTime { get; set; }
        public int ServiceTypeId { get; set; }
        public int Preferencelevel { get; set; }
        [StringLength(100)]
        public string Notes { get; set; }
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
        [InverseProperty("TblChurchServiceTimeNta")]
        public TblChurchNta Church { get; set; }
        [ForeignKey("ServiceTypeId")]
        [InverseProperty("TblChurchServiceTimeNta")]
        public TblServiceTypeNta ServiceType { get; set; }
    }
}
