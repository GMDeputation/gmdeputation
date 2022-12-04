using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
	public class QueryResult<T>
	{
		public List<T> Result { get; set; }

		public int Count { get; set; }
	}
}
