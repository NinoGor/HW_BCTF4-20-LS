using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities
{
	/*•	Movie (ფილმი):
o	Id (int, Primary Key)
o	Title (string, სავალდებულო, მაქსიმუმ 150 სიმბოლო)
o	ReleaseYear (int)
o	StudioId (int, Foreign Key)
o	Studio (ნავიგაციის ობიექტი Studio-სთან)
o	Actors (კოლექცია Actor-თან — Many-to-Many)
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
