using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.Entities
{
    [Table("Tbl_UserLog_NTA", Schema = "Auth")]
    public partial class TblUserLogNta
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LoginTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LogoutTime { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("TblUserLogNta")]
        public TblUserNta User { get; set; }
    }
}
