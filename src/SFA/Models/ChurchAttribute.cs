using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.Models
{
    public class ChurchAttribute
    {
		public int Id { get; set; }

		public int ChurchId { get; set; }

		public int AttributeId { get; set; }

		public int AttributeTypeId { get; set; }

		public decimal? AttributeValue { get; set; }

		public string Notes { get; set; }

		public List<AttributeModel> Butes { get; set; }
	}

}