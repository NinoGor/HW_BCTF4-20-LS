using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities
{
	/*
	  Id (int, Primary Key)
	  LicenseNumber (string, სავალდებულო)
	  StudioId (int, Foreign Key)
	  Studio (ნავიგაციის ობიექტი Studio-სთან)
    */
	public class StudioDetails
	{
		public int Id { get; set; }
		public string LicenseNumber { get; set; }
		public int StudioId { get; set; }
		public Studio Studio { get; set; }
	}
}
