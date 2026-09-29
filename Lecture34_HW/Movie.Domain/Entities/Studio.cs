using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie.Domain.Entities
{

	// Studio(კინოსტუდია) :
	// Id(int, Primary Key)
	// Name(string, სავალდებულო, მაქსიმუმ 100 სიმბოლო)
	// CountryId(int, Foreign Key)
	// Country(ნავიგაციის ობიექტი Country-სთან)
	// StudioDetails(ნავიგაციის ობიექტი StudioDetails-თან — One-to-One)
	// Movies(კოლექცია Movie-სთან — One-to-Many)

	public class Studio
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CountryId { get; set; }
		public Country Country { get; set; }
		public StudioDetails StudioDetails { get; set; }
		public ICollection<Movie> Movies { get; set; } = new List<Movie>();
	}
}
