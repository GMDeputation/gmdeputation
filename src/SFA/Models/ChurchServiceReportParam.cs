using SFA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
	public class ChurchServiceReportParam
	{
		public int? DistrictId { get; set; }

		public int? SectionId { get; set; }

		public int? ChurchId { get; set; }

		public int? ServiceTypeId { get; set; }
	}
}

