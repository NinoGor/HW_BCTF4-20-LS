using Movie.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Service.Interfaces
{
	public interface IMovieService
	{
		 Task<ICollection<MovieDTO>> GetAllMoviesAsync(CancellationToken cto = default);
		 Task AddMovieAsync(CreateMovieDTO movieDto);

		 Task<MovieDTO> GetMovieByIdAsync(int id);

        // იმპლემენტაცია გვეწერა მაგრამ ინტერფეისში არ გვქონდა UpdateMovieAsync
        Task UpdateMovieAsync(int id, UpdateMovieDTO movieDto);

        Task DeleteMovieAsync(int id);

		Task<ICollection<SerachMovieDTO>> SearchMoviesByCountryAsync(
				string countryName,
			int minimumYear,
			int maximumActorCount);
		Task<ICollection<SerachMovieDTO>> SearchMoviesByStudioAsync(
			int year, string studioName, int minimumActorCount);
		
		}
}
