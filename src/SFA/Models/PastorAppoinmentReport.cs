using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.Models
{
	public class PastorAppoinmentReport
	{
		public string PastorName { get; set; }

		public List<AppoinmentDetails> AppoinmentDetails { get; set; }
	}


}