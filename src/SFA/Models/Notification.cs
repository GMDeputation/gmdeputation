using System;
namespace SFA.Models
{
	public class Notification
	{
		public int Id { get; set; }

		public string Description { get; set; }

		public string EventUrl { get; set; }

		public int? EventUser { get; set; }

		public bool IsOpened { get; set; }

		public string InsertUser { get; set; }

		public DateTime? InsertDatetime { get; set; }

		public DateTime? OpenedOn { get; set; }
	}
}
