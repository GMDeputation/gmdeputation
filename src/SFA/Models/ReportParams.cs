using SFA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
	public class ReportParams
	{

		public DateTime? FromDate { get; set; }

		public DateTime? ToDate { get; set; }

		public int? UserId { get; set; }

		public int? ChurchId { get; set; }

		public string RoleName { get; set; }

		public string Action { get; set; }


	}
}
