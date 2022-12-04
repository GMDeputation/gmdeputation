using Newtonsoft.Json;
using System;

namespace SFA.Models
{
	public class MacroScheduleReportParams
	{
		public DateTime? StartFromDate { get; set; }

		public DateTime? StartToDate { get; set; }

		public DateTime? EventFromDate { get; set; }

		public DateTime? EventToDate { get; set; }

		public string Week { get; set; }
	}

}