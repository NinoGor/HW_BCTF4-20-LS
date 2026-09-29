using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie.Domain.Entities
{
	public class Actor
	{
		/*
		 Id(int, Primary Key)
         FirstName(string, სავალდებულო, მაქსიმუმ 100 სიმბოლო)
		 LastName(string, სავალდებულო, მაქსიმუმ 100 სიმბოლო)
		 Movies(კოლექცია Movie-სთან — Many-to-Many)
		*/

		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Movie> Movies { get; set; } = new List<Movie>();

	}
}
