using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_Menu_NTA", Schema = "Auth")]
    public partial class TblMenuNta
    {
        public TblMenuNta()
        {
            TblRoleMenuNta = new HashSet<TblRoleMenuNta>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Icon { get; set; }
        [StringLength(50)]
        public string Target { get; set; }
        public int MenuGroupId { get; set; }
        [Required]
        [StringLength(100)]
        public string StartingPath { get; set; }
        public int Position { get; set; }
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

        [ForeignKey("MenuGroupId")]
        [InverseProperty("TblMenuNta")]
        public TblMenuGroupNta MenuGroup { get; set; }
        [InverseProperty("Menu")]
        public ICollection<TblRoleMenuNta> TblRoleMenuNta { get; set; }
    }
}
