using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_MacroSchedule_NTA", Schema = "Global")]
    public partial class TblMacroScheduleNta
    {
        public TblMacroScheduleNta()
        {
            TblMacroScheduleDetailsNta = new HashSet<TblMacroScheduleDetailsNta>();
        }

        public int Id { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EntryDate { get; set; }
        public bool IsActive { get; set; }
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

        [InverseProperty("MacroSchedule")]
        public ICollection<TblMacroScheduleDetailsNta> TblMacroScheduleDetailsNta { get; set; }
    }
}
