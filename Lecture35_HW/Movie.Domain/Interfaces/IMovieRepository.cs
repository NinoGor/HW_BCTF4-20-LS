using System;
using System.Collections.Generic;
using Movie.Domain.Entities;
using System.Text;

namespace Movie.Domain.Interfaces
{
	public interface IMovieRepository
	{
		Task<ICollection<Movie.Domain.Entities.Movie>> GetAllMoviesAsync(
			CancellationToken cancellationToken = default);
		Task AddMovieAsync(Movie.Domain.Entities.Movie movie);

		Task<Movie.Domain.Entities.Movie> GetMovieByIdAsync(int id);


		Task UpdateMovieAsync(int id, Domain.Entities.Movie movie);
		Task DeleteMovieAsync(int id);
		Task<ICollection<Domain.Entities.Movie>> SearchMoviesAdvancedAsync(
			int fromYear,
			int toYear,
			string countryName,
			string titleText,
			int minimumActorCount);
		Task<ICollection<Domain.Entities.Movie>> SearchMoviesByCountryAsync(
		   string countryName,
		   int minimumYear,
		   int maximumActorCount);

		Task<ICollection<Domain.Entities.Movie>> SearchMoviesByStudioAsync(
			int year,
			string studioName,
			int minimumActorCount);


	}
}
