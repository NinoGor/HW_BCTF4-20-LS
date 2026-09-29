using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.DTOs
{
	public class SerachMovieDTO
	{

		public string Title { get; set; }
		public int ReleaseYear { get; set; }
		public string StudioName { get; set; }

		public string CountryName { get; set; }

		public int ActorCount { get; set; }

		public override string? ToString()
		{
			return $"{Title} ({ReleaseYear}) - {StudioName}, {CountryName}, {ActorCount} actors";
		}
	}
}
