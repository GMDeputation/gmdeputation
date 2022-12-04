using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
	public class UserReport
	{
		public string Name { get; set; }

		public string Role { get; set; }

		public string Email { get; set; }

		public string Page { get; set; }

		public string Description { get; set; }

		public string Action { get; set; }

		public DateTime? ActionTime { get; set; }
	}

}
