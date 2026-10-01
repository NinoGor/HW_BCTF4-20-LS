using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie.Domain.Entities
{
	public class Actor
	{
		/*o Id(int, Primary Key)
o FirstName(string, სავალდებულო, მაქსიმუმ 100 სიმბოლო)
o LastName(string, სავალდებულო, მაქსიმუმ 100 სიმბოლო)
o Movies(კოლექცია Movie-სთან — Many-to-Many)*/

		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Movie> Movies { get; set; } = new List<Movie>();

	}
}
