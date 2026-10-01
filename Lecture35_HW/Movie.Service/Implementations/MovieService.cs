using Movie.Domain.DTOs;
using Movie.Domain.Interfaces;
using Movie.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Service.Implementations
{
	public class MovieService : IMovieService
	{

		private readonly IMovieRepository _movieRepository;
		private readonly IUnitOfWork _unitOfWork;

		public MovieService(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
		{
			_movieRepository = movieRepository;
			_unitOfWork = unitOfWork;
		}



		public async Task<ICollection<MovieDTO>> GetAllMoviesAsync(
			CancellationToken cto = default)
		{
			var movies = await _movieRepository.GetAllMoviesAsync(cto);

			var movieDtos = movies.Select(m => new MovieDTO
			{
				Id = m.Id,
				Title = m.Title,
				ReleaseYear = m.ReleaseYear,
				StudioName = m.Studio.Name,
			}).ToList();


			return movieDtos;
		}

		public async Task<MovieDTO> GetMovieByIdAsync(int id)
		{
			var movie = await _movieRepository.GetMovieByIdAsync(id);

			if (movie == null)
			{
				throw new ArgumentException("Movie not found.", nameof(id));
			}

			return new MovieDTO
			{
                Id = movie.Id,
                Title = movie.Title,
				ReleaseYear = movie.ReleaseYear,
                StudioId = movie.StudioId, // დაემატა
                StudioName = movie.Studio.Name,
			};
		}

		public async Task AddMovieAsync(CreateMovieDTO movieDto)
		{

			if (movieDto == null)
			{
				throw new ArgumentNullException(nameof(movieDto));
			}
			if (string.IsNullOrWhiteSpace(movieDto.Title))
			{
				throw new ArgumentException("Movie title cannot be null or empty.", nameof(movieDto.Title));
			}
			if (movieDto.ReleaseYear < 0)
			{
				throw new ArgumentException("Movie release year cannot be negative.", nameof(movieDto.ReleaseYear));
			}
			if (movieDto.ReleaseYear > DateTime.Now.Year)
			{
				throw new ArgumentException("Movie release year cannot be from future.", nameof(movieDto.ReleaseYear));
			}
			if (movieDto.StudioId <= 0)
			{
				throw new ArgumentException("Movie studio ID must be a positive integer.", nameof(movieDto.StudioId));
			}

			var movie = new Movie.Domain.Entities.Movie
			{
				Title = movieDto.Title,
				ReleaseYear = movieDto.ReleaseYear,
				StudioId = movieDto.StudioId
			};


			await _movieRepository.AddMovieAsync(movie);
			await _unitOfWork.SaveChangesAsync();
		}





		public async Task UpdateMovieAsync(int id, UpdateMovieDTO movieDto)
		{
			#region validation

			if (movieDto == null)
			{
				throw new ArgumentNullException(nameof(movieDto));
			}
			if (string.IsNullOrWhiteSpace(movieDto.Title))
			{
				throw new ArgumentException("Movie title cannot be null or empty.", nameof(movieDto.Title));
			}
			if (movieDto.ReleaseYear < 0)
			{
				throw new ArgumentException("Movie release year cannot be negative.", nameof(movieDto.ReleaseYear));
			}
			if (movieDto.ReleaseYear > DateTime.Now.Year)
			{
				throw new ArgumentException("Movie release year cannot be from future.", nameof(movieDto.ReleaseYear));
			}
			if (movieDto.StudioId <= 0)
			{
				throw new ArgumentException("Movie studio ID must be a positive integer.", nameof(movieDto.StudioId));
			}
			#endregion
			var movie = new Movie.Domain.Entities.Movie
			{
				Title = movieDto.Title,
				ReleaseYear = movieDto.ReleaseYear,
				StudioId = movieDto.StudioId
			};



			await _movieRepository.UpdateMovieAsync(id, movie);
			await _unitOfWork.SaveChangesAsync();
		}



		public async Task DeleteMovieAsync(int id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Movie ID must be a positive integer.", nameof(id));
			}
			await _movieRepository.DeleteMovieAsync(id);
			await _unitOfWork.SaveChangesAsync();
		}





		public async Task<ICollection<SerachMovieDTO>> SearchMoviesByStudioAsync(
			int year, string studioName, int minimumActorCount)
		{
			#region validation
			if (year < 0)
			{
				throw new ArgumentException("Year cannot be negative.", nameof(year));
			}
			if (string.IsNullOrWhiteSpace(studioName))
			{
				throw new ArgumentException("Studio name cannot be null or empty.", nameof(studioName));
			}
			if (minimumActorCount < 0)
			{
				throw new ArgumentException("Minimum actor count cannot be negative.", nameof(minimumActorCount));
			}

			#endregion


			var movies = await _movieRepository.SearchMoviesByStudioAsync(year, studioName, minimumActorCount);
		    
			return movies.Select(MapMovieDTO).ToList();
		}




		public async Task<ICollection<SerachMovieDTO>> SearchMoviesByCountryAsync(
				string countryName,
			int minimumYear,
			int maximumActorCount)
		{
			#region validation
			if (minimumYear < 0)
			{
				throw new ArgumentException("Year cannot be negative.", nameof(minimumYear));
			}
			if (string.IsNullOrWhiteSpace(countryName))
			{
				throw new ArgumentException("Studio name cannot be null or empty.", nameof(countryName));
			}
			if (maximumActorCount < 0)
			{
				throw new ArgumentException("Minimum actor count cannot be negative.", nameof(maximumActorCount));
			}

			#endregion


			var movies = await _movieRepository.SearchMoviesByCountryAsync(countryName, minimumYear, maximumActorCount);
			return movies.Select(MapMovieDTO).ToList();
		}


		private static SerachMovieDTO MapMovieDTO(Movie.Domain.Entities.Movie movie)
		{
			return new SerachMovieDTO
			{
				Title = movie.Title,
				ReleaseYear = movie.ReleaseYear,
				StudioName = movie.Studio.Name,
				//CountryName = movie.Studio.Country?.Name  ?? "rame",
				CountryName = movie.Studio.Country.Name,
				ActorCount = movie.Actors.Count
			};

		}
	}
}


