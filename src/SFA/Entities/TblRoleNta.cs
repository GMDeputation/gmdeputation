using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_Role_NTA", Schema = "Auth")]
    public partial class TblRoleNta
    {
        public TblRoleNta()
        {
            TblRoleMenuNta = new HashSet<TblRoleMenuNta>();
            TblUserNta = new HashSet<TblUserNta>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(1)]
        public string DataAccessCode { get; set; }
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

        [InverseProperty("Role")]
        public ICollection<TblRoleMenuNta> TblRoleMenuNta { get; set; }
        [InverseProperty("Role")]
        public ICollection<TblUserNta> TblUserNta { get; set; }
    }
}
