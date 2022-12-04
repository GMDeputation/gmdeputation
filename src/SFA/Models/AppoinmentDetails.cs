using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
	public class AppoinmentDetails
	{
		public string ChurchName { get; set; }

		public DateTime? AppoinmentDate { get; set; }

		public string Time { get; set; }

		public string ServiceType { get; set; }

		public string PastorName { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string Offer { get; set; }

		public decimal? OfferingAmount { get; set; }

		public string MissionarySpouceName { get; set; }

		public string MissionaryCountry { get; set; }

		public string MissionaryWesbsite { get; set; }
	}
}
