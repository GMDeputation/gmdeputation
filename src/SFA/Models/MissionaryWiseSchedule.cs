using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.Models
{
	public class MissionaryWiseSchedule
	{
		public string MissionaryName { get; set; }

		public List<MacroscheduleWiseAppoinmentReport> MacroSchedules { get; set; }
	}


}