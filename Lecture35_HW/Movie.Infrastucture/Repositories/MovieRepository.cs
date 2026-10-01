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
			//await _movieDbContext.SaveChangesAsync();
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


			//await _movieDbContext.SaveChangesAsync();
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
			//await _movieDbContext.SaveChangesAsync();

		}



		// ===============================
		// დავალება 1
		// ===============================

		// public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesByStudioAsync(
		//     int year,
		//     string studioName,
		//     int minimumActorCount)
		// {
		//
		//     // დააბრუნეთ ისეთი ფილმები:
		//
		//     // 1. ReleaseYear უნდა იყოს year-ზე მეტი ან ტოლი
		//     // 2. Studio.Name უნდა უდრიდეს studioName-ს
		//     // 3. Actors.Count უნდა იყოს minimumActorCount-ზე მეტი ან ტოლი
		//
		//     // შედეგები დაალაგეთ:
		//     // პირველ რიგში ReleaseYear-ის მიხედვით კლებადობით
		//     // შემდეგ Title-ის მიხედვით ზრდადობით
		//
		//     // გამოიყენეთ:
		//     // Where
		//     // OrderByDescending
		//     // ThenBy
		//
		// }
		public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesByStudioAsync(
			int year,
			string studioName,
			int minimumActorCount)
		{
			return await _movieDbContext.Movies
				.Include(m => m.Studio)
				  .ThenInclude(s => s.Country) /// 
				.Include(m => m.Actors)
				.Where(m => m.ReleaseYear >= year &&
							m.Studio.Name == studioName &&
							m.Actors.Count >= minimumActorCount)

				.OrderByDescending(m => m.ReleaseYear) /// 2022  'A'  2022  'b'
				.ThenBy(m => m.Title)
				//IQueryable
				.ToListAsync();
		}





		// ===============================
		// დავალება 2
		// ===============================

		// public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesByCountryAsync(
		//     string countryName,
		//     int minimumYear,
		//     int maximumActorCount)
		// {
		//
		//     // დააბრუნეთ ისეთი ფილმები:
		//
		//     // 1. Studio.Country.Name უნდა უდრიდეს countryName-ს
		//     // 2. ReleaseYear უნდა იყოს minimumYear-ზე მეტი ან ტოლი
		//     // 3. Actors.Count უნდა იყოს maximumActorCount-ზე ნაკლები ან ტოლი
		//
		//     // შედეგები დაალაგეთ:
		//     // პირველ რიგში მსახიობების რაოდენობის მიხედვით ზრდადობით
		//     // შემდეგ ReleaseYear-ის მიხედვით კლებადობით
		//     // შემდეგ Title-ის მიხედვით ზრდადობით
		//
		//     // გამოიყენეთ:
		//     // Where
		//     // OrderBy
		//     // ThenByDescending
		//     // ThenBy
		//
		// }


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

		// ===============================
		// დავალება 3
		// ===============================

		// public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesAdvancedAsync(
		//     int fromYear,
		//     int toYear,
		//     string countryName,
		//     string titleText,
		//     int minimumActorCount)
		// {
		//
		//     // დააბრუნეთ ისეთი ფილმები:
		//
		//     // 1. ReleaseYear უნდა იყოს fromYear-სა და toYear-ს შორის
		//     //    ორივე ჩათვლით
		//
		//     // 2. Studio.Country.Name უნდა უდრიდეს countryName-ს
		//
		//     // 3. Title უნდა შეიცავდეს titleText-ს
		//
		//     // 4. Actors.Count უნდა იყოს minimumActorCount-ზე მეტი ან ტოლი
		//
		//     // შედეგები დაალაგეთ:
		//     // პირველ რიგში მსახიობების რაოდენობის მიხედვით კლებადობით
		//     // შემდეგ ReleaseYear-ის მიხედვით კლებადობით
		//     // შემდეგ Studio.Name-ის მიხედვით ზრდადობით
		//     // ბოლოს Title-ის მიხედვით ზრდადობით
		//
		//     // გამოიყენეთ:
		//     // Where
		//     // Contains
		//     // OrderByDescending
		//     // ThenByDescending
		//     // ThenBy
		//
		// }

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
