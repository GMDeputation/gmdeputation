using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_RoleMenu_NTA", Schema = "Auth")]
    public partial class TblRoleMenuNta
    {
        [Column("ID")]
        public int Id { get; set; }
        public int RoleId { get; set; }
        [Column("MenuID")]
        public int MenuId { get; set; }
        public bool HasReadAccess { get; set; }
        public bool HasWriteAccess { get; set; }
        public bool HasFullAccess { get; set; }
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

        [ForeignKey("MenuId")]
        [InverseProperty("TblRoleMenuNta")]
        public TblMenuNta Menu { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("TblRoleMenuNta")]
        public TblRoleNta Role { get; set; }
    }
}
