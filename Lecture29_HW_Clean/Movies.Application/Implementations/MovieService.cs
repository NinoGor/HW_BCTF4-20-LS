using Movies.Domain.DTOs;
using Movies.Domain.Entities;
using Movies.Domain.Interfaces;
using Movies.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Service.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository) 
        { 
            _movieRepository = movieRepository;
        }

        public async Task<ICollection<MovieDTO>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();
            var movieDTOs = movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                ReleaseYear = m.ReleaseYear,
                StudioName = m.Studio.Name
            }).ToList();
            
            return movieDTOs;
        }

        public async Task AddMovieAsync(CreateMovieDTO createMovieDTO)
        {
            if(createMovieDTO == null)
            {
                throw new ArgumentNullException(nameof(createMovieDTO));
            }
            if(string.IsNullOrEmpty(createMovieDTO.Title))
            {
                throw new ArgumentException("Title cannot be null or empty", nameof(createMovieDTO.Title));
            }
            if(createMovieDTO.ReleaseYear < 1888) // პირველი ფილმი გამოვიდა 1888-ში
            {
                throw new ArgumentOutOfRangeException(nameof(createMovieDTO.ReleaseYear), "Release year must be 1888 or later");
            }
            if(createMovieDTO.ReleaseYear > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(createMovieDTO.ReleaseYear), "Release year cannot be in the future");
            }
            if(createMovieDTO.StudioId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(createMovieDTO.StudioId), "Studio ID must be a positive integer");
            }
            var movie = new Movie
            {
                Title = createMovieDTO.Title,
                ReleaseYear = createMovieDTO.ReleaseYear,
                StudioId = createMovieDTO.StudioId
            };
            await _movieRepository.AddMovieAsync(movie);
        }

    }
}
