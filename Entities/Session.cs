using System;
using System.Collections.Generic;

namespace CodeCampService
{
	public class Session
	{
		public string Key { get; set; }
		public string Title { get; set; }
		public string Abstract { get; set; }
		public string Speaker { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public List<string> Tags { get; set; }
	}
}

