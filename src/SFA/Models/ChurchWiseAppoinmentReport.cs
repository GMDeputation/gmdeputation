using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.Models
{
	public class ChurchWiseAppoinmentReport
	{
		public string ChurchName { get; set; }

		public int TotalServiceTime { get; set; }

		public List<AppoinmentDetails> AppoinmentDetails { get; set; }
	}


}