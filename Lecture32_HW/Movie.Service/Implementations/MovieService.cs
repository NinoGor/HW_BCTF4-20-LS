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

		public MovieService(IMovieRepository movieRepository, IUnitOfWork unitOfWork) {
			_movieRepository = movieRepository;
			_unitOfWork = unitOfWork;
		}



		public async Task<ICollection<MovieDTO>> GetAllMoviesAsync()
		{
			var movies = await _movieRepository.GetAllMoviesAsync();

			var movieDtos = movies.Select(m => new MovieDTO
			{
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
				Title = movie.Title,
				ReleaseYear = movie.ReleaseYear,
				StudioName = movie.Studio.Name,
			};
		}

		public async Task AddMovieAsync(CreateMovieDTO movieDto)
		{

			if(movieDto == null)
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
			if(id <= 0)
			{
				throw new ArgumentException("Movie ID must be a positive integer.", nameof(id));
			}
			await _movieRepository.DeleteMovieAsync(id);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}


