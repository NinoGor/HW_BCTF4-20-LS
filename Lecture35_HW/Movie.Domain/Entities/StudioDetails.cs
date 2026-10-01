using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities
{
	/*o	Id (int, Primary Key)
o	LicenseNumber (string, სავალდებულო)
o	StudioId (int, Foreign Key)
o	Studio (ნავიგაციის ობიექტი Studio-სთან)
/*/
	public class StudioDetails
	{
		public int Id { get; set; }
		public string LicenseNumber { get; set; }
		public int StudioId { get; set; }
		public Studio Studio { get; set; }
	}
}
