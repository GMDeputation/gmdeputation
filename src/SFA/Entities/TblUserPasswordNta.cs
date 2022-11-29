using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_UserPassword_NTA", Schema = "Auth")]
    public partial class TblUserPasswordNta
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
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

        [ForeignKey("UserId")]
        [InverseProperty("TblUserPasswordNta")]
        public TblUserNta User { get; set; }
    }
}
