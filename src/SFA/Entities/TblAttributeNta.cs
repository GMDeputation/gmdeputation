using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_Attribute_NTA", Schema = "Global")]
    public partial class TblAttributeNta
    {
        public TblAttributeNta()
        {
            TblChurchAttributeNta = new HashSet<TblChurchAttributeNta>();
            TblUserAttributeNta = new HashSet<TblUserAttributeNta>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string AttributeName { get; set; }
        public int AttributeTypeId { get; set; }
        [StringLength(150)]
        public string AttributeNotes { get; set; }
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

        [ForeignKey("AttributeTypeId")]
        [InverseProperty("TblAttributeNta")]
        public TblAttributeTypeNta AttributeType { get; set; }
        [InverseProperty("Attribute")]
        public ICollection<TblChurchAttributeNta> TblChurchAttributeNta { get; set; }
        [InverseProperty("Attribute")]
        public ICollection<TblUserAttributeNta> TblUserAttributeNta { get; set; }
    }
}
