using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie.Domain.Entities
{

//	•	Country(ქვეყანა) :
//o Id(int, Primary Key)
//o Name(string, სავალდებულო, მაქსიმუმ 100 სიმბოლო)
//o Studios(კოლექცია Studio-სთან — One-to-Many)

	public class Country
	{

		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Studio> Studios { get; set; } = new List<Studio>();
	}
}
