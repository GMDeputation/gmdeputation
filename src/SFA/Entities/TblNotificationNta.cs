using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_Notification_NTA", Schema = "Global")]
    public partial class TblNotificationNta
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        [StringLength(100)]
        public string EventUrl { get; set; }
        public int? EventUser { get; set; }
        public bool IsOpened { get; set; }
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
    }
}
