using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
	public class MacroscheduleWiseAppoinmentReport
	{
		public string Description { get; set; }

		public List<AppoinmentDetails> AppoinmentDetails { get; set; }
	}

}
