using Microsoft.EntityFrameworkCore;
using Movie.Domain.Interfaces;
using Movie.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastucture.Repositories
{
	public class MovieRepository : IMovieRepository
	{

		private readonly MovieDbContext _movieDbContext;
		public MovieRepository(MovieDbContext movieDbContext)
		{
			_movieDbContext = movieDbContext;
		}


		public async Task AddMovieAsync(Domain.Entities.Movie movie)
		{
		    await _movieDbContext.Movies.AddAsync(movie);
		}

		public async Task<ICollection<Domain.Entities.Movie>> GetAllMoviesAsync(
			CancellationToken cto = default)
		{
			return await _movieDbContext.Movies
				.Include(m => m.Studio)
				.ToListAsync(cto);
		}


		public async Task<Domain.Entities.Movie> GetMovieByIdAsync(int id)
		{
			return await _movieDbContext.Movies
				.Include(m => m.Studio)
				.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task UpdateMovieAsync(int id, Domain.Entities.Movie movie)
		{
			var movieExists =
				await _movieDbContext.Movies
				.FirstOrDefaultAsync(m => m.Id == id);
			if (movieExists == null)
			{
				throw new ArgumentException("Movie not found");
			}

			movieExists.Title = movie.Title;
			movieExists.ReleaseYear = movie.ReleaseYear;
			movieExists.StudioId = movie.StudioId;

		}


		public async Task  DeleteMovieAsync(int id)
		{
			
			var movieExists = await _movieDbContext.Movies
				.FirstOrDefaultAsync(m => m.Id == id);
			if (movieExists == null)
			{
				throw new ArgumentException("Movie not found");
			}

			_movieDbContext.Movies.Remove(movieExists);
		}

		public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesByStudioAsync(
			int year,
			string studioName,
			int minimumActorCount)
		{
			return await _movieDbContext.Movies
				.Include(m => m.Studio)
				  .ThenInclude(s => s.Country)  
				.Include(m => m.Actors)
				.Where(m => m.ReleaseYear >= year &&
							m.Studio.Name == studioName &&
							m.Actors.Count >= minimumActorCount)

				.OrderByDescending(m => m.ReleaseYear) 
				.ThenBy(m => m.Title)
				.ToListAsync();
		}


		public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesByCountryAsync(
			string countryName,
			int minimumYear,
			int maximumActorCount)
		{
			return await _movieDbContext.Movies
				.Include(m => m.Studio)
				   .ThenInclude(s => s.Country)
				 .Include(m => m.Actors)

				 .Where(m => m.Studio.Country.Name == countryName &&
							m.ReleaseYear >= minimumYear &&
							m.Actors.Count <= maximumActorCount)
				 .OrderBy(m => m.Actors.Count)
				 .ThenByDescending(m => m.ReleaseYear)
				 .ThenBy(m => m.Title)

				 .ToListAsync();
		}

		public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesAdvancedAsync(
			int fromYear,
			int toYear,
			string countryName,
			string titleText,
			int minimumActorCount)

		{
			return await _movieDbContext.Movies
				.Include(m => m.Studio)
					.ThenInclude(s => s.Country)
				.Include(m => m.Actors)


				.Where(m => m.ReleaseYear >= fromYear &&
							m.ReleaseYear <= toYear &&
							m.Studio.Country.Name == countryName &&
							m.Title.Contains(titleText) &&
							m.Actors.Count >= minimumActorCount)


				.OrderByDescending(m => m.Actors.Count)
				.ThenByDescending(m => m.ReleaseYear)
				.ThenBy(m => m.Studio.Name)
				.ThenBy(m => m.Title)

				.ToListAsync();

		}


	}
}
