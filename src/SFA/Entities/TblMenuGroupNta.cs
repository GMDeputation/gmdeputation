using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_MenuGroup_NTA", Schema = "Auth")]
    public partial class TblMenuGroupNta
    {
        public TblMenuGroupNta()
        {
            TblMenuNta = new HashSet<TblMenuNta>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int DisplayPosition { get; set; }
        public int? Sequence { get; set; }
        [StringLength(100)]
        public string Category { get; set; }
        [StringLength(100)]
        public string Target { get; set; }
        [StringLength(20)]
        public string Icon { get; set; }
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

        [InverseProperty("MenuGroup")]
        public ICollection<TblMenuNta> TblMenuNta { get; set; }
    }
}
