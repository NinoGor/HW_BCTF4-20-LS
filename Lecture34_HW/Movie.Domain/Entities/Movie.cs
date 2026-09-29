using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities
{
	/*
		Movie (ფილმი):
 		Id (int, Primary Key)
 		Title (string, სავალდებულო, მაქსიმუმ 150 სიმბოლო)
 		ReleaseYear (int)
 		StudioId (int, Foreign Key)
 		Studio (ნავიგაციის ობიექტი Studio-სთან)
 		Actors (კოლექცია Actor-თან — Many-to-Many)																	
	*/
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int ReleaseYear { get; set; }
		public int StudioId { get; set; }
		public Studio Studio { get; set; }
		public ICollection<Actor> Actors { get; set; } = new List<Actor>();
	}
}
