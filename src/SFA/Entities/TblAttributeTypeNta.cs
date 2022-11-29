using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_AttributeType_NTA", Schema = "Global")]
    public partial class TblAttributeTypeNta
    {
        public TblAttributeTypeNta()
        {
            TblAttributeNta = new HashSet<TblAttributeNta>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
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

        [InverseProperty("AttributeType")]
        public ICollection<TblAttributeNta> TblAttributeNta { get; set; }
    }
}
