using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.test
{
    [Table("Tbl_ServiceType_NTA", Schema = "Global")]
    public partial class TblServiceTypeNta
    {
        public TblServiceTypeNta()
        {
            TblAppointmentNta = new HashSet<TblAppointmentNta>();
        }

        [Key]
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

        [InverseProperty("ServiceType")]
        public virtual ICollection<TblAppointmentNta> TblAppointmentNta { get; set; }
    }
}
