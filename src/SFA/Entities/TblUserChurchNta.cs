using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_UserChurch_NTA", Schema = "Global")]
    public partial class TblUserChurchNta
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChurchId { get; set; }
        [StringLength(50)]
        public string RelationType { get; set; }
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

        public TblChurchNta Church { get; set; }
        public TblUserNta User { get; set; }
    }
}
