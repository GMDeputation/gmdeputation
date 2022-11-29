using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_UserAttribute_NTA", Schema = "Auth")]
    public partial class TblUserAttributeNta
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AttributeId { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? AttributeValue { get; set; }
        [StringLength(150)]
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

        [ForeignKey("AttributeId")]
        [InverseProperty("TblUserAttributeNta")]
        public TblAttributeNta Attribute { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("TblUserAttributeNta")]
        public TblUserNta User { get; set; }
    }
}
